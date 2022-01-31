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
                DollData dollData = new DollData();
                dollData.Gender = doll.Gender;
                dollData.RacePreset = doll.RacePreset != null ? doll.RacePreset : doll.Race.Presets.First();
                if (doll.Head.Load() != null)
                {
                    dollData.EquipmentEntityIds.Add(doll.Head.AssetId);
                }
                if (doll.Eyebrows.Load() != null)
                {
                    dollData.EquipmentEntityIds.Add(doll.Eyebrows.AssetId);
                }
                if (doll.Hair.Load() != null)
                {
                    dollData.EquipmentEntityIds.Add(doll.Hair.AssetId);
                    if (doll.HairRampIndex >= 0)
                    {
                        dollData.EntityRampIdices[doll.Hair.AssetId] = doll.HairRampIndex;
                    }
                }
                if (doll.Beard.Load() != null)
                {
                    dollData.EquipmentEntityIds.Add(doll.Beard.AssetId);
                }
                if (doll.Horn.Load() != null)
                {
                    dollData.EquipmentEntityIds.Add(doll.Horn.AssetId);
                }
                foreach (DollState.DollPrint dollPrint in doll.Warprints)
                {
                    if (dollPrint.PaintEE.Load() != null)
                    {
                        dollData.EquipmentEntityIds.Add(dollPrint.PaintEE.AssetId);
                        if (dollPrint.PaintRampIndex >= 0)
                        {
                            dollData.EntityRampIdices[dollPrint.PaintEE.AssetId] = dollPrint.PaintRampIndex;
                        }
                    }
                }
                foreach (DollState.DollPrint dollPrint2 in doll.Tattoos)
                {
                    if (dollPrint2.PaintEE.Load() != null)
                    {
                        dollData.EquipmentEntityIds.Add(dollPrint2.PaintEE.AssetId);
                        if (dollPrint2.PaintRampIndex >= 0)
                        {
                            dollData.EntityRampIdices[dollPrint2.PaintEE.AssetId] = dollPrint2.PaintRampIndex;
                        }
                    }
                }
                if (doll.Scar.Load() != null)
                {
                    dollData.EquipmentEntityIds.Add(doll.Scar.AssetId);
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
                    if (doll.Horn.Load() != null)
                    {
                        dollData.EntitySecondaryRampIdices[doll.Horn.AssetId] = doll.SkinRampIndex;
                    }
                }
                if (doll.HairRampIndex >= 0)
                {
                    foreach (DollState.EEAdapter eeadapter2 in doll.GetHairEntities())
                    {
                        dollData.EntityRampIdices[eeadapter2.AssetId] = doll.HairRampIndex;
                    }
                }
                if (doll.EyesColorRampIndex >= 0)
                {
                    foreach (DollState.EEAdapter eeadapter3 in doll.GetHeadEntities())
                    {
                        dollData.EntitySecondaryRampIdices[eeadapter3.AssetId] = doll.EyesColorRampIndex;
                    }
                }
                if (doll.HornsRampIndex >= 0 && doll.Horn.Load() != null)
                {
                    dollData.EntityRampIdices[doll.Horn.AssetId] = doll.HornsRampIndex;
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