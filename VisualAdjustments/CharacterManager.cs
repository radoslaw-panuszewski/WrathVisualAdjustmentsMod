using HarmonyLib;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Blueprints.Root;
using Kingmaker.Cheats;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items.Slots;
using Kingmaker.PubSubSystem;
using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.View;
using Kingmaker.Visual.CharacterSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Items;
using Kingmaker.UI.MVVM._VM.Common;
using UnityEngine;
using static VisualAdjustments.Settings;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.Utility;
using Kingmaker.Visual;
using Kingmaker.Visual.Particles;

namespace VisualAdjustments
{
    // [HarmonyPatch(typeof(Character), "RestoreSavedEquipment")]
    /*static class bro_patch
    {
        static bool Prefix(Character __instance)
        {
            try
            {
                Main.logger.Log("RestoreSaved");
                __instance.AddEquipmentEntities(from eel in __instance.m_SavedEquipmentEntities
                    select eel, true);
                foreach (Character.SavedSelectedRampIndices savedSelectedRampIndices in __instance.m_SavedRampIndices)
                {
                  //  EquipmentEntity ee = savedSelectedRampIndices.EquipmentEntityLink.Load(false);
                //    __instance.SetRampIndices(ee, savedSelectedRampIndices.PrimaryIndex, savedSelectedRampIndices.SecondaryIndex,
                  //      1, 1);
                }

                return false;
            }
            catch (Exception e)
            {
               Main.logger.Log(e.ToString());
                throw;
            }
        }
    }*/

