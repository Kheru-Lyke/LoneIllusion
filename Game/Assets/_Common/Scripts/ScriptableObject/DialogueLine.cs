///-----------------------------------------------------------------
/// Author : Gabriel Massé
/// Date : 07/02/2020 17:48
///-----------------------------------------------------------------

using System;
using UnityEngine;

namespace Com.SchizophreniaStudios.LoneIllusionDestiny.Common
{

    [CreateAssetMenu(
        fileName = "Line",
        menuName = "Visual Novel/Dialogue/Line"
    )]
    public class DialogueLine : ScriptableObject
    {

        [SerializeField] protected Character _speaker;
        [SerializeField] protected LanguageString_SerializableDictionary _text;
        [SerializeField] protected bool _anonymous;
        [SerializeField] protected CharacterMovement _characterMovement;
        [SerializeField] protected Emotions _emotion;

        public Character Speaker => _speaker;
        public LanguageString_SerializableDictionary Text => _text;
        public CharacterMovement CharacterMovement => _characterMovement;
        public Emotions Emotion => _emotion;
        public bool Anonymous => _anonymous;
    }
}