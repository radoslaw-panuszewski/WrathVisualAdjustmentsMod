using HarmonyLib;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UI.MVVM._VM.CharGen;
using Kingmaker.UI.MVVM._VM.CharGen.Phases.Voice;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.UnitLogic.Class.LevelUp.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UI.LevelUp;
using Kingmaker;
using Kingmaker.UI.MVVM._VM.CharGen.Phases.Portrait;
using Kingmaker.Blueprints;
using Kingmaker.Cheats;
using Kingmaker.Enums;
using Owlcat.Runtime.UI;
using JetBrains.Annotations;
using Kingmaker.Blueprints.Classes;

namespace VisualAdjustments
{
	[HarmonyPatch(typeof(CharGenPortraitPhaseVM), MethodType.Constructor)]
	[HarmonyPatch(new Type[] { typeof(LevelUpController) })]
		internal static class Charbportrait_patch
		{
			private static void Postfix(CharGenPortraitPhaseVM __instance)
			{
			try
			{
				if (!Main.settings.AllPortraits) return;
				if(Main.settings.AllPortraits)
				{
					foreach (var bp in Main.blueprints.Entries.Where(a => a.Type == typeof(BlueprintPortrait)).Select(b => ResourcesLibrary.TryGetBlueprint<BlueprintPortrait>(b.Guid)))
					{
						if(bp == null) return;
						if (!bp.InitiativePortrait && bp.Data.SmallPortrait != null && bp.Data.HalfLengthPortrait != null && bp.Data.FullLengthPortrait != null)
						{
							if (bp.AssetGuid.ToString() == null) return;
							//if(bp.AssetGuid.ToString() != "9412ffb857efa074eb8e4945c8b94de4" && bp.AssetGuid.ToString() != "621ada02d0b4bf64387babad3a53067b" && bp.AssetGuid.ToString() != "9fe4f89ecf15b874db9d1d2bf3ef33d2")
							{
								CharGenPortraitSelectorItemVM charGenPortraitSelectorItemVM = new CharGenPortraitSelectorItemVM(bp, false);
								///__instance.m_AllPortraitsCollection.Add(charGenPortraitSelectorItemVM);
								__instance.AllPortraitsCollection.Add(charGenPortraitSelectorItemVM);
								PortraitCategory portraitCategory = bp.Data.PortraitCategory;
								if (!__instance.PortraitGroupVms.ContainsKey(portraitCategory))
								{
									__instance.PortraitGroupVms.Add(portraitCategory, new CharGenPortraitGroupVM(portraitCategory));
									__instance.PortraitGroupVms[portraitCategory].Expanded.Value = (portraitCategory != PortraitCategory.KingmakerNPC);
								}
								if (!__instance.PortraitGroupVms[portraitCategory].PortraitCollection.Any(a => a.m_BlueprintPortrait == bp))
								{
									__instance.PortraitGroupVms[0].PortraitCollection.Add(charGenPortraitSelectorItemVM);
								}
							}
						}
					}
				} 
			}
			catch (Exception e) { Main.logger.Log(e.ToString()); }
			}
		}
}
