using DG.Tweening;
using Kingmaker;
using System;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.ResourceLinks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
//using TutorialCanvas.Utilities;
//using static TutorialCanvas.Main;
using Kingmaker.UI.Selection;
using Kingmaker.Visual.CharacterSystem;
using Owlcat.Runtime.Core.Utils;
using TMPro;
using VisualAdjustments;
using TutorialCanvas.Utilities;
using System.Linq;
using System.Collections.Generic;
using static VisualAdjustments.EELpickerUI;
using VisualAdjustments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Cheats;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Globalmap.View;
using Kingmaker.ResourceLinks;
using Kingmaker.UI.MVVM._PCView.ServiceWindows.Inventory;
using Kingmaker.UI.MVVM._VM.ServiceWindows.Inventory;
using Kingmaker.UI.ServiceWindow;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Utility;
using Kingmaker.Visual.CharacterSystem;
using ModKit.Utility;
using ModMaker.Utility;
using Steamworks;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityModManagerNet;

namespace TutorialCanvas.UI
{
    /// <summary>
    ///  MAKE THE EES MORE READABLE
    /// </summary>
    public class overrideUI
    {
        public Dictionary<BlueprintRef, string> EELs;
        public BlueprintRef current;
    }
    internal class UIManager : MonoBehaviour
    {
        public void handleOverrideSelected(string Key,ref BlueprintRef bpref)
        {
            settings = VisualAdjustments.Main.settings.GetCharacterSettings(data);
        }
        public static string getGender(EquipmentEntity ee)
        {
            if (ee.name.Contains("_M_")) return "Male";
            if (ee.name.Contains("_F_")) return "Female";
            return "N/A";
        }
        public static string getRace(EquipmentEntity ee)
        {
            if (ee.name.Contains("_HM")) return "Human";
            if (ee.name.Contains("_AA")) return "Aasimar";
            if (ee.name.Contains("_HE")) return "Half-Elf";
            if (ee.name.Contains("_MM")) return "Mongrel";
            if (ee.name.Contains("_SU")) return "Succubus";
            if (ee.name.Contains("_DW")) return "Dwarf";
            if (ee.name.Contains("_TL")) return "Tiefling";
            if (ee.name.Contains("_EL")) return "Elf";
            if (ee.name.Contains("_KT")) return "Kitsune";
            if (ee.name.Contains("_GN")) return "Gnome";
            if (ee.name.Contains("_OD")) return "Oread";
            if (ee.name.Contains("_HL")) return "Halfling";
            if (ee.name.Contains("_DH")) return "Dhampir";
            if (ee.name.Contains("_HO")) return "Half-Orc";
            if (ee.name.Contains("_ZB")) return "Zombie";
            if (ee.name.Contains("_CB")) return "Cambion";
            return "N/A";
        }

        void resetEEPart()
        {
            var part = data.Parts.Get<UnitPartVAEELs>();
            if (part != null)
            {
                part.EEToAdd.Clear();
                part.EEToRemove.Clear();
            }
            haschanged = true;
            CharacterManager.RebuildCharacter(data);
            CharacterManager.UpdateModel(data.View);
            UpdatePreview();
            foreach (var button in currentEELButtons.Except(a => data.View.CharacterAvatar.EquipmentEntities.Contains(c => c.name == a.Key)))
            {
                button.Value.SafeDestroy();
            }
            foreach (var currentee in data.View.CharacterAvatar.EquipmentEntities)
            {
                if (!currentEELButtons.ContainsKey(currentee.name))
                {
                    var buttontoadd = Instantiate(buttonTemplate);
                    var buttontoaddtext = buttontoadd.Find("TextB").GetComponent<TextMeshProUGUI>();
                    buttontoaddtext.text = currentee.name;
                    buttontoaddtext.font = font;
                    buttontoaddtext.color = Color.white;
                    buttontoadd.transform.SetParent(contentCurrentEE.transform, false);
                    var buttontoaddbuttoncomponent = buttontoadd.GetComponent<Button>();
                    buttontoaddbuttoncomponent.onClick = new Button.ButtonClickedEvent();
                    buttontoaddbuttoncomponent.onClick.AddListener(new UnityAction(() =>
                    {
                        HandleEEClicked(buttontoaddtext.text);
                    }));
                    currentEELButtons.Add(currentee.name, buttontoaddbuttoncomponent);
                }
            }
        }
        void removeFromRemoveEEPart(EquipmentEntity ee)
        {
            var component = m_Data.Parts.Get<UnitPartVAEELs>();
            if (component == null)
            {
                component = m_Data.Parts.Add<UnitPartVAEELs>();
            }

            if (component.EEToRemove.Contains(EquipmentResourcesManager.AllEEL[ee.name]))
            {
                component.EEToRemove.Remove(EquipmentResourcesManager.AllEEL[ee.name]);
            }

        }
        void addToRemoveEEPart(EquipmentEntity ee)
        {
            var component = m_Data.Parts.Get<UnitPartVAEELs>();
            if (component == null)
            {
                component = m_Data.Parts.Add<UnitPartVAEELs>();
            }

            if (!component.EEToRemove.Contains(EquipmentResourcesManager.AllEEL[ee.name]))
            {
                component.EEToRemove.Add(EquipmentResourcesManager.AllEEL[ee.name]);
            }
        }
        void addToAddEEPart(EquipmentEntity ee, int primary, int secondary)
        {
            var component = m_Data.Parts.Get<UnitPartVAEELs>();
            if (component == null)
            {
                component = m_Data.Parts.Add<UnitPartVAEELs>();
            }

            if (!component.EEToAdd.Contains(a => a.AssetID == EquipmentResourcesManager.AllEEL[ee.name]))
            {
                component.EEToAdd.Add(new EEStorage(EquipmentResourcesManager.AllEEL[ee.name], primary,
                    secondary));
                //Main.logger.Log("add" + ee.name + EquipmentResourcesManager.AllEEL[ee.name]);
            }
        }
        void removeFromAddEEpart(EquipmentEntity ee)
        {
            var component = m_Data.Parts.Get<UnitPartVAEELs>();
            if (component == null)
            {
                component = m_Data.Parts.Add<UnitPartVAEELs>();
            }

            if (component.EEToAdd.Contains(a => a.AssetID == EquipmentResourcesManager.AllEEL[ee.name]))
            {
                component.EEToAdd.Remove(a => a.AssetID == EquipmentResourcesManager.AllEEL[ee.name]);
            }
        }
        static void setColorAddEEPart(EquipmentEntity ee, int primaryIndex, int secondaryIndex)
        {
            var component = m_Data.Parts.Get<UnitPartVAEELs>();
            if (component == null)
            {
                component = m_Data.Parts.Add<UnitPartVAEELs>();
            }

            if (component.EEToAdd.Contains(a => a.AssetID == EquipmentResourcesManager.AllEEL[ee.name]))
            {
                var eewithcolor = component.EEToAdd.First(a => a.AssetID == EquipmentResourcesManager.AllEEL[ee.name]);
                eewithcolor.PrimaryIndex = primaryIndex;
                eewithcolor.SecondaryIndex = primaryIndex;
            }
            else
            {
                // Main.logger.Log("coloradd" + ee.name + EquipmentResourcesManager.AllEEL[ee.name]);
                component.EEToAdd.Add(new EEStorage(EquipmentResourcesManager.AllEEL[ee.name], primaryIndex, primaryIndex));
            }

        }

