using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using imColorPicker;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.ResourceLinks;
using Kingmaker.Visual.CharacterSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityModManagerNet;

namespace VisualAdjustments
{
    public  class ColorPicker
    {
        /// public Texture2D colorPicker;'
        public  IMColorPicker colorPicker;
        public  int ImageWidth = 400;
        public  int ImageHeight = 400;
        public  Color col;

        public void OnGUI(Settings.CharacterSettings charsettings, UnitEntityData data, Color initcolor, ref float[] floatsetting,Action<UnitEntityData> func)
        {

            /*if(col == null)
            {
                col = new Color(charsettings.color[0], charsettings.color[1], charsettings.color[2]);
            }
            else
            {
                col.r = charsettings.color[0];
                col.g = charsettings.color[1];
                col.b = charsettings.color[2];
            }*/
            ///GUILayout.BeginHorizontal();
            //Color col;
            
            if (colorPicker == null)
            {
                colorPicker = new IMColorPicker();
            }
            colorPicker._color = initcolor;
            colorPicker.DrawWindow(); // draw color picker UI with GUI.Window
            colorPicker.DrawColorPicker(); // or draw color picker UI only

                ///colorPicker._color.r = charsettings.color[0];
               /// colorPicker._color.g = charsettings.color[1];
               /// colorPicker._color.b = charsettings.color[2];

           /// charsettings.color = new float[] { colorPicker.color.r, colorPicker.color.g, colorPicker.color.b };
            //colorPicker._color = new Color(charsettings.color[0],charsettings.color[1],charsettings.color[2]);
             col = colorPicker.color;
            ///Color.RGBToHSV(col,out float H,out float S,out float V);
            /// colorPicker.DrawSVHandler(colorPicker.lastrect, ref colorPicker._color);
            //col = colorPicker.color;
            /// charsettings.color = new float[] {col.r,col.g,col.b };
            ///HexCol = ColorUtility.ToHtmlStringRGB(col);
            /*if (GUILayout.RepeatButton(colorPicker,new GUILayoutOption[] {GUILayout.Width(400f),GUILayout.Height(400f) }))
            {
                Vector2 pickpos = Event.current.mousePosition;
                int aaa = Convert.ToInt32(pickpos.x);
                int bbb = Convert.ToInt32(pickpos.y);
                GUI.Label(new Rect(pickpos, pickpos),Main.dot);
                col = colorPicker.GetPixel(aaa,  bbb);
                charsettings.color = new float[] { col.r, col.g, col.b};
                HexCol = ColorUtility.ToHtmlStringRGB(col);
                ///edit with sliders
                ///
                // "col" is the color value that Unity is returning.
                // Here you would do something with this color value, like
                // set a model's material tint value to this color to have it change
                // colors, etc, etc.
                //
                // Right now we are just printing the RGBA color values to the Console
               /// Main.logger.Log(col.ToString());
            }*/
            ///col = new Color(charsettings.color[0], charsettings.color[1], charsettings.color[2]);
            /// col = colorPicker._color;
            Color.RGBToHSV(col, out float H, out float S, out float V);
            colorPicker.h = H;
            colorPicker.ColorSlider("R",0,ref col.r,H,S,V);
            colorPicker.ColorSlider("G",1,ref col.g,H,S,V);
            colorPicker.ColorSlider("B",2,ref col.b,H,S,V);
            /// charsettings.color = new float[] { col.r, col.g, col.b };
            // colorPicker._color = col;
            /*  GUILayout.BeginHorizontal();
              GUILayout.Label("R",GUILayout.ExpandWidth(false));
              charsettings.color[0] = Mathf.Clamp(GUILayout.HorizontalSlider(charsettings.color[0], 0f, 1f, GUILayout.Width(150)), 0, 255);
              ModKit.UI.FloatTextFieldClamp(charsettings.color[0],ref charsettings.color[0],1,0, options: GUILayout.Width(90f));
              GUILayout.EndHorizontal();
              GUILayout.BeginHorizontal();
              GUILayout.Label("G", GUILayout.ExpandWidth(false));
              charsettings.color[1] = Mathf.Clamp(GUILayout.HorizontalSlider(charsettings.color[1], 0f, 1f, GUILayout.Width(150)),0,255);
              ModKit.UI.FloatTextFieldClamp(charsettings.color[1], ref charsettings.color[1],1,0, options: GUILayout.Width(90f));
              GUILayout.EndHorizontal();
              GUILayout.BeginHorizontal();
              GUILayout.Label("B", GUILayout.ExpandWidth(false));
              charsettings.color[2] = Mathf.Clamp(GUILayout.HorizontalSlider(charsettings.color[2], 0f, 1f, GUILayout.Width(150)), 0, 255);
              ModKit.UI.FloatTextFieldClamp(charsettings.color[2], ref charsettings.color[2],1,0,options: GUILayout.Width(90f));
              GUILayout.EndHorizontal();*/
            /* if (colorPicker.HexCol == null)
             {
                 colorPicker.HexCol = ColorUtility.ToHtmlStringRGB(col);
             }
             var currentHexCol = colorPicker.HexCol;
             colorPicker.HexCol = GUILayout.TextField(colorPicker.HexCol, 6,GUILayout.Width(100f));
             Color.RGBToHSV(col, out float hhh, out float sss, out float vvv);
             if (currentHexCol != colorPicker.HexCol && colorPicker.HexCol != ColorUtility.ToHtmlStringRGB(col))
             {
                 if (ColorUtility.TryParseHtmlString("#" + colorPicker.HexCol, out Color color))
                 {
                     Main.logger.Log("ParsedHTML" + color.ToString());
                     Color.RGBToHSV(col, out float hh, out float ss, out float vv);
                     ///colorPicker.changeActiveColor(color, hh, ss, vv);
                   //  colorPicker._color = Color.HSVToRGB(hh, ss, vv);
                     col = color;
                     colorPicker.h = hhh;
                     colorPicker.s = sss;
                     colorPicker.v = vvv;
                     colorPicker.UpdateSVTexture(color,colorPicker.svTexture);
                     colorPicker.svTexture.Apply();
                     /// colorPicker.UpdateSVTexture(color, colorPicker.svTexture);
                     /*colorPicker.changeActiveColor(color, hh, ss, vv);
                     colorPicker._color = col;
                     colorPicker.changeActiveColor(color, hh, ss, vv);
                     colorPicker.UpdateSVTexture(color,colorPicker.svTexture);
                     colorPicker._color = col;*//*
                 }
             }*/
            colorPicker.HexCol = ColorUtility.ToHtmlStringRGB(col);
            colorPicker.HexColUI(ref col);
            /*if (GUILayout.Button("!?button?!", GUILayout.Width(150)))
            {
                //colorPicker.changeActiveColor(col, hhh, sss, vvv);
                colorPicker._color = col;//Color.HSVToRGB(hhh, sss, vvv);
            }*/
            Color.RGBToHSV(col, out float h, out float s, out float v);
             if (colorPicker.h != h || colorPicker.s != s || colorPicker.v != v)
             {
               colorPicker.changeActiveColor(colorPicker._color,h,s,v);
             }
            /*  if(skinorhair)
              {
                  charsettings.hairColor = new float[] { col.r, col.g, col.b };
              }
              else
              {
                  charsettings.skinColor = new float[] { col.r, col.g, col.b };
              }*/
            //floatsetting = new float[] {col.r,col.g,col.b };
            floatsetting[0] = col.r;
            floatsetting[1] = col.g;
            floatsetting[2] = col.b;
            if (GUILayout.Button("Rebuild Character (Apply)",GUILayout.ExpandWidth(false)))
            {
              //  floatsetting = new float[] { col.r, col.g, col.b };
                func(data);
                CharacterManager.RebuildCharacter(data);
                //Main.SetEELs(data,DollResourcesManager.GetDoll(data));
                func(data);
            }

           /// GUILayout.EndHorizontal();
        }
    }
    public static class ColorPickerLoad
    {
        public static Texture2D GetTexture(String filePath)
        {
            Byte[] data = File.ReadAllBytes(filePath);
            
            var tex = new Texture2D(2, 2);
            tex.LoadImage(data);
            return tex;
        }
    }
}
