using HarmonyLib;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.CharGen;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Root;
using Kingmaker.Cheats;
using Kingmaker.Designers;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items.Slots;
using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.View;
using Kingmaker.View.Equipment;
using Kingmaker.Visual.CharacterSystem;
using Kingmaker.Visual.Decals;
using Kingmaker.Visual.Particles;
using Kingmaker.Visual.Sound;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Kingmaker.Blueprints.Items.Weapons;
using UnityEngine;
namespace VisualAdjustments
{
    public class InfoManager
    {
        public class EquipmentEntityInfo
        {
            public string type = "Unknown";
            public string raceGenderCombos = "";
            public EquipmentEntityLink eel = null;
        }
        static private Dictionary<string, EquipmentEntityInfo> m_lookup = null;
        static Dictionary<string, EquipmentEntityInfo> lookup
        {
            get
            {
                if (m_lookup == null) BuildLookup();
                return m_lookup;
            }
        }
        private static Dictionary<string, string> m_OrphanedKingmakerEquipment;
        private static string selectedKingmakerOrphanedEquipment = "";
        private static Dictionary<string, string> m_OrphanedMaleEquipment;
        private static Dictionary<string, string> m_OrphanedFemaleEquipment;
        private static string selectedOrphanedEquipment = "";
        static BlueprintBuff[] blueprintBuffs = new BlueprintBuff[] { };
        static bool showWeapons = false;
        static bool showCharacter = false;
        static bool showBuffs = false;
        static bool showFx = false;
        static bool showAsks = false;
        static bool showDoll = false;
        static bool showPortrait = false;
        static string GetName(EquipmentEntityLink link)
        {
            if (LibraryThing.GetResourceGuidMap().ContainsKey(link.AssetId)) return LibraryThing.GetResourceGuidMap()[link.AssetId];
            return null;
        }
        static void AddLinks(EquipmentEntityLink[] links, string type, Race race, Gender gender)
        {
            foreach (var link in links)
            {
                var name = GetName(link);
                if (name == null) continue;
                if (lookup.ContainsKey(name))
                {
                    lookup[name].raceGenderCombos += ", " + race + gender;
                }
                else
                {
                    lookup[name] = new EquipmentEntityInfo
                    {
                        type = type,
                        raceGenderCombos = "" + race + gender,
                        eel = link
                    };
                }
            }
        }
        static void BuildLookup()
        {
            m_lookup = new Dictionary<string, EquipmentEntityInfo>(); ;
            /*var races = BluePrintThing.GetBlueprints<BlueprintRace>();
            var racePresets = BluePrintThing.GetBlueprints<BlueprintRaceVisualPreset>();
            var classes = BluePrintThing.GetBlueprints<BlueprintCharacterClass>();*/
            var races = Main.blueprints.OfType<BlueprintRace>();
            var racePresets = Main.blueprints.OfType<BlueprintRaceVisualPreset>();
            var classes = Main.blueprints.OfType<BlueprintCharacterClass>();

            foreach (var race in races)
            {
                foreach (var gender in new Gender[] { Gender.Male, Gender.Female })
                {
                    CustomizationOptions customizationOptions = gender != Gender.Male ? race.FemaleOptions : race.MaleOptions;
                    AddLinks(customizationOptions.Heads, "Head", race.RaceId, gender);
                    AddLinks(customizationOptions.Hair, "Hair", race.RaceId, gender);
                    AddLinks(customizationOptions.Beards, "Beards", race.RaceId, gender);
                    AddLinks(customizationOptions.Eyebrows, "Eyebrows", race.RaceId, gender);
                }
            }
            foreach (var racePreset in racePresets)
            {
                foreach (var gender in new Gender[] { Gender.Male, Gender.Female })
                {
                    var raceSkin = racePreset.Skin;
                    if (raceSkin == null) continue;
                    AddLinks(raceSkin.GetLinks(gender, racePreset.RaceId), "Skin", racePreset.RaceId, gender);
                }
            }
            foreach (var _class in classes)
            {
                foreach (var race in races)
                {
                    foreach (var gender in new Gender[] { Gender.Male, Gender.Female })
                    {
                        AddLinks(_class.GetClothesLinks(gender, race.RaceId).ToArray(), "ClassOutfit", race.RaceId, gender);
                    }
                }
            }
            ///var gear = BluePrintThing.GetBlueprints<KingmakerEquipmentEntity>();
            var gear = Main.blueprints.OfType<KingmakerEquipmentEntity>();
            foreach (var race in races)
            {
                foreach (var gender in new Gender[] { Gender.Male, Gender.Female })
                {
                    foreach (var kee in gear)
                    {
                        AddLinks(kee.GetLinks(gender, race.RaceId), "Armor", race.RaceId, gender);
                    }
                }
            }
            ///blueprintBuffs = BluePrintThing.GetBlueprints<BlueprintBuff>().ToArray();
            blueprintBuffs = Main.blueprints.OfType<BlueprintBuff>().ToArray();
        }
            public static void ShowInfo(UnitEntityData unitEntityData)
                {;
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Rebuild Character",GUILayout.Width(175f)))
                    {
                        CharacterManager.RebuildCharacter(unitEntityData);
                    }
                    if (GUILayout.Button("Rebuild Outfit",GUILayout.Width(175f)))
                    {
                       var bakedCharacter = unitEntityData.View.CharacterAvatar.BakedCharacter;
                       unitEntityData.View.CharacterAvatar.BakedCharacter = null;
                        unitEntityData.View.CharacterAvatar.RebuildOutfit();
                        unitEntityData.View.CharacterAvatar.BakedCharacter = bakedCharacter;
                    }
                    if (GUILayout.Button("Update Class Equipment",GUILayout.Width(175f)))
                    {
                        var bakedCharacter = unitEntityData.View.CharacterAvatar.BakedCharacter;
                        unitEntityData.View.CharacterAvatar.BakedCharacter = null;
                        bool useClassEquipment = unitEntityData.Descriptor.ForcceUseClassEquipment;
                        unitEntityData.Descriptor.ForcceUseClassEquipment = true;
                        unitEntityData.View.UpdateClassEquipment();
                        unitEntityData.Descriptor.ForcceUseClassEquipment = useClassEquipment;
                        unitEntityData.View.CharacterAvatar.BakedCharacter = bakedCharacter;
                    }
                    if (GUILayout.Button("Update Body Equipment",GUILayout.Width(175f)))
                    {
                        var bakedCharacter = unitEntityData.View.CharacterAvatar.BakedCharacter;
                        unitEntityData.View.CharacterAvatar.BakedCharacter = null;
                        unitEntityData.View.UpdateBodyEquipmentModel();
                        unitEntityData.View.CharacterAvatar.BakedCharacter = bakedCharacter;
                    }
                    if (GUILayout.Button("Update Model",GUILayout.Width(175f)))
                    {
                      CharacterManager.UpdateModel(unitEntityData.View);
                    }
                    if (GUILayout.Button("Update HandsEquipment",GUILayout.Width(175f)))
                    {
                        unitEntityData.View.HandsEquipment.UpdateAll();
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    /*if (GUILayout.Button("Set EEL's",GUILayout.Width(175f)))
                    {
                        Main.SetEELs(unitEntityData,DollResourcesManager.GetDoll(unitEntityData),false);
                    }*/
                    if(GUILayout.Button("Generate Procedural Hair",GUILayout.Width(175f)))
                    {
                      Main.GenerateHairColor(unitEntityData);
                    }
                    if(GUILayout.Button("Generate Procedural Skin",GUILayout.Width(175f)))
                    {
                      Main.GenerateSkinColor(unitEntityData);
                    }
                    if(GUILayout.Button("Generate Procedural Outfit Colors",GUILayout.Width(175f)))
                    { 
                      Main.GenerateOutfitcolor(unitEntityData);
                    }
                    if (GUILayout.Button("Toggle Stance",GUILayout.Width(175f)))
                    {
                        unitEntityData.View.HandsEquipment.ForceSwitch(!unitEntityData.View.HandsEquipment.InCombat);
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    ModKit.UI.Label($"Original size {unitEntityData.Descriptor.OriginalSize}", GUILayout.Width(200f));
                    ModKit.UI.Label($"Current size {unitEntityData.Descriptor.State.Size}", GUILayout.Width(200f));
                    var m_OriginalScale = Traverse.Create(unitEntityData.View).Field("m_OriginalScale").GetValue<Vector3>();
                    var m_Scale = Traverse.Create(unitEntityData.View).Field("m_Scale").GetValue<float>();
                    var realScale = unitEntityData.View.transform.localScale;
                    ModKit.UI.Label($"View Original {m_OriginalScale.x:0.#}", GUILayout.Width(200f));
                    ModKit.UI.Label($"View Current {m_Scale:0.#}", GUILayout.Width(200f));
                    ModKit.UI.Label($"View Real {realScale.x:0.#}", GUILayout.Width(200f));
                    ModKit.UI.Label($"Disabled Scaling {unitEntityData.View.DisableSizeScaling}", GUILayout.Width(200f));
                    GUILayout.EndHorizontal();
                    var message =
                            unitEntityData.View == null ? "No View" :
                            unitEntityData.View.CharacterAvatar == null ? "No Character Avatar" :
                            null;
                    if(message != null) ModKit.UI.Label(message, GUILayout.Width(200f));
                    GUILayout.BeginHorizontal();
                    showCharacter = GUILayout.Toggle(showCharacter, "Show Character",GUILayout.Width(175f));
                    showWeapons = GUILayout.Toggle(showWeapons, "Show Weapons",GUILayout.Width(175f));
                    showDoll = GUILayout.Toggle(showDoll, "Show Doll",GUILayout.Width(175f));
                    showBuffs = GUILayout.Toggle(showBuffs, "Show Buffs",GUILayout.Width(175f));
                    showFx = GUILayout.Toggle(showFx, "Show FX",GUILayout.Width(175f));
                    showPortrait = GUILayout.Toggle(showPortrait, "Show Portrait", GUILayout.Width(175f));
                    showAsks = GUILayout.Toggle(showAsks, "Show Asks", GUILayout.Width(175f));

                    GUILayout.EndHorizontal();
                    if (showCharacter) ShowCharacterInfo(unitEntityData);
                    if (showWeapons) ShowWeaponInfo(unitEntityData);
                    if (showDoll) ShowDollInfo(unitEntityData);
                    if (showBuffs) ShowBuffInfo(unitEntityData);
                    if (showFx) ShowFxInfo(unitEntityData);
                    if (showPortrait) ShowPortraitInfo(unitEntityData);
                    if (showAsks) ShowAsksInfo(unitEntityData);

                }
     
        static void BuildOrphanedEquipment()
        {
            string maleFilepath = "Mods/VisualAdjustments/MaleOrphanedEquipment.json";
            if (File.Exists(maleFilepath))
            {
                JsonSerializer serializer = new JsonSerializer();
                using (StreamReader sr = new StreamReader(maleFilepath))
                using (JsonTextReader reader = new JsonTextReader(sr))
                {
                    var result = serializer.Deserialize<Dictionary<string, string>>(reader);
                    m_OrphanedMaleEquipment = result;
                    if(m_OrphanedMaleEquipment == null) Main.Log($"Error loading {maleFilepath}");
                }
            }
            var femaleFilepath = "Mods/VisualAdjustments/FemaleOrphanedEquipment.json";
            if (File.Exists(femaleFilepath))
            {
                JsonSerializer serializer = new JsonSerializer();
                using (StreamReader sr = new StreamReader(femaleFilepath))
                using (JsonTextReader reader = new JsonTextReader(sr))
                {
                    var result = serializer.Deserialize<Dictionary<string, string>>(reader);
                    m_OrphanedFemaleEquipment = result;
                    if (m_OrphanedFemaleEquipment == null) Main.Log($"Error loading {femaleFilepath}");
                }
            }
            if (m_OrphanedMaleEquipment == null || m_OrphanedFemaleEquipment == null)
            {
                Main.Log("Rebuilding Orphaned Equipment Lookup");
                var eeBlacklist = new HashSet<string>();
                foreach (var gender in new Gender[] { Gender.Male, Gender.Female })
                {
                    foreach (var race in BlueprintRoot.Instance.Progression.CharacterRaces)
                    {
                        ///var armorLinks = BluePrintThing.GetBlueprints<KingmakerEquipmentEntity>()
                        var armorLinks = Main.blueprints.OfType<KingmakerEquipmentEntity>()
                            .SelectMany(kee => kee.GetLinks(gender, race.RaceId));
                        var options = gender == Gender.Male ? race.MaleOptions : race.FemaleOptions;
                        var links = race.Presets
                            .SelectMany(preset => preset.Skin.GetLinks(gender, race.RaceId))
                            .Concat(armorLinks)
                            .Concat(options.Beards)
                            .Concat(options.Eyebrows)
                            .Concat(options.Hair)
                            .Concat(options.Heads)
                            .Concat(options.Horns);
                        foreach (var link in links)
                        {
                            eeBlacklist.Add(link.AssetId);
                        }
                    }
                }

                m_OrphanedMaleEquipment = new Dictionary<string, string>();
                m_OrphanedFemaleEquipment = new Dictionary<string, string>();
                foreach (var kv in LibraryThing.GetResourceGuidMap().OrderBy(kv => kv.Value))
                {
                    if (eeBlacklist.Contains(kv.Key)) continue;
                    var ee = ResourcesLibrary.TryGetResource<EquipmentEntity>(kv.Key);
                    if (ee == null) continue;
                    var nameParts = ee.name.Split('_');
                    bool isMale = nameParts.Contains("M");
                    bool isFemale = nameParts.Contains("F");
                    if (!isMale && !isFemale)
                    {
                        isMale = true;
                        isFemale = true;
                    }
                    if (isMale) m_OrphanedMaleEquipment[kv.Key] = kv.Value;
                    if (isFemale) m_OrphanedFemaleEquipment[kv.Key] = kv.Value;
                }
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                using (StreamWriter sw = new StreamWriter(maleFilepath))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, m_OrphanedMaleEquipment);
                }
                using (StreamWriter sw = new StreamWriter(femaleFilepath))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, m_OrphanedFemaleEquipment);
                }
                ResourcesLibrary.CleanupLoadedCache();
            }
        }
        static void BuildOrphenedKingmakerEquipment()
        {
            m_OrphanedKingmakerEquipment = new Dictionary<string, string>();
            var itemLinks = EquipmentResourcesManager.Helm.Keys
                            .Concat(EquipmentResourcesManager.Shirt.Keys)
                            .Concat(EquipmentResourcesManager.Armor.Keys)
                            .Concat(EquipmentResourcesManager.Bracers.Keys)
                            .Concat(EquipmentResourcesManager.Gloves.Keys)
                            .Concat(EquipmentResourcesManager.Boots.Keys)
                            .Distinct()
                            .ToDictionary(key => key);
            ///foreach (var kee in BluePrintThing.GetBlueprints<KingmakerEquipmentEntity>())
            foreach(var kee in Main.blueprints.OfType<KingmakerEquipmentEntity>())
            {
                if (!itemLinks.ContainsKey(kee.AssetGuidThreadSafe))
                {
                    m_OrphanedKingmakerEquipment[kee.AssetGuidThreadSafe] = kee.name;
                }
            }
        }
        static string expandedEE = null;
        static void ShowCharacterInfo(UnitEntityData unitEntityData)
        {
            var character = unitEntityData.View.CharacterAvatar;
            if (character == null) return;
            ModKit.UI.Label($"View: {unitEntityData.View.name}", GUILayout.Width(200f));
            ModKit.UI.Label($"BakedCharacter: {character.BakedCharacter?.name ?? "NULL"}", GUILayout.Width(200f));

            if (m_OrphanedKingmakerEquipment == null) BuildOrphenedKingmakerEquipment();
            if (m_OrphanedMaleEquipment == null || m_OrphanedFemaleEquipment == null)
            {
                BuildOrphanedEquipment();
            }
            void onEquipment()
            {
                unitEntityData.View.CharacterAvatar.RemoveAllEquipmentEntities();
                var preset = unitEntityData.Descriptor.Progression.Race.Presets.First();
                var skin = preset.Skin.Load(unitEntityData.Gender, preset.RaceId);
                unitEntityData.View.CharacterAvatar.AddEquipmentEntities(skin);
                var kee = ResourcesLibrary.TryGetBlueprint<KingmakerEquipmentEntity>(selectedKingmakerOrphanedEquipment);
                if(kee != null)
                {
                    var ees = kee.Load(unitEntityData.Gender, unitEntityData.Descriptor.Progression.Race.RaceId);
                    unitEntityData.View.CharacterAvatar.AddEquipmentEntities(ees);
                    unitEntityData.View.CharacterAvatar.IsDirty = true;
                }
                var ee = ResourcesLibrary.TryGetResource<EquipmentEntity>(selectedOrphanedEquipment);
                if (ee != null)
                {
                    unitEntityData.View.CharacterAvatar.AddEquipmentEntity(ee);
                    unitEntityData.View.CharacterAvatar.IsDirty = true;
                }
            }
            var equipmentList = unitEntityData.Gender == Gender.Male ? m_OrphanedMaleEquipment : m_OrphanedFemaleEquipment;
            Util.ChooseSlider($"OrphanedKingmakerEquipment", m_OrphanedKingmakerEquipment, ref selectedKingmakerOrphanedEquipment, onEquipment);
            Util.ChooseSlider($"OrphanedEquipment", equipmentList, ref selectedOrphanedEquipment, onEquipment);

            ModKit.UI.Label("Equipment", GUILayout.Width(200f));
            foreach (var ee in character.EquipmentEntities.ToArray())
            {
                GUILayout.BeginHorizontal();
                if (ee == null)
                {
                    ModKit.UI.Label("Null");
                } 
                else
                {
                    ModKit.UI.Label(
                        String.Format("{0}:{1}:{2}:P{3}:S{4}", ee.name, ee.BodyParts.Count, ee.OutfitParts.Count,
                            character.GetPrimaryRampIndex(ee), character.GetSecondaryRampIndex(ee)),
                        GUILayout.ExpandWidth(false));
                }
                if (GUILayout.Button("Remove", GUILayout.ExpandWidth(false)))
                {
                    character.RemoveEquipmentEntity(ee);
                }
                if(ee == null)
                {
                    GUILayout.EndHorizontal();
                    continue;
                }
                bool expanded = ee.name == expandedEE;
                if (expanded && GUILayout.Button("Shrink ", GUILayout.ExpandWidth(false))) expandedEE = null;
                if (!expanded && GUILayout.Button("Expand", GUILayout.ExpandWidth(false))) expandedEE = ee.name;
                GUILayout.EndHorizontal();
                if (expanded)
                {
                    EquipmentEntityInfo settings = lookup.ContainsKey(ee.name) ? lookup[ee.name] : new EquipmentEntityInfo();
                    ModKit.UI.Label($" HideFlags: {ee.HideBodyParts}", GUILayout.Width(200f));
                    var primaryIndex = character.GetPrimaryRampIndex(ee);
                    Texture2D primaryRamp = null;
                    if (primaryIndex < 0 || primaryIndex > ee.PrimaryRamps.Count - 1) primaryRamp = ee.PrimaryRamps.FirstOrDefault();
                    else primaryRamp = ee.PrimaryRamps[primaryIndex];
                    ModKit.UI.Label($"PrimaryRamp: {primaryRamp?.name ?? "NULL"}", GUILayout.Width(200f));

                    var secondaryIndex = character.GetSecondaryRampIndex(ee);
                    Texture2D secondaryRamp = null;
                    if (secondaryIndex < 0 || secondaryIndex > ee.SecondaryRamps.Count - 1) secondaryRamp = ee.SecondaryRamps.FirstOrDefault();
                    else secondaryRamp = ee.SecondaryRamps[secondaryIndex];
                    ModKit.UI.Label($"SecondaryRamp: {secondaryRamp?.name ?? "NULL"}", GUILayout.Width(200f));

                    foreach (var bodypart in ee.BodyParts.ToArray())
                    {
                        GUILayout.BeginHorizontal();
                        ModKit.UI.Label(String.Format(" BP {0}:{1}", bodypart?.RendererPrefab?.name ?? "NULL", bodypart?.Type), GUILayout.ExpandWidth(false));
                        if (GUILayout.Button("Remove", GUILayout.ExpandWidth(false)))
                        {
                            ee.BodyParts.Remove(bodypart);
                        }
                        GUILayout.EndHorizontal();
                        
                    }
                    foreach (var outfitpart in ee.OutfitParts.ToArray())
                    {
                        GUILayout.BeginHorizontal();
                        var prefab = Traverse.Create(outfitpart).Field("m_Prefab").GetValue<GameObject>();
                        ModKit.UI.Label(String.Format(" OP {0}:{1}", prefab?.name ?? "NULL", outfitpart?.Special), GUILayout.ExpandWidth(false));
                        if (GUILayout.Button("Remove",GUILayout.ExpandWidth(false)))
                        {
                            ee.OutfitParts.Remove(outfitpart);
                        }
                        GUILayout.EndHorizontal();
                    }
                }
            }
            ModKit.UI.Label("Character", GUILayout.Width(300));
            ModKit.UI.Label("RampIndices", GUILayout.Width(200f));
            foreach(var index in Traverse.Create(character).Field("m_RampIndices").GetValue<List<Character.SelectedRampIndices>>())
            {
                var name = index.EquipmentEntity != null ? index.EquipmentEntity.name : "NULL";
                ModKit.UI.Label($"  {name} - {index.PrimaryIndex}, {index.SecondaryIndex}");
            }
            ModKit.UI.Label("SavedRampIndices", GUILayout.Width(200f));
            foreach (var index in Traverse.Create(character).Field("m_SavedRampIndices").GetValue<List<Character.SavedSelectedRampIndices>>())
            {
                ModKit.UI.Label($"  {GetName(index.EquipmentEntityLink)} - {index.PrimaryIndex}, {index.SecondaryIndex}");
            }
            ModKit.UI.Label("SavedEquipmentEntities", GUILayout.Width(200f));
            foreach (var link in Traverse.Create(character).Field("m_SavedEquipmentEntities").GetValue<List<EquipmentEntityLink>>())
            {
                var name = GetName(link);
                ModKit.UI.Label($"  {name}");
            }

        }
        static void ShowAsksInfo(UnitEntityData unitEntityData)
        {
            var asks = unitEntityData.Descriptor.Asks;
            var customAsks = unitEntityData.Descriptor.CustomAsks;
            var overrideAsks = unitEntityData.Descriptor.OverrideAsks;
            ModKit.UI.Label($"Current Asks: {asks?.name}, Display: {asks?.DisplayName}", GUILayout.Width(200f));
            ModKit.UI.Label($"Current CustomAsks: {customAsks?.name}, Display: {customAsks?.DisplayName}", GUILayout.Width(200f));
            ModKit.UI.Label($"Current OverrideAsks: {overrideAsks?.name}, Display: {overrideAsks?.DisplayName}", GUILayout.Width(200f));
            foreach (var blueprint in Main.blueprints.OfType<BlueprintUnitAsksList>())
            {
                ModKit.UI.Label($"Asks: {blueprint}, Display: {blueprint.DisplayName}", GUILayout.Width(200f));
            }

        }
        static void ShowPortraitInfo(UnitEntityData unitEntityData)
        {
            var portrait = unitEntityData.Descriptor.Portrait;
            var portraitBP = unitEntityData.Descriptor.UISettings.PortraitBlueprint;
            var uiPortrait = unitEntityData.Descriptor.UISettings.Portrait;
            var CustomPortrait = unitEntityData.Descriptor.UISettings.CustomPortraitRaw;
            ModKit.UI.Label($"Portrait Blueprint: {portraitBP}, {portraitBP?.name}", GUILayout.Width(200f));
            ModKit.UI.Label($"Descriptor Portrait: {portrait}, isCustom {portrait?.IsCustom}", GUILayout.Width(200f));
            ModKit.UI.Label($"UI Portrait: {portrait}, isCustom {portrait?.IsCustom}", GUILayout.Width(200f));
            ModKit.UI.Label($"Custom Portrait: {portrait}, isCustom {portrait?.IsCustom}", GUILayout.Width(200f));
            foreach (var blueprint in DollResourcesManager.Portrait.Values)
            {
                ModKit.UI.Label($"Portrait Blueprint: {blueprint}");
            }
        }
        static void ShowHandslotInfo(HandSlot handSlot)
        {
            GUILayout.BeginHorizontal();
            var pItem = handSlot != null && handSlot.HasItem ? handSlot.Item : null;
            ModKit.UI.Label(string.Format("Slot {0}, {1}, Active {2}", 
                pItem?.Name, pItem?.GetType(), handSlot?.Active), GUILayout.Width(500));
            if (GUILayout.Button("Remove",GUILayout.ExpandWidth(false)))
            {
                handSlot.RemoveItem();
            }
            GUILayout.EndHorizontal();
        }
            static void ShowUnitViewHandSlotData(UnitViewHandSlotData handData)
              {
            var ownerScale = handData.Owner.View.GetSizeScale() * Game.Instance.BlueprintRoot.WeaponModelSizing.GetCoeff(handData.Owner.Descriptor.OriginalSize);
            var visualScale = handData.VisualModel?.transform.localScale ?? Vector3.zero;
            var visualPosition = handData.VisualModel?.transform.localPosition ?? Vector3.zero;
            var sheathScale = handData.SheathVisualModel?.transform.localScale ?? Vector3.zero;
            var sheathPosition = handData.SheathVisualModel?.transform.localPosition ?? Vector3.zero;
            ModKit.UI.Label(string.Format($"weapon {ownerScale:0.#}, scale {visualScale} position {visualPosition}"), GUILayout.Width(500));
            ModKit.UI.Label(string.Format($"sheath {ownerScale:0.#}, scale {sheathScale} position {sheathPosition}"), GUILayout.Width(500));
            GUILayout.BeginHorizontal();
            ModKit.UI.Label(string.Format("Data {0} Slot {1} Active {2}", handData?.VisibleItem?.Name, handData?.VisualSlot, handData?.IsActiveSet), GUILayout.Width(500));

            if (GUILayout.Button("Unequip", GUILayout.Width(200f)))
            {
                handData.Unequip();
            }
            if (GUILayout.Button("Swap Slot", GUILayout.Width(200f)))
            {
                handData.VisualSlot += 1;
                if(handData.VisualSlot == UnitEquipmentVisualSlotType.Quiver) handData.VisualSlot = 0;
                handData.Owner.View.HandsEquipment.UpdateAll();
            }
            if (GUILayout.Button("ShowItem 0", GUILayout.Width(200f)))
            {
                handData.ShowItem(false);
            }
            if (GUILayout.Button("ShowItem 1", GUILayout.Width(200f)))
            {
                handData.ShowItem(true);
            }
            GUILayout.EndHorizontal();
        }
        static void ShowWeaponInfo(UnitEntityData unitEntityData)
        {
            ModKit.UI.Label("Weapons", GUILayout.Width(300));
            var hands = unitEntityData.View.HandsEquipment;
            foreach (var kv in hands.Sets)
            {
                ShowHandslotInfo(kv.Key.PrimaryHand);
                ShowUnitViewHandSlotData(kv.Value.MainHand);
                ShowHandslotInfo(kv.Key.SecondaryHand);
                ShowUnitViewHandSlotData(kv.Value.OffHand);
            }
        }
        static int buffIndex = 0;
        static void ShowBuffInfo(UnitEntityData unitEntityData)
        {
            if (blueprintBuffs.Length == 0)
            {
                BuildLookup();
            }
            GUILayout.BeginHorizontal();
            buffIndex = (int)GUILayout.HorizontalSlider(buffIndex, 0, blueprintBuffs.Length - 1, GUILayout.Width(300));
            if(GUILayout.Button("Prev", GUILayout.Width(45)))
            {
                buffIndex = buffIndex == 0 ? 0 : buffIndex - 1;
            }
            if (GUILayout.Button("Next", GUILayout.Width(45)))
            {
                buffIndex = buffIndex >= blueprintBuffs.Length - 1 ? blueprintBuffs.Length - 1 : buffIndex + 1;
            }
            ModKit.UI.Label($"{blueprintBuffs[buffIndex].NameForAcronym}, {blueprintBuffs[buffIndex].Name}", GUILayout.Width(300));
            if (GUILayout.Button("Apply", GUILayout.Width(200f)))
            {
                GameHelper.ApplyBuff(unitEntityData, blueprintBuffs[buffIndex]);
            }
            GUILayout.EndHorizontal();
            foreach(var buff in unitEntityData.Buffs)
            {
                GUILayout.BeginHorizontal();
                ModKit.UI.Label($"{buff.Blueprint.NameForAcronym}, {buff.Name}", GUILayout.Width(300));
                if (GUILayout.Button("Remove", GUILayout.Width(200f)))
                {
                    GameHelper.RemoveBuff(unitEntityData, buff.Blueprint);   
                }
                GUILayout.EndHorizontal();
            }
        }
        static void ShowDollInfo(UnitEntityData unitEntityData)
        {
            ///var doll = unitEntityData.Descriptor.m_LoadedDollData;
            var doll = unitEntityData.Parts.Get<UnitPartDollData>().ActiveDoll;
            if (doll == null)
            {
                ModKit.UI.Label("No Doll", GUILayout.Width(200f));
                return;
            }
            ModKit.UI.Label("Indices", GUILayout.Width(200f));
            foreach(var kv in doll.EntityRampIdices)
            {
                var ee = ResourcesLibrary.TryGetResource<EquipmentEntity>(kv.Key);
                ModKit.UI.Label($"{kv.Key} - {ee?.name} - {kv.Value}");
            }
            ModKit.UI.Label("EquipmentEntities", GUILayout.Width(200f));
            foreach (var id in doll.EquipmentEntityIds)
            {
                var ee = ResourcesLibrary.TryGetResource<EquipmentEntity>(id);
                ModKit.UI.Label($"{id} - {ee?.name}");
            }
        }
        static string[] FXIds = new string[] { };
        static int fxIndex = 0;
        static void LoadFxLookup(bool forceReload = false)
        {
            var filepath = $"{Main.ModEntry.Path}/fxlookup.txt";
            if (File.Exists(filepath) && !forceReload)
            {
                FXIds = File
                    .ReadAllLines($"{Main.ModEntry.Path}/fxlookup.txt")
                    .Where(id => LibraryThing.GetResourceGuidMap().ContainsKey(id))
                    .ToArray();
               // var asd = ;
            } else { 
                var idList = new List<string>();
                foreach (var kv in LibraryThing.GetResourceGuidMap())
                {
                    Main.logger.Log(kv.ToString());
                    var obj = ResourcesLibrary.TryGetResource<UnityEngine.Object>(kv.Key);
                    var go = obj as GameObject;
                    if (go != null && go.GetComponent<PooledFx>() != null)
                    {
                        idList.Add(kv.Key);
                    }
                    ResourcesLibrary.CleanupLoadedCache();
                }
                FXIds = idList
                    .OrderBy(id => LibraryThing.GetResourceGuidMap()[id])
                    .ToArray();
                File.WriteAllLines(filepath, FXIds);
            }
        }
        //Refer FxHelper.SpawnFxOnGameObject
        static void ShowFxInfo(UnitEntityData unitEntityData)
        {
            //Choose FX
            ModKit.UI.Label($"Choose FX {FXIds.Length} available", GUILayout.Width(200f));
            if(FXIds.Length == 0) LoadFxLookup();
            GUILayout.BeginHorizontal();
            fxIndex = (int)GUILayout.HorizontalSlider(fxIndex, 0, FXIds.Length - 1, GUILayout.Width(300));
            if (GUILayout.Button("Prev", GUILayout.Width(45)))
            {
                fxIndex = fxIndex == 0 ? 0 : fxIndex - 1;
            }
            if (GUILayout.Button("Next", GUILayout.Width(45)))
            {
                fxIndex = fxIndex >= FXIds.Length - 1 ? FXIds.Length - 1 : fxIndex + 1;
            }
            var fxId = FXIds[fxIndex];
            ModKit.UI.Label($"{LibraryThing.GetResourceGuidMap()[fxId]} {FXIds[fxIndex]}");
            if (GUILayout.Button("Apply", GUILayout.Width(200)))
            {
                var prefab = ResourcesLibrary.TryGetResource<GameObject>(fxId);
                FxHelper.SpawnFxOnUnit(prefab, unitEntityData.View);
            }
            if (GUILayout.Button("Clear FX Cache", GUILayout.Width(200)))
            {
                LoadFxLookup(forceReload: true);
            }
            GUILayout.EndHorizontal();
            //List of SpawnFxOnStart
            var spawnOnStart = unitEntityData.View.GetComponent<SpawnFxOnStart>();
            if (spawnOnStart)
            {
                ModKit.UI.Label("Spawn on Start", GUILayout.Width(200f));
                ModKit.UI.Label("FxOnStart " + spawnOnStart.FxOnStart?.Load()?.name, GUILayout.Width(400));
                ModKit.UI.Label("FXFxOnDeath " + spawnOnStart.FxOnStart?.Load()?.name, GUILayout.Width(400));
            }
            ModKit.UI.Label("Decals");
            var decals = Traverse.Create(unitEntityData.View).Field("m_Decals").GetValue<List<FxDecal>>();
            for (int i = decals.Count - 1; i >= 0; i--)
            {
                var decal = decals[i];
                ModKit.UI.Label("Decal: " + decal.name, GUILayout.Width(400));
                if (GUILayout.Button("Destroy", GUILayout.Width(200f)))
                {
                    GameObject.Destroy(decal.gameObject);
                    decals.RemoveAt(i);
                }
            }
            ModKit.UI.Label("CustomWeaponEffects", GUILayout.Width(200f));
            var dollroom = Game.Instance.UI.Common.DollRoom;
            foreach(var kv in EffectsManager.WeaponEnchantments)
            {
                ModKit.UI.Label($"{kv.Key.Name} - {kv.Value.Count}");
                foreach(var go in kv.Value)
                {
                    GUILayout.BeginHorizontal();
                    ModKit.UI.Label($"  {go?.name ?? "NULL"}");
                    if (dollroom != null && GUILayout.Button("UnscaleFXTimes", GUILayout.ExpandWidth(false)))
                    {
                        Traverse.Create(dollroom).Method("UnscaleFxTimes", new object[] { go }).GetValue();
                    }
                    GUILayout.EndHorizontal();
                }
            }
            ModKit.UI.Label("FXRoot", GUILayout.Width(200f));
            foreach(Transform t in FxHelper.FxRoot.transform)
            {
                var pooledFX = t.gameObject.GetComponent<PooledFx>();
                var snapToLocaters = (List<SnapToLocator>)AccessTools.Field(typeof(PooledFx), "m_SnapToLocators").GetValue(pooledFX);
                var fxBone = snapToLocaters.Select(s => s.Locator).FirstOrDefault();
                UnitEntityView unit = null;
                if (fxBone != null)
                {
                    var viewTransform = fxBone.Transform;
                    while (viewTransform != null && unit == null)
                    {
                        unit = viewTransform.GetComponent<UnitEntityView>();
                        if (unit == null)
                        {
                            viewTransform = viewTransform.parent;
                        }
                    }
                }
                GUILayout.BeginHorizontal();
                if (unit != null)
                {
                    ModKit.UI.Label($"{pooledFX.name} - {unit.EntityData.CharacterName} - {unit.name}", GUILayout.Width(200f));
                } else
                {
                    ModKit.UI.Label($"{pooledFX.name}", GUILayout.Width(200f));
                }
                if(GUILayout.Button("DestroyFX", GUILayout.Width(200))){
                    FxHelper.Destroy(t.gameObject);
                }
                GUILayout.EndHorizontal();
            }
        }
    }
}
