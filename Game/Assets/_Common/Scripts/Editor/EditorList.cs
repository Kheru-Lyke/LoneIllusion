using Com.SchizophreniaStudios.LoneIllusionDestiny.Common;
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
public class EditorChunkList {
    public static void Show(SerializedProperty list, string name = "", bool showListSize = false) {

        if (name != "") EditorGUILayout.LabelField(name);

        EditorGUI.indentLevel += 1;

        if (list.isExpanded) {

            if (showListSize) {
                EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
            }

            for (int i = 0; i < list.arraySize; i++) {
                SerializedProperty element = list.GetArrayElementAtIndex(i);
                DialogueLine line = element.objectReferenceValue as DialogueLine;

                EditorGUILayout.BeginHorizontal();
                    GUILayout.TextArea(line.Text, GUILayout.MaxWidth(200));

                    EditorGUILayout.PropertyField(element, new GUIContent(""), GUILayout.MaxWidth(100));
                    if (GUILayout.Button("Remove")) list.arraySize -= 1;
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space(5);
            }
        }
        EditorGUI.indentLevel -= 1;

        if (GUILayout.Button("Add")) list.arraySize += 1;
    }
}



