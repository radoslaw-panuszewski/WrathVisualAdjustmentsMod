using UnityEngine;
using System.Collections;
using VisualAdjustments;
using Kingmaker.EntitySystem.Entities;

namespace imColorPicker
{

    public class IMColorPicker {

        public Color color
        {
            get
            {
                return _color;
            }
        }

        public float H
        {
            get
            {
                return h;
            }
        }

        public float S
        {
            get
            {
                return s;
            }
        }

        public float V
        {
            get
            {
                return v;
            }
        }

        public Color _color;
        IMColorPreset preset;

        public float h = 0f, s = 1f, v = 1f;

        Rect windowRect = new Rect(20, 20, 165, 100);

        public Rect lastrect;
        public GUIStyle previewStyle;
        GUIStyle labelStyle;
        public GUIStyle svStyle, hueStyle;
        GUIStyle presetStyle, presetHighlightedStyle;
        int selectedPreset = -1;
        public string HexCol;

        public Texture2D hueTexture, svTexture;
        Texture2D circle, rightArrow, leftArrow, button, buttonHighlighted;

        public const int kHSVPickerSize = 225, kHuePickerWidth = 32;

        public IMColorPicker() : this(Color.red, null) { }
        public IMColorPicker(Color c) : this(c, null) { }
        public IMColorPicker(IMColorPreset pr) : this(Color.red, pr) { }
        public IMColorPicker(Color c, IMColorPreset pr)
        {
            _color = c;
            preset = pr;
            Setup();
        }

        void Setup()
        {
            IMColorUtil.RGBToHSV(_color, out h, out s, out v);
            circle = ColorPickerLoad.GetTexture(Main.ModEntry.Path + "\\ColorPicker\\imCircle.png");
            rightArrow = ColorPickerLoad.GetTexture(Main.ModEntry.Path + "\\ColorPicker\\imRight.png");
            leftArrow = ColorPickerLoad.GetTexture(Main.ModEntry.Path + "\\ColorPicker\\imLeft.png");
            button = ColorPickerLoad.GetTexture(Main.ModEntry.Path + "\\ColorPicker\\imBorder.png");
            buttonHighlighted = ColorPickerLoad.GetTexture(Main.ModEntry.Path + "\\ColorPicker\\imBorderHighlighted.png");

            previewStyle = new GUIStyle();
            previewStyle.normal.background = Texture2D.whiteTexture;

            labelStyle = new GUIStyle();
            labelStyle.fontSize = 12;

            hueTexture = CreateHueTexture(20, kHSVPickerSize);
            hueStyle = new GUIStyle();
            hueStyle.normal.background = hueTexture;

            svTexture = CreateSVTexture(_color, kHSVPickerSize);
            svStyle = new GUIStyle();
            svStyle.normal.background = svTexture;

            presetStyle = new GUIStyle();
            presetStyle.normal.background = button;

            presetHighlightedStyle = new GUIStyle();
            presetHighlightedStyle.normal.background = buttonHighlighted;
        }

        public void SetWindowPosition(float x, float y) {
            windowRect.x = x;
            windowRect.y = y;
        }

        public void DrawWindow(int id = 0, string title = "IMColorPicker")
        {
            windowRect = GUI.Window(id, windowRect, DrawColorPickerWindow, title);
        }

        void DrawColorPickerWindow(int windowID)
        {
            DrawColorPicker();

            if (Event.current.type == EventType.Repaint)
            {
                var rect = GUILayoutUtility.GetLastRect();
                windowRect.height = rect.y + rect.height + 10f;
            }

            GUI.DragWindow();
        }

        public void DrawColorPicker()
        {
            using (new GUILayout.VerticalScope())
            {
                GUILayout.Space(5f);
                DrawPreview(_color);

                GUILayout.Space(5f);
                DrawHSVPicker(ref _color);

                if (preset != null)
                {
                    GUILayout.Space(5f);
                    DrawPresets(ref _color);
                }
            }
        }

        void DrawPreview(Color c)
        {
            using (new GUILayout.VerticalScope())
            {
                var tmp = GUI.backgroundColor;
                GUI.backgroundColor = new Color(c.r, c.g, c.b);
                GUILayout.Label("", previewStyle, GUILayout.Width(kHSVPickerSize + kHuePickerWidth + 10), GUILayout.Height(12f));

                GUILayout.Space(1f);

                var alpha = c.a;
                GUI.backgroundColor = new Color(alpha, alpha, alpha);
                GUILayout.Label("", previewStyle, GUILayout.Width(kHSVPickerSize + kHuePickerWidth + 10), GUILayout.Height(2f));

                GUI.backgroundColor = tmp;
            }
        }

