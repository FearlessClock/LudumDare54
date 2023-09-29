using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerPrefBoolVariable))]
public class PlayerPrefBoolVariableEditor : Editor
{
    bool valueToSet = false;
    public override void OnInspectorGUI(){
        serializedObject.Update();
		EditorGUILayout.PropertyField(serializedObject.FindProperty("ID"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("value"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultValue"));
        valueToSet = EditorGUILayout.Toggle("New value", valueToSet, GUILayout.ExpandWidth(false));
        if(GUILayout.Button("Save Value", GUILayout.ExpandWidth(false))){
            PlayerPrefBoolVariable myScript = (PlayerPrefBoolVariable)target;
            myScript.SetValue(valueToSet);
        }
        if(GUILayout.Button("Load value", GUILayout.ExpandWidth(false))){
            PlayerPrefBoolVariable myScript = (PlayerPrefBoolVariable)target;
            myScript.Load();
        }
		serializedObject.ApplyModifiedProperties();
    }
}
