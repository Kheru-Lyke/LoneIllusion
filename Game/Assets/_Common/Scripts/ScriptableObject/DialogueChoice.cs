///-----------------------------------------------------------------
/// Author : Gabriel Massé
/// Date : 07/02/2020 17:48
///-----------------------------------------------------------------

using System;
using UnityEngine;

namespace Com.SchizophreniaStudios.LoneIllusionDestiny.Common {

    [CreateAssetMenu(
        fileName = "Choice",
        menuName = "Visual Novel/Dialogue/Choice"
    )]
    public class DialogueChoice : DialogueLine
    {

        [SerializeField] private StringDialogueChunk_SerializableDictionary choices = new StringDialogueChunk_SerializableDictionary();

        public StringDialogueChunk_SerializableDictionary Choices => choices; 
    }
}