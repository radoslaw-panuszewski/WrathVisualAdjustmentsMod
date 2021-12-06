using Kingmaker.Utility;
using ModMaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using TutorialCanvas.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VisualAdjustments;

public class FXUIHandler : MonoBehaviour
{
    public static FXUIHandler handler;
    public TextMeshProUGUI ListToggle;
    public TextMeshProUGUI ListCurrent;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI CurrentlyViewing;
    public TextMeshProUGUI ListAdd;
    public TextMeshProUGUI ListRemove;
    public TMP_InputField CurrentFXInput;
    public TMP_InputField AllFXInput;
    public GameObject content;
    public Transform contentcurrent;
    public static Dictionary<string, Button> currentFX;
    public static Dictionary<string, Button> allFX;
    public Transform ButtonForCloning;
    public static Kingmaker.UI.MVVM._PCView.ServiceWindows.Inventory.InventoryDollPCView dollPcView;
    public static bool loaded = false;

    public string selectedfx
    {
        get
        {
            return m_selectedfx;
        }
        set
        {
            Name.text = value;
            m_selectedfx = value;
        }
    }

    private static string m_selectedfx;
    public static Dictionary<string, Button> currentfxbuttons = new Dictionary<string, Button>();

    public void HandleUnitChangedOrUpdate()
    {
        try
        {
            m_WhiteOrBlackList = currentUnitPart.blackorwhitelist;
            if (listoroverride)
            {
                // Main.logger.Log("Start");
                foreach (var button in currentfxbuttons)//.Except(currentfxbuttons.Where(a => currentUnitPart.blackwhitelist.Select(b => b.Name).Contains(a.Key))))
                {
                    //UnityEngine.Object.DestroyImmediate(button.Value);
                    button.Value.SafeDestroy();
                    // button.Value.gameObject.DestroyImmediate(button.Value.gameObject,false);
                }
                currentfxbuttons.Clear();
                //  Main.logger.Log("DestroyedOld");
                foreach (var FX in currentUnitPart.blackwhitelistnew)
                {
                    if (!currentfxbuttons.ContainsKey(FX.Name))
                    {
                        var buttontoadd = UnityEngine.Object.Instantiate(ButtonForCloning);
                        var buttontoaddtext = buttontoadd.Find("TextB").GetComponent<TextMeshProUGUI>();
                        // buttontoaddtext.text = currentee.name;
                        buttontoaddtext.text = FX.Name; //UIManager.GetTextAndSetupCategory(FX.Value.Name);
                                                        //buttontoaddtext.text = FX.Name;
                                                        // buttontoaddtext.text = GetTextAndSetupCategory(currentee);
                        buttontoaddtext.color = Color.white;
                        buttontoadd.transform.SetParent(contentcurrent, false);
                        var buttontoaddbuttoncomponent = buttontoadd.GetComponent<Button>();
                        buttontoaddbuttoncomponent.onClick = new Button.ButtonClickedEvent();
                        buttontoaddbuttoncomponent.onClick.AddListener(new UnityAction(() =>
                        {
                            selectedfx = (buttontoaddtext.text);
                        }));
                        currentfxbuttons.Add(FX.Name, buttontoaddbuttoncomponent);
                    }
                }
            }
            else
            {
                //  Main.logger.Log("Start");
                foreach (var button in currentfxbuttons)//.Except(currentfxbuttons.Where(a => currentUnitPart.blackwhitelist.Select(b => b.Name).Contains(a.Key))))
                {
                    // UnityEngine.Object.DestroyImmediate(button.Value);
                    button.Value.SafeDestroy();
                }
                currentfxbuttons.Clear();
                //  Main.logger.Log("DestroyedOld");
                foreach (var FX in currentUnitPart.overrides)
                {
                    if (!currentfxbuttons.ContainsKey(FX.Name))
                    {
                        var buttontoadd = UnityEngine.Object.Instantiate(ButtonForCloning);
                        var buttontoaddtext = buttontoadd.Find("TextB").GetComponent<TextMeshProUGUI>();
                        // buttontoaddtext.text = currentee.name;
                        //buttontoaddtext.text = FX.Name;
                        buttontoaddtext.text = UIManager.GetTextAndSetupCategory(FX.Name);
                        // buttontoaddtext.text = GetTextAndSetupCategory(currentee);
                        buttontoaddtext.color = Color.white;
                        buttontoadd.transform.SetParent(contentcurrent, false);
                        var buttontoaddbuttoncomponent = buttontoadd.GetComponent<Button>();
                        buttontoaddbuttoncomponent.onClick = new Button.ButtonClickedEvent();
                        buttontoaddbuttoncomponent.onClick.AddListener(new UnityAction(() =>
                        {
                            selectedfx = (buttontoaddtext.text);
                        }));
                        currentfxbuttons.Add(FX.Name, buttontoaddbuttoncomponent);
                    }
                }
            }
        }
        catch (Exception e)
        {
            Main.logger.Log(e.ToString());
            foreach (var button in currentfxbuttons)
            {
                button.Value.SafeDestroy();
                //UnityEngine.Object.DestroyImmediate(button.Value);
            }
        }
        this.WhiteOrBlackList = currentUnitPart.blackorwhitelist;
        UIManager.data.RefreshBuffs();
        UIManager.data.SpawnOverrideBuffs();
    }

