using HelperScripts.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CR_SteamStatsHandler : MonoBehaviour, IConsoleReader
{
    private string ResetStatsAndAchievementsCommandWord = "RESET_ACHIEVEMENTS";
    [SerializeField] private EventScriptable OnResetStats = null;

    public string[] GetCommands()
    {
        return new string[1] {ResetStatsAndAchievementsCommandWord} ;
    }

    public bool ReadCommand(string command)
    {
        if (command.Trim().ToLower().Equals(ResetStatsAndAchievementsCommandWord.ToLower().Trim()))
        {
            OnResetStats.Call();
            return true;
        }
        return false;
    }
}
