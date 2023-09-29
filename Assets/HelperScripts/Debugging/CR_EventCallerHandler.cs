using HelperScripts.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CR_EventCallerHandler : MonoBehaviour, IConsoleReader
{
    [SerializeField] private EventScriptable eventToCall= null;
    [SerializeField] private string commandWord = "command_word";
    public string[] GetCommands()
    {
        return new string[1] { commandWord };
    }

    public bool ReadCommand(string command)
    {
        if (command.ToLower().Trim().Equals(commandWord.ToLower().Trim()))
        {
            eventToCall.Call();
            return true;
        }
        return false;
    }
}
