using HarmonyLib;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.CharGen;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Blueprints.Root;
using Kingmaker.BundlesLoading;
using Kingmaker.Cheats;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.ResourceLinks;
using Kingmaker.UI.MVVM._PCView.InGame;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Utility;
using Kingmaker.Visual.CharacterSystem;
using Kingmaker.Visual.Sound;
using ModKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TutorialCanvas.UI;
using UnityEngine;

//using TutorialCanvas.UI;
using UnityEngine.UI;
using UnityModManagerNet;
using static VisualAdjustments.Settings;
using Object = UnityEngine.Object;

namespace VisualAdjustments
{
#if DEBUG
    [EnableReloading]
#endif

    public class CharInfo
    {
        public string GUID;
        public string Name;
    }

    /*class UpdateDataForUGUI : IHandleInventoryInitialized
    {
        public void OnInventoryInitialized(UnitDescriptor data)
        {
            Main.logger.Log(data.CharacterName);
        }
    }
    class TestPubSub : IAreaActivationHandler
    {
        public void OnAreaActivated()
        {
            Main.logger.Log("loaded");
        }
    }*/

    public class Main
    {
        public static BlueprintList blueprints;
        private const float DefaultLabelWidth = 200f;
        private const float DefaultSliderWidth = 300f;
        public static UnityModManager.ModEntry.ModLogger logger;

        [System.Diagnostics.Conditional("DEBUG")]
        public static void Log(string msg)
        {
            if (logger != null)
                logger.Log(msg);
        }

        public static void Error(Exception ex)
        {
            if (logger != null)
                logger.Log(ex.ToString());
        }

        public static void Error(string msg)
        {
            if (logger != null)
                logger.Log(msg);
        }

        public static ColorPicker HairColorPicker = new ColorPicker();
        public static ColorPicker SkinColorPicker = new ColorPicker();
        public static ColorPicker PrimaryColorPicker = new ColorPicker();
        public static ColorPicker SecondaryColorPicker = new ColorPicker();
        public static ColorPicker HornColorPicker = new ColorPicker();
        public static ColorPicker WarpaintColorPicker = new ColorPicker();
        public static bool unlockcustomization;
        public static bool enabled;
        public static bool showsettings = true;
        public static bool classesloaded = false;
        public static Settings settings;
        public static Dictionary<string, EquipmentEntity.OutfitPart> CapeOutfitParts = new Dictionary<string, EquipmentEntity.OutfitPart>();
        public static UnityModManager.ModEntry ModEntry;

        /// public static ReferenceArrayProxy<BlueprintCharacterClass,BlueprintCharacterClassReference> classes = Game.Instance.BlueprintRoot.Progression.CharacterClasses;
        /// public static string[] classes;
        public static List<CharInfo> classes = new List<CharInfo> { };

        /*public static string[] classes = new string[] {
            "Default",
            "Alchemist",
            "Barbarian",
            "Bard",
            "Cleric",
            "Druid",
            "Fighter",
            "Inquisitor",
            "Kineticist",
            "Magus",
            "Monk",
            "Paladin",
            "Ranger",
            "Rogue",
            "Slayer",
            "Sorcerer",
            "Wizard",g
            "None"
        };*/
        public static UIManager TutorialUI { get; set; }
        public static bool forceload = false;
        public static float UIscale = 1;
        private static bool haspatched = false;

        private static bool Load(UnityModManager.ModEntry modEntry)
        {
            try
            {
               // System.Diagnostics.Stopwatch timer = new Stopwatch();
                //timer.Start();



                ModEntry = modEntry;

                logger = modEntry.Logger;
                settings = Settings.Load(modEntry);
                //Assembly.Load("UITwo.dll");
                var harmony = new Harmony(modEntry.Info.Id);
                //if (haspatched)
                //{
                //   haspatched = true;
                harmony.PatchAll(Assembly.GetExecutingAssembly());
                //}
                //TutorialCanvas.Main.Load(modEntry);
                /// colorpicker = CreateColorPicker();
                //dot = ColorPickerLoad.GetTexture(ModEntry.Path + "\\ColorPicker\\dot.png");
                TutorialCanvas.Utilities.BundleManger.AddBundle("tutorialcanvas");
                UIscale = 1; // fix this
                modEntry.OnToggle = OnToggle;
                modEntry.OnGUI = onGUI;
                modEntry.OnSaveGUI = OnSaveGUI;
                modEntry.OnUpdate = OnUpdate;
#if DEBUG
                modEntry.OnUnload = Unload;
#endif
                if (Main.blueprints == null)
                {
                    Main.blueprints = Utilities.GetAllBlueprints();
                    // Main.blueprints = Util.GetBlueprints();
                }
                if (!classesloaded)
                {
                    Main.GetClasses();
                }
               /* if(EquipmentResourcesManager.m_AllEEL == null || EquipmentResourcesManager.m_AllEEL.Count <= 0)
                {
                    var task = new Task(() =>
                    {
                        var _ = EquipmentResourcesManager.AllEEL;
                    });
                    task.Start();
                }*/
                //  EventBus.Subscribe(new UpdateDataForUGUI());
                ////EventBus.Subscribe(new TestPubSub());
                //timer.Stop();

                //TimeSpan timeTaken = timer.Elapsed;
                //Main.logger.Critical(timeTaken.ToString());
            }
            catch (Exception e)
            {
                Log(e.ToString() + "\n" + e.StackTrace);
                throw e;
            }
            return true;
        }

        public static void OnUpdate(UnityModManager.ModEntry modEntry, float a)
        {
            // Main.logger.Log("updat");
            if (settings.enableHotKey)
            {
                if (UnityEngine.Input.GetKey("left alt") && UnityEngine.Input.GetKeyDown("x"))
                {
                    Main.logger.Log("Hotkey");
                    foreach (var unit in Game.Instance.Player.AllCharacters)
                    {
                        CharacterManager.RebuildCharacter(unit);
                    }
                }
            }
        }

        public static void GenerateHairColor(UnitEntityData data)
        {
            /// might have to add race identifier to Main.HairColors
            /// bypass primarycolorsprofile and set 2dtexture directly
            try
            {
                var doll = DollResourcesManager.GetDoll(data);
                if (doll == null)
                    return;
                var settings = Main.settings.GetCharacterSettings(data);
                var colornum = settings.hairColor;
                var settingcol = new Color(colornum[0], colornum[1], colornum[2]);
                if (doll.GetHairEntities().Count <= 0)
                    return;
                if (!doll.Hair.m_Link.Load().PrimaryColorsProfile.Ramps.Where(b => b.isReadable).Any(a => a.GetPixel(1, 1).ToString() == settingcol.ToString()))
                {
                    var texture = new Texture2D(256, 1, TextureFormat.ARGB32, false)
                    {
                        filterMode = FilterMode.Bilinear
                    };
                    for (var y = 0; y < 1; y++)
                    {
                        for (var x = 0; x < 256; x++)
                        {
                            texture.SetPixel(x, y, settingcol);
                        }
                    }
                    texture.Apply();
                    /// if (!doll.Hair.m_Entity.PrimaryColorsProfile.Ramps.Where(b => b.isReadable).Any(a => a.GetPixel(1, 1).ToString() == texture.GetPixel(1, 1).ToString()))
                    /// if (!HairColors.ContainsKey(jas.ToString()))
                    {
                        /// HairColors.Add(jas.ToString(), texture);
                        doll.Hair.m_Link.Load().PrimaryColorsProfile.Ramps.Add(texture);
                    }
                    ///something with the indexing it messing up and returning -1
                    ///var index = doll.Hair.m_Entity.PrimaryColorsProfile.Ramps.Where(b => b.isReadable).First(a => a.GetPixel(1,1).ToString() == texture.GetPixel(1,1).ToString());
                }
                var index = doll.Hair.m_Link.Load().PrimaryColorsProfile.Ramps.Where(b => b.isReadable).First(a => a.GetPixel(1, 1).ToString() == settingcol.ToString());
                doll.SetHairColor(doll.Hair.m_Link.Load().PrimaryColorsProfile.Ramps.IndexOf(index));
                //Main.logger.Log(settings.HairColor.ToString());
                ///Main.logger.Log(jas.ToString());
                /*if(!doll.Hair.m_Entity.PrimaryColorsProfile.Ramps.Any(a => a == texture))
                {
                    doll.Hair.m_Entity.PrimaryColorsProfile.Ramps.Add(texture);
                }*/
                /// doll.SetHairColor(doll.Hair.m_Entity.PrimaryColorsProfile.Ramps.IndexOf(texture));
            }
            catch (Exception e)
            {
                Main.logger.Log(e.ToString());
            }
        }

        public static void GenerateSkinColor(UnitEntityData data)
        {
            /// might have to add race identifier to Main.HairColors
            /// bypass primarycolorsprofile and set 2dtexture directly
            try
            {
                var doll = DollResourcesManager.GetDoll(data);
                if (doll == null)
                    return;
                var settings = Main.settings.GetCharacterSettings(data);
                var colornum = settings.skinColor;
                var settingcol = new Color(colornum[0], colornum[1], colornum[2]);
                if (!doll.Head.m_Link.Load().PrimaryColorsProfile.Ramps.Where(b => b.isReadable).Any(a => a.GetPixel(1, 1).ToString() == settingcol.ToString()))
                {
                    var texture = new Texture2D(256, 1, TextureFormat.ARGB32, false)
                    {
                        filterMode = FilterMode.Bilinear
                    };
                    for (var y = 0; y < 1; y++)
                    {
                        for (var x = 0; x < 256; x++)
                        {
                            texture.SetPixel(x, y, settingcol);
                        }
                    }
                    texture.Apply();
                    if (!doll.Head.m_Link.Load().PrimaryColorsProfile.Ramps.Where(b => b.isReadable).Any(a => a.GetPixel(1, 1).ToString() == texture.GetPixel(1, 1).ToString()))
                    /// if (!HairColors.ContainsKey(jas.ToString()))
                    {
                        /// HairColors.Add(jas.ToString(), texture);
                        doll.Head.m_Link.Load().PrimaryColorsProfile.Ramps.Add(texture);
                    }
                    ///something with the indexing it messing up and returning -1
                    ///var index = doll.Hair.m_Entity.PrimaryColorsProfile.Ramps.Where(b => b.isReadable).First(a => a.GetPixel(1,1).ToString() == texture.GetPixel(1,1).ToString());
                }
                var index = doll.Head.m_Link.Load().PrimaryColorsProfile.Ramps.Where(b => b.isReadable).First(a => a.GetPixel(1, 1).ToString() == settingcol.ToString());
                doll.SetSkinColor(doll.Head.m_Link.Load().PrimaryColorsProfile.Ramps.IndexOf(index));
                //Main.logger.Log(settings.SkinColor.ToString());
                ///Main.logger.Log(jas.ToString());
                /*if(!doll.Hair.m_Entity.PrimaryColorsProfile.Ramps.Any(a => a == texture))
                {
                    doll.Hair.m_Entity.PrimaryColorsProfile.Ramps.Add(texture);
                }*/
                /// doll.SetHairColor(doll.Hair.m_Entity.PrimaryColorsProfile.Ramps.IndexOf(texture));
            }
            catch (Exception e)
            {
                Main.logger.Log(e.ToString());
            }
        }

        public static void GenerateHornColor(UnitEntityData data)
        {
            try
            {
                var doll = DollResourcesManager.GetDoll(data);
                if (doll == null)
                    return;
                if (doll.Horn.m_Link.Load() == null)
                    return;
                var settings = Main.settings.GetCharacterSettings(data);
                var colornum = settings.hornColor;
                var settingcol = new Color(colornum[0], colornum[1], colornum[2]);
                if (!doll.Horn.m_Link.Load().PrimaryColorsProfile.Ramps.Where(b => b.isReadable).Any(a => a.GetPixel(1, 1).ToString() == settingcol.ToString()))
                {
                    var texture = new Texture2D(256, 1, TextureFormat.ARGB32, false)
                    {
                        filterMode = FilterMode.Bilinear
                    };
                    for (var y = 0; y < 1; y++)
                    {
                        for (var x = 0; x < 256; x++)
                        {
                            texture.SetPixel(x, y, settingcol);
                        }
                    }
                    texture.Apply();
                    if (!doll.Horn.m_Link.Load().PrimaryColorsProfile.Ramps.Where(b => b.isReadable).Any(a => a.GetPixel(1, 1).ToString() == texture.GetPixel(1, 1).ToString()))
                    {
                        doll.Horn.m_Link.Load().PrimaryColorsProfile.Ramps.Add(texture);
                    }
                }
                var index = doll.Horn.m_Link.Load().PrimaryColorsProfile.Ramps.Where(b => b.isReadable).First(a => a.GetPixel(1, 1).ToString() == settingcol.ToString());
                doll.SetHornsColor(doll.Horn.m_Link.Load().PrimaryColorsProfile.Ramps.IndexOf(index));
            }
            catch (Exception e)
            {
                Main.logger.Log(e.ToString());
            }
        }

       /* public static void GenerateWarpaintColor(UnitEntityData data)
        {
            try
            {
                var doll = DollResourcesManager.GetDoll(data);
                if (doll == null) return;
                if (doll.Warpaint.m_Entity == null) return;
                var settings = Main.settings.GetCharacterSettings(data);
                var colornum = settings.warpaintColor;
                var settingcol = new Color(colornum[0], colornum[1], colornum[2]);
                if (!doll.Warpaint.m_Entity.PrimaryColorsProfile.Ramps.Where(b => b.isReadable).Any(a => a.GetPixel(1, 1).ToString() == settingcol.ToString()))
                {
                    var texture = new Texture2D(256, 1, TextureFormat.ARGB32, false)
                    {
                        filterMode = FilterMode.Bilinear
                    };
                    for (var y = 0; y < 1; y++)
                    {
                        for (var x = 0; x < 256; x++)
                        {
                            texture.SetPixel(x, y, settingcol);
                        }
                    }
                    texture.Apply();
                    if (!doll.Warpaint.m_Entity.PrimaryColorsProfile.Ramps.Where(b => b.isReadable).Any(a => a.GetPixel(1, 1).ToString() == texture.GetPixel(1, 1).ToString()))
                    {
                        doll.Warpaint.m_Entity.PrimaryColorsProfile.Ramps.Add(texture);
                    }
                }
                var index = doll.Warpaint.m_Entity.PrimaryColorsProfile.Ramps.Where(b => b.isReadable).First(a => a.GetPixel(1, 1).ToString() == settingcol.ToString());
                doll.Warpaint.m_Entity.PrimaryColorsProfile.Ramps.IndexOf(index);
                doll.SetWarpaintColor(doll.Warpaint.m_Entity.PrimaryColorsProfile.Ramps.IndexOf(index));
            }
            catch (Exception e)
            {
                Main.logger.Log(e.ToString());
            }
        }*/

