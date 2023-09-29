using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CR_RemoveCanvasesCommand : MonoBehaviour, IConsoleReader
{
    private string commandWord = "Remove_UI";
    public string[] GetCommands()
    {
        return new string[1] { commandWord + " Boolean" };
    }

    public bool ReadCommand(string command)
    {
        string[] args = command.Split(' ');
        if(args.Length == 2 && args[0].ToLower().Trim().Equals(commandWord.ToLower().Trim()))
        {
            Canvas[] canvases = FindObjectsOfType<Canvas>();
            for (int i = 0; i < canvases.Length; i++)
            {
                canvases[i].gameObject.SetActive(args[1].ToLower().Trim().Equals("true")? true:false);
            }
            return true;
        }
        return false;
    }
}
