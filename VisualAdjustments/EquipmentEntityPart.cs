using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Utility;
using Kingmaker.Visual.CharacterSystem;
using Newtonsoft.Json;
using UnityEngine;

namespace VisualAdjustments
{
    public class EEStorage
    {
        public EEStorage(string assetId, int primaryIndex, int secondaryIndex)
        {
            AssetID = assetId;
            PrimaryIndex = primaryIndex;
            SecondaryIndex = secondaryIndex;
            //Main.logger.Log(assetId + primaryIndex + secondaryIndex);
        }
        [JsonConstructor]
        public EEStorage(string assetId, int primaryIndex, int secondaryIndex, bool hascustom = false, float[] CustomColorPrimin = null,float[] CustomColorSecin = null)
        {
            AssetID = assetId;
            PrimaryIndex = primaryIndex;
            SecondaryIndex = secondaryIndex;
            //hasCustomColor = true;
            if(CustomColorPrimin != null)
            {
                CustomColorPrim = CustomColorPrimin;
            }
            if (CustomColorSecin != null)
            {
                CustomColorSec = CustomColorSecin;
            }
            hasCustomColor = hascustom;
            //CustomColorPrim = new float[] { CustomColorPrimin.r, CustomColorPrimin.g, CustomColorPrimin.b };
            //CustomColorPrim = new float[] { CustomColorPrimin.r, CustomColorPrimin.g, CustomColorPrimin.b };
            //Main.logger.Log(assetId + primaryIndex + secondaryIndex);
        }
        [JsonProperty] public string AssetID = "";
        [JsonProperty] public int PrimaryIndex = -1;
        [JsonProperty] public int SecondaryIndex = -1;
        [JsonProperty] public bool hasCustomColor = false;
       // [JsonProperty] public EEStorage parent;
        [JsonProperty] public float[] CustomColorPrim = new float[] { 0, 0, 0 };
        [JsonProperty] public float[] CustomColorSec = new float[] { 0, 0, 0 };
        public void Apply(EquipmentEntity ee,Character Avatar)
        {
            bool CheckIfNoColor(float[] col)
            {
                if (col[0] == (float)0 && col[1] == (float)0 && col[2] == (float)0) return true;
                return false;
            }
            //Run after adding EE
            try
            {
                if (hasCustomColor)
                {
                    if(!CheckIfNoColor(this.CustomColorPrim) && ee.PrimaryColorsProfile != null)
                    {
                        {
                            var colornum = this.CustomColorPrim;
                            var settingcol = new Color(colornum[0], colornum[1], colornum[2]);
                            Texture2D tex = null;
                            if (!ee.PrimaryColorsProfile.Ramps.Where(b => b.isReadable).Any(a => a.name == settingcol.ToString()))
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
                                if (!ee.PrimaryColorsProfile.Ramps.Where(b => b.isReadable).Any(a => a.name == texture.name))
                                {
                                    texture.name = settingcol.ToString();
                                    tex = texture;
                                    ee.PrimaryColorsProfile.Ramps.Add(texture);
                                }
                            }
                          //  Main.logger.Log(settingcol.ToString() + "   Setting");
                            foreach (var asd in ee.PrimaryColorsProfile.Ramps.Where(a => a.isReadable))
                            {
                            //    Main.logger.Log(asd.GetPixel(1, 1).ToString() + "   ExistingTexture");
                            }
                            if (ee.PrimaryColorsProfile.Ramps.Where(b => b.isReadable).Any(a => a.name == settingcol.ToString()))
                            {
                               // Main.logger.Log("setindex");
                                var index = ee.PrimaryColorsProfile.Ramps.Where(b => b.isReadable).First(a => a.name == settingcol.ToString());

                                Avatar.SetPrimaryRampIndex(ee, ee.PrimaryColorsProfile.Ramps.IndexOf(index));
                            }

                        }
                    }
                    if (!CheckIfNoColor(this.CustomColorSec) && ee.SecondaryColorsProfile != null)
                    {
                        if (ee.SecondaryColorsProfile == null)
                        {
                            ee.SecondaryColorsProfile = new CharacterColorsProfile();
                            ee.SecondaryColorsProfile.Ramps = new List<Texture2D>();
                        }
                        {
                            var colornum = this.CustomColorSec;
                            var settingcol = new Color(colornum[0], colornum[1], colornum[2]);
                            Texture2D tex = null;
                            if (!ee.SecondaryColorsProfile.Ramps.Where(b => b.isReadable).Any(a => a.name == settingcol.ToString()))
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
                                if (!ee.SecondaryColorsProfile.Ramps.Where(b => b.isReadable).Any(a => a.name == texture.name))
                                {
                                    texture.name = settingcol.ToString();
                                    tex = texture;
                                    ee.SecondaryColorsProfile.Ramps.Add(texture);
                                }
                            }
                            //Main.logger.Log(settingcol.ToString() + "   Setting");
                            foreach (var asd in ee.SecondaryColorsProfile.Ramps.Where(a => a.isReadable))
                            {
                               // Main.logger.Log(asd.GetPixel(1, 1).ToString() + "   ExistingTexture");
                            }
                            if (ee.SecondaryColorsProfile.Ramps.Where(b => b.isReadable).Any(a => a.name == settingcol.ToString()))
                            {
                               // Main.logger.Log("setindex");
                                var index = ee.SecondaryColorsProfile.Ramps.Where(b => b.isReadable).First(a => a.name == settingcol.ToString());

                                Avatar.SetSecondaryRampIndex(ee, ee.SecondaryColorsProfile.Ramps.IndexOf(index));
                            }

                        }
                    }
                }
                else
                {
                    Avatar.SetPrimaryRampIndex(ee, this.PrimaryIndex);
                    Avatar.SetSecondaryRampIndex(ee, this.SecondaryIndex);
                }
            }
            catch (Exception e)
            {
                Main.logger.Log(e.ToString());
            }
        }
    }
	public class UnitPartVAEELs : UnitPart
    {
        [JsonProperty] public List<EEStorage> EEToAdd = new List<EEStorage>();
        [JsonProperty] public List<string> EEToRemove = new List<string>();


    }
    public class UnitPartVAFX : UnitPart
    {
        [JsonProperty] public bool blackorwhitelist = true;
        [JsonProperty] public List<FXInfo> blackwhitelistnew = new List<FXInfo>();
        public Dictionary<string,FXInfo> blackwhitelist = new Dictionary<string, FXInfo>();
        [JsonProperty] public List<FXInfo> overrides = new List<FXInfo>();
        [NonSerialized()] public Dictionary<string,GameObject> currentoverrides = new Dictionary<string, GameObject>();
    }
    public class FXInfo
    {
        [JsonProperty] public string AssetID;
        [JsonProperty] public string Name;
        public FXInfo(string assetid,string name)
        {
            AssetID = assetid;
            Name = name;
        }
    }

}