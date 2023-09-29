using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebuggingConsole : MonoBehaviour
{
    private bool isShowingConsole = false;
    [SerializeField] private GameObject consoleUIInstance = null;
    [SerializeField] private TMP_InputField tmpInput = null;
    private IConsoleReader[] commandReaders = null;
    private List<string> commandMemory = new List<string>();
    private int commandMemoryIndex = 0;

    private void Awake()
    {
        commandReaders = GetComponents<IConsoleReader>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (isShowingConsole)
            {
                isShowingConsole = false;
                RemoveConsole();
            }
            else
            {
                isShowingConsole = true;
                ShowConsole();
            }
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter) && isShowingConsole)
        {
            ReadContents();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ReadFromCommandMemory(1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ReadFromCommandMemory(-1);
        }
    }

    private void ReadFromCommandMemory(int stepDirection)
    {
        if (commandMemory.Count == 0) return;
        tmpInput.text = commandMemory[commandMemoryIndex];
        tmpInput.ActivateInputField();
        commandMemoryIndex += stepDirection;
        if(commandMemoryIndex >= commandMemory.Count)
        {
            commandMemoryIndex = commandMemory.Count - 1;
        }
        else if(commandMemoryIndex < 0)
        {
            commandMemoryIndex = 0;
        }
    }

    private void ReadContents()
    {
        if(tmpInput.text.Length > 0)
        {
            for (int i = 0; i < commandReaders.Length; i++)
            {
                commandReaders[i].ReadCommand(tmpInput.text);
            }
            commandMemory.Insert(0,tmpInput.text);
            tmpInput.text = "";
            tmpInput.ActivateInputField();
        }
    }

    private void ShowConsole()
    {
        consoleUIInstance.SetActive(true);
        tmpInput.ActivateInputField();
    }

    private void RemoveConsole()
    {
        consoleUIInstance.SetActive(false);
    }
}
