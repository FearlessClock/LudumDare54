 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerPrefBoolVariable", menuName = "UnityHelperScripts/PlayerPrefs/PlayerPrefBoolVariable", order = 0)]
public class PlayerPrefBoolVariable : ScriptableObject
{
    public string ID;
    public bool value;
    public bool defaultValue;

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
        PlayerPrefs.SetInt(ID, value? 1:0);
        OnValueChanged?.Invoke();
    }

    public void Load()
    {
        value = PlayerPrefs.GetInt(ID, defaultValue ? 1 : 0) == 0? false : true;
    }
    public void SetValue(bool value)
    {
        this.value = value ;
        Save();
    }

    public void Reset()
    {
        SetValue(defaultValue);
    }

    public bool LatestValue
    {
        get { Load(); return value; }
    }
}