    public class CharacterManager
    {
        private static Dictionary<UnitEntityView, GameObject> wingsFxVisibilityManagers = new Dictionary<UnitEntityView, GameObject>();
        public static bool disableEquipmentClassPatch;
        /*
         * Based on DollData.CreateUnitView, DollRoom.CreateAvatar and 
         * UnitEntityData.CreateView
         */
        public static void RebuildCharacter(UnitEntityData unitEntityData)
        {
            try
            {
                if (unitEntityData == null) return;
                if (unitEntityData.IsPet) return;
                //Main.SetEELs(unitEntityData, DollResourcesManager.GetDoll(unitEntityData), false);
                var Settings = Main.settings.GetCharacterSettings(unitEntityData);
                DollData doll = null;
                if (DollResourcesManager.GetDoll(unitEntityData) != null) doll = ModifiedCreateDollData.CreateDataModified(DollResourcesManager.GetDoll(unitEntityData));
                if (doll == null && Settings.classOutfit.Name == "Default")
                {
                    unitEntityData.Descriptor.ForcceUseClassEquipment = false;
                    Traverse.Create(unitEntityData.Descriptor).Field("UseClassEquipment").SetValue(false);
                }
                var character = unitEntityData.View.CharacterAvatar;
                //Traverse.Create(unitEntityData.Descriptor).Field("UseClassEquipment").SetValue(true);
                if (character == null) return; // Happens when overriding view
                if (doll != null)
                {
                  ///  Main.logger.Log("dollnotnull " + unitEntityData.CharacterName);
                    unitEntityData.Descriptor.ForcceUseClassEquipment = true;
                    Traverse.Create(unitEntityData.Descriptor).Field("UseClassEquipment").SetValue(true);
                    var savedEquipment = false;
                    character.RemoveAllEquipmentEntities(savedEquipment);
                    if (doll.RacePreset != null)
                    {
                        /*  if()
                          {
                          }
                          else
                          {
                          }*/
                        character.Skeleton = (doll.Gender != Gender.Male) ? doll.RacePreset.FemaleSkeleton : doll.RacePreset.MaleSkeleton;
                        character.AddEquipmentEntities(doll.RacePreset.Skin.Load(doll.Gender, doll.RacePreset.RaceId), true);
                        /// Main.logger.Log(character.name);
                        ///Main.logger.Log(unitEntityData.CharacterName);
                    }
                    ///character.m_Mirror = doll.LeftHanded;
                    foreach (string assetID in doll.EquipmentEntityIds)
                    {
                        EquipmentEntity ee = ResourcesLibrary.TryGetResource<EquipmentEntity>(assetID);
                        character.AddEquipmentEntity(ee, true);
                    }
                    doll.ApplyRampIndices(character,true); 
                   // Main.SetEELs(unitEntityData, DollResourcesManager.GetDoll(unitEntityData), false);
                    Traverse.Create(unitEntityData.View).Field("m_EquipmentClass").SetValue(null); //UpdateClassEquipment won't update if the class doesn't change
                    unitEntityData.View.HandsEquipment.UpdateAll();                                                                                                   //Adds Armor
                    unitEntityData.View.UpdateBodyEquipmentModel();
                    unitEntityData.View.UpdateClassEquipment();
                    if (Settings.customSkinColor)
                    {
                        Main.GenerateSkinColor(unitEntityData);
                    }
                    if (Settings.customHairColor)
                    {
                        Main.GenerateHairColor(unitEntityData);
                    }
                    if (Settings.customHornColor)
                    {
                        Main.GenerateHornColor(unitEntityData);
                    }
                    if (Settings.customWarpaintColor)
                    {
                        Main.GenerateWarpaintColor(unitEntityData);
                    }
                    if (Settings.customOutfitColors)
                    {
                        Main.GenerateOutfitcolor(unitEntityData);
                    }
                    else
                    {
                       // doll.ApplyRampIndices(character);
                        //doll.SetEquipColors()
                    }
                   /* if (unitEntityData.Parts.Get<UnitPartDollData>())
                    {
                        unitEntityData.Parts.Get<UnitPartDollData>().SetDefault(doll);
                    }
                    else
                    {
                        unitEntityData.Parts.Add<UnitPartDollData>().SetDefault(doll);
                    }*/
                }
                else
                {
                 //   Main.logger.Log("dollnull " + unitEntityData.CharacterName);
                    character.RemoveAllEquipmentEntities(false);
                    character.RestoreSavedEquipment();
                    IEnumerable<EquipmentEntity> bodyEquipment = unitEntityData.Body.AllSlots.SelectMany(
                        new Func<ItemSlot, IEnumerable<EquipmentEntity>>(unitEntityData.View.ExtractEquipmentEntities));
                    character.AddEquipmentEntities(bodyEquipment, true);
                                        unitEntityData.View.HandsEquipment.UpdateAll();                                                                                                   //Adds Armor
                                        unitEntityData.View.UpdateClassEquipment();
                                        if (Settings.customOutfitColors)
                                        {
                                            Main.GenerateOutfitcolor(unitEntityData);
                                        }
                }
                //Main.SetEELs(unitEntityData, DollResourcesManager.GetDoll(unitEntityData), false);
               //  DollResourcesManager.GetDoll(unitEntityData).SetHairColor(Settings.HairColor);
               //Add Kineticist Tattoos
                //Main.SetEELs(unitEntityData, DollResourcesManager.GetDoll(unitEntityData), false);
                //unitEntityData.View.CharacterAvatar.OnRenderObject();
                var component = unitEntityData.Parts.Get<UnitPartVAEELs>();
                if (component != null)
                {
                    foreach (var eetoremove in component.EEToRemove)
                    {
                        var ee = ResourcesLibrary.TryGetResource<EquipmentEntity>(eetoremove);
                        if (unitEntityData.View.CharacterAvatar.EquipmentEntities.Contains(ee))
                        {
                            unitEntityData.View.CharacterAvatar.EquipmentEntities.Remove(ee);
                        }
                    }
                    foreach (var eetoadd in component.EEToAdd)
                    {
                        var ee = ResourcesLibrary.TryGetResource<EquipmentEntity>(eetoadd.AssetID);
                        unitEntityData.View.CharacterAvatar.AddEquipmentEntity(ee,primaryRamp: eetoadd.PrimaryIndex,secondaryRamp:eetoadd.SecondaryIndex);
                       // unitEntityData.View.CharacterAvatar.SetPrimaryRampIndex(ee, eetoadd.PrimaryIndex);
                       // unitEntityData.View.CharacterAvatar.SetSecondaryRampIndex(ee, eetoadd.SecondaryIndex);
                       // unitEntityData.View.CharacterAvatar.SetRampIndices(ee,eetoadd.PrimaryIndex,eetoadd.SecondaryIndex);
                    }

                }
                if (Settings.customOutfitColors)
                {
                    Main.GenerateOutfitcolor(unitEntityData);
                }
                EventBus.RaiseEvent<IUnitViewAttachedHandler>(unitEntityData, delegate (IUnitViewAttachedHandler h)
                {
                    h.HandleUnitViewAttached();
                });
            }
            catch (Exception e)
            {

                Main.logger.Log(e.ToString() + " " + unitEntityData.CharacterName);
            }
        }
       /* public static void RebuildCharacterNew(UnitEntityData unitEntityData)
        {
            try
            {
               /* Main.SetEELs(unitEntityData, DollResourcesManager.GetDoll(unitEntityData), false);
                var Settings = Main.settings.GetCharacterSettings(unitEntityData);
                if (Settings.customHairColor)
                {
                    Main.GenerateHairColor(unitEntityData);
                }
                if (Settings.customSkinColor)
                {
                    Main.GenerateSkinColor(unitEntityData);
                }
                DollData doll = null;
                if (DollResourcesManager.GetDoll(unitEntityData) != null) doll = DollResourcesManager.GetDoll(unitEntityData).CreateData();
                if (doll == null && Settings.classOutfit.Name == "Default")
                {
                    unitEntityData.Descriptor.ForcceUseClassEquipment = false;
                    Traverse.Create(unitEntityData.Descriptor).Field("UseClassEquipment").SetValue(false);
                }
                var character = unitEntityData.View.CharacterAvatar;
                ///Traverse.Create(unitEntityData.Descriptor).Field("UseClassEquipment").SetValue(true);
                if (character == null) return; // Happens when overriding view
                if (doll != null)
                {
                    unitEntityData.Descriptor.ForcceUseClassEquipment = true;
                    Traverse.Create(unitEntityData.Descriptor).Field("UseClassEquipment").SetValue(true);
                    var savedEquipment = true;
                    character.RemoveAllEquipmentEntities(savedEquipment);
                    if (doll.RacePreset != null)
                    {
                        /*  if()
                          {

                          }
                          else
                          {

                          }
                          character.Skeleton = (doll.Gender != Gender.Male) ? doll.RacePreset.FemaleSkeleton : doll.RacePreset.MaleSkeleton;*//*
                        character.AddEquipmentEntities(doll.RacePreset.Skin.Load(doll.Gender, doll.RacePreset.RaceId), savedEquipment);
                        /// Main.logger.Log(character.name);
                        ///Main.logger.Log(unitEntityData.CharacterName);
                    }
                    ///character.m_Mirror = doll.LeftHanded;
                    foreach (string assetID in doll.EquipmentEntityIds)
                    {
                        EquipmentEntity ee = ResourcesLibrary.TryGetResource<EquipmentEntity>(assetID);
                        character.AddEquipmentEntity(ee, savedEquipment);
                    }
                    doll.ApplyRampIndices(character);
                    Traverse.Create(unitEntityData.View).Field("m_EquipmentClass").SetValue(null); //UpdateClassEquipment won't update if the class doesn't change
                                                                                                   //Adds Armor
                    unitEntityData.View.UpdateBodyEquipmentModel();
                    unitEntityData.View.UpdateClassEquipment();

                    Main.SetEELs(unitEntityData, DollResourcesManager.GetDoll(unitEntityData), false);
                    // Main.GenerateHairColor(unitEntityData);
                }
                else
                {
                    character.RemoveAllEquipmentEntities();
                    character.RestoreSavedEquipment();
                    IEnumerable<EquipmentEntity> bodyEquipment = unitEntityData.Body.AllSlots.SelectMany(
                        new Func<ItemSlot, IEnumerable<EquipmentEntity>>(unitEntityData.View.ExtractEquipmentEntities));
                    character.AddEquipmentEntities(bodyEquipment, false);
                }
                Main.SetEELs(unitEntityData, DollResourcesManager.GetDoll(unitEntityData), false);
                //  DollResourcesManager.GetDoll(unitEntityData).SetHairColor(Settings.HairColor);
                //Add Kineticist Tattoos
                EventBus.RaiseEvent<IUnitViewAttachedHandler>(unitEntityData, delegate (IUnitViewAttachedHandler h)
                {
                    h.HandleUnitViewAttached();
                });
            *//*}
            catch (Exception e)
            {
                Main.logger.Log(e.ToString());
            }
        }*/
        static void ChangeCompanionOutfit(UnitEntityView __instance, CharacterSettings characterSettings)
        {
            /*
             * Note UpdateClassEquipment() works by removing the clothes of the old class, and loading the clothes of the new class
             * We can't do that here because we do not have a list of the companion clothes.
             */
            void FilterOutfit(string name)
            {
                __instance.CharacterAvatar.RemoveEquipmentEntities(
                   __instance.CharacterAvatar.EquipmentEntities.Where((ee) => ee != null && ee.name.Contains(name)).ToArray()
                );
            }
            /*
             * Colors
             * Scale Darker, Dark, Medium, Light, Lighter
             * Blue 0-4
             * Green 5-9
             * Yellow 10-14
             * Orange 15-19
             * Red 20-24
             * Purple 25-29
             * Black 30-34
             */
            int primaryIndex = 0;
            int secondaryIndex = 0;
            var asd = "";
            __instance.EntityData.Progression.Classes.Where(a => a.CharacterClass.HasEquipmentEntities()).Do(a => asd = a.CharacterClass.Name);
            ///Main.logger.Log(asd);
            ///Main.logger.Log(asd);
            FilterOutfit(asd);
           /* switch (__instance.EntityData.Blueprint.AssetGuidThreadSafe)
            {
                case "77c11edb92ce0fd408ad96b40fd27121": //"Linzi",
                    FilterOutfit("Bard");
                    //Class Color 12, 16, EE NULL, Visual 13, 17
                    primaryIndex = 13;
                    secondaryIndex = 17;
                    break;
                case "5455cd3cd375d7a459ca47ea9ff2de78": //"Tartuccio",
                    FilterOutfit("Sorcerer");
                    //Class 22, 3, EE 4, 32 (BlueLighter, BlackMedium)
                    primaryIndex = 4;
                    secondaryIndex = 32;
                    break;
                case "54be53f0b35bf3c4592a97ae335fe765": //"Valerie",
                    FilterOutfit("Fighter");
                    //Class 3, 23, EE 31, 17 (BlackDark, OrangeMedium)
                    primaryIndex = 31;
                    secondaryIndex = 17;
                    break;
                case "b3f29faef0a82b941af04f08ceb47fa2": //"Amiri",
                    FilterOutfit("Barbarian");
                    //Class 22, 2, EE 15, 3 (OrangeDarker, BlueLight)
                    primaryIndex = 15;
                    secondaryIndex = 3;
                    break;
                case "aab03d0ab5262da498b32daa6a99b507": //"Harrim",
                    FilterOutfit("Cleric");
                    //Class 34, 22, EE 30, 34 (BlackDarker, BlackLighter)
                    primaryIndex = 30;
                    secondaryIndex = 34;
                    break;
                case "32d2801eddf236b499d42e4a7d34de23": //"Jaethal",
                    FilterOutfit("Inquisitor");
                    //CLass 23, 3, EE None, Visually 22, 3
                    primaryIndex = 22;
                    secondaryIndex = 3;
                    break;
                case "b090918d7e9010a45b96465de7a104c3": //"Regongar",
                    FilterOutfit("Magus");
                    //Class 2, 22, EE 2, 22 (BlueMedium, RedMedium)
                    primaryIndex = 2;
                    secondaryIndex = 22;
                    break;
                case "f9161aa0b3f519c47acbce01f53ee217": //"Octavia",
                    FilterOutfit("Wizard");
                    //Class 27, 2, EE 3, 24 (BlueLight, RedLighter)
                    primaryIndex = 3;
                    secondaryIndex = 24;
                    break;
                case "f6c23e93512e1b54dba11560446a9e02": //"Tristian",
                    FilterOutfit("Cleric");
                    //Class 34, 22, EE 34, 13 (BlackLighter, YellowLight)
                    primaryIndex = 34;
                    secondaryIndex = 13;
                    break;
                case "d5bc1d94cd3e5be4bbc03f3366f67afc": //"Ekundayo",
                    FilterOutfit("Ranger");
                    //Class 23, 7, EE 23, 33 (RedLight, Black Light)
                    primaryIndex = 23;
                    secondaryIndex = 33;
                    break;
                case "3f5777b51d301524c9b912812955ee1e": //"Jubilost",
                    FilterOutfit("Alchemist");
                    //Class 17, 31, EE 17, 31 (OrangeMedium, BlackDark)
                    primaryIndex = 17;
                    secondaryIndex = 31;
                    break;
                case "f9417988783876044b76f918f8636455": //"Nok-Nok",
                    FilterOutfit("Rogue");
                    //Class 31, 22, EE NULL, Visual 32, 23
                    primaryIndex = 32;
                    secondaryIndex = 23;
                    break;
                case "c807d18a89f96c74f8bb48b31b616323": //"Kalikke",
                    FilterOutfit("Kineticist");
                    //Class 23, 18, EE 23, 17 (RedLight, OrangeMedium)
                    primaryIndex = 23;
                    secondaryIndex = 17;
                    break;
                case "f1c0b181a534f4940ae17f243a5968ec": //"Kanerah",
                    FilterOutfit("Kineticist");
                    //Class 23, 18, EE 23, 17 (RedLight, OrangeMedium)
                    primaryIndex = 23;
                    secondaryIndex = 17;
                    break;
            }*/
            //Can't load save if color index is out of range
            if (characterSettings.companionPrimary < 0) characterSettings.companionPrimary = primaryIndex;
            if (characterSettings.companionPrimary < 0) characterSettings.companionPrimary = secondaryIndex;
            if (characterSettings.companionPrimary >= 0) primaryIndex = characterSettings.companionPrimary;
            if (characterSettings.companionSecondary >= 0) secondaryIndex = characterSettings.companionSecondary;
            ///var _class = __instance.EntityData.Descriptor.Progression.GetEquipmentClass();
            var _class = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>(characterSettings.classOutfit.GUID);
            var gender = __instance.EntityData.Descriptor.Gender;
            var race = __instance.EntityData.Progression.Race.RaceId;/// Descriptor.m_LoadedDollData.RacePreset.RaceId;
            var ees = _class.LoadClothes(gender, race);
            __instance.CharacterAvatar.AddEquipmentEntities(ees);
            foreach (var ee in ees)
            {
                __instance.CharacterAvatar.SetPrimaryRampIndex(ee, primaryIndex);
                __instance.CharacterAvatar.SetSecondaryRampIndex(ee, secondaryIndex);
            }
            
        }
        static void HideSlot(UnitEntityView __instance, ItemSlot slot, ref bool dirty)
        {
            var ee = __instance.ExtractEquipmentEntities(slot).ToList();
            if (ee.Count > 0)
            {
                __instance.CharacterAvatar.RemoveEquipmentEntities(ee);
                dirty = true;
            }
        }
        static bool OverrideEquipment(UnitEntityView __instance, ItemSlot slot, string assetId, ref bool dirty)
        {
            var kee = ResourcesLibrary.TryGetBlueprint<KingmakerEquipmentEntity>(assetId);
            if (kee == null) return false;
            var doll = DollResourcesManager.GetDoll(__instance.EntityData);
            Race raceid;
            if (doll == null)
            {
                raceid = __instance.Data.Progression.Race.RaceId;
            }
            else
            {
                raceid = doll.Race.RaceId;
            }
            var ee = kee.Load(__instance.EntityData.Descriptor.Gender, raceid);
            if (ee == null) return false; 
            HideSlot(__instance, slot, ref dirty);
            __instance.CharacterAvatar.AddEquipmentEntities(ee);
            dirty = true;
            return true;
        }
        /*
         * Fix "bug" where Male Ranger Cape would hide hair and ears         * 
         */
        static void FixRangerCloak(UnitEntityView view)
        {
            foreach (var ee in view.CharacterAvatar.EquipmentEntities)
            {
                if (ee.name == "EE_Ranger_M_Cape")
                {
                    ee.HideBodyParts &= ~(BodyPartType.Hair | BodyPartType.Ears);
                }
            }
        }
        public static void NoClassOutfit(UnitEntityView view)
        {
            var classOutfit = view.EntityData.Descriptor.Progression.GetEquipmentClass();
            Settings.CharacterSettings characterSettings = Main.settings.GetCharacterSettings(view.EntityData);
            var doll = DollResourcesManager.GetDoll(view.Data);
            var oldClothes = classOutfit.LoadClothes(view.EntityData.Descriptor.Gender, doll.RacePreset.RaceId);
            view.CharacterAvatar.RemoveEquipmentEntities(oldClothes);
            var newClothes = BlueprintRoot.Instance.CharGen.LoadClothes(view.EntityData.Descriptor.Gender);
            view.CharacterAvatar.AddEquipmentEntities(newClothes);
        }
        public static void UpdateModel(UnitEntityView view)
        {
            try
            {
                if (view.CharacterAvatar == null || view.EntityData == null) return;
                if (!view.EntityData.IsPlayerFaction) return;
                Settings.CharacterSettings characterSettings = Main.settings.GetCharacterSettings(view.EntityData);
                if (view.Data.IsPet)
                {
                    return;
                }

                if (characterSettings == null) return;
                var doll = DollResourcesManager.GetDoll(view.EntityData);
                bool dirty = view.CharacterAvatar.IsDirty;
                if (doll == null &&
                    characterSettings.classOutfit.Name !=
                    "Default") //&& view.m_EquipmentClass != ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>(characterSettings.classOutfit.GUID))
                {
                    ChangeCompanionOutfit(view, characterSettings);
                }

                if (characterSettings.classOutfit.Name == "None" && view.m_EquipmentClass !=
                    ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>(characterSettings.classOutfit.GUID))
                    NoClassOutfit(view);
                if (characterSettings.hideHelmet)
                {
                    HideSlot(view, view.EntityData.Body.Head, ref dirty);
                }

                var equipmentClass = view.EntityData.Progression.GetEquipmentClass();
                if (characterSettings.hideItemCloak )
                {
                    /*   if(equipmentClass.NameForAcronym.Contains("Ranger") || equipmentClass.NameForAcronym.Contains("Rogue") || equipmentClass.NameForAcronym.Contains("Inquisitor"))
                       {
                         /// var j = view.CharacterAvatar.EquipmentEntities.Select(a => a.OutfitParts.Where(b => b.ToString().Contains("Cloak") || b.ToString().Contains("Cape")).First()).First();
                          var containscape = view.CharacterAvatar.EquipmentEntities.First(n => n.OutfitParts.Any(b => b.ToString().Contains("Cloak") || b.ToString().Contains("Cape")));
                          EquipmentEntity.OutfitPart jo = null;
                          if(containscape != null) jo = containscape.OutfitParts.First(c => c.ToString().Contains("Cloak") || c.ToString().Contains("Cape"));
                          if(jo != null)
                          {
                            if(Main.CapeOutfitParts[equipmentClass.NameForAcronym] == null)
                            {
                               Main.CapeOutfitParts.Add(equipmentClass.NameForAcronym,jo);
                            }
                          }
                          var outfitpart = containscape.OutfitParts.First(a => a.ToString().Contains("Cape") || a.ToString().Contains("Cloak"));
                          if(outfitpart != null)
                          {
                           containscape.OutfitParts.Remove(outfitpart);
                          }
                       }*/
                    if (equipmentClass.NameForAcronym.Contains("Ranger") ||
                        equipmentClass.NameForAcronym.Contains("Rogue") ||
                        equipmentClass.NameForAcronym.Contains("Inquisitor"))
                    {
                        var asd = view.CharacterAvatar.EquipmentEntities
                            .Where(a => Enumerable.Any(a.OutfitParts, c =>
                                c.ToString().Contains("Cape") || c.ToString().Contains("Cloak"))).Where(a =>
                                Enumerable.Any(new[] { "Inquisitor", "Rogue", "Ranger" }, c => a.name.Contains(c)));
                        if (asd.ToArray().Length > 0)
                        {
                            foreach (var a in asd)
                            {
                                var part = a.OutfitParts
                                    .Where(b => b.ToString().Contains("Cloak") || b.ToString().Contains("Cape"))
                                    .FirstOrDefault();
                                if (!Main.CapeOutfitParts.ContainsKey(a.name))
                                {
                                    Main.CapeOutfitParts.Add(a.name, part);
                                }

                                a.OutfitParts.Remove(part);
                            }
                        }
                    }

                    var thingstoremove = view.CharacterAvatar.EquipmentEntities
                        .Where(a => a.name.Contains("Cape") || a.name.Contains("Cloak")).ToList();
                    view.CharacterAvatar.RemoveEquipmentEntities(thingstoremove);
                    HideSlot(view, view.EntityData.Body.Shoulders, ref dirty);
                }
                else if (characterSettings.overrideCloak == null && !view.EntityData.Body.Shoulders.HasItem &&
                         !characterSettings.hideItemCloak)
                {
                    if (equipmentClass.NameForAcronym.Contains("Ranger") ||
                        equipmentClass.NameForAcronym.Contains("Rogue") ||
                        equipmentClass.NameForAcronym.Contains("Inquisitor"))
                    {
                        if (!Enumerable.Any(view.CharacterAvatar.EquipmentEntities, a =>
                            Enumerable.Any(a.OutfitParts, b => b.ToString().Contains("Cape") || b.ToString().Contains("Cape"))))
                            if (Enumerable.Any(view.CharacterAvatar.EquipmentEntities, a =>
                                Main.CapeOutfitParts.Keys.Contains(a.name)))
                            {
                                var ad = view.CharacterAvatar.EquipmentEntities.Where(c =>
                                    Main.CapeOutfitParts.Keys.Contains(c.name));
                                if (ad.ToArray().Length > 0)
                                {
                                    foreach (var ads in ad)
                                    {
                                        if (Main.CapeOutfitParts.Keys.Contains(ads.name))
                                            ads.OutfitParts.Add(Main.CapeOutfitParts[ads.name]);
                                    }
                                }
                            }
                    }
                }

                /*else 
                if(equipmentClass.NameForAcronym.Contains("Ranger") || equipmentClass.NameForAcronym.Contains("Rogue") || equipmentClass.NameForAcronym.Contains("Inquisitor"))
                {
                  if(!view.CharacterAvatar.EquipmentEntities.Any(a => a.OutfitParts.Any(b => b.ToString().Contains("Cloak") || b.ToString().Contains("Cape"))))
                  {
                    view.CharacterAvatar.EquipmentEntities.First(b => b.name.Contains("Ranger_") || b.name.Contains("Rogue_") || b.name.Contains("Inquisitor_")).OutfitParts.Add(Main.CapeOutfitParts[equipmentClass.NameForAcronym]);
                  }
                }*/
                if (characterSettings.hideArmor )
                {
                    HideSlot(view, view.EntityData.Body.Armor, ref dirty);
                }
                if (characterSettings.hideGlasses)
                {
                    HideSlot(view, view.EntityData.Body.Glasses, ref dirty);
                }
                if (characterSettings.hideGloves )
                {
                    HideSlot(view, view.EntityData.Body.Gloves, ref dirty);
                }
                if (characterSettings.hideBracers )
                {
                    HideSlot(view, view.EntityData.Body.Wrist, ref dirty);
                }
                if (characterSettings.hideBoots )
                {
                    HideSlot(view, view.EntityData.Body.Feet, ref dirty);
                }
                if (characterSettings.hideShirt)
                {
                    HideSlot(view, view.EntityData.Body.Shirt, ref dirty);
                }
                if (characterSettings.hideHorns)
                {
                    foreach (var ee in view.CharacterAvatar.EquipmentEntities.ToArray())
                    {
                        if (ee.BodyParts.Exists((bodypart) => bodypart.Type == BodyPartType.Horns))
                        {
                            view.CharacterAvatar.EquipmentEntities.Remove(ee);
                            dirty = true;
                        }
                    }
                }
                if (characterSettings.hideTail)
                {
                    foreach (var ee in view.CharacterAvatar.EquipmentEntities.ToArray())
                    {
                        if (ee.name.Contains("Tail"))
                        {
                            view.CharacterAvatar.EquipmentEntities.Remove(ee);
                            dirty = true;
                        }
                    }
                }
                if (characterSettings.hideClassCloak)
                {
                    foreach (var ee in view.CharacterAvatar.EquipmentEntities.ToArray())
                    {
                        if (ee.OutfitParts.Exists((outfit) =>
                        {
                            return outfit.Special == EquipmentEntity.OutfitPartSpecialType.Backpack ||
                                   outfit.Special == EquipmentEntity.OutfitPartSpecialType.Backpack;
                        }) && !view.ExtractEquipmentEntities(view.EntityData.Body.Shoulders).Contains(ee))
                        {
                            view.CharacterAvatar.EquipmentEntities.Remove(ee);
                            dirty = true;
                        }
                    }
                }

                if (characterSettings.hideCap )
                {
                    foreach (var ee in view.CharacterAvatar.EquipmentEntities.ToArray())
                    {
                        if (ee.BodyParts.Exists((bodypart) => bodypart.Type == BodyPartType.Helmet) &&
                            !view.ExtractEquipmentEntities(view.EntityData.Body.Head).Contains(ee))
                        {
                            view.CharacterAvatar.EquipmentEntities.Remove(ee);
                            dirty = true;
                        }
                    }
                }
                if (!characterSettings.overrideWingsEE.Empty() && !characterSettings.hideWings)
                {
                    foreach (var eetoremove in EquipmentResourcesManager.WingsEE.Values)
                    {
                        var ee = ResourcesLibrary.TryGetResource<EquipmentEntity>(eetoremove);
                       if(view.CharacterAvatar.EquipmentEntities.Contains(ee))  view.CharacterAvatar.RemoveEquipmentEntity(ee,false);
                    }
                    view.CharacterAvatar.AddEquipmentEntity(ResourcesLibrary.TryGetResource<EquipmentEntity>(characterSettings.overrideWingsEE));
                }
                if (!characterSettings.overrideTail.Empty() && !characterSettings.hideTail)
                {
                    foreach (var eetoremove in EquipmentResourcesManager.TailsEE.OfType<EquipmentEntity>())
                    {
                        if (view.CharacterAvatar.EquipmentEntities.Contains(eetoremove)) view.CharacterAvatar.RemoveEquipmentEntity(eetoremove, false);
                    }
                    view.CharacterAvatar.AddEquipmentEntity(EquipmentResourcesManager.TailsEE[characterSettings.overrideTail]);
                }
                if (!characterSettings.overrideHorns.Empty() && !characterSettings.hideHorns)
                {
                    foreach (var eetoremove in EquipmentResourcesManager.HornsEE.OfType<EquipmentEntity>())
                    {
                        if (view.CharacterAvatar.EquipmentEntities.Contains(eetoremove)) view.CharacterAvatar.RemoveEquipmentEntity(eetoremove, false);
                    }
                    view.CharacterAvatar.AddEquipmentEntity(EquipmentResourcesManager.HornsEE[characterSettings.overrideHorns]);
                }
                if (!characterSettings.overrideWingsFX.Empty() && !characterSettings.hideWings)
                {
                    FxHelper.Destroy(wingsFxVisibilityManagers[view]);
                   /* foreach (var fx in )
                    {
                        view.CharacterAvatar.m_AdditionalFXs;
                        if (view.)
                        {
                            
                        }
                        if (view.CharacterAvatar.EquipmentEntities.Contains(eetoremove)) view.CharacterAvatar.RemoveEquipmentEntity(eetoremove, false);
                    }*/
                    if (wingsFxVisibilityManagers.ContainsKey(view) && wingsFxVisibilityManagers[view] != null) GameObject.Destroy(wingsFxVisibilityManagers[view]);
                    if (!characterSettings.overrideWingsFX.Empty())
                    {
                        wingsFxVisibilityManagers[view] = FxHelper.SpawnFxOnUnit(EquipmentResourcesManager.WingsFX[characterSettings.overrideWingsFX], view);
                    }
                    // if (characterSettings.overrideWingsEE != null) view.CharacterAvatar.AddEquipmentEntity(EquipmentResourcesManager.WingsEE[characterSettings.overrideWingsEE]);
                }
                if (characterSettings.overrideHelm != null && !characterSettings.hideHelmet)
                {
                    if (!OverrideEquipment(view, view.EntityData.Body.Head, characterSettings.overrideHelm, ref dirty))
                    {
                        characterSettings.overrideHelm = null;
                    }
                }

                if (characterSettings.overrideCloak != null && !characterSettings.hideItemCloak)
                {
                    if (!OverrideEquipment(view, view.EntityData.Body.Shoulders, characterSettings.overrideCloak,
                        ref dirty))
                    {
                        characterSettings.overrideCloak = null;
                    }
                }

                if (characterSettings.overrideArmor != null && !characterSettings.hideArmor)
                {
                    if (!OverrideEquipment(view, view.EntityData.Body.Armor, characterSettings.overrideArmor,
                        ref dirty))
                    {
                        characterSettings.overrideArmor = null;
                    }
                }

                if (characterSettings.overrideBracers != null && !characterSettings.hideBracers)
                {
                    if (!OverrideEquipment(view, view.EntityData.Body.Wrist, characterSettings.overrideBracers,
                        ref dirty))
                    {
                        characterSettings.overrideBracers = null;
                    }
                }

                if (characterSettings.overrideGloves != null && !characterSettings.hideGloves)
                {
                    if (!OverrideEquipment(view, view.EntityData.Body.Gloves, characterSettings.overrideGloves,
                        ref dirty))
                    {
                        characterSettings.overrideGloves = null;
                    }
                }

                if (characterSettings.overrideBoots != null && !characterSettings.hideBoots)
                {
                    if (!OverrideEquipment(view, view.EntityData.Body.Feet, characterSettings.overrideBoots, ref dirty))
                    {
                        characterSettings.overrideBoots = null;
                    }
                }

                if (characterSettings.overrideGlasses != null && !characterSettings.hideGlasses)
                {
                    if (!OverrideEquipment(view, view.EntityData.Body.Glasses, characterSettings.overrideGlasses,
                        ref dirty))
                    {
                        characterSettings.overrideGlasses = null;
                    }
                }

                if (characterSettings.overrideShirt != null && !characterSettings.hideShirt)
                {
                    if (!OverrideEquipment(view, view.EntityData.Body.Shirt, characterSettings.overrideShirt,
                        ref dirty))
                    {
                        characterSettings.overrideShirt = null;
                    }
                }

                if (characterSettings.overrideTattoo != null)
                {
                    foreach (var assetId in EquipmentResourcesManager.Tattoos.Keys)
                    {
                        var ee = ResourcesLibrary.TryGetResource<EquipmentEntity>(assetId);
                        if (ee != null) view.CharacterAvatar.RemoveEquipmentEntity(ee);
                    }

                    var tattoo = ResourcesLibrary.TryGetResource<EquipmentEntity>(characterSettings.overrideTattoo);
                    if (tattoo != null) view.CharacterAvatar.AddEquipmentEntity(tattoo);
                }

                if (view.EntityData.Descriptor.Progression?.GetEquipmentClass().Name == "Ranger")
                {
                    FixRangerCloak(view);
                }
                if (!characterSettings.overrideMythic.Empty() && !characterSettings.hideMythic)
                {
                    //view.Data.Progression.AdditionalVisualSettings = Utilities.GetBlueprint<BlueprintClassAdditionalVisualSettings>(characterSettings.overrideMythic);
                    view.CharacterAvatar.SetAdditionalVisualSettings(Utilities.GetBlueprint<BlueprintClassAdditionalVisualSettings>(characterSettings.overrideMythic));
                }
                else if (characterSettings.hideMythic)
                {
                    //view.Data.Progression.AdditionalVisualSettings = null;
                    view.CharacterAvatar.SetAdditionalVisualSettings(null);
                }
                else
                {
                    //view.Data.Progression.AdditionalVisualSettings = view.Data.Progression.GetCurrentMythicClass().GetAdditionalVisualSettings();
                    view.CharacterAvatar.SetAdditionalVisualSettings(view.Data.Progression.AdditionalVisualSettings);
                }
                if (view.CharacterAvatar.IsDirty != dirty)
                {
                    view.CharacterAvatar.IsDirty = dirty;
                }

                /*var dollpart = view.Data.Parts.Get<UnitPartDollData>();
                if (dollpart != null)
                {
                    dollpart.SetDefault(ModifiedCreateDollData.CreateDataModified(doll));
                }*/

                var component = view.Data.Parts.Get<UnitPartVAEELs>();
                if (component != null)
                {
                    foreach (var eetoremove in component.EEToRemove)
                    {
                        var ee = ResourcesLibrary.TryGetResource<EquipmentEntity>(eetoremove);
                        if (view.CharacterAvatar.EquipmentEntities.Contains(ee))
                        {
                            view.CharacterAvatar.EquipmentEntities.Remove(ee);
                        }
                    }
                    foreach (var eetoadd in component.EEToAdd)
                    {
                        var ee = ResourcesLibrary.TryGetResource<EquipmentEntity>(eetoadd.AssetID);
                        view.CharacterAvatar.AddEquipmentEntity(ee);
                        view.CharacterAvatar.SetPrimaryRampIndex(ee,eetoadd.PrimaryIndex);
                        view.CharacterAvatar.SetSecondaryRampIndex(ee, eetoadd.SecondaryIndex);
                    }

                }
                if (characterSettings.customOutfitColors)
                {
                    Main.GenerateOutfitcolor(view.Data);
                }
               /* if (view.Data.Parts.Get<UnitPartDollData>())
                {
                    view.Data.Parts.Get<UnitPartDollData>().SetDefault(ModifiedCreateDollData.CreateDataModified(doll));
                }
                else if(doll != null)
                {
                    view.Data.Parts.Add<UnitPartDollData>().SetDefault(ModifiedCreateDollData.CreateDataModified(doll));
                }*/
                // Main.SetEELs(view.EntityData, DollResourcesManager.GetDoll(view.EntityData), false);
            }
            catch (Exception e)
            {
                Main.logger.Error(e.ToString());
            }
        }
        /*
         * Called by CheatsSilly.UpdatePartyNoArmor and OnDataAttached
         * Applies all EquipmentEntities from item Slots for NonBaked avatars
         * Does nothing if SCCCanSeeTheirClassSpecificClothes is enabled
         * */
        [HarmonyPatch(typeof(UnitEntityView), "UpdateBodyEquipmentModel")]
        static class UnitEntityView_UpdateBodyEquipmentModel_Patch
        {
            static void Postfix(UnitEntityView __instance)
            {
                try
                {
                    if (!Main.enabled) return;
                    if (!__instance.Data.IsPlayerFaction) return;
                   // Main.logger.Log("UpdateBody");
                    UpdateModel(__instance);
                  //  RebuildCharacter(__instance.Data);
                }
                catch (Exception ex)
                {
                    Main.Error(ex);
                }
            }
        }
        /*
         * Unclear when called
         * Handles changed hand slots, usable slots
         * When item slot is changed, removes old equipment and adds new slot
         * */
        [HarmonyPatch(typeof(UnitEntityView), "HandleEquipmentSlotUpdated")]
        static class UnitEntityView_HandleEquipmentSlotUpdated_Patch
        {
            static void Postfix(UnitEntityView __instance, ref ItemSlot slot)
            {
                try
                {
                    
                    /*Settings.CharacterSettings characterSettings = Main.settings.GetCharacterSettings(__instance.EntityData);
                    var b = ResourcesLibrary.TryGetBlueprint<KingmakerEquipmentEntity>(characterSettings.overrideArmor.assetId).Load(__instance.Data.Gender, __instance.Data.Progression.Race.RaceId);
                    var c = __instance.EntityData.View.CharacterAvatar.m_EquipmentEntities.Intersect(b);/// != characterSettings.overrideArmor.assetId;*/
                    if (!Main.enabled) return;
                    if (__instance.EntityData != null && __instance.EntityData.IsPlayerFaction)
                    {
                        if (__instance.EntityData.Body.CurrentEquipmentSlots.Contains(slot))
                        {
                            ///DollResourcesManager.GetDoll(__instance.EntityData).Updated();
                            UpdateModel(__instance);
                           /// RebuildCharacter(__instance.Data);
                        }
                    }
                  /*  if(slot.ToString().Contains("HandSlot") || slot.ToString().Contains("UsableSlot") || )
                    {
                        __instance.
                       return;
                    }*/
                    /* if(c.Count() == 0)
                     {
                         Main.logger.Log("nooverlap");
                         UpdateModel(__instance);
                     }
                     if(c.Count() != 0)
                     {
                         Main.logger.Log("overlap");
                     }
                     foreach(EquipmentEntity e in c)
                     {
                         Main.logger.Log(e.ToString()) ;
                     }*/
                   /// Main.logger.Log(slot.ToString());
                   /// UpdateModel(__instance);
                }
                catch (Exception ex)
                {
                   Main.Error(ex);
                    Main.Error(ex.StackTrace);
                }
            }
        }
        /*
         * Called when a character levels up, or on UnitEntityView.OnDataAttached
         * Removes all equipment of current class, CheatSillyShirt.
         * Adds equipment of new class
         * Adds CheatSillyShirt back
         * Applies doll colors and saves class
         * */
        [HarmonyPatch(typeof(UnitEntityView), "UpdateClassEquipment")]
        static class UnitEntityView_UpdateClassEquipment_Patch
        {
            static void Postfix(UnitEntityView __instance)
            {
                try
                {
                    if (!Main.enabled) return;
                    if (!__instance.Data.IsPlayerFaction) return;
                    UpdateModel(__instance);
                   // RebuildCharacter(__instance.EntityData);
                }
                catch (Exception ex)
                {
                    Main.Error(ex);
                }
            }
        }
        [HarmonyPatch(typeof(UnitProgressionData), "GetEquipmentClass")]
        static class UnitProgressionData_GetEquipmentClass_Patch
        {
            static bool Prefix(UnitProgressionData __instance, ref BlueprintCharacterClass __result)
            {
                try
                {
                    if (!Main.enabled) return true;
                    if (disableEquipmentClassPatch) return true;

                    if (!__instance.Owner.IsPlayerFaction) return true;
                    var characterSettings = Main.settings.GetCharacterSettings(__instance.Owner.Unit);
                    if (characterSettings == null) return true;
                    ///__result = Utilities.GetBlueprintByName<BlueprintCharacterClass>(characterSettings.classOutfit+"Class");
                    __result = Utilities.GetBlueprintByGuid<BlueprintCharacterClass>(characterSettings.classOutfit.GUID);
                    /*switch (characterSettings.classOutfit.Name)
                    {
                        case "Alchemist":
                            __result = (BlueprintCharacterClass)ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("0937bec61c0dabc468428f496580c721");
                            break;
                        case "Barbarian":
                            __result = (BlueprintCharacterClass)ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("f7d7eb166b3dd594fb330d085df41853");
                            break;
                        case "Bard":
                            __result = (BlueprintCharacterClass)ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("772c83a25e2268e448e841dcd548235f");
                            break;
                        case "Cavalier":
                            __result = (BlueprintCharacterClass)ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("3adc3439f98cb534ba98df59838f02c7");
                            break;
                        case "Cleric":
                            __result = (BlueprintCharacterClass)ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
                            break;
                        case "Druid":
                            __result = (BlueprintCharacterClass)ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
                            break;
                        case "Fighter":
                            __result = (BlueprintCharacterClass)ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("48ac8db94d5de7645906c7d0ad3bcfbd");
                            break;
                        case "Inquisitor":
                            __result = (BlueprintCharacterClass)ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
                            break;
                        case "Kineticist":
                            __result = (BlueprintCharacterClass)ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("42a455d9ec1ad924d889272429eb8391");
                            break;
                        case "Magus":
                            __result = (BlueprintCharacterClass)ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("45a4607686d96a1498891b3286121780");
                            break;
                        case "Monk":
                            __result = (BlueprintCharacterClass)ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("e8f21e5b58e0569468e420ebea456124");
                            break;
                        case "Oracle":
                            __result = (BlueprintCharacterClass)ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
                            break;
                        case "Paladin":
                            __result = (BlueprintCharacterClass)ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
                            break;
                        case "Ranger":
                            __result = (BlueprintCharacterClass)ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("cda0615668a6df14eb36ba19ee881af6");
                            break;
                        case "Rogue":
                            __result = (BlueprintCharacterClass)ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("299aa766dee3cbf4790da4efb8c72484");
                            break;
                        case "Shaman":
                            __result = (BlueprintCharacterClass)ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("145f1d3d360a7ad48bd95d392c81b38e");
                            break;
                        case "Skald":
                            __result = (BlueprintCharacterClass)ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("6afa347d804838b48bda16acb0573dc0");
                            break;
                        case "Slayer":
                            __result = (BlueprintCharacterClass)ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("c75e0971973957d4dbad24bc7957e4fb");
                            break;
                        case "Sorcerer":
                            __result = (BlueprintCharacterClass)ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("b3a505fb61437dc4097f43c3f8f9a4cf");
                            break;
                        case "Wizard":
                            __result = (BlueprintCharacterClass)ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("ba34257984f4c41408ce1dc2004e342e");
                            break;
                        default:
                            return true;
                    }*/
                    if (__result == null) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    Main.Error(ex);
                    return true;
                }
            }
        }
        static void TryPreloadKEE(BlueprintRef assetId, Gender gender, Race race)
        {
            if (string.IsNullOrEmpty(assetId)) return;
            var link = ResourcesLibrary.TryGetBlueprint<KingmakerEquipmentEntity>(assetId);
            if (link != null) link.Preload(gender, race);
        }
        static void TryPreloadEE(ResourceRef assetId, Gender gender, Race race)
        {
            if (string.IsNullOrEmpty(assetId)) return;
            ResourcesLibrary.PreloadResource<EquipmentEntity>(assetId);
        }
        static void TryPreloadUnitView(ResourceRef assetId, Gender gender, Race race)
        {
            if (string.IsNullOrEmpty(assetId)) return;
            ResourcesLibrary.PreloadResource<UnitEntityView>(assetId);
        }
        static void TryPreloadWeapon(BlueprintRef assetId, Gender gender, Race race)
        {
            if (string.IsNullOrEmpty(assetId)) return;
            var item = ResourcesLibrary.TryGetBlueprint<BlueprintItemEquipment>(assetId);
            item?.EquipmentEntity?.Preload(gender, race);
        }
        public static void PreloadUnit(UnitEntityView __instance)
        {
            if (__instance == null) return;
            if (__instance.Data.IsPet) return;
            var unit = __instance.EntityData;
            if (!unit.IsPlayerFaction) return;
            var characterSettings = Main.settings.GetCharacterSettings(unit);
            if (characterSettings == null) return;
            var races = BlueprintRoot.Instance.Progression.CharacterRaces.ToArray<BlueprintRace>();
            var blueprintRace = unit.Descriptor.Progression.Race;
            var race = blueprintRace?.RaceId ?? Race.Human;
            var gender = unit.Gender;
            if(characterSettings.overrideHelm != null)TryPreloadKEE(characterSettings.overrideHelm, gender, race);
            if (characterSettings.overrideShirt != null)TryPreloadKEE(characterSettings.overrideShirt, gender, race);
            if (characterSettings.overrideArmor != null)TryPreloadKEE(characterSettings.overrideArmor, gender, race);
            if (characterSettings.overrideBracers != null)TryPreloadKEE(characterSettings.overrideBracers, gender, race);
            if (characterSettings.overrideGloves != null)TryPreloadKEE(characterSettings.overrideGloves, gender, race);
            if (characterSettings.overrideBoots != null)TryPreloadKEE(characterSettings.overrideBoots, gender, race);
            if (characterSettings.overrideTattoo != null)TryPreloadEE(characterSettings.overrideTattoo, gender, race);
            if (characterSettings.overrideTail != null) TryPreloadEE(characterSettings.overrideTattoo, gender, race);
            foreach (var kv in characterSettings.overrideWeapons)
            {
                TryPreloadWeapon(kv.Value, gender, race);
            }
            if (!string.IsNullOrEmpty(characterSettings.overrideView))
            {
                ResourcesLibrary.PreloadResource<GameObject>(characterSettings.overrideView);
            }
            if (characterSettings.classOutfit.Name == "None")
            {
                var clothes = gender == Gender.Male ? BlueprintRoot.Instance.CharGen.MaleClothes : BlueprintRoot.Instance.CharGen.FemaleClothes;
                foreach (var clothing in clothes) clothing.Preload();
            }
        }
        [HarmonyPatch(typeof(ResourcesPreload), "PreloadUnitResources")]
        static class ResourcesPreload_PreloadUnitResources_Patch
        {
            static void Postfix()
            {
                try
                {
                    foreach (UnitEntityData unitEntityData in Game.Instance.State.PlayerState.AllCharacters)
                    {
                        PreloadUnit(unitEntityData.View);
                    }
                }
                catch (Exception ex)
                {
                    Main.Error(ex);
                }
            }
        }
        /*[HarmonyPatch(typeof(Game), "OnAreaLoaded")]
        static class Game_OnAreaLoaded_Patch
        {
            static void Postfix(Game __instance)
            {
                try
                {
                    if (!Main.enabled) return;
                    if (!Main.settings.rebuildCharacters) return;
                     Main.Asd();
                     Main.Log("Rebuilding characters");
                     foreach (UnitEntityData character in __instance.Player.PartyAndPets)
                     {
                         character.CreateView();
                         character.CreateViewForData();
                         Main.logger.Log(character.CharacterName);
                         var Settings = Main.settings.GetCharacterSettings(character);
                         var races = BlueprintRoot.Instance.Progression.CharacterRaces.ToArray<BlueprintRace>();
                         DollResourcesManager.GetDoll(character).SetRace(races[Settings.RaceIndex]);
                         Main.logger.Log(races[Settings.RaceIndex].name);
                         Main.SetEELs();
                        RebuildCharacter(character);
                       /// UpdateModel(character.View);
                        /*Main.SetEELs(character, DollResourcesManager.GetDoll(character));
                        var doll = DollResourcesManager.GetDoll(character);
                         doll.SetEquipColors(doll.EquipmentRampIndex,doll.EquipmentRampIndexSecondary);
                        character.View.HandsEquipment.UpdateAll();
                         DollResourcesManager.GetDoll(character).SetRace(races[Settings.RaceIndex]);
                         Main.logger.Log(races[Settings.RaceIndex].ToString());*/
        /* }
    } catch(Exception ex)
    {
        Main.Error(ex);
    }
}
}*/
       /* [HarmonyPatch(typeof(DollState), "Updated")]
        static class DollStateUpdated
        {
            static void Postfix(DollState __instance)
            {
                if(Main.enabled)
                {
                    foreach (var asd in Game.Instance.Player.AllCharacters)
                    {
                        CharacterManager.RebuildCharacter(asd);
                    }
                }
            }
        }*/

