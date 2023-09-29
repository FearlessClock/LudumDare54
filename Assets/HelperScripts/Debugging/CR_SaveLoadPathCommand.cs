using HelperScripts.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CR_SaveLoadPathCommand : MonoBehaviour, IConsoleReader
{
    private string commandSaveWord = "Save_Path";
    private string commandLoadWord = "Load_Path";

    [SerializeField] private PlayerPrefStringVariable laneData = null;
    [SerializeField] private EventScriptable OnPathLoaded = null;
    [SerializeField] private EventScriptable OnSavePath = null;
    [SerializeField] private CR_DebugPopupHandler debugPopupHandler = null;

    public string[] GetCommands()
    {
        return new string[2] { commandSaveWord, commandLoadWord };
    }

    public bool ReadCommand(string command)
    {
        if (command.ToLower().Trim().Equals(commandSaveWord.ToLower().Trim()))
        {
            OnSavePath.Call();
            PlayerPrefs.SetString(commandSaveWord, laneData.LatestValue);
            return true;
        }
        else if(command.ToLower().Trim().Equals(commandLoadWord.ToLower().Trim()))
        {
            laneData.SetValue(PlayerPrefs.GetString(commandSaveWord, ""));
            OnPathLoaded.Call();
            debugPopupHandler.ShowPopup(new string[1] {"A lane has been loaded"});
            return true;
        }
        
        return false;
    }
}
