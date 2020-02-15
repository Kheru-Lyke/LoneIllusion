///-----------------------------------------------------------------
/// Author : Gabriel Massé
/// Date : 15/02/2020 16:13
///-----------------------------------------------------------------

using System;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using Com.SchizophreniaStudios.LoneIllusionDestiny.Common;

namespace Com.SchizophreniaStudios.LoneIllusionDestiny.LoneIllusion {
    public enum CharacterState
    {
        BASE,
        PATTERNED_1,
        PATTERNED_2
    }

    [Serializable]
    public class StateCharacter_SerializableDictionary : SerializableDictionaryBase<CharacterState, Character>
    {
	
	}
    [Serializable]
    public class PlayerStateCharacter_SerializableDictionary : SerializableDictionaryBase<PlayerState, Character>
    {
	
	}

}