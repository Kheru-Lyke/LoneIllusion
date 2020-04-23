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
         public uint cunningLevel = 0;
         public uint bluntLevel = 0;
         public uint trueLevel = 0;
         public uint nobleLevel = 0;

         public Dictionary<CharacterChanging, float> relationshipLevel = new Dictionary<CharacterChanging, float>();


        public static implicit operator string(PlayerData data)
        {
            return JsonUtility.ToJson(data);
        }
    }
}