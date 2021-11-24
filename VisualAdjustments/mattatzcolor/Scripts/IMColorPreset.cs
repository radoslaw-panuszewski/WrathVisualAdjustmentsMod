using System.Collections.Generic;
using UnityEngine;

namespace imColorPicker
{
    public class IMColorPreset : ScriptableObject
    {
        public List<Color> Colors
        {
            get
            {
                return colors;
            }
        }

        [SerializeField] private List<Color> colors;

        public void Save(Color color)
        {
            colors.Add(color);
        }

        public void Remove(int index)
        {
            colors.RemoveAt(index);
        }
    }
}