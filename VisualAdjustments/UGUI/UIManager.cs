using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UI.MVVM._PCView.ServiceWindows.Inventory;
using Kingmaker.UnitLogic;
using Kingmaker.Utility;

//using TutorialCanvas.Utilities;
//using static TutorialCanvas.Main;
using Kingmaker.Visual.CharacterSystem;
using ModMaker.Utility;
using Owlcat.Runtime.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using TutorialCanvas.Utilities;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VisualAdjustments;
using static VisualAdjustments.EELpickerUI;

namespace TutorialCanvas.Enums
{
}

namespace TutorialCanvas.UI
{
    /// <summary>
    ///  MAKE THE EES MORE READABLE
    /// </summary>
    ///
    public class EEButtonCategories4
    {
        public string gender = "N/A";
        public string race = "N/A";
    }

    public class UIManager : MonoBehaviour
    {
        public static UIManager manager;
        public GameObject OutfitDoll;
        public GameObject EEPicker;
        public GameObject FX;
        public GameObject Equipment;
        public GameObject overridecontent;
        public bool shouldrebuildhidebuttons = true;

        public static string GetTextAndSetupCategory(string ee)
        {
            try
            {
                //  if(ee.Contains("_SU"))
                // {
                //     VisualAdjustments.Main.logger.Log(ee);
                //ok   }
                /*var str = ee;
                if(ee.Contains("_M_"))
                {
                    VisualAdjustments.Main.logger.Log("Male");
                    str = str.Replace("_M", " Male ");
                }
                else if (ee.Contains("_F_"))
                {
                    VisualAdjustments.Main.logger.Log("Female");
                    str = str.Replace("_F", " Female ");
                }
                if (raceidentifiers.Keys.Any(a => str.Contains(a)))
                {
                    var race = raceidentifiers.Keys.First(a => str.Contains(a));
                    str = str.Replace(race, " " + raceidentifiers[race] + " ");
                    VisualAdjustments.Main.logger.Log(raceidentifiers[race]);
                }
                str = str.Replace("EE_","");
                str = str.Replace("EE", "");
                str = str.Replace("_Any", "");
                str = str.Replace("Any", "");
                str = str.Replace("_U", "");
                var tempstring = str;
                var b = 0;
                str = Regex.Replace(str, @"(?<!^)(\B|b)(?!$)", " ");
                for (int i = 0; i < tempstring.Length-1; i++)
                {
                    if((char.IsUpper(tempstring,i) && (i < tempstring.Length-1 && !char.IsUpper(tempstring,i+1)) && (i == 0  || !char.IsUpper(tempstring, i - 1))) || char.IsNumber(tempstring,i))
                    {
                        if (i + b <= tempstring.Length-1)
                        {
                            tempstring = tempstring.Insert(i + b, " ");
                           // b++;
                        }
                    }
                }
                str = tempstring;
                VisualAdjustments.Main.logger.Log(str);
                return str;*/
                if (UnfilteredAndFilteredEEName.TryGetValue(ee, out string outval))
                {
                    return outval;
                }
                var stringarray = ee.Split('_');
                bool hassetrace = false;
                var newarray = new List<string>();
                foreach (var s in stringarray)
                {
                    if (!hassetrace && raceidentifiers.Keys.TryFind(a => s == a, out string raceout))
                    {
                        newarray.Add(raceidentifiers[raceout]);
                        hassetrace = true;
                    }
                    else if (s == "M")
                    {
                        newarray.Add("Male");
                    }
                    else if (s == "F")
                    {
                        newarray.Add("Female");
                    }
                    else if (s == "Any")
                    {
                        newarray.Add("Any");
                    }
                    else if ((s != "EE" && s != "KEE" && s != "Buff") && s.Length > 1)
                    {
                        StringBuilder SB = new System.Text.StringBuilder(s);
                        var b = 0;
                        for (int c = 0; c < s.Length; c++)
                        {
                            if (c > 0 && ((Char.IsUpper(s[c]) && (!Char.IsUpper(s[c - 1]) || (c < s.Length || !Char.IsUpper(s[c + 1]))) || (Char.IsNumber(s[c]) && (!Char.IsNumber(s[c - 1]) /*|| !Char.IsNumber(s[c-1])*/)))))
                            {
                                SB.Insert(c + b, " ");
                                b++;
                                //SB.Append(s[c]);
                            }
                        }
                        newarray.Add(SB.ToString());
                    }
                }
                var concatstring = "";
                foreach (var item in newarray)
                {
                    concatstring = concatstring + " " + item;
                    //  VisualAdjustments.Main.logger.Log(item);
                }
                concatstring = concatstring.TrimStart();
                concatstring.TrimEnd();
                //VisualAdjustments.Main.logger.Log(concatstring);
                if (FilteredAndUnfilteredEEName.Keys.Contains(concatstring))
                {
                    FilteredAndUnfilteredEEName[concatstring] = ee;
                }
                else
                {
                    FilteredAndUnfilteredEEName.Add(concatstring, ee);
                }
                if (UnfilteredAndFilteredEEName.Keys.Contains(ee))
                {
                    UnfilteredAndFilteredEEName[ee] = concatstring;
                }
                else
                {
                    UnfilteredAndFilteredEEName.Add(ee, concatstring);
                }
                return concatstring;
            }
            catch (Exception e)
            {
                VisualAdjustments.Main.logger.Log(e.ToString());
                throw;
            }
        }

        public static Dictionary<string, string> FilteredAndUnfilteredEEName = new Dictionary<string, string> { };
        public static Dictionary<string, string> UnfilteredAndFilteredEEName = new Dictionary<string, string> { };

        public static Dictionary<string, string> raceidentifiers = new Dictionary<string, string>
        {
            ["HM"] = "Human",
            ["AA"] = "Aasimar",
            ["HE"] = "Half-Elf",
            ["MM"] = "Mongrel",
            ["SU"] = "Succubus",
            ["DW"] = "Dwarf",
            ["TL"] = "Tiefling",
            ["EL"] = "Elf",
            ["KT"] = "Kitsune",
            ["GN"] = "Gnome",
            ["OD"] = "Oread",
            ["HL"] = "Halfling",
            ["DH"] = "Dhampir",
            ["HO"] = "Half-Orc",
            ["ZB"] = "Zombie",
            ["CB"] = "Cambion",
            ["CM"] = "Cambion",
            ["SN"] = "Skeleton"
        };

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
            if (ee.name.Contains("_GL")) return "Ghoul";
            if (ee.name.Contains("_DE")) return "Drow";
            return "N/A";
        }

        public void resetEEPart()
        {
            //var part = data.Parts.Get<UnitPartVAEELs>();
            var VisualInfo = VisualAdjustments.GlobalVisualInfo.Instance.ForCharacter(data);
            var part = VisualInfo.EEPart;
            if (part != null)
            {
                part.EEToAdd.Clear();
                part.EEToRemove.Clear();
            }
            haschanged = true;
            CharacterManager.RebuildCharacter(data);
            CharacterManager.UpdateModel(data.View);
            UpdatePreview();
            RebuildCurrentEEButtons();
        }

        public void setCustomColorToEEPart(Color col, bool primorsec)
        {
            //var component = data.Parts.Ensure<UnitPartVAEELs>();
            var VisualInfo = VisualAdjustments.GlobalVisualInfo.Instance.ForCharacter(data);
            var component = VisualInfo.EEPart;
            if (component.EEToAdd.Contains(a => a.AssetID == EquipmentResourcesManager.AllEEL[selectedEntity.name]))
            {
                if (primorsec)
                {
                    var storage = component.EEToAdd.First(a => a.AssetID == EquipmentResourcesManager.AllEEL[selectedEntity.name]);
                    storage.CustomColorPrim = new float[] { col.r, col.g, col.b };
                    storage.hasCustomColor = true;
                    storage.Apply(selectedEntity, data.View.CharacterAvatar);
                    // component.EEToAdd.Add(new EEStorage(EquipmentResourcesManager.AllEEL[selectedEntity.name], -1, -1, true, CustomColorPrimin: new float[] { col.r, col.g, col.b }));
                }
                else
                {
                    var storage = component.EEToAdd.First(a => a.AssetID == EquipmentResourcesManager.AllEEL[selectedEntity.name]);
                    storage.CustomColorSec = new float[] { col.r, col.g, col.b };
                    storage.hasCustomColor = true;
                    storage.Apply(selectedEntity, data.View.CharacterAvatar);
                }
                //Main.logger.Log("add" + ee.name + EquipmentResourcesManager.AllEEL[ee.name]);
            }
            else
            {
                var ee = ResourcesLibrary.TryGetResource<EquipmentEntity>(EquipmentResourcesManager.AllEEL[selectedEntity.name]);
                if (data.View.CharacterAvatar.EquipmentEntities.Contains(ee))
                {
                    if (primorsec)
                    {
                        var storage = new EEStorage(EquipmentResourcesManager.AllEEL[selectedEntity.name], -1, -1, true, CustomColorPrimin: new float[] { col.r, col.g, col.b });
                        component.EEToAdd.Add(storage);
                        storage.Apply(selectedEntity, data.View.CharacterAvatar);
                    }
                    else
                    {
                        var storage = new EEStorage(EquipmentResourcesManager.AllEEL[selectedEntity.name], -1, -1, true, CustomColorSecin: new float[] { col.r, col.g, col.b });
                        component.EEToAdd.Add(storage);
                        storage.Apply(selectedEntity, data.View.CharacterAvatar);
                    }
                }
            }
            primSlider.maxValue = selectedEntity.PrimaryRamps.Count;
            primSlider.value = data.View.CharacterAvatar.GetPrimaryRampIndex(selectedEntity);
            secondSlider.maxValue = selectedEntity.SecondaryRamps.Count;
            secondSlider.value = data.View.CharacterAvatar.GetSecondaryRampIndex(selectedEntity);
            UpdatePreview();
        }