    public static VisualAdjustments.SaveHookerVAFX currentUnitPart
    {
        get
        {
            if (UIManager.hasUnitChanged() || m_currentUnitPart == null || UIManager.loaded)
            {
                var VisualInfo = VisualAdjustments.GlobalVisualInfo.Instance.ForCharacter(UIManager.data);
                m_currentUnitPart = VisualInfo.FXpart;
                // m_currentUnitPart = UIManager.data.Parts.Ensure<VisualAdjustments.UnitPartVAFX>();
                m_WhiteOrBlackList = m_currentUnitPart.blackorwhitelist;
                //listoroverride = m_currentUnitPart.blackorwhitelist;
            }
            return m_currentUnitPart;
        }
        set
        {
            m_currentUnitPart = value;
        }
    }

    public static SaveHookerVAFX m_currentUnitPart;
    private bool m_listoroverride;

    public bool WhiteOrBlackList
    {
        set
        {
            //currentUnitPart.blackorwhitelist = value;
            // HandleUnitChangedOrUpdate();
            m_WhiteOrBlackList = value;
            if (value)
            {
                if (listoroverride)
                {
                    CurrentlyViewing.text = "Current: Blacklist";
                }
                ListToggle.text = "Blacklist Mode";
                ListCurrent.text = "Blacklist";
                ListAdd.text = "Add to Blacklist";
                ListRemove.text = "Unblacklist";
            }
            else
            {
                if (listoroverride)
                {
                    CurrentlyViewing.text = "Current: Whitelist";
                }
                ListToggle.text = "Whitelist Mode";
                ListCurrent.text = "Whitelist";
                ListAdd.text = "Add to Whitelist";
                ListRemove.text = "Unwhitelist";
            }
        }
        get
        {
            return m_WhiteOrBlackList;
        }
    }

    public static bool m_WhiteOrBlackList;

    public bool listoroverride
    {
        get
        {
            return m_listoroverride;
        }
        set
        {
            //Set button text and shit

            if (value == true)
            {
                if (WhiteOrBlackList)
                {
                    CurrentlyViewing.text = "Current: Blacklist";
                }
                else
                {
                    CurrentlyViewing.text = "Current: Whitelist";
                }
            }
            else
            {
                CurrentlyViewing.text = "Current: Override";
            }
            m_listoroverride = value;
            HandleUnitChangedOrUpdate();
        }
    }

    public Dictionary<string, Button> AllFXButtons = new Dictionary<string, Button>();