        public static void GenerateOutfitcolor(UnitEntityData dat)
        {
            try
            {
                var doll = DollResourcesManager.GetDoll(dat);
                // if (doll == null) return;
                if (dat.View == null)
                    return;
                if (dat.View.CharacterAvatar == null)
                    return;
                var settings = Main.settings.GetCharacterSettings(dat);
                var colornum = settings.primColor;
                var colornum2 = settings.secondColor;
                var settingcol = new Color(colornum[0], colornum[1], colornum[2]);
                var settingcolsecondary = new Color(colornum2[0], colornum2[1], colornum2[2]);
                //if (doll != null)
                {
                    ///   foreach (var a in doll.Clothes.Select(a => a.Load()))
                    foreach (var a in dat.View.CharacterAvatar.EquipmentEntities.Where(A => A.PrimaryColorsProfile != null && A.PrimaryColorsProfile.Ramps.Count > 75))
                    {
                        //   Main.logger.Log(a.ToString());
                        if (a.PrimaryColorsProfile != null)// && a.PrimaryColorsProfile.Ramps.Count > 0)
                        {
                            if (!a.PrimaryColorsProfile.Ramps.Where(b => b.isReadable).Any(h => h.GetPixel(1, 1).ToString() == settingcol.ToString()))
                            {
                                var texture = new Texture2D(256, 1, TextureFormat.ARGB32, false)
                                {
                                    filterMode = FilterMode.Bilinear
                                };
                                for (var y = 0; y < 1; y++)
                                {
                                    for (var x = 0; x < 256; x++)
                                    {
                                        texture.SetPixel(x, y, settingcol);
                                    }
                                }
                                texture.Apply();
                                if (!a.PrimaryColorsProfile.Ramps.Where(b => b.isReadable).Any(x => x.GetPixel(1, 1).ToString() == texture.GetPixel(1, 1).ToString()))
                                {
                                    a.PrimaryColorsProfile.Ramps.Add(texture);
                                    //  Main.logger.Log("addedtex");
                                    //  Main.logger.Log(texture.GetPixel(1, 1).ToString());
                                }
                                //  else
                                {
                                    // Main.logger.Log("Didntaddtex");
                                }
                            }
                            if (!a.PrimaryColorsProfile.Ramps.Where(b => b.isReadable).Any(h => h.GetPixel(1, 1).ToString() == settingcolsecondary.ToString()))
                            {
                                var texture = new Texture2D(256, 1, TextureFormat.ARGB32, false)
                                {
                                    filterMode = FilterMode.Bilinear
                                };
                                for (var y = 0; y < 1; y++)
                                {
                                    for (var x = 0; x < 256; x++)
                                    {
                                        texture.SetPixel(x, y, settingcolsecondary);
                                    }
                                }
                                texture.Apply();
                                if (!a.PrimaryColorsProfile.Ramps.Where(b => b.isReadable).Any(x => x.GetPixel(1, 1).ToString() == texture.GetPixel(1, 1).ToString()))
                                {
                                    a.PrimaryColorsProfile.Ramps.Add(texture);
                                    //  Main.logger.Log("addedtex");
                                    //  Main.logger.Log(texture.GetPixel(1, 1).ToString());
                                }
                                //  else
                                {
                                    // Main.logger.Log("Didntaddtex");
                                }
                            }
                            var index = a.PrimaryColorsProfile.Ramps.Where(b => b.isReadable).First(w => w.GetPixel(1, 1).ToString() == settingcol.ToString());
                            var primindx = a.PrimaryColorsProfile.Ramps.IndexOf(index);
                            if (doll != null) doll.SetPrimaryEquipColor(primindx);
                            settings.companionPrimary = primindx;
                            var indexsec = a.PrimaryColorsProfile.Ramps.Where(b => b.isReadable).First(w => w.GetPixel(1, 1).ToString() == settingcolsecondary.ToString());
                            var secondindx = a.PrimaryColorsProfile.Ramps.IndexOf(indexsec);
                            settings.companionSecondary = secondindx;
                            if (doll != null) doll.SetSecondaryEquipColor(secondindx);
                            if (doll != null) doll.SetEquipColors(primindx, secondindx);
                            /* foreach (var ee in dat.View.CharacterAvatar.EquipmentEntities.Where(x => x.NameSafe().Contains("Cloak") || x.NameSafe().Contains("Cape")))
                             {
                                 ee.RepaintTextures(primindx,secondindx);
                             }*/
                            /*if (dat.Parts.Get<UnitPartDollData>())
                            {
                                dat.Parts.Get<UnitPartDollData>().Default = doll.CreateData();
                            }
                            // doll.SetSkinColor(settings.SkinColor);
                            /*if (a.PrimaryColorsProfile.Ramps.Where(b => b.isReadable).Any(c => c.GetPixel(1, 1).ToString() == settingcol.ToString()))
                            {
                                var index = a.PrimaryColorsProfile.Ramps.Where(b => b.isReadable).FirstOrDefault(c => c.GetPixel(1, 1).ToString() == settingcol.ToString());
                                foreach(var asdasddffads in a.PrimaryColorsProfile.Ramps.Where(nb => nb.isReadable))
                                {
                                    Main.logger.Log(asdasddffads.ToString());
                                    Main.logger.Log(asdasddffads.GetPixel(1,1).ToString());
                                }
                                if (a.PrimaryColorsProfile.Ramps.FindIndex(h => h.isReadable && h.GetPixel(1,1).ToString() == index.GetPixel(1,1).ToString()) != -1)
                                {
                                    settings.PrimaryColor = a.PrimaryColorsProfile.Ramps.FindIndex(h => h.isReadable && h.GetPixel(1, 1).ToString() == index.GetPixel(1, 1).ToString());
                                    Main.logger.Log("index stuff");
                                }
                               /* foreach(var xsdaf in a.PrimaryColorsProfile.Ramps)
                                {
                                    if(xsdaf.isReadable)
                                    {
                                        Main.logger.Log(settingcol.ToString());
                                        Main.logger.Log(xsdaf.GetPixel(1, 1).ToString());
                                        if (xsdaf.GetPixel(1,1).ToString() == settingcol.ToString())
                                        {
                                            Main.logger.Log("indexmatch");
                                            settings.PrimaryColor = a.PrimaryColorsProfile.Ramps.IndexOf(index);
                                            Main.logger.Log(a.PrimaryColorsProfile.Ramps.IndexOf(index).ToString());
                                        }
                                    }
                                }*/
                            /* else
                             {
                                 Main.logger.Log("NoIndexMatch");
                             }
                         }*/
                        }
                    }
                }
            }
            /* else
             {
                 foreach(var asdasdasd in dat.View.CharacterAvatar.EquipmentEntities)
                 {
                     if (asdasdasd != null && asdasdasd.PrimaryColorsProfile != null && asdasdasd.PrimaryColorsProfile.Ramps != null)
                     {
                         Main.logger.Log(asdasdasd.ToString());
                         Main.logger.Log(asdasdasd.PrimaryColorsProfile.Ramps.Count.ToString());
                     }
                 }
                 var bb = dat.View.CharacterAvatar.EquipmentEntities.First();
                 Main.logger.Log(bb.ToString());
                 Main.logger.Log("nodoll");
                 if (!bb.PrimaryColorsProfile.Ramps.Where(b => b.isReadable).Any(h => h.GetPixel(1, 1).ToString() == settingcol.ToString()))
                 {
                     var texture = new Texture2D(256, 1, TextureFormat.ARGB32, false)
                     {
                         filterMode = FilterMode.Bilinear
                     };
                     for (var y = 0; y < 1; y++)
                     {
                         for (var x = 0; x < 256; x++)
                         {
                             texture.SetPixel(x, y, settingcol);
                         }
                     }
                     texture.Apply();
                     if (!bb.PrimaryColorsProfile.Ramps.Where(b => b.isReadable).Any(x => x.GetPixel(1, 1).ToString() == texture.GetPixel(1, 1).ToString()))
                     {
                         bb.PrimaryColorsProfile.Ramps.Add(texture);
                         Main.logger.Log("addedtexnodoll");
                         //  Main.logger.Log(texture.GetPixel(1, 1).ToString());
                     }
                     //  else
                     {
                          Main.logger.Log("Didntaddtexnodoll");
                     }
                 }
                 if (!bb.PrimaryColorsProfile.Ramps.Where(b => b.isReadable).Any(h => h.GetPixel(1, 1).ToString() == settingcolsecondary.ToString()))
                 {
                     var texture = new Texture2D(256, 1, TextureFormat.ARGB32, false)
                     {
                         filterMode = FilterMode.Bilinear
                     };
                     for (var y = 0; y < 1; y++)
                     {
                         for (var x = 0; x < 256; x++)
                         {
                             texture.SetPixel(x, y, settingcolsecondary);
                         }
                     }
                     texture.Apply();
                     if (!bb.PrimaryColorsProfile.Ramps.Where(b => b.isReadable).Any(x => x.GetPixel(1, 1).ToString() == texture.GetPixel(1, 1).ToString()))
                     {
                         bb.PrimaryColorsProfile.Ramps.Add(texture);
                         //  Main.logger.Log("addedtex");
                         //  Main.logger.Log(texture.GetPixel(1, 1).ToString());
                     }
                     //  else
                     {
                         // Main.logger.Log("Didntaddtex");
                     }
                 }
             }
             // var index = doll.Head.m_Entity.PrimaryColorsProfile.Ramps.Where(b => b.isReadable).First(a => a.GetPixel(1, 1).ToString() == settingcol.ToString());
             //settings.SkinColor = doll.Head.m_Entity.PrimaryColorsProfile.Ramps.IndexOf(index);
             doll.SetPrimaryEquipColor(settings.PrimaryColor);
             //Main.logger.Log(settings.SkinColor.ToString());
             ///Main.logger.Log(jas.ToString());
             /*if(!doll.Hair.m_Entity.PrimaryColorsProfile.Ramps.Any(a => a == texture))
             {
                 doll.Hair.m_Entity.PrimaryColorsProfile.Ramps.Add(texture);
             }*//*
             /// doll.SetHairColor(doll.Hair.m_Entity.PrimaryColorsProfile.Ramps.IndexOf(texture));
         }*/
            catch (Exception e)
            {
                Main.logger.Log(e.StackTrace + "   " + dat.CharacterName);
            }
        }

        /*   private static Texture2D CreateGradient(Color colorStart, Color colorEnd, int width = 256, int height = 1)
           {
               var gradient = new Texture2D(width, height, TextureFormat.ARGB32, false)
               {
                   filterMode = FilterMode.Bilinear
               };

               float inv = 1f / width;
               for (var y = 0; y < height; y++)
               {
                   for (var x = 0; x < width; x++)
                   {
                       float t = x * inv;
                       Color color = Color.Lerp(colorStart, colorEnd, t);
                       gradient.SetPixel(x, y, color);
                   }
               }

               gradient.Apply();

               return gradient;
           }*/

        private static bool Unload(UnityModManager.ModEntry modEntry)
        {
            new Harmony(modEntry.Info.Id).UnpatchAll(modEntry.Info.Id);
            //TutorialCanvas.Main.Unload(modEntry);
            //ReflectionCache.Clear();
            return true;
        }

        private static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            settings.Save(modEntry);
        }

