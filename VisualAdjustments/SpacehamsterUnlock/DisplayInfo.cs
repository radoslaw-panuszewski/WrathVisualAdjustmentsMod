using HarmonyLib;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.ResourceLinks;
using Kingmaker.UI.ServiceWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HairUnlocker
{
    class DisplayInfo
    {
        static bool showHair = false;
        static bool showHead = false;
        static bool showEyebrows = false;
        static bool showBeards = false;
        static Gender gender = Gender.Male;
        static List<BlueprintRace> races = null;
        static Dictionary<string, string> eelLookup = new Dictionary<string, string>();
        public static string GetName(EquipmentEntityLink link)
        {
            if (link == null) return "NULL";
            if (eelLookup.ContainsKey(link.AssetId))
            {
                return eelLookup[link.AssetId];
            } else
            {
                var ee = link.Load();
                eelLookup[link.AssetId] = ee.name;
                return ee.name;
            }
        }
        public static void ShowEE(EquipmentEntityLink[] links) 
        {
            foreach(var link in links)
            {
                ModKit.UI.Label(GetName(link));
            }
        }
        public static void ShowDoll()
        {
            var dollRoom = Game.Instance.UI.Common?.DollRoom;
           // var chargen = dollRoom?.GetComponent<CharGenDollRoom>();
            if (dollRoom == null)
            {
                ModKit.UI.Label("DollRoom: NULL");
                return;
            }
            /*var controller = Game.Instance.UI.CharacterBuildController;
            if (!controller.IsShow)
            {
                ModKit.UI.Label("CharacterBuildController: Not Visible");
                return;
            }
            var visible = Traverse.Create(dollRoom).Field("m_IsVisible").GetValue<bool>();
            ModKit.UI.Label($"DollRoomVisible: {visible}");
            if (controller.LevelUpController.Doll == null)
            {
                ModKit.UI.Label("Doll: Null");
            }
            var doll = controller.LevelUpController.Doll;
            ModKit.UI.Label($"Hair: {GetName(doll.Hair)}");
            ModKit.UI.Label($"Head: {GetName(doll.Head)}");
            ModKit.UI.Label($"Eyebrows: {GetName(doll.Eyebrows)}");
            ModKit.UI.Label($"Beard: {GetName(doll.Beard)}");*/

        }
        public static void ShowHair()
        {
            if (races == null) {
                races = Game.Instance.BlueprintRoot.Progression.CharacterRaces.OfType<BlueprintRace>().ToArray()
                      .Where(bp => bp.AssetGuid.ToString().Length == 32)
                      .ToList();
            }
            GUILayout.BeginHorizontal();
            showHair = GUILayout.Toggle(showHair, "Show Hair");
            showHead = GUILayout.Toggle(showHead, "Show Head");
            showEyebrows = GUILayout.Toggle(showEyebrows, "Show Eyebrows");
            showBeards = GUILayout.Toggle(showBeards, "Show Beards");
            if(gender == Gender.Male && GUILayout.Button("Show Female"))
            {
                gender = Gender.Female;
            }
            if (gender == Gender.Female && GUILayout.Button("Show Male"))
            {
                gender = Gender.Male;
            }
            GUILayout.EndHorizontal();
            
            foreach (var race in races) {
                ModKit.UI.Label(race.Name);
                var options = gender == Gender.Male ? race.MaleOptions : race.FemaleOptions;
                if (showHair)
                {
                    ModKit.UI.Label("Hair");
                    ShowEE(options.Hair);
                }
                if (showHead)
                {
                    ModKit.UI.Label("Heads");
                    ShowEE(options.Heads);
                }
                if (showEyebrows)
                {
                    ModKit.UI.Label("Eyebrows");
                    ShowEE(options.Eyebrows);
                }
                if (showBeards)
                {
                    ModKit.UI.Label("Beards");
                    ShowEE(options.Beards);
                }
            }
            

        }
    }
}
