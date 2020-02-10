///-----------------------------------------------------------------
/// Author : Gabriel Massé
/// Date : 07/02/2020 20:46
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.SchizophreniaStudios.LoneIllusionDestiny.Common.CharacterMovements {

    [CreateAssetMenu(
        fileName = "MoveTo",
        menuName = "Visual Novel/Movement/MoveTo"
    )]
    public class MoveTo : CharacterMovement
    {
        [SerializeField] private Position position;
        public override void Move()
        {
            VNManager.Instance.MoveTo(position);
        }
    }
}