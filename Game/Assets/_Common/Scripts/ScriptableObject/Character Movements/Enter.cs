///-----------------------------------------------------------------
/// Author : Gabriel Massé
/// Date : 07/02/2020 20:46
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.SchizophreniaStudios.LoneIllusionDestiny.Common.CharacterMovements {

    [CreateAssetMenu(
        fileName = "Enter",
        menuName = "Visual Novel/Movement/Enter"
    )]
    public class Enter : CharacterMovement
    {
        public override void Move()
        {
            GameManager.Instance.AddCharacterToScene();
        }
    }
}