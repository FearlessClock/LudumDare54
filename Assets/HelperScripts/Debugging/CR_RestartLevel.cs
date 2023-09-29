using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class CR_RestartLevel : MonoBehaviour, IConsoleReader
{
    private string commandWord = "restart_scene";

    public string[] GetCommands()
    {
        return new string[1] { commandWord };
    }

    public bool ReadCommand(string command)
    {
        if (command.Trim().ToLower().Equals(commandWord))
        {
            SceneChangeManager.instance.ReloadCurrentScene(); 

            return true;
        }
        return false;
    }
}
