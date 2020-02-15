///-----------------------------------------------------------------
/// Author : Gabriel Massé
/// Date : 15/02/2020 16:54
///-----------------------------------------------------------------

using Com.SchizophreniaStudios.LoneIllusionDestiny.Common;
using System;
using TMPro;
using UnityEngine;

namespace Com.SchizophreniaStudios.LoneIllusionDestiny.LoneIllusion {
    public enum PlayerState
    {
        CAT_BASE,
        CAT_PATTERNED,
        PUMA,
        BLACK_JAGUAR,
        PANTHER,
        LION,
        WHITE_TIGER,
        SNOW_LEOPARD,
        CHEETAH,
        CARACAL,
        LYNX,
        WHITE_OCELOT,
        SAND_CAT,
        SMILODON,
        BENGAL_CAT,
        SERVAL
    }

    [CreateAssetMenu(
        fileName = "Character",
        menuName = "Visual Novel/Player"
    )]

    [Serializable]
    public class PlayerCharacter : CharacterChanging
    {

        [SerializeField] private PlayerStateCharacter_SerializableDictionary characterPerState;
        private PlayerState currentState = PlayerState.CAT_BASE;

        override public bool CheckState()
        {
            PlayerState newState = PlayerState.CAT_BASE;

            //TEST STATES

            uint blunt = PlayerData.bluntLevel;
            uint cunning = PlayerData.cunningLevel;
            uint noble = PlayerData.nobleLevel;
            uint lTrue = PlayerData.trueLevel;

            if (blunt == cunning && noble == lTrue && blunt == noble)                               //Equality
            {
                newState = blunt != 0 ? PlayerState.CAT_PATTERNED : PlayerState.CAT_BASE;           //Start of the game or not
            }
            else
            {
                if (blunt > cunning && blunt > noble && blunt > lTrue) newState = PlayerState.LION;
                else if (cunning > blunt && cunning > noble && cunning > lTrue) newState = PlayerState.PUMA;
                else if (noble > blunt && noble > cunning && noble > lTrue) newState = PlayerState.SNOW_LEOPARD;
                else if (lTrue > blunt && lTrue > noble && lTrue > cunning) newState = PlayerState.LYNX;
                else
                {
                    if (cunning == noble)
                    {
                        if (cunning == lTrue) newState = PlayerState.SAND_CAT;
                        else if (cunning == blunt) newState = PlayerState.PANTHER;
                        else newState = PlayerState.WHITE_OCELOT;
                    }
                    else if (cunning == blunt)
                    {
                        newState = cunning == lTrue ? PlayerState.SERVAL : PlayerState.BLACK_JAGUAR;
                    }
                    else if (cunning == lTrue) newState = PlayerState.BENGAL_CAT;
                    else
                    {
                        if (blunt == noble)
                        {
                            newState = blunt == lTrue ? PlayerState.CHEETAH : PlayerState.WHITE_TIGER;
                        }
                        else newState = blunt == lTrue ? PlayerState.SMILODON : PlayerState.CARACAL;
                    }
                }
            }


            if (currentState != newState)
            {
                currentState = newState;

                return true;
            }
            return false;
        }

        protected override Character ReturnCurrentCharacter()
        {
            return characterPerState[currentState];
        }
    }
}