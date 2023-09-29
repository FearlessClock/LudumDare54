using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerPrefStringVariable", menuName = "UnityHelperScripts/PlayerPrefs/PlayerPrefStringVariable", order = 0)]
public class PlayerPrefStringVariable : ScriptableObject
{
    public string ID;
    public string value;
    public string defaultValue;

    public Action OnValueChanged;
    private void OnValidate()
    {
        if (ID.Equals(""))
        {
            ID = this.name;
        }
    }
    private void OnEnable()
    {
        Load();
    }

    public void Save()
    {
        PlayerPrefs.SetString(ID, value);
        OnValueChanged?.Invoke();
    }

    public void Load()
    {
        value = PlayerPrefs.GetString(ID, defaultValue);
    }
    public void SetValue(string value)
    {
        this.value = value;
        Save();
    }

    public void Reset()
    {
        PlayerPrefs.DeleteKey(ID);
    }

    public string LatestValue
    {
        get { Load(); return value; }
    }
}