        int getColorFromPart(EquipmentEntity ee,bool primorsec)
        {
            if (primorsec)
            {
                var Primindx = data.View.CharacterAvatar.GetPrimaryRampIndex(ee);
                if (Primindx >= 0) return Primindx;
            }
            else
            {
                var Secondindx = data.View.CharacterAvatar.GetSecondaryRampIndex(ee);
                if (Secondindx >= 0) return Secondindx;
            }
            return 0;
        }
        bool hasUnitChanged()
        {
            if (m_Data != null && m_Data.CharacterName != data.CharacterName)
            {
                return true;
            }

            return false;
        }
        public const string Source = "TutorialCanvas";
        public static Transform window;
        private TextMeshProUGUI _text;
        private static bool haschanged = true;
        private static InventoryDollPCView dollPcView;

        public static UnitEntityData data
        {
            get
            {
                var newData = Kingmaker.UI.Common.UIUtility.GetCurrentCharacter();
                if (m_Data != newData)
                {
                    m_Data = newData;
                     haschanged = true;
                }
                // m_Data = Kingmaker.UI.Common.UIUtility.GetCurrentCharacter();
                return m_Data;
            }
            set
            {
                m_Data = value;
            }
        }

        private static void UpdatePreview()
        {
            if (window.parent.Find("Doll").TryGetComponent<InventoryDollPCView>(out dollPcView))
            {
                try
                {
                    // fix this
                    //dollPcView = window.parent.Find("Doll").GetComponent<InventoryDollPCView>();
                  //  dollPcView.OnHide();
                    //dollPcView.BindViewImplementation();
                   // dollPcView.OnShow();
                    //dollPcView.RefreshView();
                    data.View.CharacterAvatar.IsDirty = true;
                }
                catch (Exception e)
                {
                    VisualAdjustments.Main.logger.Log(e.StackTrace);
                    throw;
                }
            }
        }

        private static Slider primSlider;
        public static int primindex
        {
            get
            {
                // check if current Selected EE is same as previous
                return m_Primxindex;
            }
            set
            {
                value = (int)Mathf.Clamp((float)value, 0, selectedEntity.PrimaryRamps.Count);
                //set color when updating index/ update buttons
                if (data.View.CharacterAvatar.EquipmentEntities.Contains(selectedEntity))
                {
                    data.View.CharacterAvatar.SetPrimaryRampIndex(selectedEntity, value);
                    //data.View.CharacterAvatar.IsAtlasesDirty = true;
                    primSlider.value = value;
                    setColorAddEEPart(selectedEntity, value, secondindex);
                    m_Primxindex = value;
                }
                PrimaryIndex.text = value.ToString();
            }
        }
        public static int m_Primxindex = 0;
        private static TMP_InputField PrimaryIndex;
        private static Slider secondSlider;
        public static int secondindex
        {
            get
            {
                // check if current Selected EE is same as previous
                return m_SecondaryIndex;
            }
            set
            {
                value = (int)Mathf.Clamp((float)value, 0, selectedEntity.SecondaryRamps.Count);
                //set color when updating index/ update buttons
                if (data.View.CharacterAvatar.EquipmentEntities.Contains(selectedEntity))
                {
                    data.View.CharacterAvatar.SetSecondaryRampIndex(selectedEntity, value);
                    //data.View.CharacterAvatar.IsAtlasesDirty = true;
                    secondSlider.value = value;
                    setColorAddEEPart(selectedEntity, primindex, value);
                    
                    m_SecondaryIndex = value;
                }
                SecondaryIndex.text = value.ToString();
            }
        }
        public static int m_SecondaryIndex = 0;
        private static TMP_InputField SecondaryIndex;
        //public static int secondindex
        //{

        // }
        private TextMeshProUGUI Name;
        private TextMeshProUGUI Race;
        private TextMeshProUGUI Sex;
        private GameObject AddRemoveButton;
        private static UnitEntityData m_Data;
        private string selectedstring = "";
        private TMP_FontAsset font;
        private Transform contentAllEE;
        private Transform contentCurrentEE;
        private static Transform buttonTemplate;
        public Dictionary<string, Button> currentEELButtons = new Dictionary<string, Button>();
        public Dictionary<string, GameObject> allEELButtons = new Dictionary<string, GameObject>();
        public static Dictionary<Toggle,bool> hidebuttons = new Dictionary<Toggle, bool>();
        VisualAdjustments.Settings.CharacterSettings settings = VisualAdjustments.Main.settings.GetCharacterSettings(data);
        public static UIManager CreateObject()
        {
            //This is the method that get's called when it is time to create the UI.  This happens every time a scene is loaded.

            try
            {
                VisualAdjustments.Main.logger.Log("createdobject");
                if (!Game.Instance.UI.Canvas) return null;
                if (!BundleManger.IsLoaded(Source)) throw new NullReferenceException();

                //
                //Attempt to get the wrath objects needed to build the UI
                //
                var staticCanvas = Game.Instance.UI.Canvas.RectTransform.Find("ServiceWindowsPCView").Find("InventoryView").Find("Inventory");
                //var background = staticCanvas.Find("HUDLayout/CombatLog_New/Background/Background_Image").GetComponent<Image>(); //Using the path we found earlier we get the sprite component 


                //get font

                //
                //Attempt to get the objects loaded from the AssetBundles and build the window.
                //
                 window = Instantiate(BundleManger.LoadedPrefabs[Source].transform.Find("Canvas")); //We ditch the TutorialCanvas as talked about in the Wiki, we will attach it to a different parent
                //window.localScale = new Vector3((float)0.76, (float)0.76, (float)0.76);
                
                window.SetParent(staticCanvas, false); //Attaches our window to the static canvas
                window.SetAsLastSibling(); //Our window will always be under other UI elements as not to interfere with the game. Top of the list has the lowest priority
                                            // if(SettingsWrapper.Reuse) window.Find("Background").GetComponent<Image>().sprite = background.sprite; //Sets the background sprite to the one used in CombatLog_New
                window.localPosition = new Vector3(0, 27, 0);
                window.localScale = new Vector3((float)0.76, (float)0.76, (float)0.76);
                window.Find("Menus").gameObject.active = false;
               // content = ;

                return window.gameObject.AddComponent<UIManager>(); //This adds this class as a component so it can handle events, button clicks, awake, update, etc.
            }
            catch (Exception ex)
            {
                VisualAdjustments.Main.logger.Error(ex.StackTrace);
            }
            return new UIManager();
        }

