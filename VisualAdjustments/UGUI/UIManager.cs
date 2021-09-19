/*using DG.Tweening;
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
using Kingmaker.UI.ServiceWindow;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Utility;
using Kingmaker.Visual.CharacterSystem;
using ModKit.Utility;
using ModMaker.Utility;
using Steamworks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityModManagerNet;

namespace TutorialCanvas.UI
{
    internal class UIManager : MonoBehaviour
    {
        public static string getGender(EquipmentEntity ee)
        {
            if (ee.name.Contains("_M_")) return "Male";
            else if (ee.name.Contains("_F_")) return "Female";
            else return "N/A";
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
            return "N/A";
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
        void setColorAddEEPart(EquipmentEntity ee, int primaryIndex, int secondaryIndex)
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

        private void UpdatePreview()
        {
            if (dollPcView == null)
            {
               dollPcView = this.transform.parent.Find("Doll").GetComponent<InventoryDollPCView>();
            }
            dollPcView.OnHide();
            dollPcView.OnShow();
            dollPcView.RefreshView();
        }
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
                window.Find("InventoryViewWindow").gameObject.active = false;
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
            contentAllEE = this.transform.Find("InventoryViewWindow/AllEEs/Scroll View/Viewport/Content");
            contentCurrentEE = this.transform.Find("InventoryViewWindow/CurrentEEs/Scroll View/Viewport/Content");
            VisualAdjustments.Main.logger.Log("awake2");
            buttonTemplate = this.transform.Find("NewButton");
            font = this.transform.parent.Find("Doll/DollTitle/TitleLabel").GetComponent<TextMeshProUGUI>().font;
            foreach (var eename in VisualAdjustments.EquipmentResourcesManager.AllEEL)
            {
                var buttontoadd = Instantiate(buttonTemplate);
                var buttontoaddtext = buttontoadd.Find("TextB").GetComponent<TextMeshProUGUI>();
                buttontoaddtext.text = eename.Key;
                buttontoaddtext.font = font;
                buttontoaddtext.color = Color.black;
                buttontoadd.transform.SetParent(contentAllEE.transform,false);
                var buttontoaddbuttoncomponent = buttontoadd.GetComponent<Button>();
                buttontoaddbuttoncomponent.onClick = new Button.ButtonClickedEvent();
                buttontoaddbuttoncomponent.onClick.AddListener(new UnityAction(() => { HandleEEClicked(buttontoaddtext.text);
                }));
            }
            m_Data = Kingmaker.UI.Common.UIUtility.GetCurrentCharacter();
            //switch font
            //var texts = ;
            foreach (var txt in this.transform.GetComponentsInChildren<TextMeshProUGUI>(true))
            {
                txt.font = font;
                txt.color = Color.black;
            }

            

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
                Name = this.transform.Find("InventoryViewWindow/EEInfo/TextboxesValues/Name")
                    .GetComponent<TextMeshProUGUI>();
                Race = this.transform.Find("InventoryViewWindow/EEInfo/TextboxesValues/Race")
                    .GetComponent<TextMeshProUGUI>();
                Sex = this.transform.Find("InventoryViewWindow/EEInfo/TextboxesValues/Sex")
                    .GetComponent<TextMeshProUGUI>();
                AddRemoveButton = this.transform.Find("InventoryViewWindow/AddRemove/Button/ButtonText").gameObject;

                var AddRemoveButtonButton = this.transform.Find("InventoryViewWindow/AddRemove/Button").GetComponent<Button>();
                AddRemoveButtonButton.onClick = new Button.ButtonClickedEvent();
                AddRemoveButtonButton.onClick.AddListener(new UnityAction(() =>
                {
                    addRemoveButton();
                }));
                // button.gameObject.AddComponent<DraggableWindow>(); //Add draggable windows component allowing the window to be dragged when the button is pressed down

                //_text = this.transform.Find("InfoWindow").transform.Find("AddRemove").Find("AddRemoveText").GetComponent<TextMeshProUGUI>(); //Find the text component so we can update later.
                //Mod.Log(_text.ToString());
            }
            // Setup Outfit/Doll
            {

            }
            //Setup Overrides
            {

            }
            //Setup Equipment
            {

            }
        }

        private void Update()
        {
            if (hasUnitChanged())
            {
                foreach (var eebutton in currentEELButtons.Values)
                {
                    eebutton.gameObject.SafeDestroy();

                }
                currentEELButtons.Clear();
            }
            if (haschanged)
            {
                foreach (var currentee in data.View.CharacterAvatar.EquipmentEntities)
                {
                    if (!currentEELButtons.ContainsKey(currentee.name))
                    {
                        var buttontoadd = Instantiate(buttonTemplate);
                        var buttontoaddtext = buttontoadd.Find("TextB").GetComponent<TextMeshProUGUI>();
                        buttontoaddtext.text = currentee.name;
                        buttontoaddtext.font = font;
                        buttontoaddtext.color = Color.black;
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
            var OutfitDoll = this.transform.Find("OutfitDoll");
            OutfitDoll.gameObject.SetActive(false);

            var HideEquipment = this.transform.Find("HideEquipment");
            HideEquipment.gameObject.SetActive(false);

            var Overrides = this.transform.Find("Overrides");
            Overrides.gameObject.SetActive(false);

            var EEPicker = this.transform.Find("InventoryViewWindow");
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
        }

        private void HandleButtonClick()
        {
            VisualAdjustments.Main.logger.Log("buttonpressed");
            var selectiongrid = window.Find("UISelectionGrid").gameObject;
            window.Find("UISelectionGrid").gameObject.SetActive(!selectiongrid.active);
        //    haschanged = true;
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
}*/