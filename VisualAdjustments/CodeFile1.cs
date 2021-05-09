using Harmony12;
using Kingmaker.Blueprints;
using UnityEngine;
using System.Collections.Generic;



namespace VisualAdjustments
{
    static class BluePrintThing
    {

        public static TBlueprint[] GetBlueprints<TBlueprint>() where TBlueprint : BlueprintScriptableObject
        {
            var bundle = (AssetBundle)AccessTools.Field(typeof(ResourcesLibrary), "s_BlueprintsBundle")
             .GetValue(null);
            return bundle.LoadAllAssets<TBlueprint>();
        }
    }
    static class LibraryThing
    {
        public static Dictionary<string, string> GetResourceGuidMap()
        {
            var locationList = (Kingmaker.BundlesLoading.LocationList)AccessTools
                .Field(typeof(Kingmaker.BundlesLoading.BundlesLoadService), "m_LocationList")
                .GetValue(Kingmaker.BundlesLoading.BundlesLoadService.Instance);
            return locationList.GuidToBundle;
        }
    }
}
