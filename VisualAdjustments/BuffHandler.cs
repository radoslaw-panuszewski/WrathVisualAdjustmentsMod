using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Visual.Particles;
using Owlcat.Runtime.Core.Utils;
using System.Linq;
using UnityEngine;

namespace VisualAdjustments
{
    //Prevent Blacklisted Buffs from spawning FX/Prevent non-whitelisted from spawning FX
    [HarmonyPatch(typeof(Buff), "TrySpawnParticleEffect")]
    internal static class TrySpawnParticleEffect_Patch
    {
        private static bool Prefix(Buff __instance)
        {
            var VisualInfo = VisualAdjustments.GlobalVisualInfo.Instance.ForCharacter(__instance.Owner);
            var component = VisualInfo.FXpart;
            if (component == null) return true;
            if (component.blackorwhitelist)
            {
                if (component.blackwhitelistnew.Any(a => a.AssetID == __instance.Blueprint.AssetGuidThreadSafe))
                {
                    return false;
                }
            }
            else
            {
                if (!component.blackwhitelistnew.Any(a => a.AssetID == __instance.Blueprint.AssetGuidThreadSafe))
                {
                    return false;
                }
            }
            return true;
        }
    }

    // Spawn Override FX's
    /* [HarmonyPatch(typeof(Buff), "TrySpawnParticleEffect")]
     static class TrySpawnParticleEffect_Patch2
     {
         static bool Prefix(Buff __instance)
         {
             var component = __instance.Owner.Unit.Parts.Get<UnitPartVAFX>();
             if (component == null) return true;
             if (component.blackorwhitelist)
             {
                 if (component.blackwhitelist.Any(a => a.AssetID == __instance.Blueprint.AssetGuidThreadSafe))
                 {
                     return false;
                 }
             }
             else
             {
                 if (!component.blackwhitelist.Any(a => a.AssetID == __instance.Blueprint.AssetGuidThreadSafe))
                 {
                     return false;
                 }
             }
             return true;
         }
     }*/

    internal static class BuffUtilities
    {
        public static void RefreshBuffs(this UnitEntityData data)
        {
            foreach (var buff in data.Buffs.Enumerable)
            {
                buff.ClearParticleEffect();
                buff.TrySpawnParticleEffect();
            }
        }

        public static void SpawnOverrideBuffs(this UnitEntityData data)
        {
            // var part = data.Parts.Get<UnitPartVAFX>();
            var VisualInfo = VisualAdjustments.GlobalVisualInfo.Instance.ForCharacter(data);
            var part = VisualInfo.FXpart;
            // if (part != null)
            {
                foreach (var overridebuff in part.overrides)
                {
                    if (part.currentoverrides.TryGetValue(overridebuff.AssetID, out GameObject bufffx))
                    {
                        if (bufffx == null)
                        {
                            part.currentoverrides.Remove(overridebuff.AssetID);
                            var buffbp = ResourcesLibrary.TryGetBlueprint<BlueprintBuff>(overridebuff.AssetID);
                            if (buffbp.FxOnStart != null)
                            {
                                // TODO [it doesn't compile] part.currentoverrides.Add(overridebuff.AssetID, FxHelper.SpawnFxOnUnit(buffbp.FxOnStart.Load(), data.View));
                            }
                            else if (buffbp.FxOnRemove != null)
                            {
                                // TODO [it doesn't compile] part.currentoverrides.Add(overridebuff.AssetID, FxHelper.SpawnFxOnUnit(buffbp.FxOnRemove.Load(), data.View));
                            }
                        }
                    }
                    else
                    {
                        var buffbp = ResourcesLibrary.TryGetBlueprint<BlueprintBuff>(overridebuff.AssetID);
                        if (buffbp.FxOnStart != null)
                        {
                            // TODO [it doesn't compile] part.currentoverrides.Add(overridebuff.AssetID, FxHelper.SpawnFxOnUnit(buffbp.FxOnStart.Load(), data.View));
                        }
                        else if (buffbp.FxOnRemove != null)
                        {
                            // TODO [it doesn't compile] part.currentoverrides.Add(overridebuff.AssetID, FxHelper.SpawnFxOnUnit(buffbp.FxOnRemove.Load(), data.View));
                        }
                    }
                }
                foreach (var currentfx in part.currentoverrides.ToTempList())
                {
                    if (!part.overrides.Any(a => a.AssetID == currentfx.Key))
                    {
                        Object.DestroyImmediate(currentfx.Value);
                        part.currentoverrides.Remove(currentfx.Key);
                    }
                }
            }
        }
    }
}