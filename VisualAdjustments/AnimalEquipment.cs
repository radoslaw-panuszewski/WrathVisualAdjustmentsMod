using Kingmaker.View;

namespace VisualAdjustments
{
    [HarmonyLib.HarmonyPatch(typeof(UnitEntityView), "UpdateBuiltinArmorView")]
    public static class AnimalEquipment_patch
    {
        public static bool Prefix(UnitEntityView __instance)
        {
            if (!__instance.Data.IsPlayerFaction || !__instance.Data.IsPet)
            {
                return true;
            }
            var settings = GlobalVisualInfo.Instance.ForCharacter(__instance.Data).settings;
            if (settings.hidebarding)
            {
                __instance.m_BuiltinArmorMediumView.SetActive(false);
                __instance.m_BuiltinArmorHeavyView.SetActive(false);
            }
            else
            {
                if (settings.overridebarding == 1)
                {
                    __instance.m_BuiltinArmorHeavyView.SetActive(false);
                    __instance.m_BuiltinArmorMediumView.SetActive(true);
                }
                else if (settings.overridebarding == 2)
                {
                    __instance.m_BuiltinArmorMediumView.SetActive(false);
                    __instance.m_BuiltinArmorHeavyView.SetActive(true);
                }
                else if (settings.overridebarding == 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}