        public void removeFromRemoveEEPart(EquipmentEntity ee)
        {
            /*var component = m_Data.Parts.Get<UnitPartVAEELs>();
            if (component == null)
            {
                component = m_Data.Parts.Add<UnitPartVAEELs>();
            }*/
            var VisualInfo = VisualAdjustments.GlobalVisualInfo.Instance.ForCharacter(data);
            var component = VisualInfo.EEPart;

            if (component.EEToRemove.Contains(EquipmentResourcesManager.AllEEL[ee.name]))
            {
                component.EEToRemove.Remove(EquipmentResourcesManager.AllEEL[ee.name]);
            }
        }

        public void addToRemoveEEPart(EquipmentEntity ee)
        {
            /* var component = m_Data.Parts.Get<UnitPartVAEELs>();
             if (component == null)
             {
                 component = m_Data.Parts.Add<UnitPartVAEELs>();
             }*/
            var VisualInfo = VisualAdjustments.GlobalVisualInfo.Instance.ForCharacter(data);
            var component = VisualInfo.EEPart;

            if (!component.EEToRemove.Contains(EquipmentResourcesManager.AllEEL[ee.name]))
            {
                component.EEToRemove.Add(EquipmentResourcesManager.AllEEL[ee.name]);
            }
        }

        public void addToAddEEPart(EquipmentEntity ee, int primary, int secondary)
        {
            /* var component = m_Data.Parts.Get<UnitPartVAEELs>();
             if (component == null)
             {
                 component = m_Data.Parts.Add<UnitPartVAEELs>();
             }*/
            var VisualInfo = VisualAdjustments.GlobalVisualInfo.Instance.ForCharacter(data);
            var component = VisualInfo.EEPart;
            if (!component.EEToAdd.Contains(a => a.AssetID == EquipmentResourcesManager.AllEEL[ee.name]))
            {
                component.EEToAdd.Add(new EEStorage(EquipmentResourcesManager.AllEEL[ee.name], primary,
                    secondary));
                //Main.logger.Log("add" + ee.name + EquipmentResourcesManager.AllEEL[ee.name]);
            }
        }

        public void removeFromAddEEpart(EquipmentEntity ee)
        {
            /*var component = m_Data.Parts.Get<UnitPartVAEELs>();
            if (component == null)
            {
                component = m_Data.Parts.Add<UnitPartVAEELs>();
            }*/
            var VisualInfo = VisualAdjustments.GlobalVisualInfo.Instance.ForCharacter(data);
            var component = VisualInfo.EEPart;

            if (component.EEToAdd.Contains(a => a.AssetID == EquipmentResourcesManager.AllEEL[ee.name]))
            {
                component.EEToAdd.Remove(a => a.AssetID == EquipmentResourcesManager.AllEEL[ee.name]);
            }
        }

        public void setColorAddEEPart(EquipmentEntity ee, int primaryIndex, int secondaryIndex)
        {
            /* var component = m_Data.Parts.Get<UnitPartVAEELs>();
             if (component == null)
             {
                 component = m_Data.Parts.Add<UnitPartVAEELs>();
             }*/
            var VisualInfo = VisualAdjustments.GlobalVisualInfo.Instance.ForCharacter(data);
            var component = VisualInfo.EEPart;

            if (component.EEToAdd.Contains(a => a.AssetID == EquipmentResourcesManager.AllEEL[ee.name]))
            {
                var eewithcolor = component.EEToAdd.First(a => a.AssetID == EquipmentResourcesManager.AllEEL[ee.name]);
                eewithcolor.PrimaryIndex = primaryIndex;
                eewithcolor.SecondaryIndex = secondaryIndex;
            }
            else
            {
                // Main.logger.Log("coloradd" + ee.name + EquipmentResourcesManager.AllEEL[ee.name]);
                component.EEToAdd.Add(new EEStorage(EquipmentResourcesManager.AllEEL[ee.name], primaryIndex, primaryIndex));
            }
        }

        private int getColorFromPart(EquipmentEntity ee, bool primorsec)
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

        public static bool hasUnitChanged()
        {
            if (m_Data != null && m_Data.CharacterName != data.CharacterName)
            {
                return true;
            }

            return false;
        }

        public const string Source = "TutorialCanvas";
        public static Transform window;

        // public TextMeshProUGUI _text;
        public static bool haschanged = true;

        private static string SelectedWeaponType = "";
        public static int slot = 0;
        public static bool primorsec;
        public static bool loaded = false;
        public static bool loaded2 = false;

        public static UnitEntityData data
        {
            get
            {
                /*var newData = Kingmaker.UI.Common.UIUtility.GetCurrentCharacter();
                if (m_Data == null)
                {
                    m_Data = newData;
                    haschanged = true;
                }
                else if (m_Data.UniqueId != newData.UniqueId)
                {
                    m_Data = newData;
                    haschanged = true;
                }*/

                if (m_Data == null) m_Data = Kingmaker.UI.Common.UIUtility.GetCurrentCharacter();
                return m_Data;
            }
            set
            {
                try
                {
                    if (value != null && (m_Data == null || (m_Data != null && (m_Data.UniqueId != value.UniqueId))))
                    {
                        m_Data = value;
                        //VisualAdjustments.Main.logger.Log("ChangedUnit : " + value.CharacterName);
                        // FXUIHandler.currentUnitPart = value.Parts.Ensure<UnitPartVAFX>();
                        // FXUIHandler.handler.WhiteOrBlackList = FXUIHandler.currentUnitPart.blackorwhitelist;
                        settings = VisualAdjustments.Main.settings.GetCharacterSettings(value);
                        // haschanged = true;
                        //  if ((manager?.UISelectGrid?.activeInHierarchy?))
                        if (manager != null && manager.UISelectGrid.activeInHierarchy)
                        {
                            // if (hasUnitChanged())
                            {
                                //VisualAdjustments.Main.settings.Save(VisualAdjustments.Main.ModEntry);
                                // settings = VisualAdjustments.Main.settings.GetCharacterSettings(m_Data);
                                //  HandleUnitChangedHideEEs();
                                foreach (var eebutton in manager.currentEELButtons.Values)
                                {
                                    eebutton.gameObject.SafeDestroy();
                                }

                                //foreach (var kv in hidebuttons)
                                {
                                    //    kv.Key.isOn = kv.Value;
                                }
                                manager.currentEELButtons.Clear();
                            }
                            // if (haschanged)
                            {
                                // settings = VisualAdjustments.Main.settings.GetCharacterSettings(m_Data);

                                /* foreach (var kv in hidebuttons)
                                 {
                                     hidebuttons[kv.Key] = kv.Key.isOn;
                                 }*/

                                foreach (var currentee in data.View.CharacterAvatar.EquipmentEntities)
                                {
                                    if (!manager.currentEELButtons.ContainsKey(currentee.name))
                                    {
                                        var buttontoadd = Instantiate(manager.buttonTemplate);
                                        var buttontoaddtext = buttontoadd.Find("TextB").GetComponent<TextMeshProUGUI>();
                                        //buttontoaddtext.text = currentee.name;
                                        buttontoaddtext.text = GetTextAndSetupCategory(currentee.name);
                                        // buttontoaddtext.font = font;
                                        // buttontoaddtext.color = Color.white;
                                        buttontoadd.transform.SetParent(manager.contentCurrentEE.transform, false);
                                        var buttontoaddbuttoncomponent = buttontoadd.GetComponent<Button>();
                                        buttontoaddbuttoncomponent.onClick = new Button.ButtonClickedEvent();
                                        buttontoaddbuttoncomponent.onClick.AddListener(new UnityAction(() =>
                                        {
                                            manager.HandleEEClicked(buttontoaddtext.text);
                                        }));
                                        manager.currentEELButtons.Add(buttontoaddtext.text, buttontoaddbuttoncomponent);
                                    }
                                }
                                manager.HandleUnitChangedHideEEs();
                                //haschanged = false;
                            }
                            FXUIHandler.handler.HandleUnitChangedOrUpdate();
                        }
                        else
                        {
                            haschanged = true;
                        }
                    }
                    else
                    {
                        m_Data = value;
                    }
                }
                catch (Exception e)
                {
                    VisualAdjustments.Main.logger.Log(e.ToString());
                }
            }
        }

