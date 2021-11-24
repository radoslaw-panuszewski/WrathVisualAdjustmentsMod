using HarmonyLib;
using Kingmaker;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Persistence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace VisualAdjustments
{
    [HarmonyPatch]
    internal static class SaveHooker
    {
        [HarmonyPatch(typeof(ZipSaver))]
        [HarmonyPatch("SaveJson"), HarmonyPostfix]
        private static void Zip_Saver(string name, ZipSaver __instance)
        {
            DoSave(name, __instance);
        }

        [HarmonyPatch(typeof(FolderSaver))]
        [HarmonyPatch("SaveJson"), HarmonyPostfix]
        private static void Folder_Saver(string name, FolderSaver __instance)
        {
            DoSave(name, __instance);
        }

        private static void DoSave(string name, ISaver saver)
        {
            if (name != "header")
                return;

            try
            {
                var serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                var writer = new StringWriter();
                serializer.Serialize(writer, GlobalVisualInfo.Instance);
                writer.Flush();
                saver.SaveJson(LoadHooker.FileName, writer.ToString());
            }
            catch (Exception e)
            {
                Main.logger.Log(e.ToString());
            }
        }
    }

    [HarmonyPatch(typeof(Game))]
    internal static class LoadHooker
    {
        public const string FileName = "header.json.barley_visualrecords";

        [HarmonyPatch("LoadGame"), HarmonyPostfix]
        private static void LoadGame(SaveInfo saveInfo)
        {
            using (saveInfo)
            {
                using (saveInfo.GetReadScope())
                {
                    ThreadedGameLoader.RunSafelyInEditor((Action)(() =>
                    {
                        string raw;
                        using (ISaver saver = saveInfo.Saver.Clone())
                        {
                            raw = saver.ReadJson(FileName);
                        }
                        if (raw != null)
                        {
                            var serializer = new JsonSerializer();
                            var rawReader = new StringReader(raw);
                            var jsonReader = new JsonTextReader(rawReader);
                            GlobalVisualInfo.Instance = serializer.Deserialize<GlobalVisualInfo>(jsonReader);
                        }
                        else
                        {
                            GlobalVisualInfo.Instance = new GlobalVisualInfo();
                        }
                    })).Wait();
                }
            }
        }
    }

    public class GlobalVisualInfo
    {
        public VisualInfo ForCharacter(UnitEntityData unit)
        {
            var key = unit.UniqueId;
            if (!PerCharacter.TryGetValue(key, out var record))
            {
                record = new VisualInfo();
                PerCharacter.Add(key, record);
                //PerCharacter[key] = record;
            }
            return record;
        }

        public Dictionary<string, VisualInfo> PerCharacter = new Dictionary<string, VisualInfo>();
        public static GlobalVisualInfo Instance = new GlobalVisualInfo();

        public class VisualInfo
        {
            public SaveHookerVAEEs EEPart = new SaveHookerVAEEs();
            public SaveHookerVAFX FXpart = new SaveHookerVAFX();
            public SaveHookerSettings settings = new SaveHookerSettings();
            // public Dictionary<int, StatType> AbilityScoresByLevel = new Dictionary<int, StatType>();
            //  public Dictionary<int, Dictionary<StatType, int>> SkillsByLevel = new Dictionary<int, Dictionary<StatType, int>>();
        }
    }
}