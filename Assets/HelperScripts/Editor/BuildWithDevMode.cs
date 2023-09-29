using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEditor.Build.Reporting;

public class BuildWithDevMode : EditorWindow
{

    [MenuItem("Tools/Build Game for Windows (Dev Mode)")]
    public static void BuildGameForWindows()
    {
        bool buildWithDevMode = EditorUtility.DisplayDialog("Build With Developer Mode", "Do you want to build the game with Developer Mode activated?", "Yes", "No");

        // Create a build folder named with the current date
        string buildFolderName = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string buildPath = Application.dataPath.Replace("/Assets", "/Build");
        string buildFolderPath = Path.Combine(buildPath, buildFolderName);

        if (!Directory.Exists(buildFolderPath))
            Directory.CreateDirectory(buildFolderPath);

        // Get the scenes chosen in the Unity editor's Build Settings
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;

        // Extract the scene paths from the EditorBuildSettingsScene array
        string[] scenePaths = new string[scenes.Length];
        for (int i = 0; i < scenes.Length; i++)
        {
            scenePaths[i] = scenes[i].path;
        }

        // The name of the executable
        string executableName = "Council of Mages - The Party Game.exe";

        BuildOptions buildOptions = BuildOptions.None;
        buildOptions |= BuildOptions.CleanBuildCache | BuildOptions.AutoRunPlayer | BuildOptions.ShowBuiltPlayer;
        if (buildWithDevMode)
        {
            buildOptions |= BuildOptions.Development | BuildOptions.AllowDebugging;
        }

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = scenePaths,
            locationPathName = Path.Combine(buildFolderPath, executableName),
            target = BuildTarget.StandaloneWindows64,
            options = buildOptions
        };

        // Build the game for Windows

        BuildReport buildReport = BuildPipeline.BuildPlayer(buildPlayerOptions);

        if(buildReport.summary.result == BuildResult.Succeeded)
        {
            if (buildWithDevMode)
            {
                // Create the steam_appid.txt file with the app ID (2506130)
                string steamAppIDFilePath = Path.Combine(buildFolderPath, "steam_appid.txt");
                File.WriteAllText(steamAppIDFilePath, "2506130");
            }

            // Show a message box to indicate the build is complete
            EditorUtility.DisplayDialog("Build Complete", "Game has been successfully built for Windows.", "OK");
        }
    }
}