using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    static public SystemLanguage language = SystemLanguage.English;

    private void OnApplicationQuit() {
        PlayerPrefs.SetInt("lang", (int) language);

        PlayerPrefs.Save();
    }

    private void Start() {
        string lang = PlayerPrefs.GetString("lang", "English");

        language = (SystemLanguage) Enum.Parse(typeof(SystemLanguage), lang);
    }
}