    public void Awake()
    {
        // if (FXUIHandlerHandler.loaded) return;
        /*      FXUIHandlerHandler.ListToggle = ListToggle;
              FXUIHandlerHandler.ListCurrent = ListCurrent;
              FXUIHandlerHandler.Name = Name;
              FXUIHandlerHandler.CurrentlyViewing = CurrentlyViewing;
              FXUIHandlerHandler.ListAdd = ListAdd;
              FXUIHandlerHandler.ListRemove = ListRemove;
              FXUIHandlerHandler.CurrentFXInput = CurrentFXInput;
              FXUIHandlerHandler.AllFXInput = AllFXInput;
              FXUIHandlerHandler.content = content;
              FXUIHandlerHandler.contentcurrent = this.transform.Find("CurrentFXs/Scroll View/Viewport/Content");*/
        {
            try
            {
                handler = this;
                //var buttontoinstantiate = this.transform.parent.parent.Find("NewButton");
                //this.ButtonForCloning = buttontoinstantiate.gameObject;
                // var font = this.transform.parent.parent.parent.Find("Doll/DollTitle/TitleLabel").GetComponent<TextMeshProUGUI>().font;
                //   Main.logger.Log("AwakeFX");
                {
                    foreach (var fx in VisualAdjustments.EffectsManager.AllFX)
                    {
                        var buttontoadd = Instantiate(ButtonForCloning.transform);
                        //Main.logger.Log("Instantiated");
                        var buttontoaddtext = buttontoadd.Find("TextB").GetComponent<TextMeshProUGUI>();
                        // buttontoaddtext.text = GetTextAndSetupCategory(eename.Key);
                        // buttontoaddtext.text = fx.Key;
                        buttontoaddtext.text = UIManager.GetTextAndSetupCategory(fx.Key);
                        // Main.logger.Log("SetText");
                        // buttontoaddtext.text = eename.Key;
                        //buttontoaddtext.font = font;
                        //buttontoaddtext.color = Color.white;
                        buttontoadd.transform.SetParent(content.transform, false);
                        // Main.logger.Log("SetParent");
                        var buttontoaddbuttoncomponent = buttontoadd.GetComponent<Button>();
                        buttontoaddbuttoncomponent.onClick = new Button.ButtonClickedEvent();
                        buttontoaddbuttoncomponent.onClick.AddListener(new UnityAction(() =>
                        {
                            selectedfx = (buttontoaddtext.text);
                        }));
                        AllFXButtons.Add(fx.Key, buttontoaddbuttoncomponent);
                    }
                }
                {
                    // var placeholder = AllFXInput.gameObject.transform.Find("Text Area/Placeholder").GetComponent<TextMeshProUGUI>();
                    // placeholder.color = new Color((float)0.05, (float)0.05, (float)0.05, (float)0.6);
                }
                {
                    // var placeholder = CurrentFXInput.gameObject.transform.Find("Text Area/Placeholder").GetComponent<TextMeshProUGUI>();
                    // placeholder.color = new Color((float)0.05, (float)0.05, (float)0.05, (float)0.6);
                }
                // FXUIHandlerHandler.loaded = true;
            }
            catch (Exception e)
            {
                Main.logger.Log(e.ToString());
            }
        }
    }

    public void HandleListToggle(bool button)
    {
        listoroverride = !listoroverride;
    }

    public void RefreshCurrentList()
    {
    }

    public void temp()
    {
        {
            // setup filter fields
            /*{
                // All EE's
                var AllEETxtField = this.transform.Find("Menus/InventoryViewWindow/AllEEs/FilterArea/InputField (TMP)").GetComponent<TMP_InputField>();
                var placeholder = AllEETxtField.gameObject.transform.Find("Text Area/Placeholder").GetComponent<TextMeshProUGUI>();
                placeholder.color = new Color((float)0.05, (float)0.05, (float)0.05, (float)0.6);
                placeholder.fontStyle = FontStyles.Italic;
                AllEETxtField.onValueChanged = new TMP_InputField.OnChangeEvent();
                AllEETxtField.onValueChanged.AddListener(HandleFilterChangedAll);

                // Current EE's
                var CurrentEETxtField = this.transform.Find("Menus/InventoryViewWindow/CurrentEEs/FilterArea/InputField (TMP)").GetComponent<TMP_InputField>();
                var placeholdercurrent = CurrentEETxtField.gameObject.transform.Find("Text Area/Placeholder").GetComponent<TextMeshProUGUI>();
                placeholdercurrent.color = new Color((float)0.05, (float)0.05, (float)0.05, (float)0.6);
                placeholdercurrent.fontStyle = FontStyles.Italic;
                CurrentEETxtField.onValueChanged = new TMP_InputField.OnChangeEvent();
                CurrentEETxtField.onValueChanged.AddListener(HandleFilterChangedCurrent);
            }
            // Reset Button
            {
                var resetbutton = this.transform.Find("Menus/InventoryViewWindow/CurrentEEs/ResetButton");
                resetbutton.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
                resetbutton.GetComponent<Button>().onClick.AddListener(resetEEPart);
                var resetbuttonText = resetbutton.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
                resetbuttonText.color = Color.white;
            }*/
            // button.gameObject.AddComponent<DraggableWindow>(); //Add draggable windows component allowing the window to be dragged when the button is pressed down

            //_text = this.transform.Find("InfoWindow").transform.Find("AddRemove").Find("AddRemoveText").GetComponent<TextMeshProUGUI>(); //Find the text component so we can update later.
            //Mod.Log(_text.ToString());
        }
    }

