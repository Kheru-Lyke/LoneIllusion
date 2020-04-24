///-----------------------------------------------------------------
/// Author : Gabriel Massé
/// Date : 15/02/2020 16:08
///-----------------------------------------------------------------

using Com.SchizophreniaStudios.LoneIllusionDestiny.Common;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Com.SchizophreniaStudios.LoneIllusionDestiny.LoneIllusion
{
    [CreateAssetMenu(
        fileName = "Character",
        menuName = "Visual Novel/Character"
    )]
    public class CharacterChangingWithRelationship : CharacterChanging
    {
        [SerializeField] private StateCharacter_SerializableDictionary characterPerState = null;
        [SerializeField] private List<float> relationShipValueLevels = null;
        private CharacterState currentState = CharacterState.BASE;


        override protected Character ReturnCurrentCharacter()
        {
            return characterPerState[currentState];
        }

        override public bool CheckState()
        {
            float currentRelationshipLevel = PlayerCharacter.relationshipLevel[this];

            CharacterState newState = CharacterState.BASE;

            for (int i = 0; i < relationShipValueLevels.Count; i++)
            {
                if (currentRelationshipLevel >= relationShipValueLevels[i])
                {
                    newState = (CharacterState)i;
                }
            }

            if (currentState != newState)
            {
                currentState = newState;

                return true;
            }
            return false;
        }
    }
}