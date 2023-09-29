using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CommandLineBuildScript
{
    public static void PerformBuild()
    {
        string[] commandLineArgs = Environment.GetCommandLineArgs();

        // Start from index 1 to skip the Unity executable path
        for (int i = 1; i < commandLineArgs.Length; i++)
        {
            Debug.Log($"Command Line Argument {i}: {commandLineArgs[i]}");
        }

        string[] scenes = EditorBuildSettings.scenes
            .Where(s => s.enabled)
            .Select(s => s.path)
            .ToArray();

        // Create a build folder named with the current date
        string buildFolderName = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string buildPath = Application.dataPath.Replace("/Assets", "/Build");
        string buildFolderPath = Path.Combine(buildPath, buildFolderName);

        if (!Directory.Exists(buildFolderPath))
            Directory.CreateDirectory(buildFolderPath);

        // The name of the executable
        string executableName = "Council of Mages - The Replacement.exe";

        BuildOptions buildOptions = BuildOptions.None;

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = Path.Combine(buildFolderPath, executableName),
            target = BuildTarget.StandaloneWindows64,
            options = buildOptions
        };

        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }
}
