using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityModManagerNet;
using static UnityModManagerNet.UnityModManager;

namespace VisualAdjustments
{
    public class Settings : UnityModManager.ModSettings
    {
        public bool rebuildCharacters = true;
        public bool AllPortraits = false;
        public bool UnlockHair = false;
        public class CharacterSettings
        {
            public string characterName = "";
            public string uniqueid = "";
            public bool showClassSelection = false;
            public bool showDollSelection = false;
            public bool showEquipmentSelection = false;
            public bool showOverrideSelection = false;
            public bool hideCap = false;
            public bool hideClassCloak = false;
            public bool hideHelmet = false;
            public bool hideItemCloak = false;
            public bool hideArmor = false;
            public bool hideBracers = false;
            public bool hideGloves = false;
            public bool hideBoots = false;
            public bool hideWings = false;
            public bool hideWeaponEnchantments = false;
            public bool hideTail = false;
            public bool hideHorns = false;
            public bool hideWeapons = false;
            public bool hideBeltSlots = false;
            public bool hidequiver = false;
            public bool hideGlasses = false;
            public bool hideShirt = false;
            public bool hideAll = false;
            public bool ReloadStuff = false;
            public int RaceIndex = -1;
            public int Face = -1;
            public int Hair = -1;
            public int Beards = -1;
            public int HairColor = -1;
            public int SkinColor = -1;
            public int Horns = -1;
            public int HornsColor = -1;
            public int PrimaryColor = -1;
            public int SecondaryColor = -1;
            public int BodyType = -1;
            public int Scar = -1;
            public int Warpaint = -1;
            public int WarpaintCol = -1;

            public BlueprintRef overrideHelm = null;
            public BlueprintRef overrideCloak = null;
            public BlueprintRef overrideShirt = null;
            public BlueprintRef overrideGlasses = null;
            public BlueprintRef overrideArmor = null;
            public BlueprintRef overrideBracers = null;
            public BlueprintRef overrideGloves = null;
            public BlueprintRef overrideBoots = null;
            public ResourceRef overrideTattoo = null;
            public ResourceRef overrideOther = null;
            public ResourceRef overrideView = null;
            public List<BlueprintRef> overrideMainWeaponEnchantments = new List<BlueprintRef>();
            public List<BlueprintRef> overrideOffhandWeaponEnchantments = new List<BlueprintRef>();
            public bool overrideScale = false;
            public bool overrideScaleShapeshiftOnly = false;
            public bool overrideScaleAdditive = false;
            public bool overrideScaleCheatMode = false;
            public bool overrideScaleFloatMode = false;
            public float overrideScaleFactor = 4;
            public float additiveScaleFactor = 0;
            public Dictionary<string, BlueprintRef> overrideWeapons = new Dictionary<string, BlueprintRef>();


#if (DEBUG)
            public bool showInfo = false;
#endif
            public CharInfo classOutfit;
            public int companionPrimary = -1;
            public int companionSecondary = -1;
            public float[] hairColor = new float[] {0,0,0};
            public float[] skinColor = new float[] { 0, 0, 0 };
            public bool customHairColor;
            public bool customSkinColor;
            public bool customWarpaintColor;
            public bool customHornColor;
            public bool showSkin;
            public bool showHair;
            public float[] primColor = new float[] { 0, 0, 0 };
            public float[] secondColor = new float[] { 0, 0, 0 };
            public float[] hornColor = new float[] { 0, 0, 0 };
            public float[] warpaintColor = new float[] { 0, 0, 0 };
            public bool customOutfitColors;
            public bool showPrimColor;
            public bool showSecondColor;
            public bool showWarpaintColor;
            public bool showHornColor;
            public bool hideSheaths;
        }
        [JsonProperty]
        public Dictionary<string, CharacterSettings> characterSettings = new Dictionary<string, CharacterSettings>();
        public override void Save(UnityModManager.ModEntry modEntry)
        {
            var filepath = Path.Combine(modEntry.Path, "Settings.json");
            try
            {

                JsonSerializer serializer = new JsonSerializer();
#if (DEBUG)
                serializer.Formatting = Formatting.Indented;
#endif
                using (StreamWriter sw = new StreamWriter(filepath))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, this);
                }
            }
            catch (Exception ex)
            {
                modEntry.Logger.Error($"Can't save {filepath}.");
                modEntry.Logger.Error(ex.ToString());
            }
        }
        public CharacterSettings GetCharacterSettings(UnitEntityData unitEntityData)
        {
            characterSettings.TryGetValue(unitEntityData.UniqueId, out CharacterSettings result);
            return result;
        }
        public void AddCharacterSettings(UnitEntityData unitEntityData, CharacterSettings newSettings)
        {
            characterSettings[unitEntityData.UniqueId] = newSettings;
        }
        public static Settings Load(ModEntry modEntry)
        {
            var filepath = Path.Combine(modEntry.Path, "Settings.json");
            if (File.Exists(filepath))
            {
                try
                {
                    JsonSerializer serializer = new JsonSerializer();
                    using (StreamReader sr = new StreamReader(filepath))
                    using (JsonTextReader reader = new JsonTextReader(sr))
                    {
                        Settings result = serializer.Deserialize<Settings>(reader);
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    modEntry.Logger.Error($"Can't read {filepath}.");
                    modEntry.Logger.Error(ex.ToString());
                }
            }
            return new Settings();
        }
    }
}
