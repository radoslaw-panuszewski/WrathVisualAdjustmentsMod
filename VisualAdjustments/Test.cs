using System;
using System.Collections.Generic;
using HarmonyLib;
using Kingmaker;
using Kingmaker.Settings;
using Kingmaker.Utility;
using Kingmaker.View.Equipment;
using Kingmaker.Visual.CharacterSystem;
using Owlcat.Runtime.Core.Utils.Locator;
using UnityEngine;

namespace VisualAdjustments
{
	/*[HarmonyPatch(typeof(EquipmentEntity), "RepaintTextures")]
	[HarmonyPatch(new Type[] { typeof(EquipmentEntity.PaintedTextures), typeof(int), typeof(int), typeof(int), typeof(int) })]
	public class patchiee
	{
		public static bool Prefix(EquipmentEntity __instance , EquipmentEntity.PaintedTextures paintedTextures, int primaryRampIndex, int secondaryRampIndex, int specialPrimaryRampIndex, int specialSecondaryRampIndex)
		{
				if (!paintedTextures.CheckNeedRepaint(__instance, primaryRampIndex, secondaryRampIndex, specialPrimaryRampIndex, specialSecondaryRampIndex))
				{
					return false;
				}
				Texture2D primaryRamp = null;
				if (specialPrimaryRampIndex >= 0 && specialPrimaryRampIndex < __instance.SpecialPrimaryRamps.Count)
				{
					primaryRamp = __instance.SpecialPrimaryRamps[specialPrimaryRampIndex];
				}
				else if (primaryRampIndex >= 0 && primaryRampIndex < __instance.PrimaryRamps.Count)
				{
					primaryRamp = __instance.PrimaryRamps[primaryRampIndex];
				}
				Texture2D secondaryRamp = null;
				if (specialSecondaryRampIndex >= 0 && specialSecondaryRampIndex < __instance.SpecialSecondaryRamps.Count)
				{
					secondaryRamp = __instance.SpecialPrimaryRamps[specialSecondaryRampIndex];
				}
				else if (secondaryRampIndex >= 0 && secondaryRampIndex < __instance.SecondaryRamps.Count)
				{
					secondaryRamp = __instance.SecondaryRamps[secondaryRampIndex];
				}
				foreach (BodyPart bodyPart in __instance.BodyParts)
				{
					foreach (CharacterTextureDescription characterTextureDescription in bodyPart.Textures)
					{
						RenderTexture rt = paintedTextures.Get(characterTextureDescription);
						characterTextureDescription.Repaint(ref rt, primaryRamp, secondaryRamp);
						paintedTextures.Add(characterTextureDescription, rt);
					}
				}
			return false;
		}
	}*/
}
