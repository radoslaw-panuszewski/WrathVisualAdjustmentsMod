using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VisualAdjustments.UGUI.Unity_Scripts
{
    class SelectedWeaponSlotHandler : MonoBehaviour
    {
        public GameObject zero;
        public GameObject zerosec;
        public GameObject one;
        public GameObject onesec;
        public GameObject two;
        public GameObject twosec;
        public GameObject three;
        public GameObject threesec;
        public void Button(int indx)
        {
            VisualAdjustments.Main.logger.Log("ind"+indx.ToString());
            switch(indx)
            {
                case 0:
                    {
                        zero.SetActive(true);
                        zerosec.SetActive(false);
                        one.SetActive(false);
                        onesec.SetActive(false);
                        two.SetActive(false);
                        twosec.SetActive(false);
                        three.SetActive(false);
                        threesec.SetActive(false);
                        TutorialCanvas.UI.UIManager.slot = 0;
                        TutorialCanvas.UI.UIManager.primorsec = true;
                        Main.logger.Log(TutorialCanvas.UI.UIManager.slot.ToString()+ TutorialCanvas.UI.UIManager.primorsec);
                        break;
                    }
                case 1:
                    {
                        zero.SetActive(false);
                        zerosec.SetActive(true);
                        one.SetActive(false);
                        onesec.SetActive(false);
                        two.SetActive(false);
                        twosec.SetActive(false);
                        three.SetActive(false);
                        threesec.SetActive(false);
                        TutorialCanvas.UI.UIManager.slot = 0;
                        TutorialCanvas.UI.UIManager.primorsec = false;
                        Main.logger.Log(TutorialCanvas.UI.UIManager.slot.ToString() + TutorialCanvas.UI.UIManager.primorsec);
                        break;
                    }
                case 2:
                    {
                        zero.SetActive(false);
                        zerosec.SetActive(false);
                        one.SetActive(true);
                        onesec.SetActive(false);
                        two.SetActive(false);
                        twosec.SetActive(false);
                        three.SetActive(false);
                        threesec.SetActive(false);
                        TutorialCanvas.UI.UIManager.slot = 1;
                        TutorialCanvas.UI.UIManager.primorsec = true;
                        Main.logger.Log(TutorialCanvas.UI.UIManager.slot.ToString() + TutorialCanvas.UI.UIManager.primorsec);
                        break;
                    }
                case 3:
                    {
                        zero.SetActive(false);
                        zerosec.SetActive(false);
                        one.SetActive(false);
                        onesec.SetActive(true);
                        two.SetActive(false);
                        twosec.SetActive(false);
                        three.SetActive(false);
                        threesec.SetActive(false);
                        TutorialCanvas.UI.UIManager.slot = 1;
                        TutorialCanvas.UI.UIManager.primorsec = false;
                        Main.logger.Log(TutorialCanvas.UI.UIManager.slot.ToString() + TutorialCanvas.UI.UIManager.primorsec);
                        break;
                    }
                case 4:
                    {
                        zero.SetActive(false);
                        zerosec.SetActive(false);
                        one.SetActive(false);
                        onesec.SetActive(false);
                        two.SetActive(true);
                        twosec.SetActive(false);
                        three.SetActive(false);
                        threesec.SetActive(false);
                        TutorialCanvas.UI.UIManager.slot = 2;
                        TutorialCanvas.UI.UIManager.primorsec = true;
                        Main.logger.Log(TutorialCanvas.UI.UIManager.slot.ToString() + TutorialCanvas.UI.UIManager.primorsec);
                        break;
                    }
                case 5:
                    {
                        zero.SetActive(false);
                        zerosec.SetActive(false);
                        one.SetActive(false);
                        onesec.SetActive(false);
                        two.SetActive(false);
                        twosec.SetActive(true);
                        three.SetActive(false);
                        threesec.SetActive(false);
                        TutorialCanvas.UI.UIManager.slot = 2;
                        TutorialCanvas.UI.UIManager.primorsec = false;
                        Main.logger.Log(TutorialCanvas.UI.UIManager.slot.ToString() + TutorialCanvas.UI.UIManager.primorsec);
                        break;
                    }
                case 6:
                    {
                        zero.SetActive(false);
                        zerosec.SetActive(false);
                        one.SetActive(false);
                        onesec.SetActive(false);
                        two.SetActive(false);
                        twosec.SetActive(false);
                        three.SetActive(true);
                        threesec.SetActive(false);
                        TutorialCanvas.UI.UIManager.slot = 3;
                        TutorialCanvas.UI.UIManager.primorsec = true;
                        Main.logger.Log(TutorialCanvas.UI.UIManager.slot.ToString() + TutorialCanvas.UI.UIManager.primorsec);
                        break;
                    }
                case 7:
                    {
                        zero.SetActive(false);
                        zerosec.SetActive(false);
                        one.SetActive(false);
                        onesec.SetActive(false);
                        two.SetActive(false);
                        twosec.SetActive(false);
                        three.SetActive(false);
                        threesec.SetActive(true);
                        TutorialCanvas.UI.UIManager.slot = 3;
                        TutorialCanvas.UI.UIManager.primorsec = false;
                        Main.logger.Log(TutorialCanvas.UI.UIManager.slot.ToString() + TutorialCanvas.UI.UIManager.primorsec);
                        break;
                    }
            }
        }
    }
}
