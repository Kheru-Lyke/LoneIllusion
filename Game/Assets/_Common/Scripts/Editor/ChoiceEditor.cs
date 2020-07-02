using Com.SchizophreniaStudios.LoneIllusionDestiny.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(DialogueChoice))]
public class ChoiceEditor : LineEditor {

    private SerializedProperty choices;

    private void OnEnable() {
        speaker = serializedObject.FindProperty("_speaker");
        emotion = serializedObject.FindProperty("_emotion");
        anonymous = serializedObject.FindProperty("_anonymous");
        movement = serializedObject.FindProperty("_characterMovement");
        choices = serializedObject.FindProperty("choices");
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        
        EditorGUILayout.PropertyField(choices);


        serializedObject.ApplyModifiedProperties();
    }
}
