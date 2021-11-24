namespace VisualAdjustments
{
    /*[HarmonyPatch(typeof(SteamManager), "Awake")]
	public static class checkneedrepaint
    {
		public static bool Prefix()
        {
			return false;
        }
    }*/

    //[HarmonyPatch(typeof(Character), "EEHasSameTexture")]
    public static class eehassametex
    {
        public static bool Prefix(ref bool __result)
        {
            __result = false;
            return false;
        }
    }

    /*[HarmonyPatch(typeof(Kingmaker.Visual.CharacterSystem.Character), "OnRenderObject")]
	public static class xfddfg
    {
		public static bool Prefix(Character __instance)
        {
			return false;
        }
    }
	//[HarmonyPatch(typeof(CharacterAtlasService),"Update")]
	/*
	public static class stuff
	{
		public static bool Prefix(CharacterAtlasService __instance)
		{
			DxtCompressorService instance = Services.GetInstance<DxtCompressorService>();
			if (instance == null)
			{
				return false;
			}
			instance.Update();
			if (__instance.m_Requests.Count == 0 && !Main.forceload)
			{
				return false;
			}
			CharacterAtlasService.AtlasRebuildRequest atlasRebuildRequest = __instance.m_Requests[0];
			if (instance.RequestsCount > 0 && !atlasRebuildRequest.IgnoreLimit && !Main.forceload)
			{
				return false;
			}
			if(!Main.forceload) __instance.m_Requests.RemoveAt(0);
			if (atlasRebuildRequest.Material == null)
			{
				PFLog.Default.Error("CharacterAtlasService.Update: Material is null in request for " + atlasRebuildRequest.ContextString, Array.Empty<object>());
				return false;
			}
			foreach (CharacterAtlas characterAtlas in atlasRebuildRequest.Atlases)
			{
				characterAtlas.Build(instance, atlasRebuildRequest.PaintedTextures, atlasRebuildRequest.Material, atlasRebuildRequest.OnTextureCompressed, true);
			}
			return false;
		}
	}

	//[HarmonyPatch(typeof(CharacterAtlas), "Build")]
	//[HarmonyPatch(new Type[] { typeof(EquipmentEntity.PaintedTextures), typeof(int), typeof(int), typeof(int), typeof(int) })]

	public class BuildAtlas
	{
		public static bool Prefix(CharacterAtlas __instance, DxtCompressorService dxtCompressorService, Material material, Action<CharacterAtlas, Texture2D> onTextureCompressed, bool cleanAtlas = false)
		{
			try
			{
				__instance.CalculateRects();
				if (__instance.AtlasTexture == null)
				{
					Texture2D texture2D = new Texture2D((int)__instance.m_Size.x, (int)__instance.m_Size.y, (UnityEngine.Experimental.Rendering.GraphicsFormat)12, UnityEngine.Experimental.Rendering.TextureCreationFlags.MipChain);
					texture2D.filterMode = FilterMode.Bilinear;
					texture2D.wrapMode = 0;
					texture2D.anisoLevel = 1;
					texture2D.name = string.Format("{0}_2D", __instance.Channel);
					__instance.AtlasTexture = new AtlasTexture
					{
						Texture = texture2D
					};
				}
				else
				{
					__instance.AtlasTexture.CompressionComplete = false;
				}
				RenderTexture renderTexture = new RenderTexture((int)__instance.m_Size.x, (int)__instance.m_Size.y, 0, (UnityEngine.Experimental.Rendering.GraphicsFormat)1);
				renderTexture.filterMode = FilterMode.Bilinear;
				renderTexture.wrapMode = 0;
				renderTexture.anisoLevel = 1;
				renderTexture.useMipMap = true;
				renderTexture.autoGenerateMips = true;
				renderTexture.name = string.Format("{0}_RT", __instance.Channel);
				RenderTexture active = RenderTexture.active;
				RenderTexture.active = renderTexture;
				if (!__instance.m_Baked || cleanAtlas)
				{
					Color color = new Color(0f, 0f, 0f, 0f);
					if (__instance.Channel == CharacterTextureChannel.Masks)
					{
						color = new Color(0.85f, 0f, 0f, 0f);
					}
					if (__instance.Channel == CharacterTextureChannel.Normal)
					{
						color = new Color(0.5f, 0.5f, 1f, 1f);
					}
					GL.Clear(false, true, color);
				}
				RenderTexture temporary = RenderTexture.GetTemporary((int)__instance.m_Size.x, (int)__instance.m_Size.y, 0, 0, (__instance.Channel == CharacterTextureChannel.Diffuse) ? 2 : 1);
				temporary.filterMode = FilterMode.Bilinear;
				temporary.wrapMode = 0;
				temporary.anisoLevel = 1;
				temporary.useMipMap = true;
				temporary.autoGenerateMips = true;
				temporary.name = string.Format("{0}_prevRT", __instance.Channel);
				__instance.m_BakeMaterial.SetTexture("_PreviousTex", temporary);
				foreach (CharacterTextureDescription characterTextureDescription in __instance.m_SortedTextures)
				{
					Rect rect;
					if (__instance.Channel == CharacterTextureChannel.Diffuse)
					{
						rect = __instance.Rects[characterTextureDescription.GetSourceTexture()];
					}
					else
					{
						rect = __instance.Rects[__instance.m_PrimaryTextureMap[characterTextureDescription]];
					}
					float num = (float)characterTextureDescription.ActiveTexture.width / __instance.ScaleFactor;
					float num2 = (float)characterTextureDescription.ActiveTexture.height / __instance.ScaleFactor;
					__instance.m_BakeMaterial.SetVector("_SrcRect", new Vector4(0f, 0f, rect.width / num, rect.height / num2));
					__instance.m_ShadowBakeMaterial.SetVector("_SrcRect", new Vector4(0f, 0f, rect.width / num, rect.height / num2));
					__instance.m_DiffuseBakeMaterial.SetVector("_SrcRect", new Vector4(0f, 0f, rect.width / num, rect.height / num2));
					__instance.m_RoughnessLightenBlend.SetVector("_SrcRect", new Vector4(0f, 0f, rect.width / num, rect.height / num2));
					__instance.m_BakeMaterial.SetVector("_DstRect", new Vector4(rect.x / __instance.m_Size.x, rect.y / __instance.m_Size.y, rect.width / __instance.m_Size.x, rect.height / __instance.m_Size.y));
					__instance.m_ShadowBakeMaterial.SetVector("_DstRect", new Vector4(rect.x / __instance.m_Size.x, rect.y / __instance.m_Size.y, rect.width / __instance.m_Size.x, rect.height / __instance.m_Size.y));
					__instance.m_DiffuseBakeMaterial.SetVector("_DstRect", new Vector4(rect.x / __instance.m_Size.x, rect.y / __instance.m_Size.y, rect.width / __instance.m_Size.x, rect.height / __instance.m_Size.y));
					__instance.m_RoughnessLightenBlend.SetVector("_DstRect", new Vector4(rect.x / __instance.m_Size.x, rect.y / __instance.m_Size.y, rect.width / __instance.m_Size.x, rect.height / __instance.m_Size.y));
					__instance.m_BakeMaterial.SetInt("_IsEmpty", characterTextureDescription.IsEmpty ? 1 : 0);
					if (characterTextureDescription.Material != null)
					{
						__instance.m_BakeMaterial.SetFloat("_Roughness", characterTextureDescription.Material.GetFloat("_Roughness"));
						__instance.m_BakeMaterial.SetFloat("_Emission", characterTextureDescription.Material.GetFloat("_Emission"));
						__instance.m_BakeMaterial.SetFloat("_Metallic", characterTextureDescription.Material.GetFloat("_Metallic"));
					}
					else
					{
						__instance.m_BakeMaterial.SetFloat("_Roughness", 1f);
						__instance.m_BakeMaterial.SetFloat("_Emission", 1f);
						__instance.m_BakeMaterial.SetFloat("_Metallic", 1f);
					}
					__instance.m_BakeMaterial.SetTexture("_AlphaMask", characterTextureDescription.DiffuseTexture);
					if (__instance.Channel != CharacterTextureChannel.Diffuse)
					{
						__instance.m_BakeMaterial.EnableKeyword("ALPHA_MASK_ON");
						if (__instance.Channel == CharacterTextureChannel.Normal)
						{
							__instance.m_BakeMaterial.EnableKeyword("NORMAL_MAP_ON");
							material.SetFloat("_UseNormalMapAtlas", 1f);
						}
					}
					else
					{
						__instance.m_BakeMaterial.DisableKeyword("ALPHA_MASK_ON");
						__instance.m_BakeMaterial.DisableKeyword("NORMAL_MAP_ON");
						material.SetFloat("_UseNormalMapAtlas", 0f);
					}
					Texture texture = characterTextureDescription.ActiveTexture;
					if (__instance.Channel == CharacterTextureChannel.Diffuse)
					{
						if (characterTextureDescription.UseShadowMask)
						{
							__instance.m_ShadowBakeMaterial.SetTexture("_Mask", characterTextureDescription.RampShadowTexture);
							Graphics.Blit(texture, renderTexture, __instance.m_ShadowBakeMaterial);
							if (!characterTextureDescription.ActiveTexture.name.EndsWith("_D"))
							{
								continue;
							}
							Graphics.Blit(texture, renderTexture, __instance.m_DiffuseBakeMaterial);
							Graphics.Blit(renderTexture, temporary);
						}
						texture = characterTextureDescription.ActiveTexture;
					}
					else if (__instance.Channel == CharacterTextureChannel.Normal)
					{
						texture = characterTextureDescription.NormalTexture;
					}
					else if (__instance.Channel == CharacterTextureChannel.Masks)
					{
						texture = characterTextureDescription.MaskTexture;
						if (characterTextureDescription.UseShadowMask && null != characterTextureDescription.RampShadowTexture)
						{
							RenderTexture renderTexture2 = new RenderTexture(characterTextureDescription.RampShadowTexture.width, characterTextureDescription.RampShadowTexture.height, 0, UnityEngine.Experimental.Rendering.GraphicsFormat.R8_SRGB);
							Graphics.Blit(characterTextureDescription.RampShadowTexture, renderTexture2);
							Graphics.Blit(renderTexture2, renderTexture, __instance.m_RoughnessLightenBlend);
							Graphics.Blit(renderTexture, temporary);
							renderTexture2.Release();
						}
					}
					Graphics.Blit(texture, renderTexture, __instance.m_BakeMaterial);
					Graphics.Blit(renderTexture, temporary);
				}
				__instance.UpdateMaterial(material, renderTexture);
				RenderTexture.active = active;
				if (dxtCompressorService != null)
				{
					dxtCompressorService.CompressTexture(renderTexture, __instance.AtlasTexture.Texture,  (Owlcat.Runtime.Visual.Dxt.DxtCompressorService.Compression)1, delegate (Texture rt, Texture2D atlas, string error)
					{
						if (atlas == null || __instance.AtlasTexture.Destroyed)
						{
							if (__instance.AtlasTexture.Destroyed)
							{
								UnityEngine.Object.Destroy(atlas);
							}
							((RenderTexture)rt).Release();
							UnityEngine.Object.Destroy(rt);
							return;
						}
						__instance.AtlasTexture.CompressionComplete = true;
						if (error != null)
						{
							PFLog.Default.Error("Failed to compress atlas to DXT: " + error, Array.Empty<object>());
							return;
						}
						atlas.Apply();
						((RenderTexture)rt).Release();
						UnityEngine.Object.Destroy(rt);
						onTextureCompressed.Invoke(__instance, atlas);
					});
				}
				RenderTexture.ReleaseTemporary(temporary);
				__instance.m_Baked = true;
				return false;
			}
			catch(Exception e)
            {
				Main.logger.Log(e.ToString());
				return false;
            }
		}
	}

				[HarmonyPatch(typeof(EquipmentEntity), "RepaintTextures")]
	[HarmonyPatch(new Type[] { typeof(EquipmentEntity.PaintedTextures), typeof(int), typeof(int), typeof(int), typeof(int) })]

	public class patchiee
	{
		public static bool Prefix(EquipmentEntity __instance , EquipmentEntity.PaintedTextures paintedTextures, int primaryRampIndex, int secondaryRampIndex, int specialPrimaryRampIndex, int specialSecondaryRampIndex)
		{
			try
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
						//RenderTexture rt = paintedTextures.Get(characterTextureDescription);
						RenderTexture rt = null;
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
	/*public static class repaint
	{
		public static void Repainte(this CharacterTextureDescription __instance)
        {
			try
            {
				if (null == __instance.ActiveTexture)
				{
					PFLog.TechArt.Warning("Отсутствует текстура в одном из EE", Array.Empty<object>());
					return;
				}
				if (!__instance.UseRamp1Mask && !__instance.UseRamp2Mask && !__instance.UseDefaultMask1 && !__instance.UseDefaultMask2)
				{
					return;
				}
				if (__instance.m_Rt == null)
				{
					__instance.m_Rt = new RenderTexture(__instance.m_Texture.width, __instance.m_Texture.height, 0, 0);
					__instance.m_Rt.name = __instance.m_Texture.name + "_RT";
				}
				if (CharacterTextureDescription.s_RepaintMaterial == null)
				{
					CharacterTextureDescription.s_RepaintMaterial = new Material(Shader.Find("Hidden/CharacterTextureRepaint"));
				}
				RenderTexture active = RenderTexture.active;
				RenderTexture.active = __instance.m_Rt;
				try
				{
					Graphics.Blit(__instance.m_Texture, __instance.m_Rt);
					if (__instance.UseRamp1Mask || __instance.UseDefaultMask1)
					{
						Texture2D texture2D;
						if (null == __instance.Ramps.PrimaryRamp)
						{
							texture2D = primaryRamp;
						}
						else
						{
							texture2D = __instance.Ramps.PrimaryRamp;
						}
						if (texture2D != null)
						{
							if (__instance.UseDefaultMask1 && null != __instance.DefaultMask1)
							{
								CharacterTextureDescription.s_RepaintMaterial.SetTexture("_Mask", __instance.DefaultMask1);
							}
							if (__instance.UseRamp1Mask && null != __instance.RampShadowTexture)
							{
								CharacterTextureDescription.s_RepaintMaterial.SetTexture("_Mask", __instance.RampShadowTexture);
							}
							CharacterTextureDescription.s_RepaintMaterial.SetFloat("_Specialmask", 1f);
							CharacterTextureDescription.s_RepaintMaterial.SetTexture("_Ramp", texture2D);
							Graphics.Blit(__instance.m_Texture, __instance.m_Rt, CharacterTextureDescription.s_RepaintMaterial);
						}
					}
					if (__instance.UseRamp2Mask)
					{
						Texture2D texture2D;
						if (null == __instance.Ramps.SecondaryRamp)
						{
							texture2D = secondaryRamp;
						}
						else
						{
							texture2D = __instance.Ramps.SecondaryRamp;
						}
						if (texture2D != null)
						{
							RenderTexture renderTexture = new RenderTexture(__instance.m_Texture.width, __instance.m_Texture.height, 0, 0);
							Graphics.Blit(__instance.m_Rt, renderTexture);
							if (__instance.UseDefaultMask2 && null != __instance.DefaultMask2)
							{
								CharacterTextureDescription.s_RepaintMaterial.SetTexture("_Mask", __instance.DefaultMask2);
							}
							if (__instance.UseRamp2Mask && null != __instance.RampShadowTexture)
							{
								CharacterTextureDescription.s_RepaintMaterial.SetTexture("_Mask", __instance.RampShadowTexture);
							}
							CharacterTextureDescription.s_RepaintMaterial.SetFloat("_Specialmask", -1f);
							CharacterTextureDescription.s_RepaintMaterial.SetTexture("_Ramp", texture2D);
							Graphics.Blit(__instance.m_Texture, renderTexture, CharacterTextureDescription.s_RepaintMaterial);
							Graphics.Blit(renderTexture, __instance.m_Rt);
							renderTexture.Release();
							Object.DestroyImmediate(renderTexture);
						}
					}
				}
				finally
				{
					RenderTexture.active = active;
				}
			}
			catch(Exception e)
            {
				Main.logger.Log(e.ToString());
            }
        }
	}*/
}