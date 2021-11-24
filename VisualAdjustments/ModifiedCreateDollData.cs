using JetBrains.Annotations;
using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.Utility;
using System;
using System.Linq;

namespace VisualAdjustments
{
    public static class ModifiedCreateDollData
    {
        [NotNull]
        public static DollData CreateDataModified(DollState doll)
        {
            try
            {
                if (doll == null)
                {
                    Main.logger.Error("nulldoll");
                    throw new Exception("null doll");
                }

                /* DollData dollData = new DollData
                 {
                     Gender = doll.Gender,
                     RacePreset = doll.RacePreset
                 };*/
                DollData dollData = new DollData();
                dollData.Gender = doll.Gender;
                dollData.RacePreset = doll.RacePreset != null ? doll.RacePreset : doll.Race.Presets.First();
                if (doll.Head.m_Entity != null && !doll.Head.AssetId.IsNullOrEmpty())
                {
                    dollData.EquipmentEntityIds.Add(doll.Head.AssetId);
                }

                if (doll.Eyebrows.m_Entity != null && !doll.Eyebrows.AssetId.IsNullOrEmpty())
                {
                    dollData.EquipmentEntityIds.Add(doll.Eyebrows.AssetId);
                }

                if (doll.Hair.m_Entity != null && !doll.Hair.AssetId.IsNullOrEmpty())
                {
                    dollData.EquipmentEntityIds.Add(doll.Hair.AssetId);
                    if (doll.HairRampIndex >= 0)
                    {
                        dollData.EntityRampIdices[doll.Hair.AssetId] = doll.HairRampIndex;
                    }
                }

                if (doll.Beard.m_Entity != null && !doll.Beard.AssetId.IsNullOrEmpty())
                {
                    dollData.EquipmentEntityIds.Add(doll.Beard.AssetId);
                }

                if (doll.Horn.m_Entity != null && !doll.Horn.AssetId.IsNullOrEmpty())
                {
                    dollData.EquipmentEntityIds.Add(doll.Horn.AssetId);
                }
                if (doll.Race != null)
                {
                    EquipmentEntityLink tail = doll.Race.GetTail(doll.Gender, doll.SkinRampIndex);
                    if (tail != null)
                    {
                        dollData.EquipmentEntityIds.Add(tail.AssetId);
                    }
                }

                if (doll.SkinRampIndex >= 0)
                {
                    foreach (DollState.EEAdapter eeadapter in doll.GetSkinEntities())
                    {
                        dollData.EntityRampIdices[eeadapter.AssetId] = doll.SkinRampIndex;
                    }

                    if (doll.Horn.m_Entity != null && !doll.Horn.AssetId.IsNullOrEmpty())
                    {
                        dollData.EntitySecondaryRampIdices[doll.Horn.AssetId] = doll.SkinRampIndex;
                    }
                }

                if (doll.HairRampIndex >= 0 && !doll.Hair.AssetId.IsNullOrEmpty())
                {
                    foreach (DollState.EEAdapter eeadapter2 in doll.GetHairEntities())
                    {
                        dollData.EntityRampIdices[eeadapter2.AssetId] = doll.HairRampIndex;
                    }
                }

                if (doll.Horn.m_Entity != null && doll.HornsRampIndex >= 0 && !doll.Horn.AssetId.IsNullOrEmpty())
                {
                    dollData.EntityRampIdices[doll.Horn.AssetId] = doll.HornsRampIndex;
                }

                if (doll.Warpaint.m_Link != null && doll.Warpaint.m_Link.AssetId != null && doll.Warpaint.Load() != null)
                {
                    dollData.EquipmentEntityIds.Add(EquipmentResourcesManager.AllEEL[doll.Warpaint.m_Entity.name]);
                    // Main.logger.Log(EquipmentResourcesManager.AllEEL[doll.Warpaint.m_Entity.name]);
                    //dollData.EquipmentEntityIds.Add(doll.Warpaint.AssetId);
                }
                if (doll.Scar.m_Link != null && doll.Scar.m_Link.AssetId != null && doll.Scar.Load() != null)
                {
                    dollData.EquipmentEntityIds.Add(EquipmentResourcesManager.AllEEL[doll.Scar.m_Entity.name]);
                    // Main.logger.Log(EquipmentResourcesManager.AllEEL[doll.Scar.m_Entity.name]);
                    //dollData.EquipmentEntityIds.Add(doll.Scar.AssetId);
                }

                dollData.ClothesPrimaryIndex = doll.EquipmentRampIndex;
                dollData.ClothesSecondaryIndex = doll.EquipmentRampIndexSecondary;
                return dollData;
            }
            catch (Exception e)
            {
                Main.logger.Log(e.StackTrace);
                throw;
            }
        }
    }
}