        [HarmonyPatch(typeof(CommonVM), "HideLoadingScreen")]
        static class Game_loadcomplete_Patch
        {
            static void Postfix()
            {
                try
                {
                    if (!Main.enabled) return;
                    if (!Main.settings.rebuildCharacters) return;
                    Main.Log("Rebuilding characters");
                    if (!Main.classesloaded)
                    {
                        Main.GetClasses();
                    }
                    foreach (var character in Game.Instance.Player.AllCharacters)
                    {
                       /* foreach (var ee in character.View.CharacterAvatar.SavedEquipmentEntities)
                        {
                            Main.logger.Log(ee.Load(true) + character.CharacterName);
                        }*/
                        //var doll = DollResourcesManager.GetDoll(character);
                        ///Main.SetEELs(character,doll);
                        ///character.View.UpdateClassEquipment();
                        //  Main.GenerateHairColor(character);
                        //Main.SetEELs(character, doll, true);
                        RebuildCharacter(character);
                        //Thread.Sleep(250);
                        UpdateModel(character.View);
                        /// character.View.HandsEquipment.UpdateAll();
                    }
                }
                catch (Exception ex)
                {
                    Main.Error(ex);
                }
            }
        }
       // [HarmonyPatch(typeof(Game), "OnAreaLoaded")]
        static class Game_OnAreaLoaded_Patch
        {
            static void Postfix()
            {
                try
                {
                    if (!Main.enabled) return;
                    if (!Main.settings.rebuildCharacters) return;
                    Main.Log("Rebuilding characters");
                    if(!Main.classesloaded)
                    {
                        Main.GetClasses();
                    }
                    foreach (var character in Game.Instance.Player.AllCharacters)
                    {
                        //var doll = DollResourcesManager.GetDoll(character);
                        ///Main.SetEELs(character,doll);
                        ///character.View.UpdateClassEquipment();
                      //  Main.GenerateHairColor(character);
                      //  Main.SetEELs(character,doll,true);
                        //RebuildCharacter(character);
                        //Thread.Sleep(250);
                        //UpdateModel(character.View);
                        /// character.View.HandsEquipment.UpdateAll();
                    }
                }
                catch (Exception ex)
                {
                    Main.Error(ex);
                }
            }
        }
    }
}
