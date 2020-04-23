///-----------------------------------------------------------------
/// Author : Gabriel Massé
/// Date : 15/02/2020 16:54
///-----------------------------------------------------------------

using Com.SchizophreniaStudios.LoneIllusionDestiny.Common;
using System;
using System.Collections.Generic;
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

        [SerializeField] private PlayerStateCharacter_SerializableDictionary characterPerState = null;
        private PlayerState currentState = PlayerState.CAT_BASE;

        static private uint blunt = 0;
        static private uint cunning = 0;
        static private uint noble = 0;
        static private uint lTrue = 0;

        static public Dictionary<CharacterChanging, float> relationshipLevel = new Dictionary<CharacterChanging, float>();

        override public bool CheckState()
        {
            PlayerState newState = PlayerState.CAT_BASE;

            //TEST STATES

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

        /// <summary>
        /// Saves  all of the important player data
        /// </summary>
        /// <returns>JSON string</returns>
        static public string GetDataJSON()
        {
            PlayerData data = new PlayerData();

            data.bluntLevel = blunt;
            data.cunningLevel = cunning;
            data.nobleLevel = noble;
            data.trueLevel = lTrue;

            data.relationshipLevel = relationshipLevel;

            return data;
        }

        /// <summary>
        /// Load all important player data from a JSON file
        /// </summary>
        /// <param name="json">JSON string of PlayerData</param>
        static public void LoadDataJSON(string json)
        {
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);


            blunt = data.bluntLevel;
            cunning = data.cunningLevel;
            noble = data.nobleLevel;
            lTrue = data.trueLevel;

            relationshipLevel = data.relationshipLevel;
        }

        private static PlayerCharacter instance;
        public static PlayerCharacter Instance { get { return instance; } }


        private void Awake()
        {
            if (instance)
            {
                return;
            }

            instance = this;
        }

        private void OnDestroy()
        {
            if (this == instance) instance = null;
        }
    }
}
