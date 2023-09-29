using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Remember to set the script excecution very high
/// </summary>
public class EnviromentChecker : MonoBehaviour
{
    [SerializeField] private StringVariable buildEnviroment = null;
    [SerializeField] private BoolVariable isStaging = null;
    [SerializeField] private BoolVariable isDev     = null;
    [SerializeField] private BoolVariable isRelease = null;

    private void Awake()
    {
        TextAsset textFile = Resources.Load<TextAsset>("PreBuildConfig");
        string[] texts = textFile.text.Split('\n');
        Dictionary<string, string> enviromentVariables = new Dictionary<string, string>();
        for (int i = 0; i < texts.Length; i++)
        {
            string[] varValues = texts[i].Split(':');
            if (varValues.Length == 2)
            {
                enviromentVariables.Add(varValues[0].Trim(), varValues[1].Trim());
            }
        }
        if (enviromentVariables.ContainsKey("BUILD_ENVIRONMENT"))
        {
            Debug.Log("Found enviroment name " + enviromentVariables["BUILD_ENVIRONMENT"]);
            buildEnviroment.SetValue(enviromentVariables["BUILD_ENVIRONMENT"]);
            isStaging.SetValue(buildEnviroment.value.ToLower() == "Staging".ToLower());
            isDev.SetValue(buildEnviroment.value.ToLower() == "Dev".ToLower());
            isRelease.SetValue(buildEnviroment.value.ToLower() == "Release".ToLower());
        }
    }
}
