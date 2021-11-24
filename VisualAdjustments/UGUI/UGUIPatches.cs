using HarmonyLib;
using Kingmaker.Controllers;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UI.ServiceWindow;

namespace VisualAdjustments.UGUI
{
    [HarmonyPatch(typeof(SelectionCharacterController), "SetSelected")]
    internal static class SelectionCharacterController_SetSelected_Patch
    {
        private static void Postfix(UnitEntityData unit)
        {
            //Main.logger.Log(unit.CharacterName);
            if (unit != null)
            {
                if (FXUIHandler.handler != null && TutorialCanvas.UI.UIManager.manager.UISelectGrid.activeInHierarchy)
                {
                    // var part = unit.Parts.Ensure<UnitPartVAFX>();
                    var VisualInfo = VisualAdjustments.GlobalVisualInfo.Instance.ForCharacter(unit);
                    var part = VisualInfo.FXpart;
                    FXUIHandler.handler.WhiteOrBlackList = part.blackorwhitelist;
                    FXUIHandler.currentUnitPart = part;
                }
                TutorialCanvas.UI.UIManager.data = unit;
            }
        }
    }

    [HarmonyPatch(typeof(Inventory), "SetCharacter")]
    internal static class Inventory_SetCharacter_Patch
    {
        private static void Postfix(UnitEntityData unit)
        {
            //Main.logger.Log(unit.CharacterName);
            if (unit != null)
            {
                if (FXUIHandler.handler != null && TutorialCanvas.UI.UIManager.manager.UISelectGrid.activeInHierarchy)
                {
                    //  var part = unit.Parts.Ensure<UnitPartVAFX>();
                    var VisualInfo = VisualAdjustments.GlobalVisualInfo.Instance.ForCharacter(unit);
                    var part = VisualInfo.FXpart;
                    FXUIHandler.handler.WhiteOrBlackList = part.blackorwhitelist;
                    FXUIHandler.currentUnitPart = part;
                }
                TutorialCanvas.UI.UIManager.data = unit;
            }
        }
    }
}