using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CR_FreeFloatingCameraActivator : MonoBehaviour, IConsoleReader
{
    [SerializeField] private GameObject freefloatingCamera = null;
    private string commandWord = "free_the_camera";

    private GameObject freeCameraInstance = null;
    public string[] GetCommands()
    {
        return new string[1] { commandWord };
    }

    public bool ReadCommand(string command)
    {
        command = command.Trim().ToLower();
        string[] str =  command.Split(' ');

        if (str.Length < 2) return false;

        if (str[0].Trim().ToLower().Equals(commandWord))
        {
            if (str[1] == "false" && freeCameraInstance != null)
            {
                Destroy(freeCameraInstance);
                freeCameraInstance = null;
            }
            else if (str[1] == "true" && freeCameraInstance == null)
            {
                freeCameraInstance = Instantiate<GameObject>(freefloatingCamera);
            }
            return true;
        }
        return false;
    }
}
