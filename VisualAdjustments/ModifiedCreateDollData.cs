using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;

namespace VisualAdjustments
{
    public static class ModifiedCreateDollData
    {
        		[NotNull]
		public static DollData CreateDataModified(this DollState doll)
		{
            if (doll == null)
            {
				Main.logger.Error("nulldoll");
                throw new Exception("null doll");
            }

            
			DollData dollData = new DollData
			{
				Gender = doll.Gender,
				RacePreset = doll.RacePreset
				
			};
            if (doll.Head.m_Entity != null)
			{
				dollData.EquipmentEntityIds.Add(doll.Head.AssetId);
			}
			if (doll.Eyebrows.m_Entity != null)
			{
				dollData.EquipmentEntityIds.Add(doll.Eyebrows.AssetId);
			}
			if (doll.Hair.m_Entity != null)
			{
				dollData.EquipmentEntityIds.Add(doll.Hair.AssetId);
				if (doll.HairRampIndex >= 0)
				{
					dollData.EntityRampIdices[doll.Hair.AssetId] = doll.HairRampIndex;
				}
			}
			if (doll.Beard.m_Entity != null)
			{
				dollData.EquipmentEntityIds.Add(doll.Beard.AssetId);
			}
			if (doll.Horn.m_Entity != null)
			{
				dollData.EquipmentEntityIds.Add(doll.Horn.AssetId);
			}
			if (doll.Warpaint.m_Entity != null)
			{
				dollData.EquipmentEntityIds.Add(doll.Warpaint.AssetId);
				if (doll.WarpaintRampIndex >= 0)
				{
					dollData.EntityRampIdices[doll.Warpaint.AssetId] = doll.WarpaintRampIndex;
				}
			}
			if (doll.Scar.m_Entity != null)
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
			if (doll.HornsRampIndex >= 0 && doll.Horn.m_Entity != null)
			{
				dollData.EntityRampIdices[doll.Horn.AssetId] = doll.HornsRampIndex;
			}
			dollData.ClothesPrimaryIndex = doll.EquipmentRampIndex;
			dollData.ClothesSecondaryIndex = doll.EquipmentRampIndexSecondary;
			return dollData;
		}
    }
}