        void DrawHSVPicker(ref Color c)
        {
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label("", svStyle, GUILayout.Width(kHSVPickerSize), GUILayout.Height(kHSVPickerSize));
                DrawSVHandler(GUILayoutUtility.GetLastRect(), ref c);
              //  lastrect = GUILayoutUtility.GetLastRect();
                GUILayout.Space(10f);

                GUILayout.Label("", hueStyle, GUILayout.Width(kHuePickerWidth), GUILayout.Height(kHSVPickerSize));
                DrawHueHandler(GUILayoutUtility.GetLastRect(), ref c);
            }
        }

        void DrawPresets(ref Color c)
        {
            const int presetSize = 16;

            GUILayout.Label("Presets", labelStyle);
            GUILayout.Space(2f);

            var tmp = GUI.backgroundColor;
            int n = preset.Colors.Count;
            var e = Event.current;
            for (int offset = 0, m = n / 10; offset <= m; offset++)
            {
                using (new GUILayout.HorizontalScope())
                {
                    GUILayout.Space(1f);
                    var limit = Mathf.Min(n, (offset + 1) * 10);
                    for (int i = offset * 10; i < limit; i++)
                    {
                        var color = preset.Colors[i];
                        GUI.backgroundColor = color;
                        if (GUILayout.Button(" ", (i == selectedPreset) ? presetHighlightedStyle : presetStyle, GUILayout.Width(presetSize), GUILayout.Height(presetSize))) {
                            switch (e.button)
                            {
                                case 0:
                                    selectedPreset = i;
                                    c = color;
                                    IMColorUtil.RGBToHSV(c, out h, out s, out v);
                                    UpdateSVTexture(c, svTexture);
                                    break;
                                case 1:
                                    {
                                        preset.Colors.RemoveAt(i);
                                        ClearPresetSelection();
                                        return;
                                    }
                                    break;
                            }
                        }
                        GUILayout.Space(1f);
                    }
                }
            }

            GUI.backgroundColor = tmp;
            const int buttonWidth = 67, buttonHeight = 20;
            using (new GUILayout.HorizontalScope()) {
                if (GUILayout.Button("Save", GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
                {
                    preset.Save(c);
                    selectedPreset = preset.Colors.Count - 1;
                }
                if (selectedPreset >= 0 && GUILayout.Button("Remove", GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
                {
                    preset.Colors.RemoveAt(selectedPreset);
                    ClearPresetSelection();
                }
            }
        }

        void ClearPresetSelection() {
            selectedPreset = -1;
        }
        public void HexColUI(ref Color col)
        {
            if (this.HexCol == null)
            {
                this.HexCol = ColorUtility.ToHtmlStringRGB(_color);
            }
            var currentHexCol = this.HexCol;
            this.HexCol = GUILayout.TextField(this.HexCol, 6, GUILayout.Width(100f));
            if (currentHexCol != this.HexCol && this.HexCol != ColorUtility.ToHtmlStringRGB(_color))
            {
                if (ColorUtility.TryParseHtmlString("#" + this.HexCol, out _color))
                {
                    Main.logger.Log("ParsedHTML" + _color.ToString());
                    /*Color.RGBToHSV(_color, out float hh, out float ss, out float vv);
                    ///colorPicker.changeActiveColor(color, hh, ss, vv);
                  //  colorPicker._color = Color.HSVToRGB(hh, ss, vv);
                    _color = color;
                    col = color;
                    this.h = hh;
                    this.s = ss;
                    this.v = vv;
                    this.UpdateSVTexture(color, this.svTexture);
                    this.svTexture.Apply();*/
                    /// colorPicker.UpdateSVTexture(color, colorPicker.svTexture);
                    /*colorPicker.changeActiveColor(color, hh, ss, vv);
                    colorPicker._color = col;
                    colorPicker.changeActiveColor(color, hh, ss, vv);
                    colorPicker.UpdateSVTexture(color,colorPicker.svTexture);
                    colorPicker._color = col;*/
                    col = _color;
                    IMColorUtil.RGBToHSV(_color, out h, out s, out v);
                    UpdateSVTexture(_color, svTexture);
                }
            }
        }
        public void ColorSlider(string RGB, int rgb, ref float col, float h, float s, float v)
        {
            float floatval = col;
            GUILayout.BeginHorizontal();
            GUILayout.Label(RGB, GUILayout.ExpandWidth(false));
            col = Mathf.Clamp(GUILayout.HorizontalSlider(col, 0f, 1f, GUILayout.Width(150)), 0, 255);
            ///charsettings.color[rgb] = Mathf.Clamp(GUILayout.HorizontalSlider(charsettings.color[rgb], 0f, 1f, GUILayout.Width(150)),0,255);
            ModKit.UI.FloatTextFieldClamp(col, ref col, 1, 0, options: GUILayout.Width(75f));
            if (floatval != col)
            {
                /// col = new Color(charsettings.color[0], charsettings.color[1], charsettings.color[2]);
                HexCol = ColorUtility.ToHtmlStringRGB(this._color);
                this.changeActiveColor(this._color, h, s, v);
            }
            GUILayout.EndHorizontal();
        }
        public void DrawSVHandler(Rect rect, ref Color c)
        {
            const float size = 10f;
            const float offset = 5f;
            GUI.DrawTexture(new Rect(rect.x + s * rect.width - offset, rect.y + (1f - v) * rect.height - offset, size, size), circle);

            var e = Event.current;
            var p = e.mousePosition;
            if (e.button == 0 && (e.type == EventType.MouseDown || e.type == EventType.MouseDrag) && rect.Contains(p))
            {
                s = (p.x - rect.x) / rect.width;
                v = 1f - (p.y - rect.y) / rect.height;
                c = IMColorUtil.HSVToRGB(h, s, v);
                _color = IMColorUtil.HSVToRGB(h, s, v);

                e.Use();
                ClearPresetSelection();
            }
        }
        public void changeActiveColor(Color c,float h, float s, float v)
        {
            UpdateSVTexture(c, svTexture,s,v);
            ///Color.RGBToHSV(ColorPicker.colorPicker._color, out float h, out float s, out float v);
            this.h = h;
            this.s = s;
            this.v = v;
        }
        public void DrawHueHandler (Rect rect, ref Color c)
        {
            const float size = 15f;
            GUI.DrawTexture(new Rect(rect.x - size * 0.75f, rect.y + (1f - h) * rect.height - size * 0.5f, size, size), rightArrow);
            GUI.DrawTexture(new Rect(rect.x + rect.width - size * 0.25f, rect.y + (1f - h) * rect.height - size * 0.5f, size, size), leftArrow);

            var e = Event.current;
            var p = e.mousePosition;
            if(e.button == 0 && (e.type == EventType.MouseDown || e.type == EventType.MouseDrag) && rect.Contains(p))
            {
                h = 1f - (p.y - rect.y) / rect.height;
                c = IMColorUtil.HSVToRGB(h, s, v);
                ///ColorPicker.col = c;///shiz might break here
                UpdateSVTexture(c, svTexture);

                e.Use();

				ClearPresetSelection();
            }
        }

        public void UpdateSVTexture(Color c, Texture2D tex)
        {
            Color.RGBToHSV(c, out float h, out float _s, out float _v);

            var size = tex.width;
            for (int y = 0; y < size; y++)
            {
                var v = 1f * y / size;
                for(int x = 0; x < size; x++)
                {
                    var s = 1f * x / size;
                    var color = IMColorUtil.HSVToRGB(h, s, v);
                    tex.SetPixel(x, y, color);
                }
            }

            tex.Apply();
        }
        void UpdateSVTexture(Color c, Texture2D tex, float s, float v)
        {
            //float h, _s, _v;
            //IMColorUtil.RGBToHSV(c, out h, out _s, out _v);

            var size = tex.width;
            for (int y = 0; y < size; y++)
            {
                v = 1f * y / size;
                for (int x = 0; x < size; x++)
                {
                    s = 1f * x / size;
                    var color = IMColorUtil.HSVToRGB(h, s, v);
                    tex.SetPixel(x, y, color);
                }
            }

            tex.Apply();
        }

        Texture2D CreateHueTexture(int width, int height)
        {
            var tex = new Texture2D(width, height);
            for(int y = 0; y < height; y++)
            {
                var h = 1f * y / height;
                var color = IMColorUtil.HSVToRGB(h, 1f, 1f);
                for(int x = 0; x < width; x++)
                {
                    tex.SetPixel(x, y, color);
                }
            }
            tex.Apply();
            return tex;
        }

        Texture2D CreateSVTexture(Color c, int size)
        {
            var tex = new Texture2D(size, size);
            UpdateSVTexture(c, tex);
            return tex;
        }

    }

}

