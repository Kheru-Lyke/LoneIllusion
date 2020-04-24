///-----------------------------------------------------------------
/// Author : Gabriel Massé
/// Date : 07/02/2020 20:46
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.SchizophreniaStudios.LoneIllusionDestiny.Common.CharacterMovements {

    [CreateAssetMenu(
        fileName = "MoveTo",
        menuName = "Visual Novel/Movement/Change Chunk"
    )]
    public class ChangeChunk : CharacterMovement
    {
        [SerializeField] private DialogueChunk nextChunk = null;
        public override void Move()
        {
            GameManager.Instance.CurrentDialogueChunk = nextChunk;
        }
    }
}