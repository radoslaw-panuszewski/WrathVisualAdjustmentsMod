using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using static Kingmaker.UI.Common.ItemsFilter;
using Kingmaker.BundlesLoading;
using UnityEngine;
using Kingmaker.Cheats;
using Kingmaker.Visual.CharacterSystem;

namespace VisualAdjustments
{
        // Token: 0x02000013 RID: 19
        internal static class LibraryThing
        {
            // Token: 0x06000054 RID: 84 RVA: 0x000039D8 File Offset: 0x00001BD8
            public static Dictionary<string, string> GetResourceGuidMap()
            {
                LocationList locationList = (LocationList)HarmonyLib.AccessTools.Field(typeof(BundlesLoadService), "m_LocationList").GetValue(BundlesLoadService.Instance);
                return locationList.GuidToBundle;
            }
        }
    internal static class BluePrintThing
    {
        // Token: 0x06000053 RID: 83 RVA: 0x000039A0 File Offset: 0x00001BA0
        public static TBlueprint[] GetBlueprints<TBlueprint>() where TBlueprint : BlueprintScriptableObject
        {
          return  Utilities.GetScriptableObjects<TBlueprint>().ToArray();
        }
    }
    public class EquipmentResourcesManager
    {
        public static Dictionary<BlueprintRef, string> Helm
        {
            get
            {
                if (!loaded) Init();
                return m_Helm;
            }
        }
        public static Dictionary<BlueprintRef, string> Glasses {
            get {
                if (!loaded) Init();
                return m_Glasses;
            }
        }
        public static Dictionary<BlueprintRef, string> Shirt 
            {
            get
            {
                if (!loaded) Init();
                return m_Shirt;
            }
        }
        public static Dictionary<BlueprintRef, string> Armor
        {
            get
            {
                if (!loaded) Init();
                return m_Armor;
            }
        }
        public static Dictionary<BlueprintRef, string> Bracers
        {
            get
            {
                if (!loaded) Init();
                return m_Bracers;
            }
        }
        public static Dictionary<BlueprintRef, string> Gloves
        {
            get
            {
                if (!loaded) Init();
                return m_Gloves;
            }
        }
        public static Dictionary<BlueprintRef, string> Boots
        {
            get
            {
                if (!loaded) Init();
                return m_Boots;
            }
        }
        public static Dictionary<ResourceRef, string> Units
        {
            get
            {
                if (!loaded) Init();
                return m_Units; ;
            }
        }
        public static Dictionary<string, Dictionary<BlueprintRef, string>> Weapons
        {
            get
            {
                if (!loaded) Init();
                return m_Weapons;
            }
        }
        public static Dictionary<BlueprintRef, string> WeaponEnchantments
        {
            get
            {
                if (!loaded) Init();
                return m_WeaponEnchantments;
            }
        }
        public static Dictionary<ResourceRef, string> Tattoos
        {
            get
            {
                if(m_Tattoo.Count == 0)
                {
                    m_Tattoo["326c1affb2a6a26489921bf588f717b6"] = "EE_KineticistTattooWind_U";
                    m_Tattoo["23b9e367a73b5534d918675405de5aa0"] = "EE_KineticistTattooEarth_U";
                    m_Tattoo["c4aee0b105e3e7e45994f4d8619a5974"] = "EE_KineticistTattooFire_U";
                    m_Tattoo["5dcf740907a3ec94bb4deeac33f0c2b3"] = "EE_KineticistTattooWater_U";
                }
                return m_Tattoo;
            }
        }
        private static Dictionary<BlueprintRef, string> m_Helm = new Dictionary<BlueprintRef, string>();
        private static Dictionary<BlueprintRef, string> m_Shirt = new Dictionary<BlueprintRef, string>();
        private static Dictionary<BlueprintRef, string> m_Glasses = new Dictionary<BlueprintRef, string>();
        private static Dictionary<BlueprintRef, string> m_Armor = new Dictionary<BlueprintRef, string>();
        private static Dictionary<BlueprintRef, string> m_Bracers = new Dictionary<BlueprintRef, string>();
        private static Dictionary<BlueprintRef, string> m_Gloves = new Dictionary<BlueprintRef, string>();
        private static Dictionary<BlueprintRef, string> m_Boots = new Dictionary<BlueprintRef, string>();
        private static Dictionary<ResourceRef, string> m_Tattoo = new Dictionary<ResourceRef, string>();
        private static Dictionary<ResourceRef, string> m_Units = new Dictionary<ResourceRef, string>();
        private static Dictionary<BlueprintRef, string> m_WeaponEnchantments = new Dictionary<BlueprintRef, string>();
        private static Dictionary<string, Dictionary<BlueprintRef, string>> m_Weapons = new Dictionary<string, Dictionary<BlueprintRef, string>>();
        private static bool loaded = false;
        static void BuildEquipmentLookup()
        {
            var blueprints = Utilities.GetScriptableObjects<KingmakerEquipmentEntity>().OrderBy(bp => bp.name);
            /// var blueprints = BluePrintThing.GetBlueprints<BlueprintItemEquipment>()
            foreach (var bp in blueprints)
            {
                if(bp.name.Contains("Goggles"))
                {
                    if (!m_Glasses.ContainsKey(bp.AssetGuid))
                    m_Glasses[bp.AssetGuid] = bp.name;
                }
                else if (bp.name.Contains("Helmet"))
                {
                    if (!m_Helm.ContainsKey(bp.AssetGuid))
                        m_Helm[bp.AssetGuid] = bp.name;
                }
                else if (bp.name.Contains("Shirt") || bp.name.Contains("Robe")||bp.name.Contains("Tabard"))
                {
                    if (!m_Shirt.ContainsKey(bp.AssetGuid))
                        m_Shirt[bp.AssetGuid] = bp.name;
                }
                else if (bp.name.Contains("Armor"))
                {
                    if (!m_Armor.ContainsKey(bp.AssetGuid))
                        m_Armor[bp.AssetGuid] = bp.name;
                    if (!m_Glasses.ContainsKey(bp.AssetGuid))
                        m_Glasses[bp.AssetGuid] = bp.name;
                }
                else if (bp.name.Contains("Bracers"))
                {
                    if (!m_Bracers.ContainsKey(bp.AssetGuid))
                        m_Bracers[bp.AssetGuid] = bp.name;
                }
                else if (bp.name.Contains("Gloves"))
                {
                    if (!m_Gloves.ContainsKey(bp.AssetGuid))
                        m_Gloves[bp.AssetGuid] = bp.name;
                }
                else if (bp.name.Contains("Boots"))
                {
                    if (!m_Boots.ContainsKey(bp.AssetGuid))
                        m_Boots[bp.AssetGuid] = bp.name;
                }
            }
        }
            static void BuildEquipmentLookupOld()
            {
            var bp2 = Utilities.GetScriptableObjects<KingmakerEquipmentEntity>().OrderBy(bp => bp.name);
           /// var blueprints = BluePrintThing.GetBlueprints<BlueprintItemEquipment>()
           var blueprints = Utilities.GetScriptableObjects<BlueprintItemEquipment>()
                .Where(bp => bp.EquipmentEntity != null)
                .OrderBy(bp => bp.EquipmentEntity.name);
            foreach (var bp in blueprints)
            {
                switch (bp.ItemType)
                {
                    case ItemType.Glasses:
                        if (m_Helm.ContainsKey(bp.EquipmentEntity.AssetGuid)) break;
                        m_Glasses[bp.EquipmentEntity.AssetGuid] = bp.EquipmentEntity.name;
                        break;
                    case ItemType.Head:
                        if (m_Helm.ContainsKey(bp.EquipmentEntity.AssetGuid)) break;
                        m_Helm[bp.EquipmentEntity.AssetGuid] = bp.EquipmentEntity.name;
                        break;
                    case ItemType.Shirt:
                        if (m_Shirt.ContainsKey(bp.EquipmentEntity.AssetGuid)) break;
                        m_Shirt[bp.EquipmentEntity.AssetGuid] = bp.EquipmentEntity.name;
                        break;
                    case ItemType.Armor:
                        if (m_Armor.ContainsKey(bp.EquipmentEntity.AssetGuid)) break;
                        m_Armor[bp.EquipmentEntity.AssetGuid] = bp.EquipmentEntity.name;
                        break;
                    case ItemType.Wrist:
                        if (m_Bracers.ContainsKey(bp.EquipmentEntity.AssetGuid)) break;
                        m_Bracers[bp.EquipmentEntity.AssetGuid] = bp.EquipmentEntity.name;
                        break;
                    case ItemType.Gloves:
                        if (m_Gloves.ContainsKey(bp.EquipmentEntity.AssetGuid)) break;
                        m_Gloves[bp.EquipmentEntity.AssetGuid] = bp.EquipmentEntity.name;
                        break;
                    case ItemType.Feet:
                        if (m_Boots.ContainsKey(bp.EquipmentEntity.AssetGuid)) break;
                        m_Boots[bp.EquipmentEntity.AssetGuid] = bp.EquipmentEntity.name;
                        break;
                    default:
                        break;
                }
            }
        }
        static void BuildWeaponLookup()
        {
            ///var weapons = BluePrintThing.GetBlueprints<BlueprintItemEquipmentHand>().OrderBy((bp) => bp.name);
            var weapons = Utilities.GetScriptableObjects<BlueprintItemEquipmentHand>().OrderBy((bp) => bp.name);
            foreach (var bp in weapons)
            {
                var visualParameters = bp.VisualParameters;
                var animationStyle = visualParameters.AnimStyle.ToString();
                if (bp.VisualParameters.Model == null) continue;
                Dictionary<BlueprintRef, string> eeList = null;
                if (!m_Weapons.ContainsKey(animationStyle))
                {
                    eeList = new Dictionary<BlueprintRef, string>();
                    m_Weapons[animationStyle] = eeList;
                }
                else
                {
                    eeList = m_Weapons[animationStyle];
                }
                if (eeList.ContainsKey(bp.AssetGuid))
                {
                    continue;
                }
                eeList[bp.AssetGuid] = bp.name;
            }
        }
        static void BuildWeaponEnchantmentLookup()
        {
            ///var enchantments = BluePrintThing.GetBlueprints<BlueprintWeaponEnchantment>()
            var enchantments = Utilities.GetScriptableObjects<BlueprintWeaponEnchantment>()
                    .Where(bp => bp.WeaponFxPrefab != null)
                    .OrderBy(bp => bp.WeaponFxPrefab.name);
            HashSet<int> seen = new HashSet<int>();
            foreach(var enchantment in enchantments)
            {
                if (seen.Contains(enchantment.WeaponFxPrefab.GetInstanceID())) continue;
                seen.Add(enchantment.WeaponFxPrefab.GetInstanceID());
                var name = enchantment.WeaponFxPrefab.name.Replace("00_WeaponBuff", "");
                name = name.TrimEnd('_');
                m_WeaponEnchantments[enchantment.AssetGuid] = name;
            }
        }
        static void BuildViewLookup()
        {
            string getViewName(BlueprintUnit bp)
            {
                return bp.NameForAcronym;
                if (!LibraryThing.GetResourceGuidMap().ContainsKey(bp.Prefab.AssetId)) return "NULL";
                var path = LibraryThing.GetResourceGuidMap()[bp.Prefab.AssetId].Split('/');
                return path[path.Length - 1];
            }
            var units = Utilities.GetScriptableObjects<BlueprintUnit>().OrderBy(getViewName);
            ///var units = BluePrintThing.GetBlueprints<BlueprintUnit>().OrderBy(getViewName);
            foreach (var bp in units)
            {
                if (bp.Prefab.AssetId == "") continue;
                if (!LibraryThing.GetResourceGuidMap().ContainsKey(bp.Prefab.AssetId)) continue;             
                if (m_Units.ContainsKey(bp.Prefab.AssetId)) continue;
                m_Units[bp.Prefab.AssetId] = getViewName(bp);
            }
        }
        static void Init()
        {
            BuildEquipmentLookup();
            BuildWeaponLookup();
            BuildWeaponEnchantmentLookup();
            BuildViewLookup();
            loaded = true;
        }
    }
}
