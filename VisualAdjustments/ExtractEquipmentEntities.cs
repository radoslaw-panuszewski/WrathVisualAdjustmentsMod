using JetBrains.Annotations;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items;
using Kingmaker.Items.Slots;
using Kingmaker.View;
using Kingmaker.Visual.CharacterSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
///
/// This was removed with patch 0.8.1 for some reason. 
///
namespace VisualAdjustments
{
	using static Kingmaker.View.UnitEntityView;
    static class ExtractEquipmentEntity
    {
		static private IEnumerable<EquipmentEntity> ExtractEquipmentEntities(this UnitEntityView view, [JetBrains.Annotations.CanBeNull] ItemEntity item)
		{
			try
			{
				if (item == null || item.Blueprint == null)
				{
					return Enumerable.Empty<EquipmentEntity>();
				}
				Gender gender = view.EntityData.Gender;
				BlueprintRace race = view.EntityData.Descriptor.Progression.Race;
				if (race == null) return Enumerable.Empty<EquipmentEntity>();
				Race race2 = (race != null) ? race.RaceId : Race.Human;
				if (item.Blueprint == null) return new List<EquipmentEntity>();
				BlueprintItemEquipment blueprintItemEquipment = item.Blueprint as BlueprintItemEquipment;
				if ((blueprintItemEquipment == null) || (blueprintItemEquipment.m_EquipmentEntity == null) || (blueprintItemEquipment.EquipmentEntity == null))
				{
					return Enumerable.Empty<EquipmentEntity>();
				}
				var list = new List<EquipmentEntity>();
				try
                {
				//	Main.logger.Log(blueprintItemEquipment.ToString());
					//Main.logger.Log(blueprintItemEquipment.EquipmentEntity.ToString());
					foreach( var ee in blueprintItemEquipment?.EquipmentEntity?.Load(gender, race2))
                    {
						list.Add(ee);
                    }
					return list;
				}
				catch(Exception e)
                {
					Main.logger.Log(e.ToString());// (( + " you know what this means");
					return list;
                }
			//	return blueprintItemEquipment.EquipmentEntity?.Load(gender, race2);
			}
			catch(Exception e)
            {
				Main.logger.Log(e.ToString());
				throw;
            }
		}
		static public IEnumerable<EquipmentEntity> ExtractEquipmentEntities(this UnitEntityView view,ItemSlot slot)
		{
			if (slot.HasItem && slot.Item != null)
			{
				return view.ExtractEquipmentEntities(slot.Item);
			}
			return Enumerable.Empty<EquipmentEntity>();
		}
	}
}
