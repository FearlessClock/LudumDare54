using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsoleReader
{
    bool ReadCommand(string command);
    string[] GetCommands();

    public static bool IsCommandword(string commandword, string text)
    {
        return commandword.Trim().ToLower().Equals(text.Trim().ToLower());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <returns>0 false, 1 true, -1 not one or the other</returns>
    public static int StringBooleanValue(string text)
    {
        if (text.Trim().ToLower().Equals("true")) return 1;
        if (text.Trim().ToLower().Equals("false")) return 0;
        return -1;
    }
}
