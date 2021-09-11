using System;
using HarmonyLib;
using Kingmaker.Utility;
using Kingmaker.View.Equipment;
using Kingmaker.Visual.CharacterSystem;

namespace VisualAdjustments
{
/*    [HarmonyPatch(typeof(Character), "AddEquipmentEntity", new Type[] {typeof(EquipmentEntity), typeof(bool),typeof(int),typeof(int) })]
    public class patchie
    {
        public static bool Prefix(Character __instance,EquipmentEntity ee, int primaryRamp, int secondaryRamp)
        {
            try
            {
                if (ee == null)
                {
                    Main.logger.Log("NullEE");
                    return false;
                }

                if (!__instance.m_EquipmentEntities.Contains(ee))
                {
                    __instance.m_EquipmentEntities.Add(ee);
                    if (primaryRamp > 0 || secondaryRamp > 0)
                    {
                        Main.logger.Log("if");
                        __instance.AddEquipmentEntity(ee,true,primaryRamp,secondaryRamp);
                       // return false;
                         __instance.SetRampIndices(ee, primaryRamp, secondaryRamp, 0, 0);
                    }
                    if ((ee.PrimaryRamps.Count > 0 || ee.PrimaryRamps.Count > 0) &&
                             __instance.m_RampIndices.FirstOrDefault((Character.SelectedRampIndices rampIndices) =>
                                 rampIndices.EquipmentEntity == ee) == null)
                    {
                        Main.logger.Log("elseif");
                        Character.SelectedRampIndices item = new Character.SelectedRampIndices
                        {
                            EquipmentEntity = ee
                        };
                        __instance.m_RampIndices.Add(item);
                    }

                    //__instance.IsDirty = true;
                }

                return false;
            }
            catch (Exception e)
            {
                Main.logger.Log(e.ToString());
                return false;
            }
        }
    }

    [HarmonyPatch(typeof(Character), "SetRampIndices")]
        //, new Type[] { typeof(EquipmentEntity), typeof(bool), typeof(int), typeof(int) })]
    public class patchiee
    {
        public static bool Prefix()
        {
            return false;
        }
    }*/
}