        private void UpdatePreview()
        {
            // if (window.parent.Find("Doll").TryGetComponent<InventoryFXUIHandlerHandler.dollPcView>(out FXUIHandlerHandler.dollPcView))
            {
                try
                {
                    // fix this
                    if (FXUIHandler.dollPcView == null && window != null && this?.gameObject?.activeInHierarchy == false)
                    {
                        FXUIHandler.dollPcView = window?.parent?.Find("Doll")?.GetComponent<InventoryDollPCView>();
                    }
                    else if (FXUIHandler.dollPcView != null)
                    {
                        //FXUIHandlerHandler.dollPcView.Initialize();
                        // FXUIHandlerHandler.dollPcView.SwitchOffDoll();
                        // FXUIHandlerHandler.dollPcView.SwitchOnDoll();
                        // FXUIHandlerHandler.dollPcView.BindViewImplementation();
                        //FXUIHandlerHandler.dollPcView.OnHide();
                        // FXUIHandlerHandler.dollPcView.BindViewImplementation();
                        //FXUIHandlerHandler.dollPcView.OnShow();
                        if (UISelectGrid?.activeInHierarchy == true)/* && FXUIHandlerHandler.dollPcView.Room && FXUIHandlerHandler.dollPcView.IsBinded)*/
                        {
                            // VisualAdjustments.Main.logger.Log("UpdatedPreview");
                            // CharacterManager.RebuildCharacter(data);
                            FXUIHandler.dollPcView?.OnHide();
                            FXUIHandler.dollPcView?.RefreshView();
                            FXUIHandler.dollPcView?.OnShow();

                            //  FXUIHandlerHandler.dollPcView.RefreshView();
                        }
                    }
                    {
                        /* var player = data;
                         Character character2 = Game.Instance.UI.Common.DollRoom.CreateAvatar(Game.Instance.UI.Common.DollRoom.m_OriginalAvatar, Game.Instance.UI.Common.DollRoom.Unit.Gender, Game.Instance.UI.Common.DollRoom.Unit.Progression.Race.RaceId, Game.Instance.UI.Common.DollRoom.m_Unit.ToString());
                         Game.Instance.UI.Common.DollRoom.SetAvatar(character2);
                         character2.transform.localScale = player.View.transform.localScale;
                         Vector3 localScale = new Vector3(player.View.OriginalScale.x / player.View.transform.localScale.x, player.View.OriginalScale.y / player.View.transform.localScale.y, player.View.OriginalScale.z / player.View.transform.localScale.z);
                         character2.transform.parent.localScale = localScale;
                         IKController component = Game.Instance.UI.Common.DollRoom.Unit.View.GetComponent<IKController>();
                         IKController ikcontroller = Game.Instance.UI.Common.DollRoom.m_Avatar.gameObject.AddComponent<IKController>();
                         ikcontroller.DollRoom = Game.Instance.UI.Common.DollRoom;
                         ikcontroller.CharacterSystem = Game.Instance.UI.Common.DollRoom.m_Avatar;
                         ikcontroller.Settings = ((component != null) ? component.Settings : null);
                         UnitEntityView component2 = Game.Instance.UI.Common.DollRoom.m_OriginalAvatar.GetComponent<UnitEntityView>();
                         if (component2 != null)
                         {
                             ikcontroller.CharacterUnitEntity = component2;
                         }
                         Game.Instance.UI.Common.DollRoom.Update(false);
                         Game.Instance.UI.Common.DollRoom.SetupAnimationManager(character2.AnimationManager);
                         character2.AnimationManager.Tick();
                         character2.AnimationManager.LocoMotionHandle.Action.OnUpdate(character2.AnimationManager.LocoMotionHandle, 0.1f);*/
                    }

                    /*Game.Instance.UI.Common.DollRoom.UpdateCharacter();
                    Game.Instance.UI.Common.DollRoom.UpdateCharacter();
                    Game.Instance.UI.Common.DollRoom.UpdateAvatarRenderers();
                    Game.Instance.UI.Common.DollRoom.OnCharacterUpdated(data.View.CharacterAvatar);
                    //Game.Instance.UI.Common.DollRoom.DollRoomVisualSettingsOff(); (Hide mythics???)
                    Game.Instance.UI.Common.DollRoom.GetAvatar();*/
                    //Game.Instance.UI.Common.DollRoom.SetAvatar(data.View.CharacterAvatar);
                    /* FXUIHandlerHandler.dollPcView.OnHide();
                     //FXUIHandlerHandler.dollPcView.BindViewImplementation();
                     FXUIHandlerHandler.dollPcView.OnShow();
                     FXUIHandlerHandler.dollPcView.RefreshView();
                     FXUIHandlerHandler.dollPcView.*/
                    //data.View.CharacterAvatar.IsDirty = true;
                }
                catch (Exception e)
                {
                    VisualAdjustments.Main.logger.Log(e.StackTrace);
                    throw;
                }
            }
        }

        public Slider primSlider;

