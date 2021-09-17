using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Utility;
using Newtonsoft.Json;
using UnityEngine;

namespace VisualAdjustments
{
    public class EEStorage
    {
        public EEStorage(string assetId, int primaryIndex, int secondaryIndex)
        {
            AssetID = assetId;
            PrimaryIndex = primaryIndex;
            SecondaryIndex = secondaryIndex;
            Main.logger.Log(assetId + primaryIndex + secondaryIndex);
        }
        [JsonProperty] public string AssetID = "";
        [JsonProperty] public int PrimaryIndex = -1;
        [JsonProperty] public int SecondaryIndex = -1;
    }
	public class UnitPartVAEELs : UnitPart
    {
        [JsonProperty] public List<EEStorage> EEToAdd = new List<EEStorage>();
        [JsonProperty] public List<string> EEToRemove = new List<string>();

    }

}