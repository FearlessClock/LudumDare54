using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New PlayerPrefIntVariable", menuName = "UnityHelperScripts/PlayerPrefs/PlayerPrefIntVariable", order = 0)]
public class PlayerPrefIntVariable : ScriptableObject
{
    public string ID;
    public int value;
    public int defaultValue;

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
        PlayerPrefs.SetInt(ID, value);
        OnValueChanged?.Invoke();
    }

    public void Load()
    {
        value = PlayerPrefs.GetInt(ID, defaultValue);
    }
    public void SetValue(int value)
    {
        this.value = value;
        Save();
    }

    public void Add(int amount)
    {
        this.value += amount;
        Save();
    }

    public int LatestValue
    {
        get { Load();  return value; }
    }

    public void Reset()
    {
        SetValue(defaultValue);
    }
}
