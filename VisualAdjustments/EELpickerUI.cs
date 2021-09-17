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
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Utility;
using Kingmaker.Visual.CharacterSystem;
using ModKit.Utility;
using Steamworks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityModManagerNet;

namespace VisualAdjustments
{
    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }
    }
    public class EELpickerUI
    {
        private static EquipmentEntity selectedEntity = null;
        private static string filter = "";
        private static string filter2 = "";
        public static Vector2 scroll1 = new Vector2(0, 0);
        public static Vector2 scroll2 = new Vector2(0, 0);
        public static List<string> list1;
        public static List<string> list2;

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
            return "N/A";

        }
        static void ChooseRamp(UnitEntityData unitEntityData, string label, List<Texture2D> textures, int currentRamp, Action<int> setter)
        {
            Main.ChooseFromListforee(label, textures, ref currentRamp, () => {
                setter(currentRamp);
                //CharacterManager.RebuildCharacter(unitEntityData);
            });
        }
        public static string getRampIndex(bool primorsec, EquipmentEntity ee,UnitEntityData data)
        {
            if (primorsec)
            {
                int index = data.View.CharacterAvatar.GetPrimaryRampIndex(ee);
                if (index == -1)
                {
                    return "Null";
                }
                {
                    return index.ToString();
                }
            }
            else
            {
                int index = data.View.CharacterAvatar.GetSecondaryRampIndex(ee);
                if (index == -1)
                {
                    return "Null";
                }
                else
                {
                    return index.ToString();
                }
            }
        }
        public static void OnGUI(UnitEntityData data)
        {
            try
            {
                void removeFromRemoveEEPart(EquipmentEntity ee)
                {
                    var component = data.Parts.Get<UnitPartVAEELs>();
                    if (component == null)
                    {
                        component = data.Parts.Add<UnitPartVAEELs>();
                    }

                    if (component.EEToRemove.Contains(EquipmentResourcesManager.AllEEL[ee.name]))
                    {
                        component.EEToRemove.Remove(EquipmentResourcesManager.AllEEL[ee.name]);
                    }
                    
                }
                void addToRemoveEEPart(EquipmentEntity ee)
                {
                    var component = data.Parts.Get<UnitPartVAEELs>();
                    if (component == null)
                    {
                        component = data.Parts.Add<UnitPartVAEELs>();
                    }

                    if (!component.EEToRemove.Contains(EquipmentResourcesManager.AllEEL[ee.name]))
                    {
                        component.EEToRemove.Add(EquipmentResourcesManager.AllEEL[ee.name]);
                    }
                }
                void addToAddEEPart(EquipmentEntity ee,int primary ,int secondary)
                {
                    var component = data.Parts.Get<UnitPartVAEELs>();
                    if (component == null)
                    {
                        component = data.Parts.Add<UnitPartVAEELs>();
                    }

                    if (!component.EEToAdd.Contains( a => a.AssetID == EquipmentResourcesManager.AllEEL[ee.name]))
                    {
                        component.EEToAdd.Add(new EEStorage(EquipmentResourcesManager.AllEEL[ee.name], primary,
                            secondary));
                        Main.logger.Log("add" + ee.name + EquipmentResourcesManager.AllEEL[ee.name]);
                    }
                }
                void removeFromAddEEpart(EquipmentEntity ee)
                {
                    var component = data.Parts.Get<UnitPartVAEELs>();
                    if (component == null)
                    {
                        component = data.Parts.Add<UnitPartVAEELs>();
                    }

                    if (component.EEToAdd.Contains(a => a.AssetID == EquipmentResourcesManager.AllEEL[ee.name]))
                    {
                        component.EEToAdd.Remove(a => a.AssetID == EquipmentResourcesManager.AllEEL[ee.name]);
                    }
                }
                void setColorAddEEPart(EquipmentEntity ee,int primaryIndex, int secondaryIndex)
                {
                    var component = data.Parts.Get<UnitPartVAEELs>();
                    if (component == null)
                    {
                        component = data.Parts.Add<UnitPartVAEELs>();
                    }

                    if (component.EEToAdd.Contains(a => a.AssetID == EquipmentResourcesManager.AllEEL[ee.name]))
                    {
                        var eewithcolor = component.EEToAdd.First(a => a.AssetID == EquipmentResourcesManager.AllEEL[ee.name]);
                        eewithcolor.PrimaryIndex = primaryIndex;
                        eewithcolor.SecondaryIndex = primaryIndex;
                    }
                    else
                    {
                        Main.logger.Log("coloradd" + ee.name + EquipmentResourcesManager.AllEEL[ee.name]);
                        component.EEToAdd.Add(new EEStorage(EquipmentResourcesManager.AllEEL[ee.name], primaryIndex, primaryIndex));
                    }
                    
                }
                if (!Main.enabled) return;
             
            GUILayout.Space(50f);
            
            //EEL selection
            //Main.logger.Log(scroll1.ToString());

            double rangestart = 0;
            if (scroll1.y != 0)
            {
                if (scroll1.y > 30)
                {
                    rangestart = (scroll1.y-30);
                }
                else
                {
                    rangestart = (scroll1.y);
                }
            }

            if (list1 == null)
            {
                list1 = EquipmentResourcesManager.AllEEL.Keys.ToList();
            }

            if (list2 == null)
            {
                list2 = data.View.CharacterAvatar.EquipmentEntities.Select(a => a.name).ToList();
            }
            var rangeCount = 30;
            if (rangestart+30 > list1.Count) rangeCount = list1.Count - (int)rangestart;
            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(false));
            scroll1.y =  GUILayout.VerticalScrollbar(scroll1.y, (int)(list1.Count * 0.05),0,(int)(list1.Count + (list1.Count * 0.05)),new GUILayoutOption[]{GUILayout.Height(400f*Main.UIscale),GUILayout.Width(30f*Main.UIscale)});
            GUILayout.BeginVertical(GUILayout.Width(380f));
            string txt1 = GUILayout.TextField(filter, GUILayout.Width(280f * Main.UIscale));
            GUILayout.BeginScrollView(scroll1, horizontalScrollbar: GUIStyle.none, verticalScrollbar: GUIStyle.none, new GUILayoutOption[] { GUILayout.Height(400f * Main.UIscale), GUILayout.Width(380f * Main.UIscale) });
            rangeCount = Math.Min(rangeCount,list1.Count);
            rangestart = Math.Max(rangestart, 0);
            //Main.logger.Log("rangecount "+ rangeCount + "rangestart " + rangestart + "list1count " + list1.Count);
            foreach (var VARIABLE in list1.GetRange((int)rangestart, rangeCount))
            {
                {
                    if (GUILayout.Button(VARIABLE /*+ "   " + list1.IndexOf(VARIABLE)*/, GUILayout.Width(280f * Main.UIscale)))
                    {
                        selectedEntity =
                            ResourcesLibrary.TryGetResource<EquipmentEntity>(
                                EquipmentResourcesManager.AllEEL[VARIABLE]);
                    }

                }
            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            if (list1 == null || txt1 != filter)
            {
                list1 = EquipmentResourcesManager.AllEEL.Keys.Where(a => a.Contains(txt1, StringComparison.OrdinalIgnoreCase)).ToList();
                filter = txt1;
            }

                // labels
                //GUILayout.BeginHorizontal(GUILayout.ExpandWidth(false));
                GUILayout.BeginHorizontal(GUILayout.Width(300f * Main.UIscale));
                if (selectedEntity != null)
                {
                    GUILayout.BeginVertical(GUILayout.Width(300f * Main.UIscale));
                    GUILayout.Label("Name: " + selectedEntity.name, GUILayout.ExpandWidth(false));
                    GUILayout.Label("Sex: " + getGender(selectedEntity), GUILayout.ExpandWidth(false));
                    GUILayout.Label("Race: " + getRace(selectedEntity), GUILayout.ExpandWidth(false));
                    GUILayout.Label("Primary Color: " + getRampIndex(true, selectedEntity, data), GUILayout.ExpandWidth(false));
                    GUILayout.Label("Secondary Color: " + getRampIndex(false, selectedEntity, data), GUILayout.ExpandWidth(false));

                    //GUILayout.EndVertical();
                }
                else
                {
                    GUILayout.BeginVertical(GUILayout.Width(300f * Main.UIscale));
                    GUILayout.Label("Null", GUILayout.ExpandWidth(false));
                    GUILayout.Label("Null", GUILayout.ExpandWidth(false));
                    GUILayout.Label("Null", GUILayout.ExpandWidth(false));
                    GUILayout.Label("Null", GUILayout.ExpandWidth(false));
                    GUILayout.Label("Null", GUILayout.ExpandWidth(false));
                  //  GUILayout.EndVertical();
                }

                //GUILayout.Space(5f);
                // values
                //GUILayout.BeginHorizontal(GUILayout.ExpandWidth(false));
            //GUILayout.BeginVertical(GUILayout.Width(260f * Main.UIscale));
            /*if (selectedEntity == null)
            {
                GUILayout.Label("Null", GUILayout.ExpandWidth(false));
                GUILayout.Label("Null", GUILayout.ExpandWidth(false));
                GUILayout.Label("Null", GUILayout.ExpandWidth(false));
                GUILayout.Label("Null", GUILayout.ExpandWidth(false));
                GUILayout.Label("Null", GUILayout.ExpandWidth(false));
            }
            else
            {
                GUILayout.Label(selectedEntity.name, GUILayout.ExpandWidth(false));
                GUILayout.Label(getGender(selectedEntity), GUILayout.ExpandWidth(false));
                GUILayout.Label(getRace(selectedEntity), GUILayout.ExpandWidth(false));
                GUILayout.Label(getRampIndex(true,selectedEntity,data), GUILayout.ExpandWidth(false));
                GUILayout.Label(getRampIndex(false, selectedEntity, data), GUILayout.ExpandWidth(false));
            }*/
            if (selectedEntity != null)
                {
                    if (data.View.CharacterAvatar.EquipmentEntities.Contains(selectedEntity))
                    {
                        if (GUILayout.Button("Remove", GUILayout.Width(150f * Main.UIscale)))
                        {
                            data.View.CharacterAvatar.RemoveEquipmentEntity(selectedEntity, true);
                            var eel = new EquipmentEntityLink();
                            eel.AssetId = EquipmentResourcesManager.AllEEL[selectedEntity.name];
                            var ssri = new Character.SavedSelectedRampIndices();
                            ssri.EquipmentEntityLink = eel;
                            ssri.PrimaryIndex = 0;
                            ssri.SecondaryIndex = 0;
                            data.View.CharacterAvatar.m_SavedRampIndices.Remove(ssri);
                            data.View.CharacterAvatar.m_SavedEquipmentEntities.Remove(eel);
                            data.View.CharacterAvatar.EquipmentEntitiesForPreload.Remove(eel);
                            removeFromAddEEpart(selectedEntity);
                            addToRemoveEEPart(selectedEntity);
                        }
                        if (getRampIndex(true, selectedEntity, data) != "null")
                        {
                            ChooseRamp(data, "Primary Color", selectedEntity.PrimaryRamps,
                                data.View.CharacterAvatar.GetPrimaryRampIndex(selectedEntity), (int index) =>
                                {
                                    data.View.CharacterAvatar.SetPrimaryRampIndex(selectedEntity, index, true);
                                    setColorAddEEPart(selectedEntity, index, data.View.CharacterAvatar.GetSecondaryRampIndex(selectedEntity));
                                    data.View.CharacterAvatar.IsDirty = true;
                                });
                        }
                        if (getRampIndex(false, selectedEntity, data) != "null")
                        {
                            ChooseRamp(data, "Secondary Color", selectedEntity.SecondaryRamps,
                                data.View.CharacterAvatar.GetSecondaryRampIndex(selectedEntity), (int index) =>
                                {
                                    data.View.CharacterAvatar.SetSecondaryRampIndex(selectedEntity, index, true);
                                    setColorAddEEPart(selectedEntity, data.View.CharacterAvatar.GetPrimaryRampIndex(selectedEntity), index);
                                    data.View.CharacterAvatar.IsDirty = true;
                                });
                        }
                    }
                    else
                    {
                        if (GUILayout.Button("Add", GUILayout.Width(150f * Main.UIscale)))
                        {

                            var eel = new EquipmentEntityLink();
                            eel.AssetId = EquipmentResourcesManager.AllEEL[selectedEntity.name];

                            data.View.CharacterAvatar.EquipmentEntitiesForPreload.Add(eel);
                            data.View.CharacterAvatar.m_SavedEquipmentEntities.Add(eel);
                            var ssri = new Character.SavedSelectedRampIndices();
                            ssri.EquipmentEntityLink = eel;
                            ssri.PrimaryIndex = 0;
                            ssri.SecondaryIndex = 0;
                            data.View.CharacterAvatar.m_SavedRampIndices.Add(ssri);
                            data.View.CharacterAvatar.AddEquipmentEntity(selectedEntity, true);
                            removeFromRemoveEEPart(selectedEntity);
                            addToAddEEPart(selectedEntity, data.View.CharacterAvatar.GetPrimaryRampIndex(selectedEntity), data.View.CharacterAvatar.GetSecondaryRampIndex(selectedEntity));
                        }
                        list2 = data.View.CharacterAvatar.EquipmentEntities.Where(a => a.name.Contains(filter2, StringComparison.OrdinalIgnoreCase)).Select(a => a.name).ToList();
                    }
                }
                GUILayout.EndVertical();
            GUILayout.EndHorizontal();



                //GUILayout.Button("")
                //current eels
                GUILayout.BeginVertical();
                string txt2 = GUILayout.TextField(filter2, GUILayout.Width(280f * Main.UIscale));
                scroll2 = GUILayout.BeginScrollView(scroll2, new GUILayoutOption[] { GUILayout.Height(400f * Main.UIscale), GUILayout.Width(380f * Main.UIscale) });
            foreach (var VARIABLE in list2)
            {
                   if (GUILayout.Button(VARIABLE, GUILayout.Width(280f * Main.UIscale)))
                   {
                    selectedEntity =
                        ResourcesLibrary.TryGetResource<EquipmentEntity>(
                            EquipmentResourcesManager.AllEEL[VARIABLE]);
                   }
            }
            GUILayout.EndScrollView();
            if (GUILayout.Button("Reset",GUILayout.Width(280f)))
            {
                var part = data.Parts.Get<UnitPartVAEELs>();
                part.EEToAdd.Clear();
                part.EEToRemove.Clear();
                CharacterManager.RebuildCharacter(data);
                }
            GUILayout.EndVertical();
            if (list2 == null || txt2 != filter2)
            {
                list2 = data.View.CharacterAvatar.EquipmentEntities.Where(a => a.name.Contains(txt2, StringComparison.OrdinalIgnoreCase)).Select(a => a.name).ToList();
                filter2 = txt2;
            }
                // GUILayout.EndHorizontal();
                // GUILayout.EndHorizontal();
                GUILayout.EndHorizontal();
            }
            catch (Exception e)
            {
                Main.logger.Log(e.ToString());
                throw;
            }
        }
    }
}
