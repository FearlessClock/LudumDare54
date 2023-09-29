using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CR_SendReportController : MonoBehaviour, IConsoleReader
{
    [SerializeField] private GameObject sendReportPrefab = null;
    private string commandWord = "Send_report";

    public string[] GetCommands()
    {
        return new string[1] { commandWord };
    }

    public bool ReadCommand(string command)
    {
        if(command.ToLower().Trim().Equals(commandWord.ToLower().Trim())) 
        {
            SendReport();
            return true;
        }
        return false;
    }

    public void SendReport()
    {
        Instantiate<GameObject>(sendReportPrefab);
    }
}
