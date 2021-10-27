using HarmonyLib;
using Kingmaker.Cheats;
using Kingmaker.UnitLogic.Buffs;
using UnityEditor;
using UnityEngine;
using System.Linq;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Visual.Particles;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Owlcat.Runtime.Core.Utils;

namespace VisualAdjustments
{
    //Prevent Blacklisted Buffs from spawning FX/Prevent non-whitelisted from spawning FX
    [HarmonyPatch(typeof(Buff), "TrySpawnParticleEffect")]
    static class TrySpawnParticleEffect_Patch
    {
        static bool Prefix(Buff __instance)
        {
            var component = __instance.Owner.Unit.Parts.Get<UnitPartVAFX>();
            if (component == null) return true;
            if(component.blackorwhitelist)
            {
                if(component.blackwhitelist.ContainsKey(__instance.Blueprint.AssetGuidThreadSafe))
                {
                    return false;
                }
            }
            else
            {
                if (!component.blackwhitelist.ContainsKey(__instance.Blueprint.AssetGuidThreadSafe))
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
    static class BuffUtilities
    {
        public static void RefreshBuffs(this UnitEntityData data)
        {
            foreach(var buff in data.Buffs.Enumerable)
            {
                buff.ClearParticleEffect();
                buff.TrySpawnParticleEffect();
            }
        }
        public static void SpawnOverrideBuffs(this UnitEntityData data)
        {
            var part = data.Parts.Get<UnitPartVAFX>();
            if (part != null)
            {
                foreach (var overridebuff in part.overrides)
                {
                    if(part.currentoverrides.TryGetValue(overridebuff.AssetID,out GameObject bufffx))
                    {
                        if (bufffx == null)
                        {
                            part.currentoverrides.Remove(overridebuff.AssetID);
                            var buffbp = ResourcesLibrary.TryGetBlueprint<BlueprintBuff>(overridebuff.AssetID);
                            if (buffbp.FxOnStart != null)
                            {
                                part.currentoverrides.Add(overridebuff.AssetID, FxHelper.SpawnFxOnUnit(buffbp.FxOnStart.Load(), data.View));
                            }
                            else if (buffbp.FxOnRemove != null)
                            {
                                part.currentoverrides.Add(overridebuff.AssetID, FxHelper.SpawnFxOnUnit(buffbp.FxOnRemove.Load(), data.View));
                            }
                        }
                    }
                    else
                    {
                        var buffbp = ResourcesLibrary.TryGetBlueprint<BlueprintBuff>(overridebuff.AssetID);
                        if (buffbp.FxOnStart != null)
                        {
                            part.currentoverrides.Add(overridebuff.AssetID, FxHelper.SpawnFxOnUnit(buffbp.FxOnStart.Load(), data.View));
                        }
                        else if (buffbp.FxOnRemove != null)
                        {
                           part.currentoverrides.Add(overridebuff.AssetID, FxHelper.SpawnFxOnUnit(buffbp.FxOnRemove.Load(), data.View));
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