        private void Awake()
        {
            //This is a unity message that runs once when the script activates (Check Unity documenation for the differences between Start() and Awake()

            //
            // Setup the listeners when the script starts
            //
            VisualAdjustments.Main.logger.Log("awake");


            // Setup scrollview content
            contentAllEE = this.transform.Find("Menus/InventoryViewWindow/AllEEs/Scroll View/Viewport/Content");
            contentCurrentEE = this.transform.Find("Menus/InventoryViewWindow/CurrentEEs/Scroll View/Viewport/Content");
            VisualAdjustments.Main.logger.Log("awake2");
            buttonTemplate = this.transform.Find("NewButton");
            font = this.transform.parent.Find("Doll/DollTitle/TitleLabel").GetComponent<TextMeshProUGUI>().font;
            foreach (var txt in this.transform.GetComponentsInChildren<TextMeshProUGUI>(true))
            {
                txt.font = font;
                txt.color = Color.black;
            }
            foreach (var eename in VisualAdjustments.EquipmentResourcesManager.AllEEL)
            {
                var buttontoadd = Instantiate(buttonTemplate);
                var buttontoaddtext = buttontoadd.Find("TextB").GetComponent<TextMeshProUGUI>();
                buttontoaddtext.text = eename.Key;
                buttontoaddtext.font = font;
                buttontoaddtext.color = Color.white;
                buttontoadd.transform.SetParent(contentAllEE.transform,false);
                var buttontoaddbuttoncomponent = buttontoadd.GetComponent<Button>();
                buttontoaddbuttoncomponent.onClick = new Button.ButtonClickedEvent();
                buttontoaddbuttoncomponent.onClick.AddListener(new UnityAction(() => { HandleEEClicked(buttontoaddtext.text);
                }));
                allEELButtons.Add(eename.Key,buttontoadd.gameObject);
            }
            m_Data = Kingmaker.UI.Common.UIUtility.GetCurrentCharacter();

            //switch font
            //var texts = ;


            

            // setup UI toggles
            {
                //primary toggle
                var button = this.transform.Find("ToggleButton").GetComponent<Button>();
                button.onClick = new Button.ButtonClickedEvent();
                button.onClick.AddListener(new UnityAction(HandleButtonClick));
                //Secondary toggles
                {
                    var UISelectionGrid = this.transform.Find("UISelectionGrid");

                    //Outfitdoll
                    var OutfitDoll = UISelectionGrid.Find("OutfitDoll").GetComponent<Button>();
                    OutfitDoll.onClick = new Button.ButtonClickedEvent();
                    OutfitDoll.onClick.AddListener(new UnityAction(() =>
                    {
                        HandleUIToggle(UISelectionGrid.Find("OutfitDoll/Text (TMP)").GetComponent<TextMeshProUGUI>().text);
                    }));

                    //Overrides
                    var Overrides = UISelectionGrid.Find("Overrides").GetComponent<Button>();
                    Overrides.onClick = new Button.ButtonClickedEvent();
                    Overrides.onClick.AddListener(new UnityAction(() =>
                    {
                        HandleUIToggle(UISelectionGrid.Find("Overrides/Text (TMP)").GetComponent<TextMeshProUGUI>().text);
                    }));

                    //EEPicker
                    var EEPicker = UISelectionGrid.Find("EEPicker").GetComponent<Button>();
                    EEPicker.onClick = new Button.ButtonClickedEvent();
                    EEPicker.onClick.AddListener(new UnityAction(() =>
                    {
                        HandleUIToggle(UISelectionGrid.Find("EEPicker/Text (TMP)").GetComponent<TextMeshProUGUI>().text);
                    }));

                    //Equipment
                    var Equipment = UISelectionGrid.Find("Equipment").GetComponent<Button>();
                    Equipment.onClick = new Button.ButtonClickedEvent();
                    Equipment.onClick.AddListener(new UnityAction(() =>
                    {
                        HandleUIToggle(UISelectionGrid.Find("Equipment/Text (TMP)").GetComponent<TextMeshProUGUI>().text);
                    }));
                    UISelectionGrid.gameObject.SetActive(false);



                }
            }
            // setup EE Picker
            {
                Name = this.transform.Find("Menus/InventoryViewWindow/EEInfo/TextboxesValues/Name")
                    .GetComponent<TextMeshProUGUI>();
                Race = this.transform.Find("Menus/InventoryViewWindow/EEInfo/TextboxesValues/Race")
                    .GetComponent<TextMeshProUGUI>();
                Sex = this.transform.Find("Menus/InventoryViewWindow/EEInfo/TextboxesValues/Sex")
                    .GetComponent<TextMeshProUGUI>();
                AddRemoveButton = this.transform.Find("Menus/InventoryViewWindow/AddRemove/Button/ButtonText").gameObject;

                var AddRemoveButtonButton = this.transform.Find("Menus/InventoryViewWindow/AddRemove/Button").GetComponent<Button>();
                AddRemoveButtonButton.onClick = new Button.ButtonClickedEvent();
                AddRemoveButtonButton.onClick.AddListener(new UnityAction(() =>
                {
                    addRemoveButton();
                }));


                //Primary Color Picker
                {
                    PrimaryIndex = this.transform.Find("Menus/InventoryViewWindow/EEInfo/ColorSelectors/Primary/InputFieldIndex").GetComponent<TMP_InputField>();
                    PrimaryIndex.onValueChanged = new TMP_InputField.OnChangeEvent();
                    PrimaryIndex.onValueChanged.AddListener((string val) =>
                    {
                        handleSliderInputField(val, true);
                    });
                    PrimaryIndex.textComponent.color = Color.white;
                    var IncreaseButton = this.transform.Find("Menus/InventoryViewWindow/EEInfo/ColorSelectors/Primary/PositiveIncrement").GetComponent<Button>();
                    var DecreaseButton = this.transform.Find("Menus/InventoryViewWindow/EEInfo/ColorSelectors/Primary/NegativeIncrement").GetComponent<Button>();
                    IncreaseButton.onClick = new Button.ButtonClickedEvent();
                    IncreaseButton.onClick.AddListener(new UnityAction(() =>
                    {
                        increasePrimaryButton();
                        UpdatePreview();
                    }));
                    DecreaseButton.onClick = new Button.ButtonClickedEvent();
                    DecreaseButton.onClick.AddListener(new UnityAction(() =>
                    {
                        decreasePrimaryButton();
                        UpdatePreview();
                    }));
                    primSlider = this.transform.Find("Menus/InventoryViewWindow/EEInfo/ColorSelectors/Primary/Slider").GetComponent<Slider>();
                    primSlider.onValueChanged = new Slider.SliderEvent();
                    primSlider.onValueChanged.AddListener(new UnityAction<float>((float value) =>
                    {
                        primindex = (int)value;
                        UpdatePreview();
                    }));
                }
                //Secondary Color Picker
                {
                    SecondaryIndex = this.transform.Find("Menus/InventoryViewWindow/EEInfo/ColorSelectors/Secondary/InputFieldIndex").GetComponent<TMP_InputField>();
                    SecondaryIndex.onValueChanged = new TMP_InputField.OnChangeEvent();
                    SecondaryIndex.onValueChanged.AddListener((string val) =>
                    {
                        handleSliderInputField(val,false);
                    });
                    SecondaryIndex.textComponent.color = Color.white;
                    var IncreaseButton = this.transform.Find("Menus/InventoryViewWindow/EEInfo/ColorSelectors/Secondary/PositiveIncrement").GetComponent<Button>();
                    var DecreaseButton = this.transform.Find("Menus/InventoryViewWindow/EEInfo/ColorSelectors/Secondary/NegativeIncrement").GetComponent<Button>();
                    IncreaseButton.onClick = new Button.ButtonClickedEvent();
                    IncreaseButton.onClick.AddListener(new UnityAction(() =>
                    {
                        increaseSecondaryButton();
                        UpdatePreview();
                    }));
                    DecreaseButton.onClick = new Button.ButtonClickedEvent();
                    DecreaseButton.onClick.AddListener(new UnityAction(() =>
                    {
                        decreaseSecondaryButton();
                        UpdatePreview();
                    }));
                    secondSlider = this.transform.Find("Menus/InventoryViewWindow/EEInfo/ColorSelectors/Secondary/Slider").GetComponent<Slider>();
                    secondSlider.onValueChanged = new Slider.SliderEvent();
                    secondSlider.onValueChanged.AddListener(new UnityAction<float>((float value) =>
                    {
                        secondindex = (int)value;
                        UpdatePreview();
                    }));
                }
                // setup filter fields
                {
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
                }
                // button.gameObject.AddComponent<DraggableWindow>(); //Add draggable windows component allowing the window to be dragged when the button is pressed down

                //_text = this.transform.Find("InfoWindow").transform.Find("AddRemove").Find("AddRemoveText").GetComponent<TextMeshProUGUI>(); //Find the text component so we can update later.
                //Mod.Log(_text.ToString());
            }
            // Setup Outfit/Doll
            {

            }
            //Setup Overrides??
            {

            }
            //Setup Equipment/Overrides
            {
                //hidebuttons = this.transform.Find("Menus/HideEquipment/RightSide/Toggles").Children().Select(a => a.gameObject.GetComponent<Toggle>()).ToDictionary();
                foreach (var buttontransform in this.transform.Find("Menus/HideEquipment/RightSide/Toggles").Children().Select(a => a.gameObject.GetComponent<Toggle>()))
                {
                    var buttontoggle = buttontransform;
                    buttontoggle.onValueChanged = new Toggle.ToggleEvent();

                    var buttontext = buttontransform.transform.Find("Label").GetComponent<TextMeshProUGUI>();
                    if (settings == null) settings = VisualAdjustments.Main.settings.GetCharacterSettings(data);
                    switch (buttontext.text)
                    {
                        case "Hide Cap":
                            buttontoggle.onValueChanged.AddListener((bool state) =>
                            {
                                handleHideEquipment(state, ref settings.hideCap);
                            });
                            buttontoggle.isOn = settings.hideCap;
                            hidebuttons.Add(buttontoggle,settings.hideCap);
                            break;
                        case "Hide Helmet":
                            buttontoggle.onValueChanged.AddListener((bool state) =>
                            {
                                handleHideEquipment(state, ref settings.hideHelmet);
                            });
                            buttontoggle.isOn = settings.hideHelmet;
                            hidebuttons.Add(buttontoggle, settings.hideHelmet);
                            break;
                        case "Hide Glasses":
                            buttontoggle.onValueChanged.AddListener((bool state) =>
                            {
                                handleHideEquipment(state, ref settings.hideGlasses);
                            });
                            buttontoggle.isOn = settings.hideGlasses;
                            hidebuttons.Add(buttontoggle, settings.hideGlasses);
                            break;
                        case "Hide Shirt":
                            buttontoggle.onValueChanged.AddListener((bool state) =>
                            {
                                handleHideEquipment(state, ref settings.hideShirt);
                            });
                            buttontoggle.isOn = settings.hideShirt;
                            hidebuttons.Add(buttontoggle, settings.hideShirt);
                            break;
                        case "Hide Class Gear":
                            buttontoggle.onValueChanged.AddListener((bool state) =>
                            {
                                handleHideEquipment(state, ref settings.hideClassCloak);
                            });
                            buttontoggle.isOn = settings.hideClassCloak;
                            hidebuttons.Add(buttontoggle, settings.hideClassCloak);
                            break;
                        case "Hide Cloak":
                            buttontoggle.onValueChanged.AddListener((bool state) =>
                            {
                                handleHideEquipment(state, ref settings.hideItemCloak);
                            });
                            buttontoggle.isOn = settings.hideItemCloak;
                            hidebuttons.Add(buttontoggle, settings.hideItemCloak);
                            break;
                        case "Hide Armor":
                            buttontoggle.onValueChanged.AddListener((bool state) =>
                            {
                                handleHideEquipment(state, ref settings.hideArmor);
                            });
                            buttontoggle.isOn = settings.hideArmor;
                            hidebuttons.Add(buttontoggle, settings.hideArmor);
                            break;
                        case "Hide Bracers":
                            buttontoggle.onValueChanged.AddListener((bool state) =>
                            {
                                handleHideEquipment(state, ref settings.hideBracers);
                            });
                            buttontoggle.isOn = settings.hideBracers;
                            hidebuttons.Add(buttontoggle, settings.hideBracers);
                            break;
                        case "Hide Gloves":
                            buttontoggle.onValueChanged.AddListener((bool state) =>
                            {
                                handleHideEquipment(state, ref settings.hideGloves);
                            });
                            buttontoggle.isOn = settings.hideGloves;
                            hidebuttons.Add(buttontoggle, settings.hideGloves);
                            break;
                        case "Hide Boots":
                            buttontoggle.onValueChanged.AddListener((bool state) =>
                            {
                                handleHideEquipment(state, ref settings.hideBoots);
                            });
                            buttontoggle.isOn = settings.hideBoots;
                            hidebuttons.Add(buttontoggle, settings.hideBoots);
                            break;
                        case "Hide Weapons":
                            buttontoggle.onValueChanged.AddListener((bool state) =>
                            {
                                handleHideEquipment(state, ref settings.hideWeapons);
                            });
                            buttontoggle.isOn = settings.hideWeapons;
                            hidebuttons.Add(buttontoggle, settings.hideWeapons);
                            break;
                        case "Hide Sheaths":
                            buttontoggle.onValueChanged.AddListener((bool state) =>
                            {
                                handleHideEquipment(state, ref settings.hideSheaths);
                            });
                            buttontoggle.isOn = settings.hideSheaths;
                            hidebuttons.Add(buttontoggle, settings.hideSheaths);
                            break;
                        case "Hide Belt Items":
                            buttontoggle.onValueChanged.AddListener((bool state) =>
                            {
                                handleHideEquipment(state, ref settings.hideBeltSlots);
                            });
                            buttontoggle.isOn = settings.hideBeltSlots;
                            hidebuttons.Add(buttontoggle, settings.hideBeltSlots);
                            break;
                        case "Hide Quiver":
                            buttontoggle.onValueChanged.AddListener((bool state) =>
                            {
                                handleHideEquipment(state, ref settings.hidequiver);
                            });
                            buttontoggle.isOn = settings.hidequiver;
                            hidebuttons.Add(buttontoggle, settings.hidequiver);
                            break;
                        case "Hide Enchantments":
                            buttontoggle.onValueChanged.AddListener((bool state) =>
                            {
                                handleHideEquipment(state, ref settings.hideWeaponEnchantments);
                            });
                            buttontoggle.isOn = settings.hideWeaponEnchantments;
                            hidebuttons.Add(buttontoggle, settings.hideWeaponEnchantments);
                            break;
                        case "Hide Wings":
                            buttontoggle.onValueChanged.AddListener((bool state) =>
                            {
                                handleHideEquipment(state, ref settings.hideWings);
                            });
                            buttontoggle.isOn = settings.hideWings;
                            hidebuttons.Add(buttontoggle, settings.hideWings);
                            break;
                        case "Hide Horns":
                            buttontoggle.onValueChanged.AddListener((bool state) =>
                            {
                                handleHideEquipment(state, ref settings.hideHorns);
                            });
                            buttontoggle.isOn = settings.hideHorns;
                            hidebuttons.Add(buttontoggle, settings.hideHorns);
                            break;
                        case "Hide Tail":
                            buttontoggle.onValueChanged.AddListener((bool state) =>
                            {
                                handleHideEquipment(state, ref settings.hideTail);
                            });
                            buttontoggle.isOn = settings.hideTail;
                            hidebuttons.Add(buttontoggle, settings.hideTail);
                            break;
                        case "Hide Mythic":
                            buttontoggle.onValueChanged.AddListener((bool state) =>
                            {
                                handleHideEquipment(state, ref settings.hideMythic);
                            });
                            buttontoggle.isOn = settings.hideMythic;
                            hidebuttons.Add(buttontoggle, settings.hideMythic);
                            break;
                    }
                    buttontext.color = Color.white;

                }
                //Overrides
                {
                    var overridesobject = this.transform.Find("Menus/HideEquipment/Overrides");
                    //var options = new List<TMP_Dropdown.OptionData>();
                    //options[0] = new TMP_Dropdown.OptionData().text;
                    var overridesdropdown = overridesobject.Find("OverrideSelect").GetComponent<TMP_Dropdown>();
                    overridesdropdown.onValueChanged = new TMP_Dropdown.DropdownEvent();
                    var overridecontent = overridesobject.Find("Scroll View/Viewport/Content");
                    var index2 = 0;
                    {
                        var optiondata = new TMP_Dropdown.OptionData("Helmet");
                        EELDictionaries.Add(index2,EquipmentResourcesManager.Helm);
                        index2++;
                        overridesdropdown.options.Add(optiondata);
                    }
                    {
                        var optiondata = new TMP_Dropdown.OptionData("Cloak");
                        EELDictionaries.Add(index2, EquipmentResourcesManager.Cloak);
                        index2++;
                        overridesdropdown.options.Add(optiondata);
                    }
                    {
                        var optiondata = new TMP_Dropdown.OptionData("Shirt");
                        EELDictionaries.Add(index2, EquipmentResourcesManager.Shirt);
                        index2++;
                        overridesdropdown.options.Add(optiondata);
                    }
                    {
                        var optiondata = new TMP_Dropdown.OptionData("Glasses/Mask");
                        EELDictionaries.Add(index2, EquipmentResourcesManager.Glasses);
                        index2++;
                        overridesdropdown.options.Add(optiondata);
                    }
                    {
                        var optiondata = new TMP_Dropdown.OptionData("Armor");
                        EELDictionaries.Add(index2, EquipmentResourcesManager.Armor);
                        index2++;
                        overridesdropdown.options.Add(optiondata);
                    }
                    {
                        var optiondata = new TMP_Dropdown.OptionData("Bracers");
                        EELDictionaries.Add(index2, EquipmentResourcesManager.Bracers);
                        index2++;
                        overridesdropdown.options.Add(optiondata);
                    }
                    {
                        var optiondata = new TMP_Dropdown.OptionData("Gloves");
                        EELDictionaries.Add(index2, EquipmentResourcesManager.Gloves);
                        index2++;
                        overridesdropdown.options.Add(optiondata);
                    }
                    {
                        var optiondata = new TMP_Dropdown.OptionData("Boots");
                        EELDictionaries.Add(index2, EquipmentResourcesManager.Boots);
                        index2++;
                        overridesdropdown.options.Add(optiondata);
                    }
                    /* {
                         var optiondata = new TMP_Dropdown.OptionData("Wings (FX)");
                         EELDictionaries.Add(index2, EquipmentResourcesManager.WingsFX);
                         index2++;
                         overridesdropdown.options.Add(optiondata);
                     }*/
                    {
                        var optiondata = new TMP_Dropdown.OptionData("Mythic");
                        EELDictionaries.Add(index2, EquipmentResourcesManager.MythicOptions);
                        index2++;
                        overridesdropdown.options.Add(optiondata);
                    }
                    {
                        var optiondata = new TMP_Dropdown.OptionData("Wings");
                        var newwings = new Dictionary<BlueprintRef, string>();
                        foreach (var wingpair in EquipmentResourcesManager.WingsEE)
                        {
                            //VisualAdjustments.Main.logger.Log(wingpair.Key + "   " + wingpair.ToString());
                            newwings.Add(new BlueprintRef(wingpair.Value), wingpair.Key);
                        }
                        EELDictionaries.Add(index2, newwings);
                        index2++;
                        overridesdropdown.options.Add(optiondata);
                    }

                    void handleOverrideDropdownChanged(int index)
                    {
                        foreach (var thing in buttonsEELOverride)
                        {
                            if (!EELDictionaries[index].ContainsKey(thing.Key) && thing.Key != "None")
                            {
                                thing.Value.gameObject.SetActive(false);
                            }
                            else if (thing.Key == "None")
                            {
                                thing.Value.gameObject.SetActive(true);
                            }
                            else
                            {
                                thing.Value.gameObject.SetActive(true);
                            }
                        }
                        if (!buttonsEELOverride.ContainsKey("None"))
                        {
                            var buttontoaddD = Instantiate(buttonTemplate);
                            var buttontoaddtextt = buttontoaddD.Find("TextB").GetComponent<TextMeshProUGUI>();
                            buttontoaddtextt.text = "None";
                            //buttontoaddtext.font = font;
                            buttontoaddtextt.color = Color.white;
                            buttontoaddD.transform.SetParent(overridecontent.transform, false);
                            var buttontoaddbuttoncomponentt = buttontoaddD.GetComponent<Button>();
                            buttontoaddbuttoncomponentt.onClick = new Button.ButtonClickedEvent();
                            buttontoaddbuttoncomponentt.onClick.AddListener(new UnityAction(() => {
                                setsetting(overridesdropdown.value, "");
                            }));
                            buttonsEELOverride.Add("None", buttontoaddD);
                        }

                        foreach (var eename in EELDictionaries[index])
                        {
                            if (!buttonsEELOverride.ContainsKey(eename.Key.assetId))
                            {
                                try
                                {
                                    var buttontoadd = Instantiate(buttonTemplate);
                                    var buttontoaddtext = buttontoadd.Find("TextB").GetComponent<TextMeshProUGUI>();
                                   // if ()
                                   {
                                       buttontoaddtext.text = eename.Value;
                                       //buttontoaddtext.text = Kingmaker.Cheats.Utilities.GetBlueprint<BlueprintScriptableObject>(eename.Key.assetId).name;
                                   }

                                    //buttontoaddtext.font = font;
                                    buttontoaddtext.color = Color.white;
                                    buttontoadd.transform.SetParent(overridecontent.transform, false);
                                    var buttontoaddbuttoncomponent = buttontoadd.GetComponent<Button>();
                                    buttontoaddbuttoncomponent.onClick = new Button.ButtonClickedEvent();
                                    buttonsEELOverride.Add(eename.Key.assetId,buttontoadd);
                                     buttontoaddbuttoncomponent.onClick.AddListener(new UnityAction(() => {
                                         setsetting(index,eename.Key.assetId);
                                     }));
                                }
                                catch (Exception e)
                                {
                                    VisualAdjustments.Main.logger.Log(e.ToString());
                                    throw;
                                }
                            }
                        }
                    }
                    overridesdropdown.onValueChanged.AddListener(handleOverrideDropdownChanged);
                    handleOverrideDropdownChanged(0);
                }

            }
        }

