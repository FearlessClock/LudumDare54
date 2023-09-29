using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CR_BoolVariableSetter : MonoBehaviour, IConsoleReader
{
    [SerializeField] private string activationCommand = "";
    [SerializeField] private PlayerPrefBoolVariable boolVariable = null;

    public string[] GetCommands()
    {
        return new string[1] { activationCommand + " Boolean"};
    }

    public bool ReadCommand(string command)
    {
        string[] args = command.Split(' ');
        if (args[0].Trim().ToLower().Equals(activationCommand))
        {
            bool val = false;
            if (args[1].Trim().ToLower().Equals("true"))
            {
                val = true;
            }
            else if (args[1].Trim().ToLower().Equals("false"))
            {
                val = false;
            }
            else
            {
                Debug.Log("Value not correct, should be boolean");
            }
            boolVariable.SetValue(val); 
            return true;
        }
        return false;
    }
}
