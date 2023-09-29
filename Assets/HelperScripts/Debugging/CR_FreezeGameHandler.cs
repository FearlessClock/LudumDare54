using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CR_FreezeGameHandler : MonoBehaviour, IConsoleReader
{
    private string commandWord = "freeze_game";

    public string[] GetCommands()
    {
        return new string[1] { commandWord };  
    }

    public bool ReadCommand(string command)
    {
        string[] args = command.Split(' ');
        if (args.Length == 2 && args[0].ToLower().Trim().Equals(commandWord.ToLower().Trim()))
        {
            TimeManager.TimeScale = float.Parse(args[1]);
            Time.timeScale = float.Parse(args[1]);
            return true;
        }
        return false;
    }
}