        void setsetting(int index,string eelguid)
        {
            if (!eelguid.IsNullOrEmpty())
            {
                switch (index)
                {
                    case 0:
                    {
                        settings.overrideHelm = new BlueprintRef(eelguid);
                        break;
                    }
                    case 1:
                    {
                        settings.overrideCloak = new BlueprintRef(eelguid);
                        break;
                    }
                    case 2:
                    {
                        settings.overrideShirt = new BlueprintRef(eelguid);
                        break;
                    }
                    case 3:
                    {
                        settings.overrideGlasses = new BlueprintRef(eelguid);
                        break;
                    }
                    case 4:
                    {
                        settings.overrideArmor = new BlueprintRef(eelguid);
                        break;
                    }
                    case 5:
                    {
                        settings.overrideBracers = new BlueprintRef(eelguid);
                        break;
                    }
                    case 6:
                    {
                        settings.overrideGloves = new BlueprintRef(eelguid);
                        break;
                    }
                    case 7:
                    {
                        settings.overrideBoots = new BlueprintRef(eelguid);
                        break;
                    }
                    case 8:
                    {
                        //VisualAdjustments.Main.logger.Log(eelguid);
                        settings.overrideMythic = eelguid;
                        break;
                    }
                    case 9:
                    {
                        //VisualAdjustments.Main.logger.Log(eelguid);
                        settings.overrideWingsEE = eelguid;
                        break;
                    }
                }
            }
            else
            {
                switch (index)
                {
                    case 0:
                        {
                            settings.overrideHelm = null;
                            break;
                        }
                    case 1:
                        {
                            settings.overrideCloak = null;
                            break;
                        }
                    case 2:
                        {
                            settings.overrideShirt = null;
                            break;
                        }
                    case 3:
                        {
                            settings.overrideGlasses = null;
                            break;
                        }
                    case 4:
                        {
                            settings.overrideArmor = null;
                            break;
                        }
                    case 5:
                        {
                            settings.overrideBracers = null;
                            break;
                        }
                    case 6:
                        {
                            settings.overrideGloves = null;
                            break;
                        }
                    case 7:
                        {
                            settings.overrideBoots = null;
                            break;
                        }
                    case 8:
                        {
                            //VisualAdjustments.Main.logger.Log(eelguid);
                            settings.overrideMythic = "";
                            break;
                        }
                    case 9:
                        {
                            // VisualAdjustments.Main.logger.Log(eelguid);
                            settings.overrideWingsEE = "";
                            break;
                        }
                }
            }

            CharacterManager.RebuildCharacter(data);
            CharacterManager.UpdateModel(data.View);
        }
        private Dictionary<string, Transform> buttonsEELOverride = new Dictionary<string, Transform>{};
        private Dictionary<int, Dictionary<BlueprintRef, string>> EELDictionaries = new Dictionary<int, Dictionary<BlueprintRef, string>>();
        public static Dictionary<string, Dictionary<BlueprintRef, string>> EelDictionaries = new Dictionary<string, Dictionary<BlueprintRef, string>>();
        private void handleHideEquipment(bool state, ref bool settingState)
        {
            settingState = state;
            CharacterManager.RebuildCharacter(data);
            UpdatePreview();
        }
        private void handleSliderInputField(string value,bool primorsec)
        {
            //ar numvalue = int.Parse(value);
            if (int.TryParse(value, out int result))
            {
                if (primorsec)
                {
                    primindex = result;
                }
                else
                {
                    secondindex = result;
                }
            }
            else
            {
                if (primorsec)
                {
                    primindex = 0;
                }
                else
                {
                    secondindex = 0;
                }
            }
        }
        private void increasePrimaryButton()
        {
            primindex = primindex+1;
        }

