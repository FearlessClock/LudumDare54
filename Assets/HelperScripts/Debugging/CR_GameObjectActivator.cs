using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CR_GameObjectActivator : MonoBehaviour, IConsoleReader
{
    [System.Serializable]
    struct KeyPairGameObjectCommand
    {
        public GameObject gameObject;
        public string commandString;
    }
    [SerializeField] private KeyPairGameObjectCommand[] commands = null;

    public bool ReadCommand(string command)
    {
        string[] args = command.Split();
        if(args.Length != 2)
        {
            return false;
        }
        for (int i = 0; i < commands.Length; i++)
        {
            if (commands[i].commandString.ToLower().Trim().Equals(args[0].ToLower().Trim()))
            {
                if (args[1].ToLower().Trim().Equals("true"))
                {
                    commands[i].gameObject.SetActive(true);
                }
                else if (args[1].ToLower().Trim().Equals("false"))
                {
                    commands[i].gameObject.SetActive(false);
                }
            }
        }
        return true;
    }

    public string[] GetCommands()
    {
        string[] commandWords = new string[commands.Length];
        for (int i = 0; i < commands.Length; i++)
        {
            commandWords[i] = commands[i].commandString + " Boolean";
        }
        return commandWords;
    }
}
