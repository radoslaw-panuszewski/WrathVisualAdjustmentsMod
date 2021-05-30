using Kingmaker.ResourceLinks;
using Kingmaker.Visual.CharacterSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VisualAdjustments
{
    public static class Util
    {
        const float DefaultLabelWidth = 200f;
        const float DefaultSliderWidth = 300f;
        public static void ChooseSlider<T>(string name, Dictionary<T, string> items, ref T currentItem, Action onChoose)
        {
            var currentIndex = currentItem == null ? -1 : Array.IndexOf(items.Keys.ToArray(), currentItem);
            GUILayout.BeginHorizontal();
            GUILayout.Label(name + " ", GUILayout.Width(DefaultLabelWidth));
            var newIndex = (int)Math.Round(GUILayout.HorizontalSlider(currentIndex, -1, items.Count - 1, GUILayout.Width(DefaultSliderWidth)), 0);
            if (GUILayout.Button("Prev", GUILayout.Width(45)) && currentIndex >= 0)
            {
                newIndex = currentIndex - 1;
            }
            if (GUILayout.Button("Next", GUILayout.Width(45)) && currentIndex < items.Count - 1)
            {
                newIndex = currentIndex + 1;
            }
            var displayText = newIndex == -1 ? "None" : items.Values.ElementAt(newIndex);
            GUILayout.Label(" " + displayText, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
            if (currentIndex != newIndex)
            {
                currentItem = newIndex == -1 ? default(T) : items.Keys.ElementAt(newIndex);
                onChoose();
            }
        }
        public static void ChooseSliderList<T>(string name, Dictionary<T, string> items, List<T> saved, int savedIndex, Action onChoose)
        {
            var currentItem = saved[savedIndex];
            var currentIndex = currentItem == null ? -1 : Array.IndexOf(items.Keys.ToArray(), currentItem);
            GUILayout.BeginHorizontal();
            GUILayout.Label(name + " ", GUILayout.Width(DefaultLabelWidth));
            var newIndex = (int)Math.Round(GUILayout.HorizontalSlider(currentIndex, -1, items.Count - 1, GUILayout.Width(DefaultSliderWidth)), 0);
            if (GUILayout.Button("Prev", GUILayout.Width(45)) && currentIndex >= 0)
            {
                newIndex = currentIndex - 1;
            }
            if (GUILayout.Button("Next", GUILayout.Width(45)) && currentIndex < items.Count - 1)
            {
                newIndex = currentIndex + 1;
            }
            if (GUILayout.Button("Remove", GUILayout.ExpandWidth(false)))
            {
                saved.RemoveAt(savedIndex);
                onChoose();
                return;
            }
            var displayText = newIndex == -1 ? "None" : items.Values.ElementAt(newIndex);
            GUILayout.Label(" " + displayText, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
            if (currentIndex != newIndex)
            {
                currentItem = newIndex == -1 ? default(T) : items.Keys.ElementAt(newIndex);
                saved[savedIndex] = currentItem;
                onChoose();
            }
        }
        public static void AddEquipmentEntities(this Character character, IEnumerable<EquipmentEntityLink> links, bool saved = false)
        {
            foreach (var eel in links) character.AddEquipmentEntity(eel);
        }
    }
}
