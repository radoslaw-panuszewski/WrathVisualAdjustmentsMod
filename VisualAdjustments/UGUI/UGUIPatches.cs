using HarmonyLib;
using Kingmaker.Controllers;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UI.ServiceWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualAdjustments.UGUI
{
    [HarmonyPatch(typeof(SelectionCharacterController), "SetSelected")]
    static class SelectionCharacterController_SetSelected_Patch
    {
        static void Postfix(UnitEntityData unit)
        {
            //Main.logger.Log(unit.CharacterName);
            if (unit != null)
            {
                if (FXUIHandler.handler != null && TutorialCanvas.UI.UIManager.manager.UISelectGrid.activeInHierarchy)
                {
                    var part = unit.Parts.Ensure<UnitPartVAFX>();
                    FXUIHandler.handler.WhiteOrBlackList = part.blackorwhitelist;
                    FXUIHandler.currentUnitPart = part;
                }
                TutorialCanvas.UI.UIManager.data = unit;
            }

        }
    }
    [HarmonyPatch(typeof(Inventory), "SetCharacter")]
    static class Inventory_SetCharacter_Patch
    {
        static void Postfix(UnitEntityData unit)
        {
            //Main.logger.Log(unit.CharacterName);
            if (unit != null)
            {
                if (FXUIHandler.handler != null && TutorialCanvas.UI.UIManager.manager.UISelectGrid.activeInHierarchy)
                {
                    var part = unit.Parts.Ensure<UnitPartVAFX>();
                    FXUIHandler.handler.WhiteOrBlackList = part.blackorwhitelist;
                    FXUIHandler.currentUnitPart = part;
                }
                TutorialCanvas.UI.UIManager.data = unit;
            }

        }
    }
}
