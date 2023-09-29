using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CR_ShowAvailableCommands : MonoBehaviour, IConsoleReader
{
    [SerializeField] private CR_DebugPopupHandler debugPopupHandler = null;
    private string commandWord = "help";

    public string[] GetCommands()
    {
        return new string[1] { commandWord };
    }

    public bool ReadCommand(string command)
    {
        if (command.Trim().ToLower().Equals(commandWord))
        {
            IConsoleReader[] readers = GetComponents<IConsoleReader>();
            List<string> commands = new List<string>();
            for (int i = 0; i < readers.Length; i++)
            {
                commands.AddRange(readers[i].GetCommands());
            }

            debugPopupHandler.gameObject.SetActive(true);
            debugPopupHandler.ShowPopup(commands.ToArray());

            return true;
        }
        return false;
    }
}
