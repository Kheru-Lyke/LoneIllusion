///-----------------------------------------------------------------
/// Author : Gabriel Massé
/// Date : 07/02/2020 17:48
///-----------------------------------------------------------------

using Com.SchizophreniaStudios.LoneIllusionDestiny.LoneIllusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SchizophreniaStudios.LoneIllusionDestiny.Common
{

    [CreateAssetMenu(
        fileName = "Line",
        menuName = "Visual Novel/Dialogue/Line"
    )]
    public class DialogueLine : ScriptableObject
    {

        [SerializeField] protected CharacterChanging _speaker;
        [SerializeField] protected LanguageString_SerializableDictionnary _text = new LanguageString_SerializableDictionnary();
        [SerializeField] protected bool _anonymous;
        [SerializeField] protected List<CharacterMovement> _characterMovement = new List<CharacterMovement>();
        [SerializeField] protected Emotions _emotion;

        public CharacterChanging Speaker => _speaker;
        public string Text { get {
                return _text[Settings.language];
            } }
        public List<CharacterMovement> CharacterMovement => _characterMovement;
        public Emotions Emotion => _emotion;
        public bool Anonymous => _anonymous;
    }
}