        // Called when the mod is turned to on/off.
        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value /* active or inactive */)
        {
            enabled = value;
            //TutorialCanvas.Main.OnToggle(modEntry, value);
            return true; // Permit or not.
        }

        /* public static ColorPicker CreateColorPicker()
         {
             var asd = new ColorPicker
             {
             colorPicker = ColorPickerLoad.GetTexture(ModEntry.Path + "\\ColorPicker\\color.png")
             };
             return asd;
         }*/

        public static void GetClasses()
        {
            if (Main.blueprints.Entries.Count == 0) Main.blueprints = Utilities.GetAllBlueprints();

            if (classes.Count == 0)
            {
                ///Main.logger.Log("bru");
                //foreach (BlueprintCharacterClass c in DollResourcesManager.classes)
                foreach (BlueprintCharacterClass c in Main.blueprints.Entries.Where(a => a.m_Type == typeof(BlueprintCharacterClass)).Select(b => ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>(b.Guid)))
                {
                    /* if (c.StartingItems.Any() && !c.PrestigeClass && !c.ToString().Contains("Scion"))
                     {
                         Main.logger.Log(c.ToString());
                     }*/
                    // if (!c.PrestigeClass && !c.IsMythic && !c.ToString().Contains("Mythic") && !c.ToString().Contains("Animal") && !c.ToString().Contains("Scion") && c.StartingItems.Any()) {
                    if (c.StartingItems.Any() && !c.PrestigeClass && !c.ToString().Contains("Scion"))
                    {
                        try
                        {
                            var charinfo = new CharInfo();
                            charinfo.Name = c.Name;
                            charinfo.GUID = c.AssetGuid.ToString();
                            if (classes.Count == 0)
                            {
                                var charinf = new CharInfo();
                                charinf.Name = "None";
                                var charinf2 = new CharInfo();
                                charinf2.Name = "Default";
                                classes.Add(charinf);
                                classes.Add(charinf2);
                            }
                            {
                                if (!classes.Any(asd => asd.Name == charinfo.Name))
                                {
                                    classes.Add(charinfo);
                                }
                            }
                            /*if(!classes.Contains(c.ToString()))
                            { */
                            /*classes.AddItem(c.ToString());
                            Main.logger.Log(c.ToString());*/
                            ///}
                        }
                        catch (Exception e) { Main.logger.Log(e.ToString()); }
                    }
                    Main.classesloaded = true;
                }
            }
        }

        private static Texture texture;

        public static void onGUI(UnityModManager.ModEntry modEntry)
        {
            try
            {
                if (!enabled)
                    return;
                if (GUILayout.Button("Fix Grey Characters (Rebuild)", UI.AutoWidth()))
                {
                    foreach (var ch in Game.Instance.Player.PartyAndPets.Concat(Game.Instance.Player.PartyAndPetsDetached))
                    {
                        CharacterManager.RebuildCharacter(ch);
                        /*foreach(var asd in settings.GetCharacterSettings(ch).weaponOverrides)
                        {
                            Settings.ParseOverrideTuple(asd.Key);
                        }*/
                    }
                }

                //fix this
                //if (UnityModManager.UI.Scale(1) != UIscale)
                // {
                // UIscale = UnityModManager.UI.Scale(1);
                // }

                /*foreach (var VARIABLE in Game.Instance.Player.Party)
                {
                    //Game.Instance.RootUiContext.InGameVM.StaticPartVM.;
                    GUILayout.Box(Object.FindObjectOfType<InGamePCView>().m_StaticPartPCView.m_ServiceWindowsPCView.m_InventoryPCView.m_DollView.GetComponentInChildren<RawImage>().mainTexture);
                    GUILayout.Box(Object.FindObjectOfType<InGamePCView>().m_StaticPartPCView.m_ServiceWindowsPCView.m_InventoryPCView.m_DollView.GetComponentInChildren<RawImage>().texture);
                    //Game.Instance.UI.ServiceWindow
                    //GUILayout.Box();
                }*/
                /* if(GUILayout.Button("joe"))
                {
                    EquipmentResourcesManager.BuildEELookup();
                    foreach (var VARIABLE in EquipmentResourcesManager.WingsEE)
                    {
                       // var va = VARIABLE.Value as UnityEngine.Object;
                       // var varr = new ResourceRef(VARIABLE.Value.ge);
                        Main.logger.Log(VARIABLE.Value.NameSafe());
                    }
                }
                /*
                if (GUILayout.Button("Joe2"))
                {
                    var playerData = Game.Instance.Player.AllCharacters.First();
                    var chrsttng = settings.GetCharacterSettings(playerData);
                    FxHelper.SpawnFxOnUnit(EquipmentResourcesManager.WingsFX[chrsttng.overrideWingsFX], playerData.View);
                }*/
#if DEBUG
                if(GUILayout.Button("Hide Barding"))
                {
                    foreach(var asdsdasdsad in Game.Instance.Player.AllCharacters.Where(a => a.IsPet))
                    {
                        GlobalVisualInfo.Instance.ForCharacter(asdsdasdsad).settings.hidebarding = true;
                    }
                }
                if (GUILayout.Button("UnHide Barding"))
                {
                    foreach (var asdsdasdsad in Game.Instance.Player.AllCharacters.Where(a => a.IsPet))
                    {
                        GlobalVisualInfo.Instance.ForCharacter(asdsdasdsad).settings.hidebarding = false;
                    }
                }
                if (GUILayout.Button("override medium Barding"))
                {
                    foreach (var asdsdasdsad in Game.Instance.Player.AllCharacters.Where(a => a.IsPet))
                    {
                        GlobalVisualInfo.Instance.ForCharacter(asdsdasdsad).settings.overridebarding = 1;
                    }
                }
                if (GUILayout.Button("override heavy Barding"))
                {
                    foreach (var asdsdasdsad in Game.Instance.Player.AllCharacters.Where(a => a.IsPet))
                    {
                        GlobalVisualInfo.Instance.ForCharacter(asdsdasdsad).settings.overridebarding = 2;
                    }
                }
                if (GUILayout.Button("clear Barding override"))
                {
                    foreach (var asdsdasdsad in Game.Instance.Player.AllCharacters.Where(a => a.IsPet))
                    {
                        GlobalVisualInfo.Instance.ForCharacter(asdsdasdsad).settings.overridebarding = 0;
                    }
                }
                if (GUILayout.Button("Generate EEs"))
                {
                    EquipmentResourcesManager.BuildEELookup();
                }
                if (GUILayout.Button("Print Buffs"))
                {
                    foreach(var buff in blueprints.Entries.Where(a => a.Type == typeof(BlueprintBuff)).Select(b => ResourcesLibrary.TryGetBlueprint<BlueprintBuff>(b.Guid)).Where(c => c.FxOnStart != null))
                    {
                        Main.logger.Log("m_AllFX[\"" + buff.NameForAcronym+ "\"] = \"" + buff.AssetGuidThreadSafe + "\";");
                    }
                }
                if (GUILayout.Button("Spawn Override FX", UI.AutoWidth()))
                {
                    foreach (var ch in Game.Instance.Player.PartyAndPets.Concat(Game.Instance.Player.PartyAndPetsDetached))
                    {
                        ch.SpawnOverrideBuffs();
                        /*foreach(var asd in settings.GetCharacterSettings(ch).weaponOverrides)
                        {
                            Settings.ParseOverrideTuple(asd.Key);
                        }*/
                    }
                }
                var strings = new List<string>();
                if (GUILayout.Button("Generate FX Map"))
                {
                    foreach (var kv in Main.blueprints.Entries.Where(a => a.Type == typeof(Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff)).Select(b => ResourcesLibrary.TryGetBlueprint<Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff>(b.Guid)).Where(a => a.FxOnRemove != null || a.FxOnStart != null).ToArray())
                    {
                        Main.logger.Log("m_AllFX[■" + kv.name + "■] = ■" + kv.AssetGuid + "■");
                    }
                }
                if (GUILayout.Button("Print FX Asset ID's to Log"))
                {
                    foreach (var assetid in strings)
                    {
                        var thing = ResourcesLibrary.TryGetResource<UnityEngine.Object>(assetid);
                        Main.logger.Log("m_AllFX[■" + thing.name + "■] = ■" + assetid + "■");
                    }
                }
#endif
                /*  if (GUILayout.Button("focreload"))
                  {
                      forceload = !forceload;
                  }*/
                UI.DisclosureToggle("Settings", ref showsettings, 175f);
                if (showsettings)
                {
                    UI.Label("Settings");
                    using (UI.HorizontalScope())
                    {
                        GUILayout.Space(20f);
                        using (UI.VerticalScope())
                        {
                            ModKit.UI.Toggle("Unlock all Portraits", ref settings.AllPortraits);
                            ModKit.UI.Toggle("Unlock Hair", ref settings.UnlockHair, HairUnlocker.RestoreOptions,
                                HairUnlocker.UnlockHair);
                            ModKit.UI.Label(
                                "Warning: Unlock hair might have some hairstyles that clip or are otherwise malformed");
                            ModKit.UI.Toggle("Enable Rebuild All Hotkey (Alt + X)", ref settings.enableHotKey);
                            /*ModKit.UI.Toggle("Unlock Hair Options",ref HairUnlocker.Main.settings.UnlockHair);
                            if (HairUnlocker.Main.settings.UnlockHair) ModKit.UI.Toggle("Unlock All Hair Options (Includes incompatible options)",ref HairUnlocker.Main.settings.UnlockAllHair);
                            ModKit.UI.Toggle("Unlock Horns",ref HairUnlocker.Main.settings.UnlockHorns);
                            ModKit.UI.Toggle("Unlock Tails",ref HairUnlocker.Main.settings.UnlockTail);
                            ModKit.UI.Toggle("Unlock Female Dwarf Beards (Includes incompatible options",ref HairUnlocker.Main.settings.UnlockFemaleDwarfBeards);*/
                        }
                    }
                }

                ///Asd();
                ///Main.logger.Log(classes.Count.ToString());
                /*foreach(CharInfo s in classes)
                {
                    Main.logger.Log(s.Name + s.GUID);
                }*/
                if (Game.Instance.Player.AllCharacters.Count > 0)
                {
                    foreach (UnitEntityData unitEntityData in Game.Instance.Player.AllCharacters)
                    {
                        if (!settings.characterSettings.Values.Any(settin => settin.uniqueid == unitEntityData.UniqueId))
                        {
                            if (!unitEntityData.IsPet)
                            {
                                var charinfo = new CharInfo
                                {
                                    GUID = unitEntityData.Progression.GetEquipmentClass().AssetGuidThreadSafe,
                                    Name = unitEntityData.Progression.GetEquipmentClass().Name
                                };
                                if (unitEntityData.IsStoryCompanion())
                                {
                                    charinfo.Name = "Default";
                                }
                                var fb = new CharacterSettings
                                {
                                    characterName = unitEntityData.CharacterName,
                                    classOutfit = charinfo,
                                    uniqueid = unitEntityData.UniqueId
                                };
                                settings.AddCharacterSettings(unitEntityData, fb);
                                // Main.GetIndices(unitEntityData);
                                // Main.SetEELs(unitEntityData, DollResourcesManager.GetDoll(unitEntityData));
                                /* Settings.CharacterSettings characterSettings = new CharacterSettings{
                                 characterSettings.characterName = unitEntityData.CharacterName;
                                 characterSettings.classOutfit = new CharInfo
                                 {
                                     ///GUID = unitEntityData.Progression.GetEquipmentClass().AssetGuidThreadSafe,
                                     Name = "Default"
                                 };
                                 characterSettings.PrimaryColor = unitEntityData.Descriptor.m_LoadedDollData.ClothesPrimaryIndex;
                                 characterSettings.SecondaryColor = unitEntityData.Descriptor.m_LoadedDollData.ClothesSecondaryIndex;
                                 settings.AddCharacterSettings(unitEntityData, characterSettings);*/
                                ///if (!unitEntityData.IsStoryCompanion())
                                {
                                    /// var races = BlueprintRoot.Instance.Progression.CharacterRaces.ToArray<BlueprintRace>();
                                    /// var gender = unitEntityData.Gender;
                                    /// var race = unitEntityData.Progression.Race;
                                    /// var customizationOptions = gender != Gender.Male ? race.FemaleOptions : race.MaleOptions;
                                    /// GetIndices(unitEntityData, characterSettings, DollResourcesManager.GetDoll(unitEntityData), customizationOptions);
                                }
                            }
                            else
                            {
                                var fb = new CharacterSettings
                                {
                                    characterName = unitEntityData.CharacterName,
                                    uniqueid = unitEntityData.UniqueId
                                };
                                settings.AddCharacterSettings(unitEntityData, fb);
                            }
                        }
                    }
                    foreach (UnitEntityData unitEntityData in Game.Instance.Player.PartyAndPets)
                    {
                        var characterSettings = settings.GetCharacterSettings(unitEntityData);
                        if (!unitEntityData.IsPet)
                        {
                            if (characterSettings.classOutfit == null)
                            {
                                characterSettings = Main.settings.GetCharacterSettings(unitEntityData);
                                characterSettings.classOutfit.Name = unitEntityData.Descriptor.Progression.GetEquipmentClass().Name;
                                characterSettings.classOutfit.GUID = unitEntityData.Descriptor.Progression.GetEquipmentClass().AssetGuidThreadSafe;
                            }
                            UI.Space(4f);
                            DollState doll;
                            using (UI.HorizontalScope())
                            {
                                UI.Label(string.Format("{0}", unitEntityData.CharacterName), GUILayout.Width(DefaultLabelWidth));
                                UI.Space(25);
                                UI.DisclosureToggle("Select EEs", ref characterSettings.showEELsSelection);
                                UI.Space(25);
                                UI.DisclosureToggle("Select Outfit", ref characterSettings.showClassSelection);
                                UI.Space(25);
                                doll = DollResourcesManager.GetDoll(unitEntityData);
                                UI.DisclosureToggle("Select Doll", ref characterSettings.showDollSelection);
                                UI.Space(25);
                                UI.DisclosureToggle("Select Equipment", ref characterSettings.showEquipmentSelection);
                                UI.Space(75);
                                UI.DisclosureToggle("Select Overrides", ref characterSettings.showOverrideSelection);
                                UI.Space(75);
                                ///characterSettings.ReloadStuff = GUILayout.Toggle(characterSettings.ReloadStuff, "Reload", GUILayout.ExpandWidth(false));
#if (DEBUG)
                                /*  characterSettings.showInfo = */
                                ModKit.UI.DisclosureToggle("Show Info", ref characterSettings.showInfo);
#endif
                            }
                            if (characterSettings.showEELsSelection == true)
                            {
                                EELpickerUI.OnGUI(unitEntityData);
                                GUILayout.Space(5f);
                            }
                            if (characterSettings.showClassSelection)
                            {
                                ChooseClassOutfit(characterSettings, unitEntityData);
                                GUILayout.Space(5f);
                            }
                            if (doll != null && characterSettings.showDollSelection)
                            {
                                ChooseDoll(unitEntityData);
                                GUILayout.Space(5f);
                            }
                            if (doll == null && characterSettings.showDollSelection)
                            {
                                ChooseCompanionColor(characterSettings, unitEntityData);
                                GUILayout.Space(5f);
                            }
                            if (characterSettings.showEquipmentSelection)
                            {
                                ChooseEquipment(unitEntityData, characterSettings);
                                GUILayout.Space(5f);
                            }
                            if (characterSettings.showOverrideSelection)
                            {
                                ChooseEquipmentOverride(unitEntityData, characterSettings);
                                GUILayout.Space(5f);
                            }
#if (DEBUG)
                            if (characterSettings.showInfo) {
                                InfoManager.ShowInfo(unitEntityData);
                                GUILayout.Space(5f);
                            }
#endif
                        }
                        else
                        {
                            GUILayout.Space(4f);
                            GUILayout.BeginHorizontal();
                            ModKit.UI.Label(string.Format("{0}", unitEntityData.CharacterName), GUILayout.Width(DefaultLabelWidth));
                            ModKit.UI.DisclosureToggle("Show Override Selection", ref characterSettings.showOverrideSelection);
                            ModKit.UI.DisclosureToggle("Select Equipment", ref characterSettings.showEquipmentSelection);
                            GUILayout.EndHorizontal();
                            if (characterSettings.showOverrideSelection)
                            {
                                ChooseEquipmentOverridePet(unitEntityData, characterSettings);
                            }
                            if (characterSettings.showEquipmentSelection)
                            {
                                ChooseEquipmentPet(unitEntityData, characterSettings);
                            }
                        }
                    }
                    if (Game.Instance.Player.AllCharacters.Except(Game.Instance.Player.Party).Count() > 0)
                    {
                        GUILayout.Space(20);
                        GUILayout.BeginHorizontal();
                        ModKit.UI.Label(string.Format("{0}", "Remote Characters"), GUILayout.Width(400f));
                        GUILayout.EndHorizontal();
                        foreach (UnitEntityData unitEntityData in Game.Instance.Player.AllCharacters.Except(Game.Instance.Player.PartyAndPets))
                        {
                            var characterSettings = settings.GetCharacterSettings(unitEntityData);
                            if (!unitEntityData.IsPet)
                            {
                                if (characterSettings.classOutfit == null)
                                {
                                    characterSettings = Main.settings.GetCharacterSettings(unitEntityData);
                                    characterSettings.classOutfit.Name = unitEntityData.Descriptor.Progression.GetEquipmentClass().Name;
                                    characterSettings.classOutfit.GUID = unitEntityData.Descriptor.Progression.GetEquipmentClass().AssetGuidThreadSafe;
                                }
                                GUILayout.Space(4f);
                                GUILayout.BeginHorizontal();
                                ModKit.UI.Label(string.Format("{0}", unitEntityData.CharacterName), GUILayout.Width(DefaultLabelWidth));
                                ModKit.UI.DisclosureToggle("Select Outfit", ref characterSettings.showClassSelection);
                                var doll = DollResourcesManager.GetDoll(unitEntityData);
                                ModKit.UI.DisclosureToggle("Select Doll", ref characterSettings.showDollSelection);
                                ModKit.UI.DisclosureToggle("Select Equipment", ref characterSettings.showEquipmentSelection);
                                ModKit.UI.DisclosureToggle("Select Overrides", ref characterSettings.showOverrideSelection);
                                ///characterSettings.ReloadStuff = GUILayout.Toggle(characterSettings.ReloadStuff, "Reload", GUILayout.ExpandWidth(false));
#if (DEBUG)
                                /*  characterSettings.showInfo = */
                                ModKit.UI.DisclosureToggle("Show Info", ref characterSettings.showInfo);
#endif
                                GUILayout.EndHorizontal();
                                /*if (characterSettings.ReloadStuff == true)
                                {
                                    CharacterManager.UpdateModel(unitEntityData.View);
                                }*/
                                if (characterSettings.showClassSelection)
                                {
                                    ChooseClassOutfit(characterSettings, unitEntityData);
                                    GUILayout.Space(5f);
                                }
                                if (doll != null && characterSettings.showDollSelection)
                                {
                                    ChooseDoll(unitEntityData);
                                    GUILayout.Space(5f);
                                }
                                if (doll == null && characterSettings.showDollSelection)
                                {
                                    ChooseCompanionColor(characterSettings, unitEntityData);
                                    GUILayout.Space(5f);
                                }
                                if (characterSettings.showEquipmentSelection)
                                {
                                    ChooseEquipment(unitEntityData, characterSettings);
                                    GUILayout.Space(5f);
                                }
                                if (characterSettings.showOverrideSelection)
                                {
                                    ChooseEquipmentOverride(unitEntityData, characterSettings);
                                    GUILayout.Space(5f);
                                }
#if (DEBUG)
                                if (characterSettings.showInfo)
                                {
                                    InfoManager.ShowInfo(unitEntityData);
                                    GUILayout.Space(5f);
                                }
#endif
                            }
                            else
                            {
                                GUILayout.Space(4f);
                                GUILayout.BeginHorizontal();
                                ModKit.UI.Label(string.Format("{0}", unitEntityData.CharacterName), GUILayout.Width(DefaultLabelWidth));
                                ModKit.UI.DisclosureToggle("Show Override Selection", ref characterSettings.showOverrideSelection);
                                ModKit.UI.DisclosureToggle("Select Equipment", ref characterSettings.showEquipmentSelection);
                                GUILayout.EndHorizontal();
                                if (characterSettings.showOverrideSelection)
                                {
                                    ChooseEquipmentOverridePet(unitEntityData, characterSettings);
                                }
                                if (characterSettings.showEquipmentSelection)
                                {
                                    ChooseEquipmentPet(unitEntityData, characterSettings);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log(e.ToString() + " " + e.StackTrace);
            }
        }

        private static void ChooseClassOutfit(CharacterSettings characterSettings, UnitEntityData unitEntityData)
        {
            var focusedStyle = new GUIStyle(GUI.skin.button);
            focusedStyle.normal.textColor = Color.yellow;
            focusedStyle.focused.textColor = Color.yellow;
            GUILayout.BeginHorizontal();
            foreach (var _class in classes)
            {
                if (_class.Name == "Druid")
                {
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                }
                if (_class.Name == "Ranger")
                {
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                }
                var style = characterSettings.classOutfit == _class ? focusedStyle : GUI.skin.button;
                if (GUILayout.Button(_class.Name, style, GUILayout.Width(100f)))
                {
                    characterSettings.classOutfit = _class;
                    CharacterManager.RebuildCharacter(unitEntityData);
                    unitEntityData.View.UpdateClassEquipment();
                }
            }
            GUILayout.EndHorizontal();
        }

        private static void ChoosePortrait(UnitEntityData unitEntityData)
        {
            if (unitEntityData.Portrait.IsCustom)
            {
                var key = unitEntityData.Descriptor.UISettings.CustomPortraitRaw.CustomId;
                var currentIndex = DollResourcesManager.CustomPortraits.IndexOf(key);
                int newIndex;
                string value;
                using (UI.HorizontalScope())
                {
                    UI.Label("Portrait ", GUILayout.Width(DefaultLabelWidth));
                    newIndex = (int)Math.Round(GUILayout.HorizontalSlider(currentIndex, 0, DollResourcesManager.CustomPortraits.Count, GUILayout.Width(DefaultSliderWidth)), 0);
                    UI.Space(204);
                    if (GUILayout.Button("<", GUILayout.Width(55)) && currentIndex >= 0)
                    {
                        newIndex = currentIndex - 1;
                    }
                    if (GUILayout.Button(">", GUILayout.Width(55)) && currentIndex < DollResourcesManager.CustomPortraits.Count - 1)
                    {
                        newIndex = currentIndex + 1;
                    }
                    UI.Space(25);
                    if (GUILayout.Button("Use Normal", GUILayout.Width(200)))
                    {
                        unitEntityData.Descriptor.UISettings.SetPortrait(ResourcesLibrary.TryGetBlueprint<BlueprintPortrait>("621ada02d0b4bf64387babad3a53067b"));
                        EventBus.RaiseEvent<IUnitPortraitChangedHandler>(delegate (IUnitPortraitChangedHandler h)
                        {
                            h.HandlePortraitChanged(unitEntityData);
                        });
                        return;
                    }
                    value = newIndex >= 0 && newIndex < DollResourcesManager.CustomPortraits.Count ? DollResourcesManager.CustomPortraits[newIndex] : null;
                    ModKit.UI.Label(" " + value, UI.AutoWidth());
                }
                if (newIndex != currentIndex && value != null)
                {
                    unitEntityData.Descriptor.UISettings.SetPortrait(new PortraitData(value));
                    EventBus.RaiseEvent<IUnitPortraitChangedHandler>(delegate (IUnitPortraitChangedHandler h)
                    {
                        h.HandlePortraitChanged(unitEntityData);
                    });
                }
            }
            else
            {
                var key = unitEntityData.Descriptor.UISettings.PortraitBlueprint?.name;
                var currentIndex = DollResourcesManager.Portrait.IndexOfKey(key ?? "");
                GUILayout.BeginHorizontal();
                ModKit.UI.Label("Portrait ", GUILayout.Width(DefaultLabelWidth));
                var newIndex = (int)Math.Round(GUILayout.HorizontalSlider(currentIndex, 0, DollResourcesManager.Portrait.Count, GUILayout.Width(DefaultSliderWidth)), 0);
                if (GUILayout.Button("<", GUILayout.Width(55)) && currentIndex >= 0)
                {
                    newIndex = currentIndex - 1;
                }
                if (GUILayout.Button(">", GUILayout.Width(55)) && currentIndex < DollResourcesManager.Portrait.Count - 1)
                {
                    newIndex = currentIndex + 1;
                }
                if (GUILayout.Button("Use Custom", GUILayout.Width(125f)))
                {
                    unitEntityData.Descriptor.UISettings.SetPortrait(CustomPortraitsManager.Instance.CreateNewOrLoadDefault());
                    EventBus.RaiseEvent<IUnitPortraitChangedHandler>(delegate (IUnitPortraitChangedHandler h)
                    {
                        h.HandlePortraitChanged(unitEntityData);
                    });
                    return;
                }
                var value = newIndex >= 0 && newIndex < DollResourcesManager.Portrait.Count ? DollResourcesManager.Portrait.Values[newIndex] : null;
                ModKit.UI.Label(" " + value, GUILayout.ExpandWidth(false));

                GUILayout.EndHorizontal();
                if (newIndex != currentIndex && value != null)
                {
                    unitEntityData.Descriptor.UISettings.SetPortrait(value);
                    EventBus.RaiseEvent<IUnitPortraitChangedHandler>(delegate (IUnitPortraitChangedHandler h)
                    {
                        h.HandlePortraitChanged(unitEntityData);
                    });
                }
            }
        }

        private static void ChooseAsks(UnitEntityData unitEntityData)
        {
            int currentIndex = -1;
            if (unitEntityData.Descriptor.CustomAsks != null)
            {
                currentIndex = DollResourcesManager.Asks.IndexOfKey(unitEntityData.Descriptor.CustomAsks.name);
            }
            int newIndex;
            BlueprintUnitAsksList value;
            using (UI.HorizontalScope())
            {
                ModKit.UI.Label("Custom Voice ", GUILayout.Width(DefaultLabelWidth));
                newIndex = (int)Math.Round(GUILayout.HorizontalSlider(currentIndex, -1, DollResourcesManager.Asks.Count - 1, GUILayout.Width(DefaultSliderWidth)), 0);
                UI.Space(204);
                if (GUILayout.Button("<", GUILayout.Width(55)) && currentIndex >= 0)
                {
                    newIndex = currentIndex - 1;
                }
                if (GUILayout.Button(">", GUILayout.Width(55)) && currentIndex < DollResourcesManager.Asks.Count)
                {
                    newIndex = currentIndex + 1;
                }
                value = (newIndex >= 0 && newIndex < DollResourcesManager.Asks.Count) ? DollResourcesManager.Asks.Values[newIndex] : null;
                UI.Space(25);
                if (GUILayout.Button("Preview", GUILayout.ExpandWidth(false)))
                {
                    var component = value?.GetComponent<UnitAsksComponent>();
                    if (component != null && component.PreviewSound != "")
                    {
                        component.PlayPreview();
                    }
                    else if (component != null && component.Selected.HasBarks)
                    {
                        var bark = component.Selected.Entries.Random();
                        AkSoundEngine.PostEvent(bark.AkEvent, unitEntityData.View.gameObject);
                    }
                }
                ModKit.UI.Label(" " + (value?.name ?? "None"), GUILayout.ExpandWidth(false));
            }
            if (newIndex != currentIndex)
            {
                unitEntityData.Descriptor.CustomAsks = value;
                unitEntityData.View?.UpdateAsks();
            }
        }

        private static void ChooseFromList<T>(string label, IReadOnlyList<T> list, ref int currentIndex, Action onChoose)
        {
            if (list.Count == 0)
                return;
            GUILayout.BeginHorizontal();
            UI.Label(label + " ", GUILayout.Width(DefaultLabelWidth));
            var newIndex = (int)Math.Round(GUILayout.HorizontalSlider(currentIndex, 0, list.Count - 1, GUILayout.Width(DefaultSliderWidth)), 0);
            UI.Label($"  {newIndex}".orange() + $" ({list.Count})".cyan(), UI.Width(203));
            ;
            if (GUILayout.Button("<", GUILayout.Width(55f)) && newIndex > 0)
                newIndex--;
            if (GUILayout.Button(">", GUILayout.Width(55f)) && newIndex < list.Count - 1)
                newIndex++;
            GUILayout.EndHorizontal();
            if (newIndex != currentIndex && newIndex < list.Count)
            {
                currentIndex = newIndex;
                onChoose();
            }
        }

        public static void ChooseFromListforee<T>(string label, IReadOnlyList<T> list, ref int currentIndex, Action onChoose)
        {
            if (list.Count == 0)
                return;
            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(false));
            UI.Label(label + "", GUILayout.Width(120f * Main.UIscale));
            var newIndex = (int)Math.Round(GUILayout.HorizontalSlider(currentIndex, 0, list.Count - 1, GUILayout.Width(175f * Main.UIscale)), 0);
            UI.Label($"  {newIndex}".orange() + $" ({list.Count})".cyan(), UI.Width(50f * Main.UIscale));
            ;
            if (GUILayout.Button("<", GUILayout.Width(35f * Main.UIscale)) && newIndex > 0)
                newIndex--;
            if (GUILayout.Button(">", GUILayout.Width(35f * Main.UIscale)) && newIndex < list.Count - 1)
                newIndex++;
            GUILayout.EndHorizontal();
            if (newIndex != currentIndex && newIndex < list.Count)
            {
                currentIndex = newIndex;
                onChoose();
            }
        }

        private static void ChooseFromList2<T>(string label, IReadOnlyList<T> list, ref int currentIndex, Action onChoose)
        {
            if (list.Count == 0)
                return;
            ///GUILayout.BeginHorizontal();
            ModKit.UI.Label(label + " ", GUILayout.Width(DefaultLabelWidth));
            var newIndex = (int)Math.Round(GUILayout.HorizontalSlider(currentIndex, 0, list.Count - 1, GUILayout.Width(DefaultSliderWidth)), 0);
            ModKit.UI.Label(" " + newIndex, GUILayout.ExpandWidth(false));
            if (newIndex != currentIndex && newIndex < list.Count)
            {
                currentIndex = newIndex;
                onChoose();
            }
            ///GUILayout.EndHorizontal();
        }

        private static void ChooseEEL(UnitEntityData unitEntityData, DollState doll, string label, EquipmentEntityLink[] links, EquipmentEntityLink link, Action<EquipmentEntityLink> setter)
        {
            if (links.Length == 0)
            {
                GUILayout.Label($"Missing equipment for {label}");
            }
            var index = links.ToList().FindIndex((eel) => eel != null && eel.AssetId == link?.AssetId);
            ChooseFromList(label, links, ref index, () =>
            {
                setter(links[index]);
                unitEntityData.Parts.Get<UnitPartDollData>().Default = ModifiedCreateDollData.CreateDataModified(doll);
                CharacterManager.RebuildCharacter(unitEntityData);
            });
        }

        private static void ChooseAdditionalVisual(UnitEntityData unitEntityData, string label, List<string> links, CharacterSettings settings)
        {
            if (links.Count == 0)
            {
                GUILayout.Label($"Missing equipment for {label}");
            }
            var index = links.FindIndex((eel) => eel != null && eel == unitEntityData.View.CharacterAvatar?.m_AdditionalVisualSettings?.name);
            if (index == -1) index = 0;
            ChooseFromList(label, links, ref index, () =>
            {
                settings.overrideMythic = links[index];
                CharacterManager.RebuildCharacter(unitEntityData);
            });
        }

        private static void ChooseEEL(ref int setting, UnitEntityData unitEntityData, DollState doll, string label, EquipmentEntityLink[] links, EquipmentEntityLink link, Action<EquipmentEntityLink> setter)
        {
            var settings = Main.settings.GetCharacterSettings(unitEntityData);
            if (links.Length == 0)
            {
                ModKit.UI.Label($"Missing equipment for {label}");
            }
            var index = setting;
            ///var index = links.ToList().FindIndex((eel) => eel != null && eel.AssetId == link?.AssetId);
            ChooseFromList(label, links, ref index, () =>
            {
                //Main.SetEELs(unitEntityData, doll);

                setter(links[index]);
            });
            if (setting != index)
            {
                setting = index;
                // unitEntityData.Parts.Get<UnitPartDollData>().Default = doll.CreateData();
                // CharacterManager.RebuildCharacter(unitEntityData);
                //Main.SetEELs(unitEntityData, doll);
            }
        }

        private static void ChooseEEL(ref int setting, UnitEntityData unitEntityData, DollState doll, string label, EquipmentEntityLink[] links, EquipmentEntityLink link)
        {
            if (links.Length == 0)
            {
                ModKit.UI.Label($"Missing equipment for {label}");
            }
            var index = links.ToList().FindIndex((eel) => eel != null && eel.AssetId == link?.AssetId); ///var index = setting;
            ChooseFromList(label, links, ref index, () =>
            {
                unitEntityData.Descriptor.Doll = ModifiedCreateDollData.CreateDataModified(doll);
            });
            if (setting != index)
            {
                setting = index;
                //SetEELs(unitEntityData, doll);
                //CharacterManager.RebuildCharacter(unitEntityData);
            }
        }

        private static void ChooseRamp(ref int setting, UnitEntityData unitEntityData, DollState doll, string label, List<Texture2D> textures, int currentRamp, Action<int> setter)
        {
            try
            {
                GUILayout.BeginHorizontal();
                ChooseFromList(label, textures, ref currentRamp, () =>
                {
                    setter(currentRamp);
                    var DollPart = unitEntityData.Parts.Get<UnitPartDollData>().Default = ModifiedCreateDollData.CreateDataModified(doll);
                    //   Traverse.Create(DollPart).Field("ActiveDoll").SetValue(doll.CreateData());
                    /// unitEntityData.Parts.Get<UnitPartDollData>().ActiveDoll = doll.CreateData();
                });
                if (setting != currentRamp)
                {
                    setting = currentRamp;
                    // CharacterManager.RebuildCharacter(unitEntityData);
                    // SetEELs(unitEntityData, DollResourcesManager.GetDoll(unitEntityData));
                }
                GUILayout.EndHorizontal();
            }
            catch (Exception e)
            {
                Main.logger.Log(e.ToString());
            }
        }

        private static void ChooseRamp(UnitEntityData unitEntityData, DollState doll, string label, List<Texture2D> textures, int currentRamp, Action<int> setter)
        {
            ChooseFromList(label, textures, ref currentRamp, () =>
            {
                setter(currentRamp);
                unitEntityData.Parts.Get<UnitPartDollData>().Default = ModifiedCreateDollData.CreateDataModified(doll);
                CharacterManager.RebuildCharacter(unitEntityData);
            });
        }

        /*public static int GetRaceIndex(UnitEntityData data)
        {
            var race = data.Progression.Race;
            var Settings = settings.GetCharacterSettings(data);
            //Main.logger.Log(race.Name.ToString());
            var result = -2;
            if (race.Name.ToString().Contains("Human")) {
                result = 0;
            }
            else if (race.Name.ToString().Contains("Elf") && !race.Name.ToString().Contains("Half")) {
                result = 1;
            }
            else if (race.Name.ToString().Contains("Dwarf")) {
                result = 2;
            }
            else if (race.Name.ToString().Contains("Gnome")) {
                result = 3;
            }
            else if (race.Name.ToString().Contains("Halfling")) {
                result = 4;
            }
            else if (race.Name.ToString().Contains("Half-Elf")) {
                result = 5;
            }
            else if (race.Name.ToString().Contains("Half-Orc")) {
                result = 6;
            }
            else if (race.Name.ToString().Contains("Aasimar")) {
                result = 7;
            }
            else if (race.Name.ToString().Contains("Tiefling")) {
                result = 8;
            }
            else if (race.Name.ToString().Contains("Oread")) {
                result = 9;
            }
            else if (race.Name.ToString().Contains("Dhampir")) {
                result = 10;
            }
            else if (race.Name.ToString().Contains("Kitsune")) {
                result = 11;
            }
            Settings.RaceIndex = result;
            Main.logger.Log(result.ToString());
            return result;
        }*/
        // still doesnt move the silder
        /*  static void ChooseRace(UnitEntityData unitEntityData, DollState doll)
          {
              var Settings = settings.GetCharacterSettings(unitEntityData);
              var index = -3;
              if (Settings.RaceIndex == -1 || Settings.RaceIndex == -2)
              {
                  Settings.RaceIndex = GetRaceIndex(unitEntityData);
              }
              ///Main.logger.Log(unitEntityData.Progression.Race.Name.ToString());
              ///var currentRace = Settings.dollrace;
              var races = BlueprintRoot.Instance.Progression.CharacterRaces;
              var racelist = BlueprintRoot.Instance.Progression.CharacterRaces.ToArray<BlueprintRace>();
              if (index == -3)
              {
                  index = Settings.RaceIndex;
              }
              index = Settings.RaceIndex;
             /// var index = Array.FindIndex(racelist, (race) => race == currentRace);
             /// Main.logger.Log(index.ToString());
             var newindex = (int)Math.Round(GUILayout.HorizontalSlider(index, (float)0.0, (float)racelist.Count() - (float)1, GUILayout.Width(DefaultSliderWidth)), 1);
              ModKit.UI.Label(" " + index, GUILayout.ExpandWidth(false));
              ModKit.UI.Label(" " + newindex, GUILayout.ExpandWidth(false));
              GUILayout.BeginHorizontal();

              ChooseFromList("Race", racelist,ref index, () => {
                  doll.SetRace(racelist[index]);
                  unitEntityData.Descriptor.m_LoadedDollData = doll.CreateData();
                  CharacterManager.RebuildCharacter(unitEntityData);
              });
              ModKit.UI.Label(" " + racelist[index].Name);
              GUILayout.EndHorizontal();
          }*/
        /*static void ChooseRace(UnitEntityData unitEntityData, DollState doll)
        {
            var Settings = settings.GetCharacterSettings(unitEntityData);
            var races = BlueprintRoot.Instance.Progression.CharacterRaces.ToArray<BlueprintRace>();
            var currentRace = races.ElementAt(Settings.RaceIndex);
            Main.logger.Log(currentRace.ToString()+ " " +currentRace.Name);
            var index = Array.FindIndex<BlueprintRace>(races, (race) => race == currentRace);
            var sus = Settings.RaceIndex;
            if (Settings.RaceIndex == -1)
            {
                Settings.RaceIndex = Array.FindIndex<BlueprintRace>(races, (race) => race == currentRace);
            }
            GUILayout.BeginHorizontal();
            ChooseFromList2("Race", races, ref sus, () => {
                doll.SetRace(races[sus]);
                unitEntityData.Descriptor.m_LoadedDollData = doll.CreateData();
                CharacterManager.RebuildCharacter(unitEntityData);
            });
            ModKit.UI.Label(" " + races[sus].Name);
            if(sus != index)
            {
                sus = index;
                Settings.RaceIndex = sus;
            }
            GUILayout.EndHorizontal();
        }*/

        private static void ChooseRace(UnitEntityData unitEntityData, DollState doll)
        {
            var currentRace = doll.Race;
            var races = BlueprintRoot.Instance.Progression.CharacterRaces.ToArray<BlueprintRace>();
            var index = Array.FindIndex(races, (race) => race == currentRace);
            if (index > races.Length - 1 || index < 0)
            {
                index = 0;
            }
            GUILayout.BeginHorizontal();
            ModKit.UI.Label("Race" + " ", GUILayout.Width(DefaultLabelWidth));
            index = (int)Math.Round(GUILayout.HorizontalSlider((float)index, 0, races.Length - 1, GUILayout.Width(DefaultSliderWidth)));
            UI.Label($"  {index}".orange() + $" ({races.Length})".cyan(), UI.Width(85));
            ModKit.UI.Label(" " + races[index].Name, UI.Width(115));
            if (GUILayout.Button("<", GUILayout.Width(55f)) && index > 0)
                index--;
            if (GUILayout.Button(">", GUILayout.Width(55f)) && index < races.Length - 1)
                index++;
            GUILayout.EndHorizontal();
            if (index != races.IndexOf(currentRace) && index < races.Count())
            {
                doll.SetRace(races[index]);
                // unitEntityData.Descriptor.Doll = ModifiedCreateDollData.CreateDataModified(doll);
                unitEntityData.Parts.Get<UnitPartDollData>().Default = ModifiedCreateDollData.CreateDataModified(doll);
                CharacterManager.RebuildCharacter(unitEntityData);
            }
        }

        private static void ChooseVisualPreset(UnitEntityData unitEntityData, DollState doll, string label, BlueprintRaceVisualPreset[] presets,
            BlueprintRaceVisualPreset currentPreset)
        {
            var index = Array.FindIndex(presets, (vp) => vp == currentPreset);
            ChooseFromList(label, presets, ref index, () =>
            {
                doll.SetRacePreset(presets[index]);
                unitEntityData.Parts.Get<UnitPartDollData>().Default = ModifiedCreateDollData.CreateDataModified(doll);
                //unitEntityData.Descriptor.Doll = doll.CreateData();

                CharacterManager.RebuildCharacter(unitEntityData);
            });
        }

        /* public static void GetIndices(UnitEntityData dat)
         {
             try
             {
                 var doll2 = DollResourcesManager.GetDoll(dat);
                 if (dat.IsStoryCompanion() && doll2 == null)
                     return;
                 // Main.logger.Log("triedgetindices");
                 var doll = DollResourcesManager.GetDoll(dat);
                 var gender = dat.Gender;
                 var race = doll.Race;
                 CustomizationOptions customizationOptions = gender != Gender.Male ? race.FemaleOptions : race.MaleOptions;
                 ///CustomizationOptions customizationOptions = new CustomizationOptions();
                 var charsettings = settings.GetCharacterSettings(dat);
                 /*if (gender == Gender.Male)
                 {
                     customizationOptions = new CustomizationOptions
                     {
                         Beards = race.MaleOptions.Beards,
                         Eyebrows = race.MaleOptions.Eyebrows,
                         Hair = race.MaleOptions.Hair,
                         Heads = race.MaleOptions.Beards,
                         Horns = race.MaleOptions.Beards,
                         TailSkinColors = race.MaleOptions.TailSkinColors
                     };
                 }
                 else if (gender == Gender.Female)
                 {
                     customizationOptions = new CustomizationOptions
                     {
                         Beards = race.FemaleOptions.Beards,
                         Eyebrows = race.FemaleOptions.Eyebrows,
                         Hair = race.FemaleOptions.Hair,
                         Heads = race.FemaleOptions.Beards,
                         Horns = race.FemaleOptions.Beards,
                         TailSkinColors = race.FemaleOptions.TailSkinColors
                     };
                 }*//*
                 ///var doll = dat.Descriptor.m_LoadedDollData;
                 /// var doll = DollResourcesManager.CreateDollState(dat);

                 if (doll == null) {
                     try {
                         doll = DollResourcesManager.CreateDollState(dat);
                     }
                     catch (Exception e) { Main.logger.Error(e.Message + "  " + e.StackTrace); }
                 }
                 charsettings.Face = Array.IndexOf(customizationOptions.Heads, doll.Head.m_Link);
                 Main.Log($"hairCount: {customizationOptions.Hair.Count()}");
                 if (customizationOptions.Hair.Count() > 0)
                     charsettings.Hair = Array.IndexOf(customizationOptions.Hair, doll.Hair.m_Link);
                 if (customizationOptions.Beards.Count() > 0)
                     charsettings.Beards = Array.IndexOf(customizationOptions.Beards, doll.Beard.m_Link);
                 if (customizationOptions.Horns.Count() > 0)
                     charsettings.Horns = Array.IndexOf(customizationOptions.Horns, doll.Horn.m_Link);
                 /// Main.logger.Log("hornpassed");
                 if (customizationOptions.Hair.Count() > 0)
                     charsettings.HairColor = doll.HairRampIndex;
                 /// Main.logger.Log("haircolorpassed");
                 charsettings.SkinColor = doll.SkinRampIndex;
                 ///charsettings.SkinColor = 1;
                 ///Main.logger.Log("skincolorpassed");
                 if (customizationOptions.Horns.Count() > 0)
                     charsettings.HornsColor = doll.HornsRampIndex;
                 /// Main.logger.Log("horncolorpassed");
                 charsettings.PrimaryColor = doll.EquipmentRampIndex;
                 charsettings.SecondaryColor = doll.EquipmentRampIndexSecondary;
                 charsettings.RaceIndex = Array.IndexOf(BlueprintRoot.Instance.Progression.CharacterRaces.ToArray<BlueprintRace>(), doll.Race);
                 // charsettings.BodyType = doll.;
                 charsettings.Warpaint = doll.Warpaints.IndexOf(doll.Warpaint.m_Link);
                 charsettings.WarpaintCol = doll.WarpaintRampIndex;
                 charsettings.Scar = doll.Scars.IndexOf(doll.Scar.m_Link);
                 ///Main.logger.Log("Got Indices");
             }
             catch (Exception e) {
                 Main.logger.Log(e.Message);
                 Main.logger.Log(e.StackTrace);
                 Main.logger.Log(e.Source);
                 Main.logger.Log(e.InnerException.Message);
             };
         }
         public static void SetEELs(UnitEntityData dat, DollState doll, bool shouldRebuild = true) {
             try {
                 if (doll == null)
                     return;
         }*/
        /* public static void SetEELs(UnitEntityData dat, DollState doll, bool shouldRebuild = true)
         {
             try
             {
                 return;
                 if (doll == null) return;
                 var Settings = settings.GetCharacterSettings(dat);
                 var gender = dat.Gender;
                 var races = BlueprintRoot.Instance.Progression.CharacterRaces.ToArray<BlueprintRace>();
                 BlueprintRace race;
                 if (Settings.RaceIndex != -1) {
                     race = races[Settings.RaceIndex];
                 }
                 else {
                     if (!dat.Descriptor.Progression.Race.NameForAcronym.Contains("Mongrel")) {
                         race = dat.Progression.Race;
                     }
                     else {
                         race = Utilities.GetBlueprint<BlueprintRace>("0a5d473ead98b0646b94495af250fdc4");
                     }
                 }
                 doll.SetRace(race);
                 Settings.BodyType = EELIndex(Settings.BodyType, doll.Race.Presets.Length);
                 doll.SetRacePreset(doll.Race.Presets[Settings.BodyType]);
                 if (dat.Gender == Gender.Male) {
                     dat.View.CharacterAvatar.Skeleton = doll.RacePreset.MaleSkeleton;
                     dat.View.CharacterAvatar.m_Skeleton = doll.RacePreset.MaleSkeleton;
                     dat.View.CharacterAvatar.m_SkeletonChanged = true;
                 }
                 else {
                     dat.View.CharacterAvatar.Skeleton = doll.RacePreset.FemaleSkeleton;
                     dat.View.CharacterAvatar.m_Skeleton = doll.RacePreset.FemaleSkeleton;
                     dat.View.CharacterAvatar.m_SkeletonChanged = true;
                 }
                 dat.View.CharacterAvatar.UpdateSkeleton();
                 // dat.View.CharacterAvatar.m_Skeleton = (doll.Gender != Gender.Male) ? doll.RacePreset.FemaleSkeleton : doll.RacePreset.MaleSkeleton;

                 CustomizationOptions customizationOptions = gender != Gender.Male ? race.FemaleOptions : race.MaleOptions;
                 Settings.Face = Main.EELIndex(Settings.Face, customizationOptions.Heads.Length);
                 doll.SetHead(customizationOptions.Heads[Settings.Face]);
                 if (customizationOptions.Hair.Length > 0)
                     Settings.Hair = Main.EELIndex(Settings.Hair, customizationOptions.Hair.Length);

                 if (customizationOptions.Hair.Length > 0)
                     doll.SetHair(customizationOptions.Hair[Settings.Hair]);

                 if (customizationOptions.Beards.Length > 0)
                     Settings.Beards = Main.EELIndex(Settings.Beards, customizationOptions.Beards.Length);

                 if (customizationOptions.Beards.Length > 0)
                     doll.SetBeard(customizationOptions.Beards[Settings.Beards]);

                 if (customizationOptions.Horns.Length > 0)
                     Settings.Horns = Main.EELIndex(Settings.Horns, customizationOptions.Horns.Length);

                 if (customizationOptions.Horns.Length > 0)
                     doll.SetHorn(customizationOptions.Horns[Settings.Horns]);

                 if (customizationOptions.Hair.Length > 0)
                     Settings.HairColor = Main.EELIndex(Settings.HairColor, doll.Hair.m_Entity.PrimaryColorsProfile.Ramps.Count);

                 if (customizationOptions.Hair.Length > 0)
                     doll.SetHairColor(Settings.HairColor);

                 if (customizationOptions.Horns.Length > 0)
                     Settings.HornsColor = Main.EELIndex(Settings.HornsColor, doll.GetHornsRamps().Count);

                 if (customizationOptions.Horns.Length > 0)
                     doll.SetHornsColor(Settings.HornsColor);

                 if (doll.Scars.Count > 0) Settings.Scar = Main.EELIndex(Settings.Scar, doll.Scars.Count);
                 if (doll.Scars.Count > 0) doll.SetScar(doll.Scars[Settings.Scar]);
                 if (doll.Warpaints.Count > 0) Settings.Warpaint = Main.EELIndex(Settings.Warpaint, doll.Warpaints.Count);
                 if (doll.Warpaints.Count > 0) Settings.WarpaintCol = Main.EELIndex(Settings.Warpaint, doll.Warpaints.Count);
                 if (doll.Warpaints.Count > 0)
                 {
                     if (doll.Warpaints.Count > 0) doll.SetWarpaint(doll.Warpaints[Settings.Warpaint]);
                     if (doll.Warpaints.Count > 0) doll.SetWarpaintColor(Settings.WarpaintCol);
                 }

                 Settings.SkinColor = Main.EELIndex(Settings.SkinColor, doll.Head.m_Entity.PrimaryColorsProfile.Ramps.Count);

                 doll.SetSkinColor(Settings.SkinColor);

                 Settings.PrimaryColor = EELIndex(Settings.PrimaryColor, doll.GetOutfitRampsPrimary().Count);

                 Settings.SecondaryColor = EELIndex(Settings.SecondaryColor, doll.GetOutfitRampsSecondary().Count);

                 //doll.SetHair(customizationOptions.Hair[Settings.Hair]);

                 doll.SetEquipColors(Settings.PrimaryColor, Settings.SecondaryColor);
                 if (Settings.classOutfit.Name != "Default") {
                     doll.SetPrimaryEquipColor(Settings.PrimaryColor);
                     doll.SetSecondaryEquipColor(Settings.SecondaryColor);
                 }
                 dat.Parts.Get<UnitPartDollData>().Default = doll.CreateData();
                 ///  CharacterManager.UpdateModel(dat.View);
                 if (shouldRebuild) {
                     CharacterManager.RebuildCharacter(dat);
                 }
             }
             catch (Exception e) { Main.logger.Log(e.ToString()); }
         }*/

        private static void ChooseDoll(UnitEntityData unitEntityData)
        {
            try
            {
                if (!unitEntityData.IsMainCharacter && !unitEntityData.IsCustomCompanion() && GUILayout.Button("Destroy Doll", GUILayout.Width(DefaultLabelWidth)))
                {
                    unitEntityData.Parts.Get<UnitPartDollData>().ViewWillDetach();
                    unitEntityData.Parts.Get<UnitPartDollData>().OnViewWillDetach();
                    unitEntityData.Parts.Get<UnitPartDollData>().RemoveSelf();
                    unitEntityData.Parts.Remove<UnitPartDollData>();
                    unitEntityData.PostLoad();
                    unitEntityData.Parts.PostLoad();
                    unitEntityData.Descriptor.Doll = null;
                    unitEntityData.Descriptor.m_LoadedDollData = null;
                    unitEntityData.Descriptor.ForcceUseClassEquipment = false;
                    ///   unitEntityData.View.CharacterAvatar.LoadBakedCharacter();
                    ///   unitEntityData.DetachView();
                    /// unitEntityData.View.Destroy();
                    /// unitEntityData.OnViewWillDetach();
                    ///unitEntityData.AttachView(unitEntityData.CreateViewForData());
                    /// unitEntityData.View.CharacterAvatarUpdated();
                    CharacterManager.RebuildCharacter(unitEntityData);
                }
                var Settings = settings.GetCharacterSettings(unitEntityData);
                var races = BlueprintRoot.Instance.Progression.CharacterRaces.ToArray<BlueprintRace>();
                var gender = unitEntityData.Gender;

                /* if (Settings.RaceIndex == -1)
                 {
                     if (!unitEntityData.Descriptor.Progression.Race.NameForAcronym.Contains("Mongrel"))
                     {
                         race = unitEntityData.Progression.Race;
                         if (Settings.RaceIndex == -1) {
                             Settings.RaceIndex = races.IndexOf(race);
                         }
                     }
                     else {
                         race = (Utilities.GetBlueprint<BlueprintRace>("0a5d473ead98b0646b94495af250fdc4"));
                         if (Settings.RaceIndex == -1) {
                             Settings.RaceIndex = races.IndexOf(race);
                         }
                     }
                 }
                 else {
                     race = races[Settings.RaceIndex];
                 }*/
                //var race = unitEntityData.Progression.Race;
                // CustomizationOptions customizationOptions = gender != Gender.Male ? race.FemaleOptions : race.MaleOptions;
                /* if (Settings.Beards == -1 || Settings.Face == -1 || Settings.Hair == -1 || Settings.HairColor == -1 || Settings.Horns == -1 || Settings.HornsColor == -1 || Settings.PrimaryColor == -1 || Settings.SecondaryColor == -1 || Settings.SkinColor == -1)
                 {
                     GetIndices(unitEntityData,Settings, DollResourcesManager.GetDoll(unitEntityData), customizationOptions);
                 }*/
                /*if (!unitEntityData.Descriptor.Progression.Race.NameForAcronym.Contains("Mongrel"))
                {
                    race = races.ElementAt(Settings.RaceIndex);
                }*/
                //customizationOptions = gender != Gender.Male ? race.FemaleOptions : race.MaleOptions;

                //var doll2 = DollResourcesManager.GetDoll(unitEntityData);
                /// var race = doll.Race;
                void onEquipment()
                {
                    CharacterManager.UpdateModel(unitEntityData.View);
                    CharacterManager.RebuildCharacter(unitEntityData);
                }
                var doll = DollResourcesManager.GetDoll(unitEntityData);
                var race = doll.Race;
                CustomizationOptions customizationOptions = gender != Gender.Male ? race.FemaleOptions : race.MaleOptions;
                ReferenceArrayProxy<BlueprintRaceVisualPreset, BlueprintRaceVisualPresetReference> presets = doll.Race.Presets;
                BlueprintRaceVisualPreset racePreset = doll.RacePreset;
                UI.Div(154, 30, 1200);
                using (UI.HorizontalScope())
                {
                    using (UI.VerticalScope())
                    {
                        UI.HStack("Doll", 1,
                            () =>
                            {
                                ChooseRace(unitEntityData, doll);
                            },
                            () => ChooseVisualPreset(unitEntityData, doll, "Body Type", doll.Race.m_Presets.Select(a => (BlueprintRaceVisualPreset)a.GetBlueprint()).ToArray(), doll.RacePreset),
                            () => ChooseEEL(unitEntityData, doll, "Face", customizationOptions.Heads, doll.Head.m_Link, (EquipmentEntityLink ee) => doll.SetHead(ee)),
                            () =>
                            {
                                if (doll.Scars.Count > 0)
                                    ChooseEEL(unitEntityData, doll, "Scar", doll.Scars.ToArray(), doll.Scar.m_Link, (EquipmentEntityLink ee) => doll.SetScar(ee));
                            },
                          //  () => ChooseEEL(unitEntityData, doll, "Warpaint", DollState.GetWarpaintsList(doll.Race.RaceId).ToArray(), doll.Warpaint.m_Link, (EquipmentEntityLink ee) => doll.SetW(ee)),
                            () =>
                            {
                                if (customizationOptions.Hair.Any())
                                    ChooseEEL(unitEntityData, doll, "Hair", customizationOptions.Hair, doll.Hair.m_Link, (EquipmentEntityLink ee) => doll.SetHair(ee));
                            },
                            () =>
                            {
                                if (customizationOptions.Beards.Any())
                                    ChooseEEL(unitEntityData, doll, "Beards", customizationOptions.Beards, doll.Beard.m_Link, (EquipmentEntityLink ee) => doll.SetBeard(ee));
                            },
                            () =>
                            {
                                if (customizationOptions.Horns.Any())
                                    ChooseEEL(unitEntityData, doll, "Horns", customizationOptions.Horns, doll.Horn.m_Link, (EquipmentEntityLink ee) => doll.SetHorn(ee));
                            },
                            () =>
                            {
                                if (doll.Warprints.Count > 0)
                                    ChooseRamp(unitEntityData, doll, "Warpaint Color", doll.GetWarpaintRamps(), doll.Warprints[0].PaintRampIndex, (int index) => doll.SetWarpaintColor(index,doll.Warprints[0].PaintRampIndex));
                            },
                            () => ChooseRamp(unitEntityData, doll, "Hair Color", doll.GetHairRamps(), doll.HairRampIndex, (int index) =>
                            {
                                doll.SetHairColor(index);
                            }),
                            () => ChooseRamp(unitEntityData, doll, "Skin Color", doll.GetSkinRamps(), doll.SkinRampIndex, (int index) => doll.SetSkinColor(index)),
                            () => ChooseRamp(unitEntityData, doll, "Horn Color", doll.GetHornsRamps(), doll.HornsRampIndex, (int index) => doll.SetHornsColor(index)),
                            () => ChooseRamp(unitEntityData, doll, "Primary Outfit Color", doll.GetOutfitRampsPrimary(), doll.EquipmentRampIndex, (int index) => doll.SetEquipColors(index, doll.EquipmentRampIndexSecondary)),
                            () => ChooseRamp(unitEntityData, doll, "Secondary Outfit Color", doll.GetOutfitRampsSecondary(), doll.EquipmentRampIndexSecondary, (int index) => doll.SetEquipColors(doll.EquipmentRampIndex, index)),

                        /*  if(Settings.PrimaryColor != doll.EquipmentRampIndex || Settings.SecondaryColor != doll.EquipmentRampIndexSecondary)
                          {
                              doll.SetEquipColors(Settings.PrimaryColor,Settings.SecondaryColor);
                          }*/
                        //  ReferenceArrayProxy<BlueprintRaceVisualPreset, BlueprintRaceVisualPresetReference> presets = doll.Race.Presets;
                        // BlueprintRaceVisualPreset racePreset = doll.RacePreset;
                        /*if (unitEntityData.Descriptor.LeftHandedOverride == true && GUILayout.Button("Set Right Handed", GUILayout.Width(DefaultLabelWidth)))
                          {
                              unitEntityData.Descriptor.LeftHandedOverride = false;
                              unitEntityData.Parts.Get<UnitPartDollData>().Default = doll.CreateData();
                              unitEntityData.Descriptor.m_LoadedDollData = doll.CreateData();
                              ViewManager.ReplaceView(unitEntityData, null);
                              unitEntityData.View.HandsEquipment.HandleEquipmentSetChanged();
                          }
                          else if (unitEntityData.Descriptor.LeftHandedOverride == false && GUILayout.Button("Set Left Handed", GUILayout.Width(DefaultLabelWidth)))
                          {
                              unitEntityData.Descriptor.LeftHandedOverride = true;
                              unitEntityData.Parts.Get<UnitPartDollData>().Default = doll.CreateData();
                              unitEntityData.Descriptor.m_LoadedDollData = doll.CreateData();
                              ViewManager.ReplaceView(unitEntityData, null);
                              unitEntityData.View.HandsEquipment.HandleEquipmentSetChanged();
                          }*/
                        () => ChoosePortrait(unitEntityData),
                        () => ChooseAsks(unitEntityData),
                        () => UI.Space(),
                        () => UI.Div(0, 30, 1200),
                        () =>
                        {
                            using (UI.HorizontalScope())
                            {
                                UI.Label("Custom", UI.AutoWidth());
                                using (UI.VerticalScope())
                                {
                                    using (UI.HorizontalScope(UI.Width(800)))
                                    {
                                        using (UI.VerticalScope())
                                        {
                                            UI.Toggle("Hair Color", ref Settings.customHairColor);
                                            if (Settings.customHairColor)
                                            {
                                                UI.Toggle("Show Hair Color Picker", ref Settings.showHair);
                                                if (Settings.showHair)
                                                {
                                                    HairColorPicker.OnGUI(
                                                        Settings,
                                                        unitEntityData,
                                                        new Color(Settings.hairColor[0], Settings.hairColor[1],
                                                            Settings.hairColor[2]),
                                                        ref Settings.hairColor,
                                                        Main.GenerateHairColor
                                                    );
                                                }
                                            }
                                        }
                                        using (UI.VerticalScope())
                                        {
                                            UI.Toggle("Skin Color", ref Settings.customSkinColor);
                                            if (Settings.customSkinColor)
                                            {
                                                UI.Toggle("Show Skin Color Picker", ref Settings.showSkin);
                                                if (Settings.showSkin)
                                                {
                                                    SkinColorPicker.OnGUI(
                                                        Settings,
                                                        unitEntityData,
                                                        new Color(Settings.skinColor[0], Settings.skinColor[1],
                                                            Settings.skinColor[2]),
                                                        ref Settings.skinColor,
                                                        Main.GenerateSkinColor
                                                    );
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        () => UI.Div(0, 30, 1200),
                        () =>
                        {
                            UI.Label("Custom", UI.AutoWidth());
                            using (UI.VerticalScope())
                            {
                                UI.Toggle("Outfit Colors", ref Settings.customOutfitColors);
                                using (UI.HorizontalScope(UI.Width(800)))
                                {
                                    using (UI.VerticalScope())
                                    {
                                        if (Settings.customOutfitColors)
                                        {
                                            UI.Toggle("Show Primary Outfit Color Picker", ref Settings.showPrimColor);
                                            if (Settings.showPrimColor)
                                            {
                                                PrimaryColorPicker.OnGUI(
                                                Settings,
                                                unitEntityData,
                                                new Color(Settings.primColor[0], Settings.primColor[1], Settings.primColor[2]),
                                                ref Settings.primColor,
                                                Main.GenerateOutfitcolor
                                            );
                                            }
                                        }
                                    }
                                    using (UI.VerticalScope())
                                    {
                                        if (Settings.customOutfitColors)
                                        {
                                            UI.Toggle("Show Secondary Outfit Color Picker", ref Settings.showSecondColor);
                                            if (Settings.showSecondColor)
                                            {
                                                SecondaryColorPicker.OnGUI(
                                                    Settings,
                                                    unitEntityData,
                                                    new Color(Settings.secondColor[0], Settings.secondColor[1],
                                                        Settings.secondColor[2]),
                                                    ref Settings.secondColor,
                                                    Main.GenerateOutfitcolor
                                                );
                                            }
                                        } // SecondaryColorPicker.OnGUI(Settings, unitEntityData, new Color(Settings.secondColor[0], Settings.secondColor[1], Settings.secondColor[2]), ref Settings.secondColor, Main.GenerateOutfitcolor);
                                    }
                                }
                            }
                        },
                        () => UI.Div(0, 30, 1200),
                        () =>
                        {
                            using (UI.HorizontalScope())
                            {
                                UI.Label("Custom", UI.AutoWidth());
                                using (UI.VerticalScope())
                                {
                                    using (UI.HorizontalScope(UI.Width(800)))
                                    {
                                        using (UI.VerticalScope())
                                        {
                                            UI.Toggle("Horn Color", ref Settings.customHornColor);
                                            if (Settings.customHornColor)
                                            {
                                                UI.Toggle("Show Horn Color Picker", ref Settings.showHornColor);
                                                if (Settings.showHornColor)
                                                {
                                                    HornColorPicker.OnGUI(
                                                        Settings, unitEntityData,
                                                        new Color(Settings.hornColor[0], Settings.hornColor[1],
                                                            Settings.hornColor[2]),
                                                        ref Settings.hornColor,
                                                        Main.GenerateHornColor
                                                    );
                                                }
                                            }
                                        }

                                       /* using (UI.VerticalScope())
                                        {
                                            UI.Toggle("Warpaint Color", ref Settings.customWarpaintColor);
                                            if (Settings.customWarpaintColor)
                                            {
                                                UI.Toggle("Show Warpaint Color Picker", ref Settings.showWarpaintColor);
                                                if (Settings.showWarpaintColor)
                                                {
                                                    WarpaintColorPicker.OnGUI(
                                                    Settings,
                                                    unitEntityData,
                                                    new Color(Settings.warpaintColor[0], Settings.warpaintColor[1],
                                                        Settings.warpaintColor[2]),
                                                    ref Settings.warpaintColor,
                                                    Main.GenerateWarpaintColor
                                                );
                                                }
                                            }
                                        }*/
                                    }
                                }
                            }
                        });
                    }
                    using (UI.VerticalScope())
                    {
                        if (texture == null)
                        {
                            //texture = PreviewSystem.texture;
                            texture = Object.FindObjectOfType<InGamePCView>().m_StaticPartPCView.m_ServiceWindowsPCView.m_InventoryPCView.m_DollView.GetComponentInChildren<RawImage>().texture;
                        }
                        GUILayout.Box(texture);
                        // GUILayout.HorizontalSlider();
                        // GUILayout.HorizontalSlider();
                    }
                }

                // if (unitEntityData.IsMainCharacter || unitEntityData.IsCustomCompanion()) ChooseAsks(unitEntityData);
            }
            catch (Exception e) { Main.logger.Log(e.ToString()); }
        }

        public static Dictionary<int, string> barding = new Dictionary<int, string>()
        {
            //   [0] = "None",
            [1] = "Medium/Light Barding",
            [2] = "Heavy Barding",
        };

        private static void ChooseCompanionColor(CharacterSettings characterSettings, UnitEntityData unitEntityData)
        {
            try
            {
                if (GUILayout.Button("Create Doll", GUILayout.Width(DefaultLabelWidth)))
                {
                    var race = unitEntityData.Descriptor.Progression.Race;
                    var options = unitEntityData.Descriptor.Gender == Gender.Male ? race.MaleOptions : race.FemaleOptions;
                    var dollState = new DollState();
                    if (!unitEntityData.Descriptor.Progression.Race.name.Contains("Mongrel") && !unitEntityData.Descriptor.Progression.Race.name.Contains("Succubus"))
                    {
                        dollState.SetRace(unitEntityData.Descriptor.Progression.Race); //Race must be set before class
                                                                                       //This is a hack to work around harmony not allowing calls to the unpatched
                    }
                    else
                    {
                        var humanrace = Utilities.GetBlueprint<BlueprintRace>("0a5d473ead98b0646b94495af250fdc4");
                        dollState.SetRace(humanrace);
                        race = humanrace;
                    }
                    CharacterManager.disableEquipmentClassPatch = true;
                    dollState.SetClass(unitEntityData.Descriptor.Progression.GetEquipmentClass());
                    CharacterManager.disableEquipmentClassPatch = false;
                    dollState.SetGender(unitEntityData.Descriptor.Gender);
                    dollState.SetRacePreset(race.Presets[0]);
                    unitEntityData.Descriptor.LeftHandedOverride = false;
                    if (options.Hair.Length > 0)
                        dollState.SetHair(options.Hair[0]);
                    if (options.Heads.Length > 0)
                        dollState.SetHead(options.Hair[0]);
                    if (options.Beards.Length > 0)
                        dollState.SetBeard(options.Hair[0]);
                    //dollState.Validate();
                    //SetEELs(unitEntityData, dollState);
                    // unitEntityData.Descriptor.Doll = dollState.CreateData();
                    unitEntityData.Parts.Add<UnitPartDollData>();
                    unitEntityData.Parts.Get<UnitPartDollData>().Default = ModifiedCreateDollData.CreateDataModified(dollState);
                    unitEntityData.Parts.Get<UnitPartDollData>().OnDidAttachToEntity();
                    unitEntityData.Parts.Get<UnitPartDollData>().OnViewDidAttach();
                    unitEntityData.View.HandsEquipment.UpdateLocatorTrackers();
                    // Traverse.Create(unitEntityData.Parts.Get<UnitPartDollData>()).Field("ActiveDoll").SetValue(dollState.CreateData());
                    //    unitEntityData.Descriptor.ForcceUseClassEquipment = true;
                    CharacterManager.RebuildCharacter(unitEntityData);
                    // SetEELs(unitEntityData,dollState);
                    ///SetEELs(unitEntityData, dollState);
                    //  CharacterManager.RebuildCharacter(unitEntityData);
                    //SetEELs(unitEntityData, dollState);
                    //  CharacterManager.UpdateModel(unitEntityData.View);
                    // SetEELs(unitEntityData, dollState);
                    //CharacterManager.RebuildCharacter(unitEntityData);
                    // SetEELs(unitEntityData, dollState);
                    // SetEELs(unitEntityData, dollState, true);
                }
            }
            catch (Exception e)
            {
                Main.logger.Log(e.ToString());
            }
            ModKit.UI.Label("Note: Colors only applies to non-default outfits, the default companion custom voice is None");
            {
                GUILayout.BeginHorizontal();
                ModKit.UI.Label("Primary Outfit Color ", GUILayout.Width(DefaultLabelWidth));
                var newIndex = (int)Math.Round(GUILayout.HorizontalSlider(characterSettings.companionPrimary, -1, 90, GUILayout.Width(DefaultSliderWidth)), 0);
                ModKit.UI.Label(" " + newIndex, GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();
                if (newIndex != characterSettings.companionPrimary)
                {
                    characterSettings.companionPrimary = newIndex;
                    CharacterManager.UpdateModel(unitEntityData.View);
                    CharacterManager.RebuildCharacter(unitEntityData);
                }
            }
            {
                GUILayout.BeginHorizontal();
                ModKit.UI.Label("Secondary Outfit Color ", GUILayout.Width(DefaultLabelWidth));
                var newIndex = (int)Math.Round(GUILayout.HorizontalSlider(characterSettings.companionSecondary, -1, 90, GUILayout.Width(DefaultSliderWidth)), 0);
                ModKit.UI.Label(" " + newIndex, GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();
                if (newIndex != characterSettings.companionSecondary)
                {
                    characterSettings.companionSecondary = newIndex;
                    if (characterSettings.customOutfitColors)
                    {
                        Main.GenerateOutfitcolor(unitEntityData);
                    }
                    CharacterManager.UpdateModel(unitEntityData.View);
                    CharacterManager.RebuildCharacter(unitEntityData);
                }
            }
            ModKit.UI.Toggle("Custom Outfit Colors", ref characterSettings.customOutfitColors);
            if (characterSettings.customOutfitColors)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(15f);
                ModKit.UI.DisclosureToggle("Show Primary Picker", ref characterSettings.showPrimColor);
                GUILayout.EndHorizontal();
                if (characterSettings.showPrimColor)
                {
                    PrimaryColorPicker.OnGUI(characterSettings, unitEntityData, new Color(characterSettings.primColor[0], characterSettings.primColor[1], characterSettings.primColor[2]), ref characterSettings.primColor, Main.GenerateOutfitcolor);
                }
                GUILayout.Space(5f);
                GUILayout.BeginHorizontal();
                GUILayout.Space(15f);
                ModKit.UI.DisclosureToggle("Show Secondary Picker", ref characterSettings.showSecondColor);
                GUILayout.EndHorizontal();
                if (characterSettings.showSecondColor)
                {
                    SecondaryColorPicker.OnGUI(characterSettings, unitEntityData, new Color(characterSettings.secondColor[0], characterSettings.secondColor[1], characterSettings.secondColor[2]), ref characterSettings.secondColor, Main.GenerateOutfitcolor);
                }
                GUILayout.Space(5f);
            }
            ChoosePortrait(unitEntityData);
            ChooseAsks(unitEntityData);
        }

        private static void ChooseToggle(string label, ref bool currentValue, Action onChoose)
        {
            bool temp = currentValue;
            ModKit.UI.Toggle(label, ref temp);
            if (temp != currentValue)
            {
                currentValue = temp;
                onChoose();
            }
        }

        public static int EELIndex(int setting, int length)
        {
            if (setting >= 0 && setting < length)
                return setting;
            else
                return 0;
        }

        private static void ChooseEquipment(UnitEntityData unitEntityData, CharacterSettings characterSettings)
        {
            void onHideEquipment()
            {
                CharacterManager.UpdateModel(unitEntityData.View);
                CharacterManager.RebuildCharacter(unitEntityData);
                foreach (var buff in unitEntityData.Buffs)
                    buff.ClearParticleEffect();
            }
            void onHideBuff()
            {
                foreach (var buff in unitEntityData.Buffs)
                    buff.ClearParticleEffect();
                /*beta3*/
                //unitEntityData.SpawnBuffsFxs();
            }
            void onWeaponChanged()
            {
                ///if(unitEntityData.View.HandsEquipment.GetWeaponModel(false) != characterSettings.overrideWeapons)
                unitEntityData.View.HandsEquipment.UpdateAll();
            }
            // characterSettings.hideAll = unitEntityData.UISettings.ShowClassEquipment;
            /* ChooseToggle("Hide All Equipment", ref characterSettings.hideAll, onHideEquipment);
             unitEntityData.UISettings.ShowClassEquipment = !characterSettings.hideAll;
             if(characterSettings.hideAll)
             {
                 if (!characterSettings.hideArmor) characterSettings.hideArmor = true;
                 if (!characterSettings.hideHelmet) characterSettings.hideHelmet = true;
                 if (!characterSettings.hideGlasses) characterSettings.hideGlasses = true;
                 if (!characterSettings.hideShirt) characterSettings.hideShirt = true;
                 if (!characterSettings.hideItemCloak) characterSettings.hideItemCloak = true;
                 if (!characterSettings.hideBracers) characterSettings.hideBracers = true;
                 if (!characterSettings.hideGloves) characterSettings.hideGloves = true;
                 if (!characterSettings.hideBoots) characterSettings.hideBoots = true;
                 if (!characterSettings.hideCap) characterSettings.hideCap = true;
             }*/
            /*else
            {
                if (characterSettings.hideArmor) characterSettings.hideArmor = false;
                if (characterSettings.hideHelmet) characterSettings.hideHelmet = false;
                if (characterSettings.hideGlasses) characterSettings.hideGlasses = false;
                if (characterSettings.hideShirt) characterSettings.hideShirt = false;
                if (characterSettings.hideItemCloak) characterSettings.hideItemCloak = false;
                if (characterSettings.hideBracers) characterSettings.hideBracers = false;
                if (characterSettings.hideGloves) characterSettings.hideGloves = false;
                if (characterSettings.hideBoots) characterSettings.hideBoots = false;
                if (characterSettings.hideCap) characterSettings.hideCap = false;
            }*/
            ChooseToggle("Hide Cap", ref characterSettings.hideCap, onHideEquipment);
            ChooseToggle("Hide Helmet", ref characterSettings.hideHelmet, onHideEquipment);
            ChooseToggle("Hide Glasses", ref characterSettings.hideGlasses, onHideEquipment);
            ChooseToggle("Hide Shirt", ref characterSettings.hideShirt, onHideEquipment);
            ChooseToggle("Hide Class Equipment", ref characterSettings.hideClassCloak, onHideEquipment);
            ChooseToggle("Hide Cloak", ref characterSettings.hideItemCloak, onHideEquipment);
            ChooseToggle("Hide Armor", ref characterSettings.hideArmor, onHideEquipment);
            ChooseToggle("Hide Bracers", ref characterSettings.hideBracers, onHideEquipment);
            ChooseToggle("Hide Gloves", ref characterSettings.hideGloves, onHideEquipment);
            ChooseToggle("Hide Boots", ref characterSettings.hideBoots, onHideEquipment);
            ChooseToggle("Hide Inactive Weapons", ref characterSettings.hideWeapons, onWeaponChanged);
            ChooseToggle("Hide Sheaths/Scabbards", ref characterSettings.hideSheaths, onWeaponChanged);
            ChooseToggle("Hide Belt Slots", ref characterSettings.hideBeltSlots, onWeaponChanged);
            ChooseToggle("Hide Quiver", ref characterSettings.hidequiver, onWeaponChanged);
            ChooseToggle("Hide Weapon Enchantments", ref characterSettings.hideWeaponEnchantments, onWeaponChanged);
            ChooseToggle("Hide Wings", ref characterSettings.hideWings, onHideBuff);
            ChooseToggle("Hide Horns", ref characterSettings.hideHorns, onHideEquipment);
            ChooseToggle("Hide Tail", ref characterSettings.hideTail, onHideEquipment);
            ChooseToggle("Hide Mythic", ref characterSettings.hideMythic, onHideEquipment);
        }

        private static void ChooseEquipmentPet(UnitEntityData unitEntityData, CharacterSettings characterSettings)
        {
            void onHideEquipment()
            {
                CharacterManager.UpdateModel(unitEntityData.View);
                CharacterManager.RebuildCharacter(unitEntityData);
                foreach (var buff in unitEntityData.Buffs)
                    buff.ClearParticleEffect();
            }
            void onHideBuff()
            {
                foreach (var buff in unitEntityData.Buffs)
                    buff.ClearParticleEffect();
                /*beta3*/
                //unitEntityData.SpawnBuffsFxs();
            }
            void onWeaponChanged()
            {
                ///if(unitEntityData.View.HandsEquipment.GetWeaponModel(false) != characterSettings.overrideWeapons)
                unitEntityData.View.HandsEquipment.UpdateAll();
            }

            // characterSettings.hideAll = unitEntityData.UISettings.ShowClassEquipment;
            /* ChooseToggle("Hide All Equipment", ref characterSettings.hideAll, onHideEquipment);
             unitEntityData.UISettings.ShowClassEquipment = !characterSettings.hideAll;
             if(characterSettings.hideAll)
             {
                 if (!characterSettings.hideArmor) characterSettings.hideArmor = true;
                 if (!characterSettings.hideHelmet) characterSettings.hideHelmet = true;
                 if (!characterSettings.hideGlasses) characterSettings.hideGlasses = true;
                 if (!characterSettings.hideShirt) characterSettings.hideShirt = true;
                 if (!characterSettings.hideItemCloak) characterSettings.hideItemCloak = true;
                 if (!characterSettings.hideBracers) characterSettings.hideBracers = true;
                 if (!characterSettings.hideGloves) characterSettings.hideGloves = true;
                 if (!characterSettings.hideBoots) characterSettings.hideBoots = true;
                 if (!characterSettings.hideCap) characterSettings.hideCap = true;
             }*/
            /*else
            {
                if (characterSettings.hideArmor) characterSettings.hideArmor = false;
                if (characterSettings.hideHelmet) characterSettings.hideHelmet = false;
                if (characterSettings.hideGlasses) characterSettings.hideGlasses = false;
                if (characterSettings.hideShirt) characterSettings.hideShirt = false;
                if (characterSettings.hideItemCloak) characterSettings.hideItemCloak = false;
                if (characterSettings.hideBracers) characterSettings.hideBracers = false;
                if (characterSettings.hideGloves) characterSettings.hideGloves = false;
                if (characterSettings.hideBoots) characterSettings.hideBoots = false;
                if (characterSettings.hideCap) characterSettings.hideCap = false;
            }*/
            var settings = GlobalVisualInfo.Instance.ForCharacter(unitEntityData).settings;
            ChooseToggle("Hide Barding", ref settings.hidebarding, onHideEquipment);
        }

        /*
         * m_Size is updated from GetSizeScale (EntityData.Descriptor.State.Size) and
         * is with m_OriginalScale to adjust the transform.localScale
         * Adjusting GetSizeScale will effect character corpulence and cause gameplay sideeffects
         * Changing m_OriginalScale will effect ParticlesSnapMap.AdditionalScaleGUID
         */

        private static void ChooseSizeAdditive(UnitEntityData unitEntityData, CharacterSettings characterSettings)
        {
            GUILayout.BeginHorizontal();
            ModKit.UI.Label("Additive Scale Factor", GUILayout.Width(300));
            var sizeModifier = GUILayout.HorizontalSlider(characterSettings.additiveScaleFactor, -4, 4, GUILayout.Width(DefaultSliderWidth));
            if (!characterSettings.overrideScaleFloatMode)
                sizeModifier = (int)sizeModifier;
            characterSettings.additiveScaleFactor = sizeModifier;
            var sign = sizeModifier >= 0 ? "+" : "";
            ModKit.UI.Label($" {sign}{sizeModifier:0.##}", GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();
        }

        private static void ChooseSizeOverride(UnitEntityData unitEntityData, CharacterSettings characterSettings)
        {
            GUILayout.BeginHorizontal();
            ModKit.UI.Label("Override Scale Factor", GUILayout.Width(300));
            var sizeModifier = GUILayout.HorizontalSlider(characterSettings.overrideScaleFactor, 0, 8, GUILayout.Width(DefaultSliderWidth));
            if (!characterSettings.overrideScaleFloatMode)
                sizeModifier = (int)sizeModifier;
            characterSettings.overrideScaleFactor = sizeModifier;
            ModKit.UI.Label($" {(Size)(sizeModifier)}", GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();
        }

        private static void ChooseEquipmentOverridePet(UnitEntityData unitEntityData, CharacterSettings characterSettings)
        {
            Util.ChooseSlider("Override Barding", barding, ref GlobalVisualInfo.Instance.ForCharacter(unitEntityData).settings.overridebarding, () => { });
            ModKit.UI.Label("View", GUILayout.Width(DefaultLabelWidth));
            void onView() => ViewManager.ReplaceView(unitEntityData, characterSettings.overrideView);
            Util.ChooseSlider("Override View", EquipmentResourcesManager.Units, ref characterSettings.overrideView, onView);
            void onChooseScale()
            {
                Traverse.Create(unitEntityData.View).Field("m_Scale").SetValue(unitEntityData.View.GetSizeScale() + 0.01f);
            }
            ModKit.UI.Label("Scale", GUILayout.Width(DefaultLabelWidth));
            GUILayout.BeginHorizontal();
            ChooseToggle("Enable Override Scale", ref characterSettings.overrideScale, onChooseScale);
            ChooseToggle("Restrict to polymorph", ref characterSettings.overrideScaleShapeshiftOnly, onChooseScale);
            ChooseToggle("Use Additive Factor", ref characterSettings.overrideScaleAdditive, onChooseScale);
            ChooseToggle("Use Cheat Mode", ref characterSettings.overrideScaleCheatMode, onChooseScale);
            ChooseToggle("Use Continuous Factor", ref characterSettings.overrideScaleFloatMode, onChooseScale);
            GUILayout.Space(10f);
            GUILayout.EndHorizontal();
            if (characterSettings.overrideScale && characterSettings.overrideScaleAdditive)
                ChooseSizeAdditive(unitEntityData, characterSettings);
            if (characterSettings.overrideScale && !characterSettings.overrideScaleAdditive)
                ChooseSizeOverride(unitEntityData, characterSettings);
            ModKit.UI.Label("Portrait/Voice", GUILayout.Width(DefaultLabelWidth));
            ChoosePortrait(unitEntityData);
            ChooseAsks(unitEntityData);
        }

        private static void ChooseEquipmentOverride(UnitEntityData unitEntityData, CharacterSettings characterSettings)
        {
            void onEquipment()
            {
                CharacterManager.UpdateModel(unitEntityData.View);
                CharacterManager.RebuildCharacter(unitEntityData);
            }
            ModKit.UI.Label("Equipment", GUILayout.Width(DefaultLabelWidth));
            void onView() => ViewManager.ReplaceView(unitEntityData, characterSettings.overrideView);
            Util.ChooseSlider("Override Helm", EquipmentResourcesManager.Helm, ref characterSettings.overrideHelm, onEquipment);
            Util.ChooseSlider("Override Cloak", EquipmentResourcesManager.Cloak, ref characterSettings.overrideCloak, onEquipment);
            Util.ChooseSlider("Override Shirt", EquipmentResourcesManager.Shirt, ref characterSettings.overrideShirt, onEquipment);
            Util.ChooseSlider("Override Glasses/Mask", EquipmentResourcesManager.Glasses, ref characterSettings.overrideGlasses, onEquipment);
            Util.ChooseSlider("Override Armor", EquipmentResourcesManager.Armor, ref characterSettings.overrideArmor, onEquipment);
            Util.ChooseSlider("Override Bracers", EquipmentResourcesManager.Bracers, ref characterSettings.overrideBracers, onEquipment);
            Util.ChooseSlider("Override Gloves", EquipmentResourcesManager.Gloves, ref characterSettings.overrideGloves, onEquipment);
            Util.ChooseSlider("Override Boots", EquipmentResourcesManager.Boots, ref characterSettings.overrideBoots, onEquipment);
            Util.ChooseSlider("Override Tattoos", EquipmentResourcesManager.Tattoos, ref characterSettings.overrideTattoo, onEquipment);
            Util.ChooseSlider("Override WingsFX", EquipmentResourcesManager.WingsFX, ref characterSettings.overrideWingsFX, onEquipment);
            Util.ChooseSliderInvert<string>("Override WingsEE", EquipmentResourcesManager.WingsEE, ref characterSettings.overrideWingsEE, onEquipment);
            Util.ChooseSlider("Override Horns", EquipmentResourcesManager.HornsEE, ref characterSettings.overrideHorns, onEquipment);
            Util.ChooseSlider("Override Tail", EquipmentResourcesManager.TailsEE, ref characterSettings.overrideTail, onEquipment);
            Util.ChooseSliderM("Override Mythic", EquipmentResourcesManager.MythicOptions, ref characterSettings.overrideMythic, onEquipment);
            // Util.ChooseSlider("Override Misc", EquipmentResourcesManager.Tattoos, ref characterSettings.overrideOther, onEquipment);

            ModKit.UI.Label("Weapons", GUILayout.Width(DefaultLabelWidth));
            foreach (var kv in EquipmentResourcesManager.Weapons)
            {
                var animationStyle = kv.Key;
                var weaponLookup = kv.Value;
                characterSettings.overrideWeapons.TryGetValue(animationStyle, out BlueprintRef currentValue);
                void onWeapon()
                {
                    characterSettings.overrideWeapons[animationStyle] = currentValue;
                    unitEntityData.View.HandsEquipment.UpdateAll();
                }
                Util.ChooseSlider($"Override {animationStyle} ", weaponLookup, ref currentValue, onWeapon);
            }
            GUILayout.BeginHorizontal();
            ModKit.UI.Label("Main Weapon Enchantments", GUILayout.Width(DefaultLabelWidth));
            if (GUILayout.Button("Add Enchantment", GUILayout.ExpandWidth(false)))
            {
                characterSettings.overrideMainWeaponEnchantments.Add(null);
            }
            GUILayout.EndHorizontal();
            void onWeaponEnchantment()
            {
                unitEntityData.View.HandsEquipment.UpdateAll();
            }
            for (int i = 0; i < characterSettings.overrideMainWeaponEnchantments.Count; i++)
            {
                Util.ChooseSliderList($"Override Main Hand", EquipmentResourcesManager.WeaponEnchantments,
                    characterSettings.overrideMainWeaponEnchantments, i, onWeaponEnchantment);
            }
            GUILayout.BeginHorizontal();
            ModKit.UI.Label("Offhand Weapon Enchantments", GUILayout.Width(DefaultLabelWidth));
            if (GUILayout.Button("Add Enchantment", GUILayout.ExpandWidth(false)))
            {
                characterSettings.overrideOffhandWeaponEnchantments.Add("");
            }
            GUILayout.EndHorizontal();
            for (int i = 0; i < characterSettings.overrideOffhandWeaponEnchantments.Count; i++)
            {
                Util.ChooseSliderList($"Override Off Hand", EquipmentResourcesManager.WeaponEnchantments,
                    characterSettings.overrideOffhandWeaponEnchantments, i, onWeaponEnchantment);
            }
            ModKit.UI.Label("View", GUILayout.Width(DefaultLabelWidth));
            Util.ChooseSlider("Override View", EquipmentResourcesManager.Units, ref characterSettings.overrideView, onView);
            void onChooseScale()
            {
                Traverse.Create(unitEntityData.View).Field("m_Scale").SetValue(unitEntityData.View.GetSizeScale() + 0.01f);
            }
            ModKit.UI.Label("Scale", GUILayout.Width(DefaultLabelWidth));
            GUILayout.BeginHorizontal();
            ChooseToggle("Enable Override Scale", ref characterSettings.overrideScale, onChooseScale);
            ChooseToggle("Restrict to polymorph", ref characterSettings.overrideScaleShapeshiftOnly, onChooseScale);
            ChooseToggle("Use Additive Factor", ref characterSettings.overrideScaleAdditive, onChooseScale);
            ChooseToggle("Use Cheat Mode", ref characterSettings.overrideScaleCheatMode, onChooseScale);
            ChooseToggle("Use Continuous Factor", ref characterSettings.overrideScaleFloatMode, onChooseScale);
            GUILayout.Space(10f);
            GUILayout.EndHorizontal();
            if (characterSettings.overrideScale && characterSettings.overrideScaleAdditive)
                ChooseSizeAdditive(unitEntityData, characterSettings);
            if (characterSettings.overrideScale && !characterSettings.overrideScaleAdditive)
                ChooseSizeOverride(unitEntityData, characterSettings);
        }
    }

    [HarmonyPatch(typeof(BlueprintsCache), "Init")]
    internal static class ResourcesLibrary_InitializeLibrary_Patch
    {
        private static void Postfix()
        {
            /*var bps = Util.GetBlueprints();
            var portraits = bps.OfType<BlueprintPortrait>();
            var classes = bps.OfType<BlueprintCharacterClass>();
            Main.blueprints = portraits.Concat<SimpleBlueprint>(classes).ToArray();*/
            Main.blueprints = Utilities.GetAllBlueprints();
        }
    }
}