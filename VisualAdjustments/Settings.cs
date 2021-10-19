using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Kingmaker.Items.Slots;
using Kingmaker.UI.GenericSlot;
using ModMaker.Utility;
using UnityEngine;
using UnityModManagerNet;
using static UnityModManagerNet.UnityModManager;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Linq;
using Kingmaker.Utility;
using Kingmaker.UnitLogic;

namespace VisualAdjustments
{
   /* public class TupleConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            var match = Regex.Match(objectType.Name, "Tuple`([0-9])", RegexOptions.IgnoreCase);
            return match.Success;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            try
            {
                var asd = new Type[] {typeof(string),typeof(int),typeof(bool) };
                var tupleTypes = objectType.GetProperties().ToList().Select(p => p.PropertyType).ToArray();
                var jObject = Newtonsoft.Json.Linq.JObject.Load(reader);
                Main.logger.Log(tupleTypes.ToString()) ;

                var valueItems = new List<object>();
                Main.logger.Log(asd[0].ToString());
                 for (var i = 1; i <= tupleTypes.Length; i++)
                    valueItems.Add(jObject.[$"m_Item{i}"].ToObject(asd[i - 1]));

                 var convertedObject = objectType.GetConstructor(tupleTypes)?.Invoke(valueItems.ToArray());

                 return convertedObject;
                throw new Exception("Mal");
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong in this implementation", ex);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }*/
    public class Settings : UnityModManager.ModSettings
    {
        public static Tuple<string,int,bool> ParseOverrideTuple(string rawtuple)
        {
            var splitstring = rawtuple.Split(',');
           /* foreach(var asdasda in splitstring)
            {
                Main.logger.Log("su " + asdasda);
            }*/
            var trimarray = new char[] { ' ', '(', ')' };
            var style = splitstring[0].Trim(trimarray);
            var slot = int.Parse(splitstring[1].Trim(trimarray));
            //Main.logger.Log(splitstring[2].Trim(trimarray));
            var primorsec = bool.Parse(splitstring[2].Trim(trimarray));
            var newtuple = new Tuple<string, int, bool>(style, slot, primorsec);
            //Main.logger.Log(newtuple.ToString());
            return newtuple;
        }
        public static Tuple<int, bool> ParseOverrideEnchant(string rawtuple)
        {
            var splitstring = rawtuple.Split(',');
            /* foreach(var asdasda in splitstring)
             {
                 Main.logger.Log("su " + asdasda);
             }*/
            var trimarray = new char[] { ' ', '(', ')' };
            var slot = int.Parse(splitstring[0].Trim(trimarray));
            //Main.logger.Log(splitstring[2].Trim(trimarray));
            var primorsec = bool.Parse(splitstring[1].Trim(trimarray));
            var newtuple = new Tuple<int, bool>(slot, primorsec);
            //Main.logger.Log(newtuple.ToString());
            return newtuple;
        }
        public bool rebuildCharacters = true;
        public bool AllPortraits = false;
        public bool UnlockHair = false;
        public bool enableHotKey = true;
        public class CharacterSettings
        {
            public string characterName = "";
            public string uniqueid = "";
            public bool showClassSelection = false;
            public bool showEELsSelection = false;
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
            /*public int RaceIndex = -1;
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
            public int WarpaintCol = -1;*/

            public Dictionary<string, string> overridesLookup = new SerializableDictionary<string, string>() { };
            public string overrideMythic = "";
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
            public string overrideWingsEE = "";
            public string overrideHorns = "";
            public string overrideTail = "";
            public string overrideWingsFX = "";
            public List<BlueprintRef> overrideMainWeaponEnchantments = new List<BlueprintRef>();
            public List<BlueprintRef> overrideOffhandWeaponEnchantments = new List<BlueprintRef>();
            public bool overrideScale = false;
            public bool overrideScaleShapeshiftOnly = false;
            public bool overrideScaleAdditive = false;
            public bool overrideScaleCheatMode = false;
            public bool overrideScaleFloatMode = false;
            public float overrideScaleFactor = 4;
            public float additiveScaleFactor = 0;
            //public List<KeyValuePair<Tuple<string, int, bool>, BlueprintRef>> overrideswpn = new List<KeyValuePair<Tuple<string, int, bool>, BlueprintRef>>();
            //public Dictionary<OverrideInfo, BlueprintRef> weaponOverrides = new Dictionary<OverrideInfo, BlueprintRef>();
            public Dictionary<string, BlueprintRef> weaponOverrides = new Dictionary<string, BlueprintRef>();
            public Dictionary<string, BlueprintRef> weaponEnchantments = new Dictionary<string, BlueprintRef>();
            //public Dictionary<string, Dictionary<int,Dictionary<bool,BlueprintRef>>> weaponOverrides = new Dictionary<string, Dictionary<int, Dictionary<bool, BlueprintRef>>>();
            //[JsonConverter(typeof(TupleConverter))] public Dictionary<Tuple<string,int,bool>, BlueprintRef> weaponOverrides = new Dictionary<Tuple<string, int, bool>, BlueprintRef>();//new // Animstyle/Slot/Hand (prim or offhand)
            public Dictionary<string, BlueprintRef> overrideWeapons = new Dictionary<string, BlueprintRef>();//old


#if (DEBUG)
            public bool showInfo = false;
#endif
            public CharInfo classOutfit;
            public int companionPrimary = -1;
            public int companionSecondary = -1;
            public float[] hairColor = new float[] { 0, 0, 0};
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
            public bool hideMythic;
        }
        [JsonProperty]
        public Dictionary<string, CharacterSettings> characterSettings = new Dictionary<string, CharacterSettings>();
        public override void Save(UnityModManager.ModEntry modEntry)
        {
            var filepath = Path.Combine(modEntry.Path, "Settings.json");
            try
            {

                JsonSerializer serializer = new JsonSerializer();
                // serializer.Converters.Add(new overrideconverter());
                //serializer.Converters.Add(new TupleConverter());
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
            try
            {
               // modEntry.Logger.Log("triedloadsettings");
                var filepath = Path.Combine(modEntry.Path, "Settings.json");
                if (File.Exists(filepath))
                {
                    try
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        // serializer.Converters.Add(new overrideconverter());
                        //serializer.Converters.Add(new TupleConverter());
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
            catch(Exception e)
            {
                Main.logger.Log("errorsettings");
                throw;
            }
        }
    }
}
