using Com.SchizophreniaStudios.LoneIllusionDestiny.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogueLine))]
public class LineEditor : Editor {
    protected SystemLanguage lang = SystemLanguage.English;

    protected SerializedProperty speaker;
    protected SerializedProperty emotion;
    protected SerializedProperty anonymous;
    protected SerializedProperty movement;

    private void OnEnable() {
        speaker = serializedObject.FindProperty("_speaker");
        emotion = serializedObject.FindProperty("_emotion");
        anonymous = serializedObject.FindProperty("_anonymous");
        movement = serializedObject.FindProperty("_characterMovement");
    }

    public override void OnInspectorGUI() {

        DialogueLine line = (DialogueLine)target;


        Texture sprite = line.Speaker.Sprites[line.Emotion].texture;

        float width = sprite.width * 400 / sprite.height;
        Rect test = new Rect(Screen.width /2 - width/2, 0, width, 400);

        GUI.DrawTexture(test, sprite);
        EditorGUILayout.Space(400);

        lang = (SystemLanguage)EditorGUILayout.EnumPopup(lang, GUILayout.Height(25));

        if (!line.FullText.ContainsKey(lang)) {

            if (GUILayout.Button("Create translation")) {
                line.FullText.Add(lang, "");
            }
        }
        else {

            line.FullText[lang] = GUILayout.TextArea(line.FullText[lang], GUILayout.MinHeight(50));
            if (GUILayout.Button("Remove translation")) line.FullText.Remove(lang);
        }

        EditorGUILayout.Space(15);

        EditorGUILayout.PropertyField(speaker);
        EditorGUILayout.PropertyField(emotion);
        EditorGUILayout.PropertyField(anonymous);

        EditorGUILayout.Space(15);

        EditorList.Show(movement, "Character Movement");


        serializedObject.ApplyModifiedProperties();
    }


}

