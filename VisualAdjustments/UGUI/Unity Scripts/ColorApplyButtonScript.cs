using System.Collections.Generic;
using UnityEngine;

public class ColorApplyButtonScript : MonoBehaviour
{
    //public static Color cols;
    // Start is called before the first frame update
    public bool primorsec = true;

    public FlexibleColorPicker fcp;
    public GameObject tohide;
    public List<GameObject> toshow;
    public TMPro.TextMeshProUGUI txt;
    public TutorialCanvas.UI.UIManager uimanager;

    public void ApplyColor(bool col)
    {
        VisualAdjustments.Main.logger.Log("ColSet");
        uimanager.setCustomColorToEEPart(fcp.color, primorsec);
    }

    public void SetPrimOrSec(bool primorsecin)
    {
        primorsec = !primorsec;
        if (primorsec == true)
        {
            txt.text = "Primary";
        }
        else
        {
            txt.text = "Secondary";
        }
    }

    public void HideShowColor(bool h)
    {
        tohide.SetActive(!tohide.active);
        foreach (var ToShow in toshow)
        {
            ToShow.SetActive(!ToShow.active);
        }
    }
}