using Com.SchizophreniaStudios.LoneIllusionDestiny.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;


[CustomEditor(typeof(DialogueChunk))]
public class LevelDataEditor : Editor {

    private SerializedProperty lines;


    private void OnEnable() {
        lines = serializedObject.FindProperty("_lines");
    }

    public override void OnInspectorGUI() {
        EditorChunkList.Show(lines, "Lines in " + target.name);
        serializedObject.ApplyModifiedProperties();
    }

}