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
        static public uint cunningLevel = 0;
        static public uint bluntLevel = 0;
        static public uint trueLevel = 0;
        static public uint nobleLevel = 0;

        static public Dictionary<CharacterChanging, float> relationshipLevel = new Dictionary<CharacterChanging, float>();
	}
}