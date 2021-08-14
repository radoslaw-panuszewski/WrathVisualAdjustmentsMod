using HarmonyLib;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.View;
using Kingmaker.View.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VisualAdjustments
{
    class HandsEquipmentManager
    {
        [HarmonyPatch(typeof(UnitViewHandsEquipment), "UpdateVisibility")]
        static class UnitViewHandsEquipment_UpdateVisibility_Patch
        {
            static void Postfix(UnitViewHandsEquipment __instance)
            {
                try
                {
                    /// Main.logger.Log("UpdateVisibility");
                    if (!Main.enabled) return;
                    if (!__instance.Owner.IsPlayerFaction) return;
                    Settings.CharacterSettings characterSettings = Main.settings.GetCharacterSettings(__instance.Owner);
                    if (characterSettings == null) return;
                    if (characterSettings.hideWeapons)
                    {
                        foreach (var kv in __instance.Sets)
                        {
                            if (kv.Key.PrimaryHand.Active) continue;
                            kv.Value.MainHand.ShowItem(false);
                            kv.Value.OffHand.ShowItem(false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Main.Error(ex);
                }

            }
        }
        /*
         * Hide Belt Slots and fix belt item scale
         * 
         */
        [HarmonyPatch(typeof(UnitViewHandsEquipment), "UpdateBeltPrefabs")]
        static class UnitViewHandsEquipment_UpdateBeltPrefabs_Patch
        {
            static void Postfix(UnitViewHandsEquipment __instance, GameObject[] ___m_ConsumableSlots)
            {
                try
                {
                    if (!Main.enabled) return;
                    if (!__instance.Owner.IsPlayerFaction) return;
                    // Main.logger.Log("UpdateBeltPrefabs");
                    Settings.CharacterSettings characterSettings = Main.settings.GetCharacterSettings(__instance.Owner);
                    if (characterSettings == null) return;
                    if (characterSettings.hideBeltSlots)
                    {
                        foreach (var go in ___m_ConsumableSlots) go?.SetActive(false);
                    }
                    if (characterSettings.overrideScale && !__instance.Character.PeacefulMode)
                    {
                        foreach (var go in ___m_ConsumableSlots)
                        {
                            if (go == null) continue;
                            go.transform.localScale *= ViewManager.GetRealSizeScale(__instance.Owner.View, characterSettings);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Main.Error(ex);
                }
            }
        }
        /*
         * Hide Quiver
         */
        [HarmonyPatch(typeof(UnitViewHandSlotData), "ReattachSheath")]
        static class UnitViewHandsSlotData_ReattachSheath_Patch
        {
            static bool HasQuiver(UnitViewHandSlotData slotData)
            {
                if (slotData.VisibleItem == null) return false;
                var blueprint = slotData.VisibleItem.Blueprint as BlueprintItemEquipmentHand;
                if (blueprint == null) return false;
                return blueprint.VisualParameters.HasQuiver;
            }
            static bool Prefix(UnitViewHandSlotData __instance, UnitViewHandsEquipment ___m_Equipment)
            {
                try
                {
                    if (!Main.enabled) return true;
                    if (!__instance.Owner.IsPlayerFaction) return true;
                    var characterSettings = Main.settings.GetCharacterSettings(__instance.Owner);
                    if (characterSettings == null) return true;
                    if (!HasQuiver(__instance)) return true;
                    if (characterSettings.hideQuiver)
                    {
                        UnitViewHandSlotData unitViewHandSlotData = ___m_Equipment.QuiverHandSlot;
                        if (unitViewHandSlotData == null) return true;
                        if (unitViewHandSlotData == __instance) return false;
                        if (unitViewHandSlotData.IsActiveSet || unitViewHandSlotData.SheathVisualModel == null || !HasQuiver(unitViewHandSlotData))
                        {
                            unitViewHandSlotData.DestroySheathModel();
                            unitViewHandSlotData = null;
                        }
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Main.Error(ex);
                }
                return true;
            }
        }
        /*
         * Hide Weapon Models 
         */
        [HarmonyPatch(typeof(UnitViewHandSlotData), "AttachModel", new Type[] { })]
        static class UnitViewHandsSlotData_AttachModel_Patch
        {
            static void Postfix(UnitViewHandSlotData __instance)
            {
                try
                {
                    if (!Main.enabled) return;
                    if (!__instance.Owner.IsPlayerFaction) return;
                    var characterSettings = Main.settings.GetCharacterSettings(__instance.Owner);
                    if (characterSettings == null) return;
                    if (characterSettings.hideWeapons)
                    {
                        if (!__instance.IsActiveSet)
                        {
                            __instance.ShowItem(false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Main.Error(ex);
                }
            }
        }
        /*
         * Override Weapon Model
         * 
         */
        [HarmonyPatch(typeof(UnitViewHandSlotData), "VisibleItemBlueprint", MethodType.Getter)]
        static class UnitViewHandsSlotData_VisibleItemBlueprint_Patch
        {
            static void Postfix(UnitViewHandSlotData __instance, ref BlueprintItemEquipmentHand __result)
            {
                try
                {
                    if (!Main.enabled) return;
                    if (!__instance.Owner.IsPlayerFaction) return;
                    var characterSettings = Main.settings.GetCharacterSettings(__instance.Owner);
                    if (characterSettings == null) return;
                    if (__instance.VisibleItem == null) return;
                    var blueprint = __instance.VisibleItem.Blueprint as BlueprintItemEquipmentHand;
                    var animationStyle = blueprint.VisualParameters.AnimStyle.ToString();
                    characterSettings.overrideWeapons.TryGetValue(animationStyle, out BlueprintRef blueprintId);
                    if (blueprintId == null || blueprintId == "") return;
                    var newBlueprint = ResourcesLibrary.TryGetBlueprint<BlueprintItemEquipmentHand>(blueprintId);
                    if (newBlueprint == null) return;
                    __result = newBlueprint;
                }
                catch (Exception ex)
                {
                    Main.Error(ex);
                }
            }
        }
        /*
         * Use real size scaling for weapons
         */
        [HarmonyPatch(typeof(UnitViewHandSlotData), "OwnerWeaponScale", MethodType.Getter)]
        static class UnitViewHandsSlotData_OwnerWeaponScale_Patch
        {
            static void Postfix(UnitViewHandSlotData __instance, ref float __result)
            {
                try
                {
                    /// Main.logger.Log("OwnerWeaponScale");
                    if (!Main.enabled) return;
                    if (!__instance.Owner.IsPlayerFaction) return;
                    var characterSettings = Main.settings.GetCharacterSettings(__instance.Owner);
                    if (characterSettings == null) return;
                    if (!characterSettings.overrideScale) return;
                    var realScale = ViewManager.GetRealSizeScale(__instance.Owner.View, characterSettings);
                    __result = realScale;
                }
                catch (Exception ex)
                {
                    Main.Error(ex);
                }
            }
        }
        [HarmonyPatch(typeof(UnitViewHandsEquipment), "UpdateAll")]
        static class unitViewHandsEquipment_Patch
        {
            //static bool Prefix(UnitViewHandsEquipment __instance)
            // {
            /// return false;
            // }
            static void Postfix(UnitViewHandsEquipment __instance)
            {
                if (!Main.enabled && !__instance.Owner.IsPlayerFaction) return;
                try
                {
                    foreach (var slot in __instance.m_SlotsByVisualSlot)
                    {
                        if (slot != null)
                        {
                            if (slot.SheathVisualModel == null) return;
                            foreach (Transform sheath in slot.SheathVisualModel.transform)
                            {
                                if (sheath == null) return;
                                Main.logger.Log("UpdateAll");
                                Main.logger.Log("Sheath " + sheath.position.x.ToString() +" "+ sheath.position.y.ToString() +" "+ sheath.position.z.ToString());
                                Main.logger.Log("Weapon " + slot.VisualModel.transform.position.x.ToString() + " " + slot.VisualModel.transform.position.y.ToString() +" "+ slot.VisualModel.transform.position.z.ToString());
                                string xdiff = (slot.VisualModel.transform.position.x - sheath.position.x).ToString();
                                string ydiff = (slot.VisualModel.transform.position.y - sheath.position.y).ToString();
                                string zdiff = (slot.VisualModel.transform.position.z - sheath.position.z).ToString();
                                Main.logger.Log("diff " + xdiff + " " + zdiff + " " + ydiff);
                                string xdiff2 = (slot.VisualModel.transform.localPosition.x - sheath.localPosition.x).ToString();
                                string ydiff2 = (slot.VisualModel.transform.localPosition.y - sheath.localPosition.y).ToString();
                                string zdiff2 = (slot.VisualModel.transform.localPosition.z - sheath.localPosition.z).ToString();
                                Main.logger.Log("diff2 " + xdiff2 + " " + zdiff2 + " " + ydiff2);
                               /// slot.SheathVisualModel.transform.localPosition = slot.VisualModel.transform.localPosition;
                                // sheath.position = slot.VisualModel.transform.position;
                                //sheath.position = new Vector3(0f, 0f, 0f);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Main.logger.Error(e.ToString());
                }
            }
        }
        static void updateSheaths(UnitEntityView data)
        {
            try
            {
                foreach (var slot in data.HandsEquipment.m_SlotsByVisualSlot)
                {
                    if (slot != null)
                    {
                        if (slot.SheathVisualModel == null) return;
                        foreach (Transform sheath in slot.SheathVisualModel.transform)
                        {
                            Main.logger.Log("updatesheaths");
                          //  Main.logger.Log(sheath.position.x.ToString() + sheath.position.y.ToString() + sheath.position.z.ToString());
                            Main.logger.Log("Sheath " + sheath.position.x.ToString() +" "+ sheath.position.y.ToString() +" "+ sheath.position.z.ToString());
                            Main.logger.Log("Weapon " + slot.VisualModel.transform.position.x.ToString() +" "+ slot.VisualModel.transform.position.y.ToString() +" "+ slot.VisualModel.transform.position.z.ToString());
                            string xdiff = (slot.VisualModel.transform.position.x - sheath.position.x).ToString();
                            string ydiff = (slot.VisualModel.transform.position.y - sheath.position.y).ToString();
                            string zdiff = (slot.VisualModel.transform.position.z - sheath.position.z).ToString();
                            Main.logger.Log("diff " + xdiff + " " + zdiff + " " + ydiff);
                          ///  slot.SheathVisualModel.transform.localPosition = slot.VisualModel.transform.localPosition;
                            //sheath.position;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Main.logger.Error(e.ToString());
            }
        }
        [HarmonyPatch(typeof(UnitViewHandSlotData), "MatchVisuals")]
        static class unitViewHandSlotData_Patch
        {
            static void Postfix(UnitViewHandSlotData __instance)
            {
                try
                {
                    //if (__instance == null) return;
                    if (__instance.VisualModel == null) return;
                    //if (__instance.Owner == null) return;
                    if (Main.enabled && __instance.Owner.IsPlayerFaction)
                    {
                        /*if (!Main.settings.GetCharacterSettings(__instance.Owner).hideSheaths)*/
                       /// Main.logger.Log(__instance.Owner.CharacterName);
                        if (!Main.settings.GetCharacterSettings(__instance.Owner).hideSheaths)
                        {
                            updateSheaths(__instance.Owner.View);
                        }
                        else
                        {
                            __instance.DestroySheathModel();
                        }
                    }
                }
                catch(Exception e)
                {
                    Main.logger.Error(e.ToString());
                }
            }
        }
    }
}
