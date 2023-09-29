using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;
using Sirenix.OdinInspector.Editor;

public class DeveloperModeToggle : OdinEditorWindow
{
    [MenuItem("Tools/Toggle developer mode")]
    private static void ToggleDevMode()
    {
        UnityEditor.EditorPrefs.SetBool("DeveloperMode", !UnityEditor.EditorPrefs.GetBool("DeveloperMode"));
    }
}
