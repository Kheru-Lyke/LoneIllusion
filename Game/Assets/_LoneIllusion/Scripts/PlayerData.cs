///-----------------------------------------------------------------
/// Author : Gabriel Massé
/// Date : 15/02/2020 16:01
///-----------------------------------------------------------------

using Com.SchizophreniaStudios.LoneIllusionDestiny.Common;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SchizophreniaStudios.LoneIllusionDestiny.LoneIllusion {

    [Serializable]
	public class PlayerData {
         public int cunningLevel = 0;
         public int bluntLevel = 0;
         public int trueLevel = 0;
         public int nobleLevel = 0;

         public Dictionary<CharacterChanging, float> relationshipLevel = new Dictionary<CharacterChanging, float>();


        public static implicit operator string(PlayerData data)
        {
            return JsonUtility.ToJson(data);
        }
    }
}