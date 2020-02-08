///-----------------------------------------------------------------
/// Author : Gabriel Massé
/// Date : 07/02/2020 19:05
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.SchizophreniaStudios.LoneIllusionDestiny.Common {

    public enum Position
    {
        LEFT,
        CENTER,
        RIGHT
    }

    public abstract class CharacterMovement : ScriptableObject {

        public abstract void Move();
    }
}