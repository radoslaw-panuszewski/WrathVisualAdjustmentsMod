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
			if (item == null)
			{
				return Enumerable.Empty<EquipmentEntity>();
			}
			Gender gender = view.EntityData.Gender;
			BlueprintRace race = view.EntityData.Descriptor.Progression.Race;
			Race race2 = (race != null) ? race.RaceId : Race.Human;
			BlueprintItemEquipment blueprintItemEquipment = item.Blueprint as BlueprintItemEquipment;
			if (!(blueprintItemEquipment != null) || !(blueprintItemEquipment.EquipmentEntity != null))
			{
				return Enumerable.Empty<EquipmentEntity>();
			}
			return blueprintItemEquipment.EquipmentEntity.Load(gender, race2);
		}
		static public IEnumerable<EquipmentEntity> ExtractEquipmentEntities(this UnitEntityView view,ItemSlot slot)
		{
			if (slot.HasItem)
			{
				return view.ExtractEquipmentEntities(slot.Item);
			}
			return Enumerable.Empty<EquipmentEntity>();
		}
	}
}
