﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerPrefIntVariable))]
public class PlayerPrefIntVariableEditor : Editor
{
    int valueToSet = 0;
    public override void OnInspectorGUI(){
        serializedObject.Update();
		EditorGUILayout.PropertyField(serializedObject.FindProperty("ID"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("value"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultValue"));
        valueToSet = EditorGUILayout.IntField("New value", valueToSet, GUILayout.ExpandWidth(false));
        if(GUILayout.Button("Save Value", GUILayout.ExpandWidth(false))){
            PlayerPrefIntVariable myScript = (PlayerPrefIntVariable)target;
            myScript.SetValue(valueToSet);
        }
        if(GUILayout.Button("Load value", GUILayout.ExpandWidth(false))){
            PlayerPrefIntVariable myScript = (PlayerPrefIntVariable)target;
            myScript.Load();
        }
		serializedObject.ApplyModifiedProperties();
    }
}
