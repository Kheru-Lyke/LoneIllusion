
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
///-----------------------------------------------------------------
/// Author : Gabriel Massé
/// Date : 07/02/2020 17:48
///-----------------------------------------------------------------
namespace Com.SchizophreniaStudios.LoneIllusionDestiny.Common {
    public enum Emotions
    {
        NONE,
        ANGER,
        HAPPY
    }

    [CreateAssetMenu(
        fileName = "Character",
        menuName = "Visual Novel/Character Details"
    )]

    public class Character : ScriptableObject
    {
        [SerializeField] private string _characterName = "Empty";
        [SerializeField] private EnumSprite_SerializableDictionary _sprites = new EnumSprite_SerializableDictionary();
        [SerializeField] private Color _textColor = Color.white;
        [SerializeField] private TMP_FontAsset _textFont = null;
        [SerializeField] private FontStyles _textFontStyle = FontStyles.Normal;

        virtual public string CharacterName => _characterName; 
        virtual public Color TextColor => _textColor; 
        virtual public TMP_FontAsset TextFont => _textFont;
        virtual public FontStyles TextFontStyle => _textFontStyle; 
        virtual public EnumSprite_SerializableDictionary Sprites => _sprites;
    }
}