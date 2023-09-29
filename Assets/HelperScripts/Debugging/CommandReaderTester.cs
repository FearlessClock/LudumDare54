using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandReaderTester : MonoBehaviour, IConsoleReader
{
    public string[] GetCommands()
    {
        return new string[0];
    }

    public bool ReadCommand(string command)
    {
        Debug.Log(command);
        return true;
    }
}