    public void HandleFilterChangedAll(string val)
    {
        var value = AllFXInput.text;
        bool hassplitter = value.Contains(" ");
        var splitstring = value.Split(UIManager.splitchars);
        bool isempty = value.IsNullOrEmpty();
        foreach (var eelbutton in AllFXButtons)
        {
            if (hassplitter)
            {
                if (splitstring.All(a => eelbutton.Key.Contains(a, StringComparison.OrdinalIgnoreCase)))
                {
                    eelbutton.Value.gameObject.SetActive(true);
                }
                else
                {
                    eelbutton.Value.gameObject.SetActive(false);
                }
            }
            else
            {
                if (isempty)
                {
                    eelbutton.Value.gameObject.SetActive(true);
                }
                else if (eelbutton.Key.Contains(value, StringComparison.OrdinalIgnoreCase))
                {
                    eelbutton.Value.gameObject.SetActive(true);
                }
                else
                {
                    eelbutton.Value.gameObject.SetActive(false);
                }
            }
        }
    }

    // public BlueprintExplorer.FuzzyMatchContext<List<string>> sdasddasdsa;
    public void HandleFilterChangedCurrent(string val)
    {
        var value = CurrentFXInput.text;
        //var value = val;
        bool hassplitter = value.Contains(" ");
        var splitstring = value.Split(UIManager.splitchars);
        foreach (var eelbutton in currentfxbuttons)
        {
            if (hassplitter)
            {
                if (splitstring.All(a => eelbutton.Key.Contains(a, StringComparison.OrdinalIgnoreCase)) && !eelbutton.Value.gameObject.activeSelf)
                {
                    eelbutton.Value.gameObject.SetActive(true);
                }
                else if (eelbutton.Value.gameObject.activeSelf)
                {
                    eelbutton.Value.gameObject.SetActive(false);
                }
            }
            else
            {
                if (value.IsNullOrEmpty() && !eelbutton.Value.gameObject.activeSelf)
                {
                    eelbutton.Value.gameObject.SetActive(true);
                }
                else if (eelbutton.Key.Contains(value, StringComparison.OrdinalIgnoreCase) && !eelbutton.Value.gameObject.activeSelf)
                {
                    eelbutton.Value.gameObject.SetActive(true);
                }
                else if (eelbutton.Value.gameObject.activeSelf)
                {
                    eelbutton.Value.gameObject.SetActive(false);
                }
            }
        }
    }

    public void HandleModeChanged()
    {
        WhiteOrBlackList = !WhiteOrBlackList;
        currentUnitPart.blackorwhitelist = WhiteOrBlackList;
    }

    public void ResetButton()
    {
        if (listoroverride)
        {
            currentUnitPart.blackwhitelistnew.Clear();
        }
        else
        {
            currentUnitPart.overrides.Clear();
        }
        HandleUnitChangedOrUpdate();
    }

    public void CurrentView()
    {
    }

    public void AddOverride()
    {
        if (!(currentUnitPart).overrides.Any(a => a.Name == selectedfx))
        {
            (currentUnitPart).overrides.Add(new FXInfo(EffectsManager.AllFX[UIManager.FilteredAndUnfilteredEEName[selectedfx]], selectedfx));
        }
        HandleUnitChangedOrUpdate();
    }

    public void RemoveOverride()
    {
        (currentUnitPart).overrides.RemoveAll(a => a.Name == UIManager.FilteredAndUnfilteredEEName[selectedfx]);
        HandleUnitChangedOrUpdate();
    }

    public void AddList()
    {
        try
        {
            //Main.logger.Log(selectedfx);
            var fxinf = new FXInfo(EffectsManager.AllFX[UIManager.FilteredAndUnfilteredEEName[selectedfx]], selectedfx);
            // Main.logger.Log(fxinf.ToString()+"  "+fxinf.Name+"  "+fxinf.AssetID);
            if (!(currentUnitPart).blackwhitelistnew.Any(a => a.Name == selectedfx))
            {
                (currentUnitPart).blackwhitelistnew.Add(new FXInfo(EffectsManager.AllFX[UIManager.FilteredAndUnfilteredEEName[selectedfx]], selectedfx));
                HandleUnitChangedOrUpdate();
            }
        }
        catch (Exception e)
        {
            Main.logger.Log(e.ToString());
        }
    }

    public void RemoveList()
    {
        (currentUnitPart).blackwhitelistnew.RemoveAll(a => a.Name == selectedfx);
        HandleUnitChangedOrUpdate();
    }

    public void SwitchMode()
    {
        listoroverride = !listoroverride;
    }
}