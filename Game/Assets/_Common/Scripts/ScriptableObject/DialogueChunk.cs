///-----------------------------------------------------------------
/// Author : Gabriel Massé
/// Date : 07/02/2020 17:48
///-----------------------------------------------------------------

using System;
using UnityEngine;

namespace Com.SchizophreniaStudios.LoneIllusionDestiny.Common {

    [CreateAssetMenu(
        fileName = "Chunk",
        menuName = "Visual Novel/Dialogue/Chunk"
    )]
    public class DialogueChunk : ScriptableObject {

        [SerializeField] private DialogueLine[] _lines;

        public DialogueLine[] Lines => _lines; 
    }
}