        private void decreasePrimaryButton()
        {
            primindex = primindex-1;
        }

        private void increaseSecondaryButton()
        {
            secondindex = secondindex+1;
        }

        private void decreaseSecondaryButton()
        {
            secondindex = secondindex-1;
        }

        public static Char[] splitchars = new Char[] { char.Parse(" ") };
        private void HandleFilterChangedAll(string value)
        {
           // VisualAdjustments.Main.logger.Log("filterchange");
            /*var buttonstodisable = allEELButtons.Except(a => a.Key.Contains(value));
            foreach (var button in buttonstodisable)
            {
                button.Value.SetActive(false);
            }*/
            bool hassplitter = value.Contains(" ");
            var splitstring = value.Split(splitchars);
            //splitstring = splitstring.Select(a => a.Trim()).Where(b => !b.IsNullOrEmpty()).ToArray();
            foreach (var eelbutton in allEELButtons)
            {
                if (hassplitter)
                {
                    if (splitstring.All(a => eelbutton.Key.Contains(a,StringComparison.OrdinalIgnoreCase)))
                    {
                        eelbutton.Value.SetActive(true);
                    }
                    else
                    {
                        eelbutton.Value.SetActive(false);
                    }
                    /*bool shoulddisable = true;
                    foreach (var String in splitstring)
                    {
                        if (eelbutton.Key.Contains(String))
                        {
                            eelbutton.Value.SetActive(true);
                            shoulddisable = false;
                        }
                    }
                    if (shoulddisable)
                    {
                        eelbutton.Value.SetActive(false);
                    }*/
                }
                   /* foreach (var VARIABLE in splitstring)
                    {
                        if (eelbutton.Key.Contains(VARIABLE, StringComparison.OrdinalIgnoreCase))
                        {
                            eelbutton.Value.SetActive(true);
                            return;
                        }
                        eelbutton.Value.SetActive(false);
                    }*/
                       /* if(splitstring.Any(a => a.Contains(eelbutton.Key,StringComparison.OrdinalIgnoreCase)))
                        {
                            eelbutton.Value.SetActive(true);
                        }
                        else
                        {
                            eelbutton.Value.SetActive(false);
                        }*/
                //}
                else
                {
                    if (value.IsNullOrEmpty())
                    {
                        eelbutton.Value.SetActive(true);
                    }
                    else if (eelbutton.Key.Contains(value, StringComparison.OrdinalIgnoreCase))
                    {
                        eelbutton.Value.SetActive(true);
                    }
                    else
                    {
                        eelbutton.Value.SetActive(false);
                    }
                }

            }
        }
        private void HandleFilterChangedCurrent(string value)
        {
            //VisualAdjustments.Main.logger.Log("filterchange");
            /*var buttonstodisable = allEELButtons.Except(a => a.Key.Contains(value));
            foreach (var button in buttonstodisable)
            {
                button.Value.SetActive(false);
            }*/
            bool hassplitter = value.Contains(" ");
            var splitstring = value.Split(splitchars);
            foreach (var eelbutton in currentEELButtons)
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
                    /*bool shoulddisable = true;
                    foreach (var String in splitstring)
                    {
                        if (eelbutton.Key.Contains(String))
                        {
                            eelbutton.Value.SetActive(true);
                            shoulddisable = false;
                        }
                    }
                    if (shoulddisable)
                    {
                        eelbutton.Value.SetActive(false);
                    }*/
                }
                else
                {
                    if (value.IsNullOrEmpty())
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

        private void HandleUnitChangedHideEEs()
        {
            foreach (var transform in hidebuttons)
            {

                switch (transform.Key.gameObject.transform.Find("Label").GetComponent<TextMeshProUGUI>().text)
                {
                    case "Hide Cap":
                        transform.Key.isOn = settings.hideCap;
                        break;
                    case "Hide Helmet":
                        transform.Key.isOn = settings.hideHelmet;
                        break;
                    case "Hide Glasses":
                        transform.Key.isOn = settings.hideGlasses ;
                        break;
                    case "Hide Shirt":
                        transform.Key.isOn = settings.hideShirt ;
                        break;
                    case "Hide Class Gear":
                        transform.Key.isOn = settings.hideClassCloak ;
                        break;
                    case "Hide Cloak":
                        transform.Key.isOn = settings.hideItemCloak ;
                        break;
                    case "Hide Armor":
                        transform.Key.isOn = settings.hideArmor ;
                        break;
                    case "Hide Bracers":
                        transform.Key.isOn = settings.hideBracers ;
                        break;
                    case "Hide Gloves":
                        transform.Key.isOn = settings.hideGloves ;
                        break;
                    case "Hide Boots":
                        transform.Key.isOn = settings.hideBoots ;
                        break;
                    case "Hide Weapons":
                        transform.Key.isOn = settings.hideWeapons ;
                        break;
                    case "Hide Sheaths":
                        transform.Key.isOn = settings.hideSheaths ;
                        break;
                    case "Hide Belt Items":
                        transform.Key.isOn = settings.hideBeltSlots ;
                        break;
                    case "Hide Quiver":
                        transform.Key.isOn = settings.hidequiver ;
                        break;
                    case "Hide Enchantments":
                        transform.Key.isOn = settings.hideWeaponEnchantments ;
                        break;
                    case "Hide Wings":
                        transform.Key.isOn = settings.hideWings ;
                        break;
                    case "Hide Horns":
                        transform.Key.isOn = settings.hideHorns ;
                        break;
                    case "Hide Tail":
                        transform.Key.isOn = settings.hideTail ;
                        break;
                    case "Hide Mythic":
                        transform.Key.isOn = settings.hideMythic ;
                        break;
                }
            }
        }
        private void Update()
        {
            if (hasUnitChanged())
            {
                VisualAdjustments.Main.settings.Save(VisualAdjustments.Main.ModEntry);
                HandleUnitChangedHideEEs();
                foreach (var eebutton in currentEELButtons.Values)
                {
                    eebutton.gameObject.SafeDestroy();
                }
                settings = VisualAdjustments.Main.settings.GetCharacterSettings(data);
                foreach (var kv in hidebuttons)
                {
                    kv.Key.isOn = kv.Value;
                }
                currentEELButtons.Clear();

            }
            if (haschanged)
            {
                HandleUnitChangedHideEEs();
                foreach (var currentee in data.View.CharacterAvatar.EquipmentEntities)
                {
                    if (!currentEELButtons.ContainsKey(currentee.name))
                    {
                        var buttontoadd = Instantiate(buttonTemplate);
                        var buttontoaddtext = buttontoadd.Find("TextB").GetComponent<TextMeshProUGUI>();
                        buttontoaddtext.text = currentee.name;
                        buttontoaddtext.font = font;
                        buttontoaddtext.color = Color.white;
                        buttontoadd.transform.SetParent(contentCurrentEE.transform, false);
                        var buttontoaddbuttoncomponent = buttontoadd.GetComponent<Button>();
                        buttontoaddbuttoncomponent.onClick = new Button.ButtonClickedEvent();
                        buttontoaddbuttoncomponent.onClick.AddListener(new UnityAction(() =>
                        {
                        HandleEEClicked(buttontoaddtext.text);
                        }));
                        currentEELButtons.Add(currentee.name,buttontoaddbuttoncomponent);
                    }
                }

                haschanged = false;
            }
            //This is a unity message that runs each frame.
        }

        private void addRemoveButton()
        {
            haschanged = true;
            if (EELpickerUI.selectedEntity == null)
            {
                
            }
            else if (data.View.CharacterAvatar.EquipmentEntities.Contains(EELpickerUI.selectedEntity))
            {
                    data.View.CharacterAvatar.RemoveEquipmentEntity(selectedEntity, true);
                    removeFromAddEEpart(selectedEntity);
                    addToRemoveEEPart(selectedEntity);
                    currentEELButtons[selectedEntity.name].gameObject.SafeDestroy();
                    //currentEELButtons[selectedEntity.name] = null;
                    currentEELButtons.Remove(selectedEntity.name);
                    UpdatePreview();
            }
            else
            {
                    data.View.CharacterAvatar.AddEquipmentEntity(selectedEntity, true);
                    removeFromRemoveEEPart(selectedEntity);
                    addToAddEEPart(selectedEntity, data.View.CharacterAvatar.GetPrimaryRampIndex(selectedEntity),
                        data.View.CharacterAvatar.GetSecondaryRampIndex(selectedEntity));
                    UpdatePreview();
            }
            if (EELpickerUI.selectedEntity == null)
            {
                AddRemoveButton.GetComponent<TextMeshProUGUI>().text = "Null";
            }
            else if (data.View.CharacterAvatar.EquipmentEntities.Contains(EELpickerUI.selectedEntity))
            {
                AddRemoveButton.GetComponent<TextMeshProUGUI>().text = "Remove";
            }
            else
            {
                AddRemoveButton.GetComponent<TextMeshProUGUI>().text = "Add";
            }
        }
        private void HandleUIToggle(string button)
        {
            var OutfitDoll = this.transform.Find("Menus/OutfitDoll");
            OutfitDoll.gameObject.SetActive(false);

            var HideEquipment = this.transform.Find("Menus/HideEquipment");
            HideEquipment.gameObject.SetActive(false);

            var Overrides = this.transform.Find("Menus/Overrides");
            Overrides.gameObject.SetActive(false);

            var EEPicker = this.transform.Find("Menus/InventoryViewWindow");
            EEPicker.gameObject.SetActive(false);
            switch (button)
            {
                case "Outfit/Doll":
                    OutfitDoll.gameObject.SetActive(true);
                    break;
                case "EE Picker":
                    EEPicker.gameObject.SetActive(true);
                    break;
                case "Equipment":
                    HideEquipment.gameObject.SetActive(true);
                    break;
                case "Overrides":
                    Overrides.gameObject.SetActive(true);
                    break;
            }
        }
        private void HandleEEClicked(string eename)
        {
           // selectedstring = eename;
           //haschanged = true;
            EELpickerUI.selectedEntity = ResourcesLibrary.TryGetResource<EquipmentEntity>(EquipmentResourcesManager.AllEEL[eename]);
            //this.transform.Find("InventoryViewWindow").Find("TextboxesValues").Find("Name").GetComponent<TextMeshProUGUI>().text = eename;
            // this.transform.Find("InventoryViewWindow").Find("TextboxesValues").Find("Race").GetComponent<TextMeshProUGUI>().text = eename;
            //this.transform.Find("InventoryViewWindow").Find("TextboxesValues").Find("Sex").GetComponent<TextMeshProUGUI>().text = eename;

            if (EELpickerUI.selectedEntity == null)
            {
                AddRemoveButton.GetComponent<TextMeshProUGUI>().text = "Null";
            }
            else if (data.View.CharacterAvatar.EquipmentEntities.Contains(EELpickerUI.selectedEntity))
            {
                AddRemoveButton.GetComponent<TextMeshProUGUI>().text = "Remove";
            }
            else
            {
                AddRemoveButton.GetComponent<TextMeshProUGUI>().text = "Add";
            }

            Name.text = eename;
            Race.text = getRace(selectedEntity);
            Sex.text = getGender(selectedEntity);
            if (selectedEntity != null && data.View.CharacterAvatar.EquipmentEntities.Contains(selectedEntity))
            {
                primindex = getColorFromPart(selectedEntity, true);
                secondindex = getColorFromPart(selectedEntity, false);
                primSlider.maxValue = selectedEntity.PrimaryRamps.Count;
                primSlider.value = data.View.CharacterAvatar.GetPrimaryRampIndex(selectedEntity);
                secondSlider.maxValue = selectedEntity.SecondaryRamps.Count;
                secondSlider.value = data.View.CharacterAvatar.GetSecondaryRampIndex(selectedEntity);

            }
        }

        private void HandleButtonClick()
        {
            VisualAdjustments.Main.logger.Log("buttonpressed");
            var selectiongrid = window.Find("UISelectionGrid").gameObject;
            window.Find("Menus").gameObject.SetActive(!selectiongrid.active);
            selectiongrid.SetActive(!selectiongrid.active);
            //haschanged = true;
            //window.Find("InventoryViewWindow").gameObject.active = !window.Find("InventoryViewWindow").gameObject.active;
            // _text.text = "Add";
        }
    }

    public static class ServiceWindowsMenuVM_Patch
    {
        public static void Postfix(UnitDescriptor unitDescriptor)
        {
            UIManager.data = unitDescriptor.Unit;
            VisualAdjustments.Main.logger.Log("unit" + unitDescriptor.CharacterName);
        }
    }
}