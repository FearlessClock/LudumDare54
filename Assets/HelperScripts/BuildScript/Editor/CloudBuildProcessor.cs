#if UNITY_CLOUD_BUILD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using UnityEditor;

public class CloudBuildProcessor : MonoBehaviour
{
    public static void PreExport(UnityEngine.CloudBuild.BuildManifestObject manifest)
    {
        string buildNum = manifest.GetValue<string>("buildNumber");
        Debug.Log("PREBUILD Script launched, build number is " + buildNum);

        string buildEnv = "";
        buildEnv += "BUILD_ENVIRONMENT:" +Environment.GetEnvironmentVariable("BUILD_ENVIRONMENT") + "\n";
        Debug.LogError("Current enviroment " + buildEnv);
        File.WriteAllText("Assets/Resources/PreBuildConfig.txt", buildEnv);
    }
}
#endif