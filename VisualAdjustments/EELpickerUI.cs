using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Cheats;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Globalmap.View;
using Kingmaker.Utility;
using Kingmaker.Visual.CharacterSystem;
using ModKit.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityModManagerNet;

namespace VisualAdjustments
{

    class EELpickerUI
    {
        private static EquipmentEntity selectedEntity = null;
        private static readonly Dictionary<string, Func<object>> TARGET_LIST = new Dictionary<string, Func<object>>()
        {
            { "None", null },
            { "Game", () => Game.Instance },
            { "Player", () => Game.Instance?.Player },
            { "Characters", () => Game.Instance?.Player?.AllCharacters },
            { "Units", () => Game.Instance?.State?.Units },
            { "Inventory", () => Game.Instance?.Player?.Inventory },
            { "Dialog", () => Game.Instance?.DialogController },
            { "Vendor", () => Game.Instance?.Vendor },
            { "Scene", () => SceneManager.GetActiveScene() },
            { "UI", () => Game.Instance?.UI },
            { "Static Canvas", () => Game.Instance?.UI?.Canvas?.gameObject },
            { "Quest Book", () => Game.Instance?.Player?.QuestBook },
            { "Kingdom", () => Game.Instance?.Player?.Kingdom },
            { "Area", () => Game.Instance?.CurrentlyLoadedArea },
            { "GlobalMap", () => Game.Instance?.Player?.GlobalMap },
            { "GlobalMapController", () => Game.Instance.GlobalMapController },
            { "GlobalMapView", () => GlobalMapView.Instance },
            { "GlobalMapUI", () => Game.Instance.UI.GlobalMapUI },
            { "Game Objects", () => UnityEngine.Object.FindObjectsOfType<GameObject>() },
            { "Unity Resources", () =>  Resources.FindObjectsOfTypeAll(typeof(GameObject)) },
        };
        private static string filter = "";
        private static string filter2 = "";
        public static Vector2 scroll1 = new Vector2(0,0);
        public static Vector2 scroll2 = new Vector2(0,0);
        public static List<string> list1;
        public static List<string> list2;
        public static void OnGUI(UnitEntityData data)
        {
            if (!Main.enabled)
                return;
            GUILayout.BeginHorizontal();
                string txt1 = GUILayout.TextField(filter,GUILayout.Width(200f));
                GUILayout.Space(50f);
                string txt2 = GUILayout.TextField(filter2, GUILayout.Width(200f));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                //EEL selection
                scroll1 = GUILayout.BeginScrollView(scroll1,  new GUILayoutOption[] {GUILayout.Height(400f),GUILayout.Width(380)});
                if (list1 == null || txt1 != filter)
                {
                    list1 = EquipmentResourcesManager.AllEEL.Keys.Where(a => a.Contains(txt1)).ToList();
                    filter = txt1;
                }
                foreach (var VARIABLE in list1)
                {
                    if(list1.IndexOf(VARIABLE) > scroll1.y-15 && list1.IndexOf(VARIABLE) < scroll1.y + 15)
                    {
                        if (GUILayout.Button(VARIABLE, GUILayout.Width(280f)))
                        {
                            selectedEntity =
                                ResourcesLibrary.TryGetResource<EquipmentEntity>(
                                    EquipmentResourcesManager.AllEEL[VARIABLE]);
                        }
                    }
                }
                GUILayout.EndScrollView();
                // selected

                //current eels
                scroll2 = GUILayout.BeginScrollView(scroll2, new GUILayoutOption[] { GUILayout.Height(400f), GUILayout.Width(380) });
                if (list2 == null || txt2 != filter2)
                {
                    list2 = data.View.CharacterAvatar.EquipmentEntities.Where(a => a.name.Contains(txt2)).Select(b => b.name).ToList();
                    filter2 = txt2;
                }
#if false
            foreach (var VARIABLE in list2.GetRange())
                {
                    if (GUILayout.Button(VARIABLE, GUILayout.Width(280f)))
                    {
                            selectedEntity =
                                ResourcesLibrary.TryGetResource<EquipmentEntity>(
                                    EquipmentResourcesManager.AllEEL[VARIABLE]);
                    }
                }
#endif
                GUILayout.EndScrollView();
                //scroll2 = GUILayout.VerticalScrollbar(scroll2, 5f, 0f, 300f);
                GUILayout.EndHorizontal();
        }
    }
}
