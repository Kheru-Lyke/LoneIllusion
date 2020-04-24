using Com.SchizophreniaStudios.LoneIllusionDestiny.Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class CharacterChanging : ScriptableObject
{

    public static implicit operator Character(CharacterChanging character)
    {
        return character.ReturnCurrentCharacter();
    }

    abstract public bool CheckState();
    abstract protected Character ReturnCurrentCharacter();

    public string CharacterName => ReturnCurrentCharacter().CharacterName;
    public TMP_FontAsset TextFont => ReturnCurrentCharacter().TextFont;
    public FontStyles TextFontStyle => ReturnCurrentCharacter().TextFontStyle;
    public Color TextColor => ReturnCurrentCharacter().TextColor;
    public EnumSprite_SerializableDictionary Sprites => ReturnCurrentCharacter().Sprites;
}
