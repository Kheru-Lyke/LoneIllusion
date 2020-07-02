using RotaryHeart.Lib.SerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorList {
    public static void Show(SerializedProperty list, string name = "", bool showListSize = false) {

        if (name != "") EditorGUILayout.LabelField(name);

        EditorGUI.indentLevel += 1;

        if (list.isExpanded) {

            if (showListSize) {
                EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
            }

            for (int i = 0; i < list.arraySize; i++) {
                EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), new GUIContent(""));
                    if (GUILayout.Button("Remove")) list.arraySize -= 1;
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUI.indentLevel -= 1;

        if (GUILayout.Button("Add")) list.arraySize += 1;
    }
}

