///-----------------------------------------------------------------
/// Author : Gabriel Massé
/// Date : 07/02/2020 20:46
///-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.SchizophreniaStudios.LoneIllusionDestiny.Common.CharacterMovements {

    [CreateAssetMenu(
        fileName = "Go to Scene ",
        menuName = "Visual Novel/Movement/Change Scene"
    )]
    public class ChangeScene : CharacterMovement
    {
        [SerializeField] Sprite newBackground = null;
        [SerializeField] AudioClip newMusic = null;
        [SerializeField] AudioClip newBackgroundEffect = null;
        [SerializeField] DialogueChunk nextDialogue = null;
        public override void Move()
        {
            GameManager.Instance.ChangeScene(newBackground, newMusic, newBackgroundEffect);
            GameManager.Instance.CurrentDialogueChunk = nextDialogue;
        }
    }
}