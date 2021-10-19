using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.CharGen;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Root;
using Kingmaker.Cheats;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Visual.Sound;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.Visual.CharacterSystem;
using Kingmaker.Utility;

namespace VisualAdjustments
{

    static class dollstateextension
    {
        public static void SetupFromUnitLocal(this DollState state ,UnitEntityData unit, SpecialDollType specialDollType = SpecialDollType.None)
        {
            DollData dollData;
            if (specialDollType == SpecialDollType.None)
            {
                UnitPartDollData unitPartDollData = unit.Get<UnitPartDollData>();
                dollData = ((unitPartDollData != null) ? unitPartDollData.Default : null);
            }
            else
            {
                UnitPartDollData unitPartDollData2 = unit.Get<UnitPartDollData>();
                dollData = ((unitPartDollData2 != null) ? unitPartDollData2.GetSpecial(specialDollType) : null);
            }
            DollData dollData2 = dollData;
            if (dollData2 == null)
            {
                return;
            }
            state.Gender = dollData2.Gender;
            state.Race = Kingmaker.Game.Instance.BlueprintRoot.Progression.CharacterRaces.First(a => a.Presets.Contains(dollData2.RacePreset));
            state.SetRacePreset(dollData2.RacePreset);
            state.SetRace(state.Race);
            state.SetRacePreset(dollData2.RacePreset);
            state.Scars = DollState.GetScarsList(state.Race.RaceId);
            //state.RacePreset = dollData2.RacePreset;
            ClassData classData = unit.Progression.Classes.FirstOrDefault<ClassData>();
            state.CharacterClass = ((classData != null) ? classData.CharacterClass : null);
            foreach (ClassData classData2 in unit.Progression.Classes)
            {
                state.m_EquipmentIndex.Add(classData2.CharacterClass, new DollState.EquipmentIndexByClass
                {
                    EquipmentRampIndex = classData2.CharacterClass.PrimaryColor,
                    EquipmentRampIndexSecondary = classData2.CharacterClass.SecondaryColor
                });
            }
            CustomizationOptions customizationOptions = (state.Gender == Gender.Male) ? state.Race.MaleOptions : state.Race.FemaleOptions;
            using (List<EquipmentEntity>.Enumerator enumerator2 = unit.View.CharacterAvatar.EquipmentEntities.GetEnumerator())
            {
                while (enumerator2.MoveNext())
                {
                    EquipmentEntity ee = enumerator2.Current;
                    if (customizationOptions.Heads.Contains((EquipmentEntityLink link) => link.Load(false) == ee))
                    {
                        state.Head = new DollState.EEAdapter(ee);
                    }
                    if (customizationOptions.Eyebrows.Contains((EquipmentEntityLink link) => link.Load(false) == ee))
                    {
                        state.Eyebrows = new DollState.EEAdapter(ee);
                    }
                    if (customizationOptions.Hair.Contains((EquipmentEntityLink link) => link.Load(false) == ee))
                    //if (ee.name.Contains("Hair") || ee.name.Contains("hair"))
                    {
                        state.Hair = new DollState.EEAdapter(ee);
                    }
                    if (customizationOptions.Beards.Contains((EquipmentEntityLink link) => link.Load(false) == ee))
                    {
                        state.Beard = new DollState.EEAdapter(ee);
                    }
                    if (customizationOptions.Horns.Contains((EquipmentEntityLink link) => link.Load(false) == ee))
                    {
                        state.Horn = new DollState.EEAdapter(ee);
                    }
                    if (state.Scars.Contains((EquipmentEntityLink link) => link.Load(false) == ee))
                    {
                        var NewEEAdapter = new DollState.EEAdapter(ee);
                        NewEEAdapter.m_Link = DollState.GetScarsList(state.Race.RaceId).FirstOrDefault((EquipmentEntityLink link2) => link2.Load(false) == ee);
                        state.Scar = NewEEAdapter;
                    }
                    if (state.Warpaints.Contains((EquipmentEntityLink link) => link.Load(false) == ee))
                    {
                        var NewEEAdapter = new DollState.EEAdapter(ee);
                        NewEEAdapter.m_Link = state.Warpaints.FirstOrDefault((EquipmentEntityLink link2) => link2.Load(false) == ee);
                        state.Warpaint = NewEEAdapter;
                       // state.Warpaint = new DollState.EEAdapter(ee);
                    }
                }
            }
            List<EquipmentEntityLink> clothes;
            if (state.CharacterClass == null)
            {
                clothes = ((state.Gender == Gender.Male) ? DollState.Root.MaleClothes.ToList<EquipmentEntityLink>() : DollState.Root.FemaleClothes.ToList<EquipmentEntityLink>());
            }
            else
            {
                BlueprintCharacterClass characterClass = state.CharacterClass;
                Gender gender = state.Gender;
                BlueprintRace race = state.Race;
                clothes = characterClass.GetClothesLinks(gender, (race != null) ? race.RaceId : Kingmaker.Blueprints.Race.Human);
            }
            state.Clothes = clothes;
            state.Warpaints = BlueprintRoot.Instance.CharGen.WarpaintsForCustomization.ToList<EquipmentEntityLink>();
            BlueprintRace race2 = state.Race;
            state.Scars = DollState.GetScarsList((race2 != null) ? race2.RaceId : Kingmaker.Blueprints.Race.Human);
            if (state.Hair.m_Entity != null)
            {
                state.HairRampIndex = unit.View.CharacterAvatar.GetPrimaryRampIndex(state.Hair.Load());
            }
            DollState.EEAdapter eeadapter = state.GetSkinEntities().FirstOrDefault<DollState.EEAdapter>();
            if (eeadapter.m_Entity != null)
            {
                state.SkinRampIndex = unit.View.CharacterAvatar.GetPrimaryRampIndex(eeadapter.Load());
            }
            if (state.Horn.m_Entity != null)
            {
                state.HornsRampIndex = unit.View.CharacterAvatar.GetPrimaryRampIndex(state.Horn.Load());
            }
            if (state.Warpaint.m_Entity != null)
            {
                state.WarpaintRampIndex = unit.View.CharacterAvatar.GetPrimaryRampIndex(state.Warpaint.Load());
            }
            state.m_TrackPortrait = false;
            state.UpdateMechanicsEntities(unit.Descriptor);
            state.EquipmentRampIndex = dollData2.ClothesPrimaryIndex;
            state.EquipmentRampIndexSecondary = dollData2.ClothesSecondaryIndex;
            state.Updated();
        }
    }
    class DollResourcesManager
    {
        static private SortedList<string, EquipmentEntityLink> head = new SortedList<string, EquipmentEntityLink>();
        static public SortedList<string, EquipmentEntityLink> hair = new SortedList<string, EquipmentEntityLink>();
        static private SortedList<string, EquipmentEntityLink> beard = new SortedList<string, EquipmentEntityLink>();
        static private SortedList<string, EquipmentEntityLink> eyebrows = new SortedList<string, EquipmentEntityLink>();
        static private SortedList<string, EquipmentEntityLink> skin = new SortedList<string, EquipmentEntityLink>();
        static private SortedList<string, EquipmentEntityLink> tails = new SortedList<string, EquipmentEntityLink>();
        static private SortedList<string, EquipmentEntityLink> horns = new SortedList<string, EquipmentEntityLink>();
        static private SortedList<string, EquipmentEntityLink> classOutfits = new SortedList<string, EquipmentEntityLink>();
        static private SortedList<string, BlueprintPortrait> portraits = new SortedList<string, BlueprintPortrait>();
        static private SortedList<string, BlueprintUnitAsksList> asks = new SortedList<string, BlueprintUnitAsksList>();
        static private Dictionary<string, DollState> characterDolls = new Dictionary<string, DollState>();
        static private List<string> customPortraits = new List<string>();
        static public List<string> CustomPortraits
        {
            get
            {
                if (!loaded) Init();
                return customPortraits;
            }
        }
        static public SortedList<string, BlueprintPortrait> Portrait
        {
            get
            {
                if (!loaded) Init();
                return portraits;
            }
        }
        static public SortedList<string, BlueprintUnitAsksList> Asks
        {
            get
            {
                if(!loaded) Init();
                return asks;
            }
        }
        static public SortedList<string, EquipmentEntityLink> ClassOutfits
        {
            get
            {
                if (!loaded) Init();
                return classOutfits;
            }
        }
        static private bool loaded = false;
        static private void AddLinks(SortedList<string, EquipmentEntityLink> dict, EquipmentEntityLink[] links)
        {
            foreach (var eel in links)
            {
                dict[eel.AssetId] = eel;
            }
        }
        public static List<BlueprintRaceVisualPreset> racePresets;
        public static List<BlueprintCharacterClass> classes 
        { 
            get
            {
                if(m_classes == null) m_classes = Main.blueprints.Entries.Where(a => a.Type == typeof(BlueprintCharacterClass)).Select(b => ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>(b.Guid)).ToList();
                return m_classes;
            }
            set
            {
                m_classes = value;
            }
        }
        public static List<BlueprintCharacterClass> m_classes;
        static private void Init()
        {
            var races = Main.blueprints.Entries.Where(a => a.Type == typeof(BlueprintRace)).Select(b => ResourcesLibrary.TryGetBlueprint<BlueprintRace>(b.Guid));
            racePresets = Main.blueprints.Entries.Where(a => a.Type == typeof(BlueprintRaceVisualPreset)).Select(b => ResourcesLibrary.TryGetBlueprint<BlueprintRaceVisualPreset>(b.Guid)).ToList();
            classes = Main.blueprints.Entries.Where(a => a.Type == typeof(BlueprintCharacterClass)).Select(b => ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>(b.Guid)).ToList();
            /* var races = BluePrintThing.GetBlueprints<BlueprintRace>();
             var racePresets = BluePrintThing.GetBlueprints<BlueprintRaceVisualPreset>();
             var classes = BluePrintThing.GetBlueprints<BlueprintCharacterClass>();*/
            foreach (var race in races)
            {
                foreach (var gender in new Gender[] { Gender.Male, Gender.Female })
                {
                    CustomizationOptions customizationOptions = gender != Gender.Male ? race.FemaleOptions : race.MaleOptions;
                    AddLinks(head, customizationOptions.Heads);
                    AddLinks(hair, customizationOptions.Hair);
                    AddLinks(beard, customizationOptions.Beards);
                    AddLinks(eyebrows, customizationOptions.Eyebrows);
                    AddLinks(tails, customizationOptions.TailSkinColors);
                    AddLinks(horns, customizationOptions.Horns);
                }
            }
            foreach (var racePreset in racePresets)
            {
                foreach (var gender in new Gender[] { Gender.Male, Gender.Female })
                {
                    var raceSkin = racePreset.Skin;
                    if (raceSkin == null) continue;
                    AddLinks(skin, raceSkin.GetLinks(gender, racePreset.RaceId));
                }
            }
            foreach (var _class in classes)
            {
                foreach (var race in races)
                {
                    foreach (var gender in new Gender[] { Gender.Male, Gender.Female })
                    {
                        AddLinks(classOutfits, _class.GetClothesLinks(gender, race.RaceId).ToArray());
                    }
                }
            }
            ///foreach(var bp in BluePrintThing.GetBlueprints<BlueprintPortrait>())
            foreach(var bp in Main.blueprints.Entries.Where(a => a.Type == typeof(BlueprintPortrait)).Select(a => ResourcesLibrary.TryGetBlueprint<BlueprintPortrait>(a.Guid)))
            {
                //Note there are two wolf portraits
                if (bp == BlueprintRoot.Instance.CharGen.CustomPortrait || bp.Data.IsCustom)
                {
                    continue;
                }
                if (!portraits.ContainsKey(bp.name)) portraits.Add(bp.name, bp);
            }
            customPortraits.AddRange(CustomPortraitsManager.Instance.GetExistingCustomPortraitIds());
            ///foreach (var bp in BluePrintThing.GetBlueprints<BlueprintUnitAsksList>())
            foreach(var bp in Main.blueprints.Entries.Where(a => a.Type == typeof(BlueprintUnitAsksList)).Select(a => ResourcesLibrary.TryGetBlueprint<BlueprintUnitAsksList>(a.Guid)))
            {
                var component = bp.GetComponent<UnitAsksComponent>();
                if (component == null) continue;
                if (!component.Selected.HasBarks && bp.name != "PC_None_Barks") continue;
                asks.Add(bp.name, bp);
            }
            loaded = true;
        }
        static public DollState GetDoll(UnitEntityData unitEntityData)
        {
            if (!loaded) Init();
            if (unitEntityData.Parts.Get<UnitPartDollData>() == null) return null;
            var doll = unitEntityData.Parts.Get<UnitPartDollData>().Default;
            if (doll == null) return null;
            if (!characterDolls.ContainsKey(unitEntityData.CharacterName))
            {
                characterDolls[unitEntityData.CharacterName] = CreateDollState(unitEntityData);
            }
            return characterDolls[unitEntityData.CharacterName];
        }
        static public string GetType(string assetID)
        {
            if (!loaded) Init();
            if (head.ContainsKey(assetID)) return "Head";
            if (hair.ContainsKey(assetID)) return "Hair";
            if (beard.ContainsKey(assetID)) return "Beard";
            if (eyebrows.ContainsKey(assetID)) return "Eyebrows";
            if (skin.ContainsKey(assetID)) return "Skin";
            if (horns.ContainsKey(assetID)) return "Horn";
            if (tails.ContainsKey(assetID)) return "Tail";
            if (classOutfits.ContainsKey(assetID)) return "ClassOutfit";
            return "Unknown";
        }
        static public DollState CreateDollState(UnitEntityData unitEntityData)
        {
            var asd = DollResourcesManager.racePresets.FirstOrDefault(a => a.RaceId == unitEntityData.Progression.Race.RaceId);
            //var asd = Main.blueprints.OfType<BlueprintRaceVisualPreset>().ToArray().FirstOrDefault(a => a.RaceId == unitEntityData.Progression.Race.RaceId);
            var dollData = unitEntityData.Parts.Get<UnitPartDollData>().Default;
            var dollState = new DollState
            {
            };
            dollState.SetupFromUnitLocal(unitEntityData);
            //dollState.SetRace(unitEntityData.Progression.Race); //Race must be set before class
            //This is a hack to work around harmony not allowing calls to the unpatched method
            CharacterManager.disableEquipmentClassPatch = true; 
            dollState.SetClass(unitEntityData.Descriptor.Progression.GetEquipmentClass());
            CharacterManager.disableEquipmentClassPatch = false;
            dollState.SetGender(dollData.Gender);
            //dollState.SetRacePreset(asd);
            unitEntityData.Descriptor.LeftHandedOverride = true;

            dollState.SetEquipColors(dollData.ClothesPrimaryIndex, dollData.ClothesSecondaryIndex);
            foreach(var assetID in dollData.EquipmentEntityIds)
            {
                if (head.ContainsKey(assetID))
                {
                    dollState.SetHead(head[assetID]);
                    if (dollData.EntityRampIdices.ContainsKey(assetID))
                    {
                        dollState.SetSkinColor(dollData.EntityRampIdices[assetID]);
                    }
                }
                if (hair.ContainsKey(assetID) && !hair[assetID].Load().name.Contains("EMPTY"))
                {
                   // Main.logger.Log(hair[assetID].Load().name);
                    dollState.SetHair(hair[assetID]);
                    if (dollData.EntityRampIdices.ContainsKey(assetID))
                    {
                       dollState.SetHairColor(dollData.EntityRampIdices[assetID]);
                    }
                }
                if (beard.ContainsKey(assetID))
                {
                    dollState.SetBeard(beard[assetID]);
                    if (dollData.EntityRampIdices.ContainsKey(assetID))
                    {
                        dollState.SetHairColor(dollData.EntityRampIdices[assetID]);
                    }
                }
                if (skin.ContainsKey(assetID))
                {
                    if (dollData.EntityRampIdices.ContainsKey(assetID))
                    {
                        dollState.SetSkinColor(dollData.EntityRampIdices[assetID]);
                    }
                }
                if (horns.ContainsKey(assetID))
                {
                    dollState.SetHorn(horns[assetID]);
                    if (dollData.EntityRampIdices.ContainsKey(assetID))
                    {
                        dollState.SetHornsColor(dollData.EntityRampIdices[assetID]);
                    }
                }
                if (skin.ContainsKey(assetID))
                {
                    if (dollData.EntityRampIdices.ContainsKey(assetID))
                    {
                        dollState.SetSkinColor(dollData.EntityRampIdices[assetID]);
                    }
                }
                if (classOutfits.ContainsKey(assetID))
                {
                    if (dollData.EntityRampIdices.ContainsKey(assetID) &&
                        dollData.EntitySecondaryRampIdices.ContainsKey(assetID))
                    {
                        dollState.SetEquipColors(dollData.EntityRampIdices[assetID], dollData.EntitySecondaryRampIdices[assetID]);
                    }
                }

            }
            dollState.Validate();
            return dollState;
        }
    }

}