        public int primindex
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
                    if (value != m_Primxindex)
                    {
                        m_Primxindex = value;
                        UpdatePreview();
                    }
                }
                PrimaryIndex.text = value.ToString();
            }
        }

        public static int m_Primxindex = 0;
        public TMP_InputField PrimaryIndex;
        public Slider secondSlider;

        public int secondindex
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

                    if (value != m_SecondaryIndex)
                    {
                        m_SecondaryIndex = value;
                        UpdatePreview();
                    }
                }
                SecondaryIndex.text = value.ToString();
            }
        }

        public static int m_SecondaryIndex = 0;
        public TMP_InputField SecondaryIndex;

        //public static int secondindex
        //{
        // }
        public TextMeshProUGUI Name;

        public TextMeshProUGUI Name2;
        public TextMeshProUGUI Race;
        public TextMeshProUGUI Sex;
        public TextMeshProUGUI AddRemoveButton;
        private static UnitEntityData m_Data;

        //public static TMP_FontAsset font;
        public Transform contentAllEE;

        public Transform contentCurrentEE;
        public Transform buttonTemplate;
        public Dictionary<string, Button> currentEELButtons = new Dictionary<string, Button>();
        public Dictionary<string, GameObject> allEELButtons = new Dictionary<string, GameObject>();
        public Dictionary<int, string> weaponIndices = new Dictionary<int, string>();
        public Dictionary<Toggle, TextMeshProUGUI> hidebuttons = new Dictionary<Toggle, TextMeshProUGUI>();

        public static VisualAdjustments.Settings.CharacterSettings settings
        {
            get
            {
                if (m_settings == null /*|| data != m_Data*/ || loaded)
                {
                    if (VisualAdjustments.Main.settings.characterSettings.TryGetValue(data.UniqueId, out VisualAdjustments.Settings.CharacterSettings setting))
                    {
                        m_settings = setting;
                    }
                    else
                    {
                        var charinfo = new CharInfo
                        {
                            GUID = data.Progression.GetEquipmentClass().AssetGuidThreadSafe,
                            Name = data.Progression.GetEquipmentClass().Name
                        };
                        if (data.IsStoryCompanion())
                        {
                            charinfo.Name = "Default";
                        }
                        var fb = new VisualAdjustments.Settings.CharacterSettings
                        {
                            characterName = data.CharacterName,
                            classOutfit = charinfo,
                            uniqueid = data.UniqueId
                        };
                        VisualAdjustments.Main.settings.AddCharacterSettings(data, fb);
                        m_settings = fb;
                    }
                    loaded = false;
                }
                return m_settings;
            }
            set
            {
                m_settings = value;
            }
        }

        private static VisualAdjustments.Settings.CharacterSettings m_settings;

        public static UIManager CreateObject()
        {
            //This is the method that get's called when it is time to create the UI.  This happens every time a scene is loaded.

            try
            {
                //  VisualAdjustments.Main.logger.Log("createdobject");
                //if (Game.Instance == null) return null;
                //  VisualAdjustments.Main.logger.Log("HasInstance");
                if (Game.Instance.UI == null) return null;
                // VisualAdjustments.Main.logger.Log("HasUIInstance");
                if (Game.Instance.UI.Canvas == null) return null;
                //  VisualAdjustments.Main.logger.Log("HasUICanvas");
                if (!BundleManger.IsLoaded(Source)) throw new NullReferenceException("NotLoaded");
                //  VisualAdjustments.Main.logger.Log("IsLoaded");
                //
                //Attempt to get the wrath objects needed to build the UI
                //
                var staticCanvas = Game.Instance.UI.Canvas.RectTransform.Find("ServiceWindowsPCView/InventoryPCView/Inventory");
                // VisualAdjustments.Main.logger.Log("Found Static Canvas");
                //var background = staticCanvas.Find("HUDLayout/CombatLog_New/Background/Background_Image").GetComponent<Image>(); //Using the path we found earlier we get the sprite component

                //get font

                //
                //Attempt to get the objects loaded from the AssetBundles and build the window.
                //
                window = GameObject.Instantiate((BundleManger.LoadedPrefabs[Source].transform.Find("Canvas"))); //We ditch the TutorialCanvas as talked about in the Wiki, we will attach it to a different parent
                //VisualAdjustments.Main.logger.Log("Instantiated");                                             //window.localScale = new Vector3((float)0.76, (float)0.76, (float)0.76);
                //VisualAdjustments.Main.logger.Log("createdobject2");
                window.SetParent(staticCanvas, false); //Attaches our window to the static canvas
                window.SetAsLastSibling(); //Our window will always be under other UI elements as not to interfere with the game. Top of the list has the lowest priority
                                           // VisualAdjustments.Main.logger.Log("attached");
                                           // if(SettingsWrapper.Reuse) window.Find("Background").GetComponent<Image>().sprite = background.sprite; //Sets the background sprite to the one used in CombatLog_New
                window.localPosition = new Vector3(0, 27, 0);
                window.localScale = new Vector3((float)0.76, (float)0.76, (float)0.76);
                window.Find("Menus").gameObject.active = false;
                //   VisualAdjustments.Main.logger.Log("disabled");
                //VisualAdjustments.Main.logger.Log("createdobject3");
                // content = ;
                var cmp = window.gameObject.EnsureComponent<UIManager>();
                manager = cmp;
                //VisualAdjustments.Main.logger.Log("added component");
                // cmp.Awake();
                return cmp; //This adds this class as a component so it can handle events, button clicks, awake, update, etc.
            }
            catch (Exception ex)
            {
                VisualAdjustments.Main.logger.Error(ex.StackTrace);
            }
            return new UIManager();
        }

        public Transform overridesobject;
        public TMP_Dropdown overridedropdown;
        public static bool initiated = false;

        public void handleOverrideDropdownChanged(int index)
        {
            // VisualAdjustments.Main.logger.Log(index.ToString());
            if (index != 10)
            {
                overridesobject.Find("WeaponStyleSelect").gameObject.SetActive(false);
                foreach (var thing in buttonsEELOverride)
                {
                    // if (index != 13)
                    if ((index == 13 && thing.Key != "None") || (index != 13 && !EELDictionaries[index].ContainsKey(thing.Key) && thing.Key != "None"))
                    {
                        thing.Value.gameObject.SetActive(false);
                    }
                    else
                if (thing.Key == "None")
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
                    buttontoaddbuttoncomponentt.onClick.AddListener(new UnityAction(() =>
                    {
                        setsetting(overridedropdown.value, "");
                        UpdatePreview();
                    }));
                    buttonsEELOverride.Add("None", buttontoaddD);
                }

                if (index != 13)
                {
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
                                    //buttontoaddtext.text = eename.Value;
                                    buttontoaddtext.text = GetTextAndSetupCategory(eename.Value);
                                    //buttontoaddtext.text = Kingmaker.Cheats.Utilities.GetBlueprint<BlueprintScriptableObject>(eename.Key.assetId).name;
                                }

                                //buttontoaddtext.font = font;
                                //buttontoaddtext.color = Color.white;
                                buttontoadd.transform.SetParent(overridecontent.transform, false);
                                var buttontoaddbuttoncomponent = buttontoadd.GetComponent<Button>();
                                buttontoaddbuttoncomponent.onClick = new Button.ButtonClickedEvent();
                                buttonsEELOverride.Add(eename.Key.assetId, buttontoadd);
                                buttontoaddbuttoncomponent.onClick.AddListener(new UnityAction(() =>
                                {
                                    setsetting(index, eename.Key.assetId);
                                    UpdatePreview();
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
                else
                {
                    try
                    {
                        {
                            var buttontoadd = Instantiate(buttonTemplate);
                            var buttontoaddtext = buttontoadd.Find("TextB").GetComponent<TextMeshProUGUI>();
                            // if ()
                            {
                                //buttontoaddtext.text = eename.Value;
                                buttontoaddtext.text = "Light/Medium Barding";
                                //buttontoaddtext.text = Kingmaker.Cheats.Utilities.GetBlueprint<BlueprintScriptableObject>(eename.Key.assetId).name;
                            }

                            //buttontoaddtext.font = font;
                            //buttontoaddtext.color = Color.white;
                            buttontoadd.transform.SetParent(overridecontent.transform, false);
                            var buttontoaddbuttoncomponent = buttontoadd.GetComponent<Button>();
                            buttontoaddbuttoncomponent.onClick = new Button.ButtonClickedEvent();
                            buttonsEELOverride.Add("Light/Medium Barding", buttontoadd);
                            buttontoaddbuttoncomponent.onClick.AddListener(new UnityAction(() =>
                            {
                                GlobalVisualInfo.Instance.ForCharacter(data).settings.overridebarding = 1;
                                UpdatePreview();
                            }));
                        }
                        {
                            var buttontoadd = Instantiate(buttonTemplate);
                            var buttontoaddtext = buttontoadd.Find("TextB").GetComponent<TextMeshProUGUI>();
                            // if ()
                            {
                                //buttontoaddtext.text = eename.Value;
                                buttontoaddtext.text = "Heavy Barding";
                                //buttontoaddtext.text = Kingmaker.Cheats.Utilities.GetBlueprint<BlueprintScriptableObject>(eename.Key.assetId).name;
                            }

                            //buttontoaddtext.font = font;
                            //buttontoaddtext.color = Color.white;
                            buttontoadd.transform.SetParent(overridecontent.transform, false);
                            var buttontoaddbuttoncomponent = buttontoadd.GetComponent<Button>();
                            buttontoaddbuttoncomponent.onClick = new Button.ButtonClickedEvent();
                            buttonsEELOverride.Add("Heavy Barding", buttontoadd);
                            buttontoaddbuttoncomponent.onClick.AddListener(new UnityAction(() =>
                            {
                                GlobalVisualInfo.Instance.ForCharacter(data).settings.overridebarding = 2;
                                UpdatePreview();
                            }));
                        }
                    }
                    catch (Exception e)
                    {
                        VisualAdjustments.Main.logger.Log(e.ToString());
                        throw;
                    }
                }
            }
            else if (index == 10)
            {
                overridesobject.Find("WeaponStyleSelect").gameObject.SetActive(true);
                HandleWeaponSelectorSelected(0);
            }
            else if (index == 13)
            {
            }
        }

        public void HandleWeaponSelectorSelected(int index)
        {
            if (weaponIndices.TryGetValue(index, out string weaponstyle))
            {
                SelectedWeaponType = weaponstyle;
                if (EquipmentResourcesManager.Weapons.TryGetValue(weaponstyle, out Dictionary<BlueprintRef, string> Weapons))
                {
                    //overridecontent; //= overridesobject.Find("Scroll View/Viewport/Content");
                    foreach (var thing in buttonsEELOverride)
                    {
                        if (!Weapons.ContainsKey(thing.Key) && thing.Key != "None")
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
                        buttontoaddbuttoncomponentt.onClick.AddListener(new UnityAction(() =>
                        {
                            //settings.weaponOverrides[(new OverrideInfo(weaponstyle, slot, primorsec))] = null;
                            //settings.weaponOverrides[(new Tuple<string, int, bool>(weaponstyle, slot, primorsec))] = null;
                            settings.weaponOverrides[new Tuple<string, int, bool>(weaponstyle, slot, primorsec).ToString()] = null;
                            //  VisualAdjustments.Main.logger.Log("settonull");
                            UpdatePreview();
                            //Set appropriate assetguid to nothing
                            // setsetting(overridesdropdown.value, "");
                        }));
                        buttonsEELOverride.Add("None", buttontoaddD);
                    }
                    foreach (var weapon in Weapons)
                    {
                        if (!buttonsEELOverride.ContainsKey(weapon.Key.assetId))
                        {
                            try
                            {
                                var buttontoadd = Instantiate(buttonTemplate);
                                var buttontoaddtext = buttontoadd.Find("TextB").GetComponent<TextMeshProUGUI>();
                                // if ()
                                {
                                    buttontoaddtext.text = weapon.Value;
                                    //buttontoaddtext.text = Kingmaker.Cheats.Utilities.GetBlueprint<BlueprintScriptableObject>(eename.Key.assetId).name;
                                }
                                //buttontoaddtext.font = font;
                                // buttontoaddtext.color = Color.white;
                                buttontoadd.transform.SetParent(overridecontent.transform, false);
                                var buttontoaddbuttoncomponent = buttontoadd.GetComponent<Button>();
                                buttontoaddbuttoncomponent.onClick = new Button.ButtonClickedEvent();
                                buttonsEELOverride.Add(weapon.Key.assetId, buttontoadd);
                                buttontoaddbuttoncomponent.onClick.AddListener(new UnityAction(() =>
                                {
                                    settings.weaponOverrides[new Tuple<string, int, bool>(weaponstyle, slot, primorsec).ToString()] = weapon.Key;
                                    UpdatePreview();
                                    //settings.weaponOverrides[(new Tuple<string,int,bool>(weaponstyle, slot, primorsec))] = weapon.Key;
                                    //Set appropriate weapon override
                                    //setsetting(index, weapon.Key.assetId);
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
            }
        }

        private void Awake()
        {
            //if (loaded2) return;
            try
            {
                //This is a unity message that runs once when the script activates (Check Unity documenation for the differences between Start() and Awake()

                //
                // Setup the listeners when the script starts
                //
                // VisualAdjustments.Main.logger.Log("awake");

                // Setup scrollview content
                //  contentAllEE = this.transform.Find("Menus/InventoryViewWindow/AllEEs/Scroll View/Viewport/Content");
                //contentCurrentEE = this.transform.Find("Menus/InventoryViewWindow/CurrentEEs/Scroll View/Viewport/Content");

                //buttonTemplate = this.transform.Find("NewButton");
                //font = this.transform.parent.Find("Doll/DollTitle/TitleLabel").GetComponent<TextMeshProUGUI>().font;
                /*foreach (var txt in this.transform.GetComponentsInChildren<TextMeshProUGUI>(true))
                {
                    txt.font = font;
                    txt.color = Color.black;
                }
                foreach (var txt in this.transform.Find("Menus/FXBrowser/All FXs/Scroll View").GetComponentsInChildren<TextMeshProUGUI>())
                {
                    //txt.font = font;
                    txt.color = Color.white;
                }
                foreach (var txt in this.transform.Find("Menus/FXBrowser/FXInfo").GetComponentsInChildren<TextMeshProUGUI>())
                {
                   // txt.font = font;
                    txt.color = Color.white;
                }*/
                foreach (var eename in VisualAdjustments.EquipmentResourcesManager.AllEEL)
                {
                    var buttontoadd = Instantiate(buttonTemplate);
                    var buttontoaddtext = buttontoadd.Find("TextB").GetComponent<TextMeshProUGUI>();
                    buttontoaddtext.text = GetTextAndSetupCategory(eename.Key);
                    // buttontoaddtext.text = eename.Key;
                    // buttontoaddtext.font = font;
                    // buttontoaddtext.color = Color.white;
                    buttontoadd.transform.SetParent(contentAllEE.transform, false);
                    var buttontoaddbuttoncomponent = buttontoadd.GetComponent<Button>();
                    buttontoaddbuttoncomponent.onClick = new Button.ButtonClickedEvent();
                    buttontoaddbuttoncomponent.onClick.AddListener(new UnityAction(() =>
                    {
                        HandleEEClicked(buttontoaddtext.text);
                    }));
                    allEELButtons.Add(buttontoaddtext.text, buttontoadd.gameObject);
                }
                //m_Data = Kingmaker.UI.Common.UIUtility.GetCurrentCharacter();
                // UISelectGrid = transform.Find("UISelectionGrid").gameObject;
                //switch font
                //var texts = ;

                // setup UI toggles
                {
                    /*  //primary toggle
                      var button = this.transform.Find("ToggleButton").GetComponent<Button>();
                      this.transform.Find("ToggleButton/Text (TMP)").GetComponent<TextMeshProUGUI>().color = Color.white;
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

                          //FX Browser
                          var FXBrowser = UISelectionGrid.Find("FX").GetComponent<Button>();
                          FXBrowser.onClick = new Button.ButtonClickedEvent();
                          FXBrowser.onClick.AddListener(new UnityAction(() =>
                          {
                              HandleUIToggle(UISelectionGrid.Find("FX/Text (TMP)").GetComponent<TextMeshProUGUI>().text);
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
                      }*/
                }
                // setup EE Picker
                {
                    /*  var eepicker = this.transform.Find("Menus/InventoryViewWindow/EEInfo");
                      foreach (var text in eepicker.GetComponentsInChildren<TextMeshProUGUI>())
                      {
                          text.color = Color.white;
                      }
                      eepicker.Find("FlexibleColorPicker").gameObject.SetActive(false);
                      Name = eepicker.Find("TextboxesValues/Name")
                          .GetComponent<TextMeshProUGUI>();
                      Name2 = eepicker.Find("FlexibleColorPicker/NameValue")
                          .GetComponent<TextMeshProUGUI>();
                      Race = eepicker.Find("TextboxesValues/Race")
                          .GetComponent<TextMeshProUGUI>();
                      Sex = eepicker.Find("TextboxesValues/Sex")
                          .GetComponent<TextMeshProUGUI>();
                      AddRemoveButton = this.transform.Find("Menus/InventoryViewWindow/AddRemove/Button/ButtonText").gameObject;

                      var AddRemoveButtonButton = this.transform.Find("Menus/InventoryViewWindow/AddRemove/Button").GetComponent<Button>();
                      AddRemoveButtonButton.onClick = new Button.ButtonClickedEvent();
                      AddRemoveButtonButton.onClick.AddListener(new UnityAction(() =>
                      {
                          addRemoveButton();
                      }));*/

                    //Primary Color Picker
                    {
                        // PrimaryIndex = EE.Find("ColorSelectors/Primary/InputFieldIndex").GetComponent<TMP_InputField>();
                        //  PrimaryIndex.onValueChanged = new TMP_InputField.OnChangeEvent();
                        //  PrimaryIndex.onValueChanged.AddListener((string val) =>
                        // {
                        //     handleSliderInputField(val, true);
                        // });
                        /*  PrimaryIndex.textComponent.color = Color.white;
                          var IncreaseButton = eepicker.Find("ColorSelectors/Primary/PositiveIncrement").GetComponent<Button>();
                          var DecreaseButton = eepicker.Find("ColorSelectors/Primary/NegativeIncrement").GetComponent<Button>();
                          IncreaseButton.onClick = new Button.ButtonClickedEvent();
                          IncreaseButton.onClick.AddListener(new UnityAction(() =>
                          {
                              increasePrimaryButton();
                          //UpdatePreview();
                      }));
                          DecreaseButton.onClick = new Button.ButtonClickedEvent();
                          DecreaseButton.onClick.AddListener(new UnityAction(() =>
                          {
                              decreasePrimaryButton();
                          //UpdatePreview();
                      }));
                          primSlider = eepicker.Find("ColorSelectors/Primary/Slider").GetComponent<Slider>();
                          primSlider.onValueChanged = new Slider.SliderEvent();
                          primSlider.onValueChanged.AddListener(new UnityAction<float>((float value) =>
                          {
                              primindex = (int)value;
                          // UpdatePreview();
                      }));
                          PrimNoColor = eepicker.Find("ColorSelectors/Primary/Available").gameObject;
                          PrimNoColor.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = Color.white;
                      }
                      //Secondary Color Picker
                      {
                          SecondaryIndex = eepicker.Find("ColorSelectors/Secondary/InputFieldIndex").GetComponent<TMP_InputField>();
                          SecondaryIndex.onValueChanged = new TMP_InputField.OnChangeEvent();
                          SecondaryIndex.onValueChanged.AddListener((string val) =>
                          {
                              handleSliderInputField(val, false);
                          });
                          SecondaryIndex.textComponent.color = Color.white;
                          var IncreaseButton = eepicker.Find("ColorSelectors/Secondary/PositiveIncrement").GetComponent<Button>();
                          var DecreaseButton = eepicker.Find("ColorSelectors/Secondary/NegativeIncrement").GetComponent<Button>();
                          IncreaseButton.onClick = new Button.ButtonClickedEvent();
                          IncreaseButton.onClick.AddListener(new UnityAction(() =>
                          {
                              increaseSecondaryButton();
                          // UpdatePreview();
                      }));
                          DecreaseButton.onClick = new Button.ButtonClickedEvent();
                          DecreaseButton.onClick.AddListener(new UnityAction(() =>
                          {
                              decreaseSecondaryButton();
                          //UpdatePreview();
                      }));
                          secondSlider = eepicker.Find("ColorSelectors/Secondary/Slider").GetComponent<Slider>();
                          secondSlider.onValueChanged = new Slider.SliderEvent();
                          secondSlider.onValueChanged.AddListener(new UnityAction<float>((float value) =>
                          {
                              secondindex = (int)value;
                          // UpdatePreview();
                      }));
                          SecNoColor = eepicker.Find("ColorSelectors/Secondary/Available").gameObject;
                          SecNoColor.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = Color.white;*/
                    }
                    //Custom Color Picker
                    {
                        // var newpickersarray = new FlexibleColorPicker.Picker[]{mainpicker,rpicker};
                    }
                    // setup filter fields
                    {
                        /* // All EE's
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
                         resetbuttonText.color = Color.white;*/
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
                        if (buttontransform != null)
                        {
                            var buttontoggle = buttontransform;
                            buttontoggle.onValueChanged = new Toggle.ToggleEvent();

                            TextMeshProUGUI buttontext = buttontransform.transform.Find("Label").GetComponent<TextMeshProUGUI>();

                            //VisualAdjustments.Main.logger.Log(buttontext.text);
                            //VisualAdjustments.Main.logger.Log(settings.ToString());
                            switch (buttontext.text)
                            {
                                case "Hide Cap":
                                    buttontoggle.onValueChanged.AddListener((bool state) =>
                                    {
                                        handleHideEquipment(state, ref settings.hideCap);
                                    });
                                    buttontoggle.isOn = settings.hideCap;
                                    hidebuttons.Add(buttontoggle, buttontext);
                                    break;

                                case "Hide Helmet":
                                    buttontoggle.onValueChanged.AddListener((bool state) =>
                                    {
                                        handleHideEquipment(state, ref settings.hideHelmet);
                                    });
                                    buttontoggle.isOn = settings.hideHelmet;
                                    hidebuttons.Add(buttontoggle, buttontext);
                                    break;

                                case "Hide Glasses":
                                    buttontoggle.onValueChanged.AddListener((bool state) =>
                                    {
                                        handleHideEquipment(state, ref settings.hideGlasses);
                                    });
                                    buttontoggle.isOn = settings.hideGlasses;
                                    hidebuttons.Add(buttontoggle, buttontext);
                                    break;

                                case "Hide Shirt":
                                    buttontoggle.onValueChanged.AddListener((bool state) =>
                                    {
                                        handleHideEquipment(state, ref settings.hideShirt);
                                    });
                                    buttontoggle.isOn = settings.hideShirt;
                                    hidebuttons.Add(buttontoggle, buttontext);
                                    break;

                                case "Hide Class Gear":
                                    buttontoggle.onValueChanged.AddListener((bool state) =>
                                    {
                                        handleHideEquipment(state, ref settings.hideClassCloak);
                                    });
                                    buttontoggle.isOn = settings.hideClassCloak;
                                    hidebuttons.Add(buttontoggle, buttontext);
                                    break;

                                case "Hide Cloak":
                                    buttontoggle.onValueChanged.AddListener((bool state) =>
                                    {
                                        handleHideEquipment(state, ref settings.hideItemCloak);
                                    });
                                    buttontoggle.isOn = settings.hideItemCloak;
                                    hidebuttons.Add(buttontoggle, buttontext);
                                    break;

                                case "Hide Armor":
                                    buttontoggle.onValueChanged.AddListener((bool state) =>
                                    {
                                        handleHideEquipment(state, ref settings.hideArmor);
                                    });
                                    buttontoggle.isOn = settings.hideArmor;
                                    hidebuttons.Add(buttontoggle, buttontext);
                                    break;

                                case "Hide Bracers":
                                    buttontoggle.onValueChanged.AddListener((bool state) =>
                                    {
                                        handleHideEquipment(state, ref settings.hideBracers);
                                    });
                                    buttontoggle.isOn = settings.hideBracers;
                                    hidebuttons.Add(buttontoggle, buttontext);
                                    break;

                                case "Hide Gloves":
                                    buttontoggle.onValueChanged.AddListener((bool state) =>
                                    {
                                        handleHideEquipment(state, ref settings.hideGloves);
                                    });
                                    buttontoggle.isOn = settings.hideGloves;
                                    hidebuttons.Add(buttontoggle, buttontext);
                                    break;

                                case "Hide Boots":
                                    buttontoggle.onValueChanged.AddListener((bool state) =>
                                    {
                                        handleHideEquipment(state, ref settings.hideBoots);
                                    });
                                    buttontoggle.isOn = settings.hideBoots;
                                    hidebuttons.Add(buttontoggle, buttontext);
                                    break;

                                case "Hide Weapons":
                                    buttontoggle.onValueChanged.AddListener((bool state) =>
                                    {
                                        handleHideEquipment(state, ref settings.hideWeapons);
                                    });
                                    buttontoggle.isOn = settings.hideWeapons;
                                    hidebuttons.Add(buttontoggle, buttontext);
                                    break;

                                case "Hide Sheaths":
                                    buttontoggle.onValueChanged.AddListener((bool state) =>
                                    {
                                        handleHideEquipment(state, ref settings.hideSheaths);
                                    });
                                    buttontoggle.isOn = settings.hideSheaths;
                                    hidebuttons.Add(buttontoggle, buttontext);
                                    break;

                                case "Hide Belt Items":
                                    buttontoggle.onValueChanged.AddListener((bool state) =>
                                    {
                                        handleHideEquipment(state, ref settings.hideBeltSlots);
                                    });
                                    buttontoggle.isOn = settings.hideBeltSlots;
                                    hidebuttons.Add(buttontoggle, buttontext);
                                    break;

                                case "Hide Quiver":
                                    buttontoggle.onValueChanged.AddListener((bool state) =>
                                    {
                                        handleHideEquipment(state, ref settings.hidequiver);
                                    });
                                    buttontoggle.isOn = settings.hidequiver;
                                    hidebuttons.Add(buttontoggle, buttontext);
                                    break;

                                case "Hide Enchantments":
                                    buttontoggle.onValueChanged.AddListener((bool state) =>
                                    {
                                        handleHideEquipment(state, ref settings.hideWeaponEnchantments);
                                    });
                                    buttontoggle.isOn = settings.hideWeaponEnchantments;
                                    hidebuttons.Add(buttontoggle, buttontext);
                                    break;

                                case "Hide Wings":
                                    buttontoggle.onValueChanged.AddListener((bool state) =>
                                    {
                                        handleHideEquipment(state, ref settings.hideWings);
                                    });
                                    buttontoggle.isOn = settings.hideWings;
                                    hidebuttons.Add(buttontoggle, buttontext);
                                    break;

                                case "Hide Horns":
                                    buttontoggle.onValueChanged.AddListener((bool state) =>
                                    {
                                        handleHideEquipment(state, ref settings.hideHorns);
                                    });
                                    buttontoggle.isOn = settings.hideHorns;
                                    hidebuttons.Add(buttontoggle, buttontext);
                                    break;

                                case "Hide Tail":
                                    buttontoggle.onValueChanged.AddListener((bool state) =>
                                    {
                                        handleHideEquipment(state, ref settings.hideTail);
                                    });
                                    buttontoggle.isOn = settings.hideTail;
                                    hidebuttons.Add(buttontoggle, buttontext);
                                    break;

                                case "Hide Mythic":
                                    buttontoggle.onValueChanged.AddListener((bool state) =>
                                    {
                                        handleHideEquipment(state, ref settings.hideMythic);
                                    });
                                    buttontoggle.isOn = settings.hideMythic;
                                    hidebuttons.Add(buttontoggle, buttontext);
                                    break;

                                case "Hide Barding":
                                    buttontoggle.onValueChanged.AddListener((bool state) =>
                                    {
                                        handleHideEquipment(state, ref GlobalVisualInfo.Instance.ForCharacter(data).settings.hidebarding);
                                    });
                                    buttontoggle.isOn = GlobalVisualInfo.Instance.ForCharacter(data).settings.hidebarding;
                                    hidebuttons.Add(buttontoggle, buttontext);
                                    break;
                            }
                            // buttontext.color = Color.white;
                        }
                    }
                    // overridesobject = this.transform.Find("Menus/HideEquipment/Overrides");
                    //Overridesbe
                    {
                        //var options = new List<TMP_Dropdown.OptionData>();
                        //options[0] = new TMP_Dropdown.OptionData().text;
                        // var overridesdropdown = overridesobject.Find("OverrideSelect").GetComponent<TMP_Dropdown>();
                        // overridesdropdown.transform.Find("Label").GetComponent<TextMeshProUGUI>().color = Color.white;
                        // overridesdropdown.onValueChanged = new TMP_Dropdown.DropdownEvent();
                        //overridecontent;// = overridesobject.Find("Scroll View/Viewport/Content");
                        var index2 = 0;
                        {
                            //var optiondata = new TMP_Dropdown.OptionData("Helmet");
                            EELDictionaries.Add(index2, EquipmentResourcesManager.Helm);
                            index2++;
                            //overridesdropdown.options.Add(optiondata);
                        }
                        {
                            //var optiondata = new TMP_Dropdown.OptionData("Cloak");
                            EELDictionaries.Add(index2, EquipmentResourcesManager.Cloak);
                            index2++;
                            // overridesdropdown.options.Add(optiondata);
                        }
                        {
                            //var optiondata = new TMP_Dropdown.OptionData("Shirt");
                            EELDictionaries.Add(index2, EquipmentResourcesManager.Shirt);
                            index2++;
                            // overridesdropdown.options.Add(optiondata);
                        }
                        {
                            //var optiondata = new TMP_Dropdown.OptionData("Glasses/Mask");
                            EELDictionaries.Add(index2, EquipmentResourcesManager.Glasses);
                            index2++;
                            // overridesdropdown.options.Add(optiondata);
                        }
                        {
                            //var optiondata = new TMP_Dropdown.OptionData("Armor");
                            EELDictionaries.Add(index2, EquipmentResourcesManager.Armor);
                            index2++;
                            // overridesdropdown.options.Add(optiondata);
                        }
                        {
                            // var optiondata = new TMP_Dropdown.OptionData("Bracers");
                            EELDictionaries.Add(index2, EquipmentResourcesManager.Bracers);
                            index2++;
                            // overridesdropdown.options.Add(optiondata);
                        }
                        {
                            // var optiondata = new TMP_Dropdown.OptionData("Gloves");
                            EELDictionaries.Add(index2, EquipmentResourcesManager.Gloves);
                            index2++;
                            // overridesdropdown.options.Add(optiondata);
                        }
                        {
                            // var optiondata = new TMP_Dropdown.OptionData("Boots");
                            EELDictionaries.Add(index2, EquipmentResourcesManager.Boots);
                            index2++;
                            //overridesdropdown.options.Add(optiondata);
                        }
                        /* {
                             var optiondata = new TMP_Dropdown.OptionData("Wings (FX)");
                             EELDictionaries.Add(index2, EquipmentResourcesManager.WingsFX);
                             index2++;
                             overridesdropdown.options.Add(optiondata);
                         }*/
                        {
                            // var optiondata = new TMP_Dropdown.OptionData("Mythic");
                            EELDictionaries.Add(index2, EquipmentResourcesManager.MythicOptions);
                            index2++;
                            //overridesdropdown.options.Add(optiondata);
                        }
                        {
                            //var optiondata = new TMP_Dropdown.OptionData("Wings");
                            var newwings = new Dictionary<BlueprintRef, string>();
                            foreach (var wingpair in EquipmentResourcesManager.WingsEE)
                            {
                                //VisualAdjustments.Main.logger.Log(wingpair.Key + "   " + wingpair.ToString());
                                newwings.Add(new BlueprintRef(wingpair.Value), wingpair.Key);
                            }
                            EELDictionaries.Add(index2, newwings);
                            index2++;
                            // overridesdropdown.options.Add(optiondata);
                        }
                        //Weapons
                        {
                            //   var optiondata = new TMP_Dropdown.OptionData("Weapons");
                            // overridesdropdown.options.Add(optiondata);
                            index2++;
                            // VisualAdjustments.Main.logger.Log(index2.ToString());
                            // Weaponstyles dropdown
                            {
                                var weapondropdown = overridesobject.Find("WeaponStyleSelect").GetComponent<TMP_Dropdown>();
                                weapondropdown.transform.Find("Label").GetComponent<TextMeshProUGUI>().color = Color.white;
                                var index = 0;
                                foreach (var a in EquipmentResourcesManager.Weapons)
                                {
                                    var optiondatas = new TMP_Dropdown.OptionData(a.Key);
                                    weaponIndices.Add(index, a.Key);
                                    //EELDictionaries.Add(index2, EquipmentResourcesManager.Weapons[((WeaponAnimationStyle)a).ToString()]);
                                    index++;
                                    weapondropdown.options.Add(optiondatas);
                                }
                            }
                        }
                        //Enchantments
                        {
                            // var optiondata = new TMP_Dropdown.OptionData("Enchantments");
                            //VisualAdjustments.Main.logger.Log("Enchantments" + index2);
                            EELDictionaries.Add(index2, EquipmentResourcesManager.WeaponEnchantments);
                            index2++;
                            // overridesdropdown.options.Add(optiondata);
                        }
                        //Views
                        {
                            //var optiondata = new TMP_Dropdown.OptionData("View");
                            // VisualAdjustments.Main.logger.Log("View" + index2);
                            var newunits = new Dictionary<BlueprintRef, string>();
                            foreach (var unit in EquipmentResourcesManager.Units)
                            {
                                //VisualAdjustments.Main.logger.Log(wingpair.Key + "   " + wingpair.ToString());
                                newunits.Add(new BlueprintRef(unit.Key), unit.Value);
                            }
                            EELDictionaries.Add(index2, newunits);
                            index2++;
                            // overridesdropdown.options.Add(optiondata);
                        }
                        //Barding
                        //overridesdropdown.onValueChanged.AddListener(handleOverrideDropdownChanged);
                        handleOverrideDropdownChanged(0);
                    }
                }
                //var overridesobject = this.transform.Find("Menus/HideEquipment/Overrides");

                //Weaponstyle Selector
                {
                    /*var WeaponSelector = overridesobject.Find("WeaponStyleSelect").GetComponent<TMP_Dropdown>();
                    WeaponSelector.onValueChanged = new TMP_Dropdown.DropdownEvent();
                    WeaponSelector.onValueChanged.AddListener(new UnityAction<int>((int index) =>
                    {
                        HandleWeaponSelectorSelected(index);
                        VisualAdjustments.Main.logger.Log(index.ToString());
                    }));*/
                    //handleOverrideDropdownChanged(0);
                    //Weapon Slot Selector
                    {
                        /* var SetSelectors = overridesobject.Find("WeaponSetSelectors");
                         //1
                         {
                             var toggle = SetSelectors.Find("1");
                             var togglecomponent = toggle.GetComponent<Toggle>();
                             togglecomponent.onValueChanged = new Toggle.ToggleEvent();
                             togglecomponent.onValueChanged.AddListener((bool a) =>
                             {
                                 slot = 0;
                             });
                             var PrimSec = toggle.Find("PrimSec");
                             //Prim
                             {
                                 var toggl = PrimSec.Find("Prim").GetComponent<Toggle>();
                                 toggl.onValueChanged = new Toggle.ToggleEvent();
                                 toggl.onValueChanged.AddListener((bool state) =>
                                 {
                                     slot = 0;
                                     primorsec = true;
                                 });
                             }
                             //Sec
                             {
                                 var toggl = PrimSec.Find("Sec").GetComponent<Toggle>();
                                 toggl.onValueChanged = new Toggle.ToggleEvent();
                                 toggl.onValueChanged.AddListener((bool state) =>
                                 {
                                     slot = 0;
                                     primorsec = false;
                                 });
                             }
                         }
                         //2
                         {
                             var toggle = SetSelectors.Find("2");
                             var togglecomponent = toggle.GetComponent<Toggle>();
                             togglecomponent.onValueChanged = new Toggle.ToggleEvent();
                             togglecomponent.onValueChanged.AddListener((bool a) =>
                             {
                                 slot = 1;
                             });
                             var PrimSec = toggle.Find("PrimSec");
                             //Prim
                             {
                                 var toggl = PrimSec.Find("Prim").GetComponent<Toggle>();
                                 toggl.onValueChanged = new Toggle.ToggleEvent();
                                 toggl.onValueChanged.AddListener((bool state) =>
                                 {
                                     slot = 1;
                                     primorsec = true;
                                 });
                             }
                             //Sec
                             {
                                 var toggl = PrimSec.Find("Sec").GetComponent<Toggle>();
                                 toggl.onValueChanged = new Toggle.ToggleEvent();
                                 toggl.onValueChanged.AddListener((bool state) =>
                                 {
                                     slot = 1;
                                     primorsec = false;
                                 });
                             }
                         }
                         //3
                         {
                             var toggle = SetSelectors.Find("3");
                             var togglecomponent = toggle.GetComponent<Toggle>();
                             togglecomponent.onValueChanged = new Toggle.ToggleEvent();
                             togglecomponent.onValueChanged.AddListener((bool a) =>
                             {
                                 slot = 2;
                             });
                             var PrimSec = toggle.Find("PrimSec");
                             //Prim
                             {
                                 var toggl = PrimSec.Find("Prim").GetComponent<Toggle>();
                                 toggl.onValueChanged = new Toggle.ToggleEvent();
                                 toggl.onValueChanged.AddListener((bool state) =>
                                 {
                                     slot = 2;
                                     primorsec = true;
                                 });
                             }
                             //Sec
                             {
                                 var toggl = PrimSec.Find("Sec").GetComponent<Toggle>();
                                 toggl.onValueChanged = new Toggle.ToggleEvent();
                                 toggl.onValueChanged.AddListener((bool state) =>
                                 {
                                     slot = 2;
                                     primorsec = false;
                                 });
                             }
                         }
                         //4
                         {
                             var toggle = SetSelectors.Find("4");
                             var togglecomponent = toggle.GetComponent<Toggle>();
                             togglecomponent.onValueChanged = new Toggle.ToggleEvent();
                             togglecomponent.onValueChanged.AddListener((bool a) =>
                             {
                                 slot = 3;
                             });
                             var PrimSec = toggle.Find("PrimSec");
                             //Prim
                             {
                                 var toggl = PrimSec.Find("Prim").GetComponent<Toggle>();
                                 toggl.onValueChanged = new Toggle.ToggleEvent();
                                 toggl.onValueChanged.AddListener((bool state) =>
                                 {
                                     slot = 3;
                                     primorsec = true;
                                 });
                             }
                             //Sec
                             {
                                 var toggl = PrimSec.Find("Sec").GetComponent<Toggle>();
                                 toggl.onValueChanged = new Toggle.ToggleEvent();
                                 toggl.onValueChanged.AddListener((bool state) =>
                                 {
                                     slot = 3;
                                     primorsec = false;
                                 });
                             }
                         }*/
                    }
                }
                FX.SetActive(false);
                EEPicker.SetActive(false);
                UISelectGrid.SetActive(false);
                //this.transform.Find("Menus/HideEquipment").gameObject.SetActive(false);
                initiated = true;
            }
            catch (Exception e)
            {
                VisualAdjustments.Main.logger.Log(e.ToString());
            }
        }

        public void setsetting(int index, string eelguid)
        {
            try
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
                        case 10:
                            {
                                settings.weaponOverrides[new Tuple<string, int, bool>(SelectedWeaponType, slot, primorsec).ToString()] = eelguid;
                                break;
                            }
                        case 11:
                            {
                                settings.weaponEnchantments[new Tuple<int, bool>(slot, primorsec).ToString()] = eelguid;
                                break;
                            }
                        case 12:
                            {
                                settings.overrideView = eelguid;
                                ViewManager.ReplaceView(data, settings.overrideView);
                                break;
                            }
                        case 13:
                            {
                                if (eelguid == "")
                                {
                                    GlobalVisualInfo.Instance.ForCharacter(data).settings.overridebarding = 0;
                                }
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
                        case 10:
                            {
                                settings.weaponOverrides[new Tuple<string, int, bool>(SelectedWeaponType, slot, primorsec).ToString()] = "";
                                break;
                            }
                        case 11:
                            {
                                settings.weaponEnchantments[new Tuple<int, bool>(slot, primorsec).ToString()] = "";
                                break;
                            }
                        case 12:
                            {
                                // VisualAdjustments.Main.logger.Log(eelguid);
                                settings.overrideView = "";
                                ViewManager.ReplaceView(data, settings.overrideView);
                                break;
                            }
                    }
                }
                UpdatePreview();
                CharacterManager.RebuildCharacter(data);
            }
            catch (Exception e)
            {
                VisualAdjustments.Main.logger.Log(e.ToString());
            }
        }

        private Dictionary<string, Transform> buttonsEELOverride = new Dictionary<string, Transform> { };
        private Dictionary<string, Transform> weaponbuttons = new Dictionary<string, Transform> { };
        private Dictionary<int, Dictionary<BlueprintRef, string>> EELDictionaries = new Dictionary<int, Dictionary<BlueprintRef, string>>();
        public static Dictionary<string, Dictionary<BlueprintRef, string>> EelDictionaries = new Dictionary<string, Dictionary<BlueprintRef, string>>();

        public void handleHideEquipment(bool state, ref bool settingState)
        {
            if (!this.gameObject.activeInHierarchy || window == null || !window.gameObject.activeInHierarchy) return;
            settingState = state;
            if (shouldrebuildhidebuttons)
            {
                CharacterManager.RebuildCharacter(data);
                UpdatePreview();
                // UpdatePreview();
            }
        }

        public void handleSliderInputFieldPrim(string value)
        {
            //ar numvalue = int.Parse(value);
            //value = primSlider.value.ToString();
            if (int.TryParse(value, out int result))
            {
                {
                    primindex = result;
                }
            }
            else
            {
                {
                    primindex = 0;
                }
            }
            //UpdatePreview();
        }

        public void handleSliderInputFieldSec(string value)
        {
            //ar numvalue = int.Parse(value);
            // value = secondSlider.value.ToString();
            if (int.TryParse(value, out int result))
            {
                secondindex = result;
            }
            else
            {
                {
                    secondindex = 0;
                }
            }
            //UpdatePreview();
        }

        public void increasePrimaryButton()
        {
            primindex = primindex + 1;
            //  UpdatePreview();
        }

        public void decreasePrimaryButton()
        {
            primindex = primindex - 1;
            //  UpdatePreview();
        }

        public void increaseSecondaryButton()
        {
            secondindex = secondindex + 1;
            //UpdatePreview();
        }

        public void decreaseSecondaryButton()
        {
            secondindex = secondindex - 1;
            // UpdatePreview();
        }

        public static Char[] splitchars = new Char[] { char.Parse(" ") };

        public async void HandleFilterChangedAll(string value)
        {
            // VisualAdjustments.Main.logger.Log("filterchange");
            /*var buttonstodisable = allEELButtons.Except(a => a.Key.Contains(value));
            foreach (var button in buttonstodisable)
            {
                button.Value.SetActive(false);  
            }*/
            value = AllInputField.text;
            bool hassplitter = value.Contains(" ");
            var splitstring = value.Split(UIManager.splitchars);
            bool isempty = value.IsNullOrEmpty();
            foreach (var eelbutton in allEELButtons)
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

        public void HandleFilterChangedCurrent(string value)
        {
            //VisualAdjustments.Main.logger.Log("filterchange");
            /*var buttonstodisable = allEELButtons.Except(a => a.Key.Contains(value));
            foreach (var button in buttonstodisable)
            {
                button.Value.SetActive(false);
            }*/
            value = CurrentInputField.text;
            bool hassplitter = value.Contains(" ");
            var splitstring = value.Split(UIManager.splitchars);
            bool isempty = value.IsNullOrEmpty();
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

        public void HandleUnitChangedHideEEs()
        {
            try
            {
                shouldrebuildhidebuttons = false;
                foreach (var transform in hidebuttons)
                {
                    //VisualAdjustments.Main.logger.Log(transform.Value.text);
                    var txt = transform.Value;
                    // var txt = transform.Key.transform.Find("Label")?.GetComponent<TextMeshProUGUI>();
                    //if (txt != null && txt.text != null)

                    switch (txt.text)
                    {
                        case "Hide Cap":
                            transform.Key.isOn = m_settings.hideCap;
                            break;

                        case "Hide Helmet":
                            transform.Key.isOn = m_settings.hideHelmet;
                            break;

                        case "Hide Glasses":
                            transform.Key.isOn = m_settings.hideGlasses;
                            break;

                        case "Hide Shirt":
                            transform.Key.isOn = m_settings.hideShirt;
                            break;

                        case "Hide Class Gear":
                            transform.Key.isOn = m_settings.hideClassCloak;
                            break;

                        case "Hide Cloak":
                            transform.Key.isOn = m_settings.hideItemCloak;
                            break;

                        case "Hide Armor":
                            transform.Key.isOn = m_settings.hideArmor;
                            break;

                        case "Hide Bracers":
                            transform.Key.isOn = m_settings.hideBracers;
                            break;

                        case "Hide Gloves":
                            transform.Key.isOn = m_settings.hideGloves;
                            break;

                        case "Hide Boots":
                            transform.Key.isOn = m_settings.hideBoots;
                            break;

                        case "Hide Weapons":
                            transform.Key.isOn = m_settings.hideWeapons;
                            break;

                        case "Hide Sheaths":
                            transform.Key.isOn = m_settings.hideSheaths;
                            break;

                        case "Hide Belt Items":
                            transform.Key.isOn = m_settings.hideBeltSlots;
                            break;

                        case "Hide Quiver":
                            transform.Key.isOn = m_settings.hidequiver;
                            break;

                        case "Hide Enchantments":
                            transform.Key.isOn = m_settings.hideWeaponEnchantments;
                            break;

                        case "Hide Wings":
                            transform.Key.isOn = m_settings.hideWings;
                            break;

                        case "Hide Horns":
                            transform.Key.isOn = m_settings.hideHorns;
                            break;

                        case "Hide Tail":
                            transform.Key.isOn = m_settings.hideTail;
                            break;

                        case "Hide Mythic":
                            transform.Key.isOn = m_settings.hideMythic;
                            break;
                    }
                }
                shouldrebuildhidebuttons = true;
            }
            catch (Exception e)
            {
                VisualAdjustments.Main.logger.Log(e.StackTrace);
            }
        }

        public GameObject UISelectGrid;

        public void RebuildCurrentEEButtons()
        {
            m_Data = Kingmaker.UI.Common.UIUtility.GetCurrentCharacter();
            foreach (var eebutton in currentEELButtons?.Values.ToTempList())
            {
                eebutton?.gameObject?.SafeDestroy();
            }
            currentEELButtons?.Clear();
            foreach (var currentee in data?.View?.CharacterAvatar?.EquipmentEntities)
            {
                if (!currentEELButtons.ContainsKey(currentee.name))
                {
                    var buttontoadd = Instantiate(buttonTemplate);
                    var buttontoaddtext = buttontoadd.Find("TextB").GetComponent<TextMeshProUGUI>();
                    //buttontoaddtext.text = currentee.name;
                    buttontoaddtext.text = GetTextAndSetupCategory(currentee.name);
                    // buttontoaddtext.font = font;
                    // buttontoaddtext.color = Color.white;
                    buttontoadd.transform.SetParent(contentCurrentEE.transform, false);
                    var buttontoaddbuttoncomponent = buttontoadd.GetComponent<Button>();
                    buttontoaddbuttoncomponent.onClick = new Button.ButtonClickedEvent();
                    buttontoaddbuttoncomponent.onClick.AddListener(new UnityAction(() =>
                    {
                        HandleEEClicked(buttontoaddtext.text);
                    }));
                    currentEELButtons.Add(buttontoaddtext.text, buttontoaddbuttoncomponent);
                }
            }
        }

        public void Update()
        {
            try
            {
                if (UISelectGrid.activeInHierarchy && haschanged)
                {
                    {
                        settings = VisualAdjustments.Main.settings.GetCharacterSettings(data);
                        RebuildCurrentEEButtons();
                        HandleUnitChangedHideEEs();
                        haschanged = false;
                    }
                }
            }
            catch (Exception e)
            {
                VisualAdjustments.Main.logger.Log(e.ToString());
            }
            //This is a unity message that runs each frame.
        }

        public void addRemoveButton()
        {
            //haschanged = true;
            if (EELpickerUI.selectedEntity == null)
            {
            }
            else if (data.View.CharacterAvatar.EquipmentEntities.Contains(EELpickerUI.selectedEntity))
            {
                data.View.CharacterAvatar.RemoveEquipmentEntity(selectedEntity, true);
                removeFromAddEEpart(selectedEntity);
                addToRemoveEEPart(selectedEntity);
                currentEELButtons[GetTextAndSetupCategory(selectedEntity.name)].gameObject.SafeDestroy();
                //currentEELButtons[selectedEntity.name] = null;
                currentEELButtons.Remove((GetTextAndSetupCategory(selectedEntity.name)));
                RebuildCurrentEEButtons();
                UpdatePreview();
            }
            else
            {
                data.View.CharacterAvatar.AddEquipmentEntity(selectedEntity, true);
                removeFromRemoveEEPart(selectedEntity);
                addToAddEEPart(selectedEntity, data.View.CharacterAvatar.GetPrimaryRampIndex(selectedEntity),
                    data.View.CharacterAvatar.GetSecondaryRampIndex(selectedEntity));
                RebuildCurrentEEButtons();
                UpdatePreview();
            }
            if (EELpickerUI.selectedEntity == null)
            {
                AddRemoveButton.GetComponent<TextMeshProUGUI>().text = "Null";
            }
            else
            {
                {
                    primindex = getColorFromPart(selectedEntity, true);
                    secondindex = getColorFromPart(selectedEntity, false);
                    primSlider.maxValue = selectedEntity.PrimaryRamps.Count;
                    primSlider.value = data.View.CharacterAvatar.GetPrimaryRampIndex(selectedEntity);
                    secondSlider.maxValue = selectedEntity.SecondaryRamps.Count;
                    secondSlider.value = data.View.CharacterAvatar.GetSecondaryRampIndex(selectedEntity);
                    if (selectedEntity.PrimaryColorsProfile == null)
                    {
                        PrimNoColor.SetActive(true);
                    }
                    else
                    {
                        PrimNoColor.SetActive(false);
                    }
                    if (selectedEntity.SecondaryColorsProfile == null)
                    {
                        SecNoColor.SetActive(true);
                    }
                    else
                    {
                        SecNoColor.SetActive(false);
                    }
                }
            }
            if (data.View.CharacterAvatar.EquipmentEntities.Contains(EELpickerUI.selectedEntity))
            {
                AddRemoveButton.GetComponent<TextMeshProUGUI>().text = "Remove";
            }
            else
            {
                AddRemoveButton.GetComponent<TextMeshProUGUI>().text = "Add";
            }
            RebuildCurrentEEButtons();
        }

        public void HandleUIToggle(string button)
        {
            //OutfitDoll.SetActive(false);
            //Equipment.SetActive(false);
            // FX.SetActive(false);
            //EEPicker.SetActive(false);
            switch (button)
            {
                case "Outfit/Doll":
                    OutfitDoll.SetActive(true);
                    Equipment.SetActive(false);
                    FX.SetActive(false);
                    EEPicker.SetActive(false);
                    break;

                case "EE Picker":
                    EEPicker.SetActive(true);
                    OutfitDoll.SetActive(false);
                    Equipment.SetActive(false);
                    FX.SetActive(false);
                    break;

                case "Equipment":
                    Equipment.SetActive(true);
                    OutfitDoll.SetActive(false);
                    FX.SetActive(false);
                    EEPicker.SetActive(false);
                    break;

                case "FX":
                    FX.SetActive(true);
                    OutfitDoll.SetActive(false);
                    Equipment.SetActive(false);
                    EEPicker.SetActive(false);
                    break;
            }
        }

        public GameObject PrimNoColor;
        public GameObject SecNoColor;

        private void HandleEEClicked(string eename)
        {
            try
            {
                // selectedstring = eename;
                //haschanged = true;
                selectedEntity = ResourcesLibrary.TryGetResource<EquipmentEntity>(EquipmentResourcesManager.AllEEL[FilteredAndUnfilteredEEName[eename]]);
                //this.transform.Find("InventoryViewWindow").Find("TextboxesValues").Find("Name").GetComponent<TextMeshProUGUI>().text = eename;
                // this.transform.Find("InventoryViewWindow").Find("TextboxesValues").Find("Race").GetComponent<TextMeshProUGUI>().text = eename;
                //this.transform.Find("InventoryViewWindow").Find("TextboxesValues").Find("Sex").GetComponent<TextMeshProUGUI>().text = eename;

                if (selectedEntity == null)
                {
                    AddRemoveButton.text = "Null";
                }
                else if (data.View.CharacterAvatar.EquipmentEntities.Contains(selectedEntity))
                {
                    AddRemoveButton.text = "Remove";
                }
                else
                {
                    AddRemoveButton.text = "Add";
                }

                Name.text = eename;
                Name2.text = eename;
                Race.text = getRace(selectedEntity);
                Sex.text = getGender(selectedEntity);
                /*if (selectedEntity != null /*&& data.View.CharacterAvatar.EquipmentEntities.Contains(selectedEntity)*///)
                {
                    secondSlider.maxValue = selectedEntity.SecondaryRamps.Count;
                    primSlider.maxValue = selectedEntity.PrimaryRamps.Count;
                    var indxprim = getColorFromPart(selectedEntity, true);
                    m_Primxindex = indxprim;
                    PrimaryIndex.text = indxprim.ToString();
                    var indxsec = getColorFromPart(selectedEntity, false);
                    m_SecondaryIndex = indxsec;
                    SecondaryIndex.text = indxsec.ToString();

                    //primSlider.value = data.View.CharacterAvatar.GetPrimaryRampIndex(selectedEntity);

                    //secondSlider.value = data.View.CharacterAvatar.GetSecondaryRampIndex(selectedEntity);
                    if (selectedEntity.PrimaryColorsProfile == null)
                    {
                        PrimNoColor.SetActive(true);
                    }
                    else
                    {
                        PrimNoColor.SetActive(false);
                    }
                    if (selectedEntity.SecondaryColorsProfile == null)
                    {
                        SecNoColor.SetActive(true);
                    }
                    else
                    {
                        SecNoColor.SetActive(false);
                    }
                }
            }
            catch (Exception e)
            {
                VisualAdjustments.Main.logger.Error(e.ToString());
            }
        }

        public static GameObject stash;
        public GameObject menus;
        public TMP_InputField CurrentInputField;
        public TMP_InputField AllInputField;

        public void HandleButtonClick()
        {
            if (stash == null)
            {
                stash = this.transform.parent.Find("Stash/StashContainer/PC_FilterBlock").gameObject;
            }
            // VisualAdjustments.Main.logger.Log("buttonpressed");
            //var selectiongrid = window.Find("UISelectionGrid").gameObject;

            menus.SetActive(!UISelectGrid.active);
            UISelectGrid.SetActive(!UISelectGrid.active);
            stash.SetActive(!UISelectGrid.active);

            //menus.SetActiveFast();
            //UISelectGrid.SetActiveFast();

            //haschanged = true;
            //window.Find("InventoryViewWindow").gameObject.active = !window.Find("InventoryViewWindow").gameObject.active;
            // _text.text = "Add";
        }

        public void SliderColPrimary()
        {
            primindex = (int)primSlider.value;
        }

        public void SliderColSecondary()
        {
            secondindex = (int)secondSlider.value;
        }
    }

    /*  public static class ServiceWindowsMenuVM_Patch
      {
          public static void Postfix(UnitDescriptor unitDescriptor)
          {
              UIManager.data = unitDescriptor.Unit;
              VisualAdjustments.Main.logger.Log("unit" + unitDescriptor.CharacterName);
          }
      }*/

    public static class GameObjectExtensions
    {
        /// <summary>
        /// Sets local scale instead of disabling, should be faster
        /// </summary>
        public static void SetActiveFast(this GameObject gameObject)
        {
            if (gameObject.transform.localScale == new Vector3(1, 1, 1))
            {
                gameObject.transform.localScale = new Vector3(0, 0, 0);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}