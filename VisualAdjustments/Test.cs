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

	[HarmonyPatch(typeof(Kingmaker.Visual.CharacterSystem.Character), "OnRenderObject")]
	public static class xfddfg
    {
		public static bool Prefix(Character __instance)
        {
			return false;
        }
    }
	//[HarmonyPatch(typeof(EquipmentEntity), "RepaintTextures")]
	//[HarmonyPatch(new Type[] { typeof(EquipmentEntity.PaintedTextures), typeof(int), typeof(int), typeof(int), typeof(int) })]

	public class patchiee
	{
		public static bool Prefix(EquipmentEntity __instance , EquipmentEntity.PaintedTextures paintedTextures, int primaryRampIndex, int secondaryRampIndex, int specialPrimaryRampIndex, int specialSecondaryRampIndex)
		{
			try
			{
				/*if (!paintedTextures.CheckNeedRepaint(__instance, primaryRampIndex, secondaryRampIndex, specialPrimaryRampIndex, specialSecondaryRampIndex))
				{
					//Main.logger.Log("needrepaintfalse");
					//return false;
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
						//RenderTexture rt = paintedTextures.Get(characterTextureDescription);
						RenderTexture rt = null;
						if(rt == null)
                        {
							Main.logger.Log("rtnull");
                        }
						else
                        {
							Main.logger.Log("rtnotnull   "+rt.ToString());
						}
						characterTextureDescription.Repaint(ref rt, primaryRamp, secondaryRamp);

						Main.logger.Log("postrt   " +rt.ToString());
						paintedTextures.Add(characterTextureDescription, rt);
					}
				}*/
				Texture2D primaryRamp = null;
				if (primaryRampIndex >= 0 && primaryRampIndex < __instance.PrimaryRamps.Count)
				{
					primaryRamp = __instance.PrimaryRamps[primaryRampIndex];
				}
				Texture2D secondaryRamp = null;
				if (secondaryRampIndex >= 0 && secondaryRampIndex < __instance.SecondaryRamps.Count)
				{
					secondaryRamp = __instance.SecondaryRamps[secondaryRampIndex];
				}
				foreach (BodyPart bodyPart in __instance.BodyParts)
				{
					foreach (CharacterTextureDescription characterTextureDescription in bodyPart.Textures)
					{
						//RenderTexture rt = paintedTextures.Get(characterTextureDescription);
						RenderTexture rt = new RenderTexture(characterTextureDescription.m_Texture.width,characterTextureDescription.m_Texture.height,0,RenderTextureFormat.ARGB32);
						characterTextureDescription.Repaint(ref rt, primaryRamp, secondaryRamp);
						paintedTextures.Add(characterTextureDescription, rt);
					}
				}
				return false;
			}
			catch(Exception e)
            {
				Main.logger.Error(e.ToString());
				return false;
            }
		}
	}
	//[HarmonyPatch(typeof(CharacterTextureDescription), "Repaint")]
	public static class repaint
	{
		public static bool Prefix(CharacterTextureDescription __instance,ref RenderTexture rtToPaint, Texture2D primaryRamp, Texture2D secondaryRamp)
		{
			try
			{
				Main.logger.Log("-----");
				if (null == __instance.ActiveTexture)
				{
					VisualAdjustments.Main.logger.Warning("Отсутствует текстура в одном из EE");
					//return false;
				}
				if (!__instance.UseRamp1Mask && !__instance.UseRamp2Mask && !__instance.UseDefaultMask1 && !__instance.UseDefaultMask2)
				{
					//Main.logger.Log("Malar");
					//return;
				}
				if (rtToPaint == null)
				{
					rtToPaint = new RenderTexture(__instance.m_Texture.width, __instance.m_Texture.height, 0, RenderTextureFormat.ARGB32);
					rtToPaint.name = __instance.m_Texture.name + "_RT";
					Main.logger.Log("newrt");
				}
				if (CharacterTextureDescription.s_RepaintMaterial == null)
				{
					CharacterTextureDescription.s_RepaintMaterial = new Material(Shader.Find("Hidden/CharacterTextureRepaint"));
					Main.logger.Log("newRepaintMaterial");
				}
				RenderTexture active = RenderTexture.active;
				RenderTexture.active = rtToPaint;
				try
				{
					Graphics.Blit(__instance.m_Texture, rtToPaint);
					if (__instance.UseRamp1Mask || __instance.UseDefaultMask1)
					{
						Main.logger.Log("Mask1");
						Texture2D texture2D;
						if (null == __instance.Ramps.PrimaryRamp)
						{
							texture2D = primaryRamp;
							Main.logger.Log("primrampnull");
						}
						else
						{
							texture2D = __instance.Ramps.PrimaryRamp;
							Main.logger.Log("primrampnotnull");
						}
						if (texture2D != null)
						{
							Main.logger.Log("tex2dnotnull");
							if (__instance.UseDefaultMask1 && null != __instance.DefaultMask1)
							{
								CharacterTextureDescription.s_RepaintMaterial.SetTexture("_Mask", __instance.DefaultMask1);
								Main.logger.Log("defaultmask");
							}
							if (__instance.UseRamp1Mask && null != __instance.RampShadowTexture)
							{
								CharacterTextureDescription.s_RepaintMaterial.SetTexture("_Mask", __instance.RampShadowTexture);
								Main.logger.Log("shadowramp");
							}
							CharacterTextureDescription.s_RepaintMaterial.SetFloat("_Specialmask", 1f);
							CharacterTextureDescription.s_RepaintMaterial.SetTexture("_Ramp", texture2D);
							Graphics.Blit(__instance.m_Texture, rtToPaint, CharacterTextureDescription.s_RepaintMaterial);
						}
						else
                        {
							Main.logger.Log("tex2dnull");
                        }
					}
					if (__instance.UseRamp2Mask)
					{
						Main.logger.Log("Mask2");
						Texture2D texture2D;
						if (null == __instance.Ramps.SecondaryRamp)
						{
							texture2D = secondaryRamp;
							Main.logger.Log("secrampnull");
						}
						else
						{
							texture2D = __instance.Ramps.SecondaryRamp;
							Main.logger.Log("secrampnotnull");
						}
						if (texture2D != null)
						{
							Main.logger.Log("tex2dnotnull");
							RenderTexture renderTexture = new RenderTexture(__instance.m_Texture.width, __instance.m_Texture.height, 0, RenderTextureFormat.ARGB32);
							Graphics.Blit(rtToPaint, renderTexture);
							if (__instance.UseDefaultMask2 && null != __instance.DefaultMask2)
							{
								CharacterTextureDescription.s_RepaintMaterial.SetTexture("_Mask", __instance.DefaultMask2);
								Main.logger.Log("defaultmask");
							}
							if (__instance.UseRamp2Mask && null != __instance.RampShadowTexture)
							{
								CharacterTextureDescription.s_RepaintMaterial.SetTexture("_Mask", __instance.RampShadowTexture);
								Main.logger.Log("shadowmask");
							}
							CharacterTextureDescription.s_RepaintMaterial.SetFloat("_Specialmask", -1f);
							CharacterTextureDescription.s_RepaintMaterial.SetTexture("_Ramp", texture2D);
							Graphics.Blit(__instance.m_Texture, renderTexture, CharacterTextureDescription.s_RepaintMaterial);
							Graphics.Blit(renderTexture, rtToPaint);
							renderTexture.Release();
							UnityEngine.Object.DestroyImmediate(renderTexture);
						}
						else
						{
							Main.logger.Log("tex2dnull");
							RenderTexture renderTexture = new RenderTexture(__instance.m_Texture.width, __instance.m_Texture.height, 0, RenderTextureFormat.ARGB32);
							Graphics.Blit(rtToPaint, renderTexture);
							if (__instance.UseDefaultMask2 && null != __instance.DefaultMask2)
							{
								CharacterTextureDescription.s_RepaintMaterial.SetTexture("_Mask", __instance.DefaultMask2);
								Main.logger.Log("defaultmask");
							}
							if (__instance.UseRamp2Mask && null != __instance.RampShadowTexture)
							{
								CharacterTextureDescription.s_RepaintMaterial.SetTexture("_Mask", __instance.RampShadowTexture);
								Main.logger.Log("shadowmask");
							}
							CharacterTextureDescription.s_RepaintMaterial.SetFloat("_Specialmask", -1f);
							CharacterTextureDescription.s_RepaintMaterial.SetTexture("_Ramp", new Texture2D(256,256));
							Graphics.Blit(__instance.m_Texture, renderTexture, CharacterTextureDescription.s_RepaintMaterial);
							Graphics.Blit(renderTexture, rtToPaint);
							renderTexture.Release();
							UnityEngine.Object.DestroyImmediate(renderTexture);
						}
					}
				}
				finally
				{
					RenderTexture.active = active;
				}
				return false;
			}
			catch(Exception e)
            {
				Main.logger.Error(e.ToString());
				return false;
            }
		}
	}
}
