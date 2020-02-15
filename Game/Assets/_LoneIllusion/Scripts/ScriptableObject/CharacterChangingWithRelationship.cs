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
    [Serializable]
    public abstract class CharacterChanging : ScriptableObject
    {

        abstract public bool CheckState();
        abstract protected Character ReturnCurrentCharacter();

        public string CharacterName => ReturnCurrentCharacter().CharacterName;
        public TMP_FontAsset TextFont => ReturnCurrentCharacter().TextFont;
        public FontStyles TextFontStyle => ReturnCurrentCharacter().TextFontStyle;
        public Color TextColor => ReturnCurrentCharacter().TextColor;
        public EnumSprite_SerializableDictionary Sprites => ReturnCurrentCharacter().Sprites;
    }

    public class CharacterChanginWithRelationship : CharacterChanging
    {
        [SerializeField] private StateCharacter_SerializableDictionary characterPerState;
        [SerializeField] private List<float> relationShipValueLevels;
        private CharacterState currentState = CharacterState.BASE;


        override protected Character ReturnCurrentCharacter()
        {
            return characterPerState[currentState];
        }

        override public bool CheckState()
        {
            float currentRelationshipLevel = PlayerData.relationshipLevel[this];

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