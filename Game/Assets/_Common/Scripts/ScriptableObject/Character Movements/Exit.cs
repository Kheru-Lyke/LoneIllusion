///-----------------------------------------------------------------
/// Author : Gabriel Massé
/// Date : 07/02/2020 20:46
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.SchizophreniaStudios.LoneIllusionDestiny.Common.CharacterMovements {

    [CreateAssetMenu(
        fileName = "Exit",
        menuName = "Visual Novel/Movement/Exit"
    )]
    public class Exit : CharacterMovement
    {
        public override void Move()
        {
            VNManager.Instance.RemoveCharacterFromScreen();
        }
    }
}