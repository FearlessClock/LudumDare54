using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerPrefFloatVariable", menuName = "UnityHelperScripts/PlayerPrefs/PlayerPrefFloatVariable", order = 0)]
public class PlayerPrefFloatVariable : ScriptableObject
{
    public string ID;
    public float value;
    public float defaultValue;
    public Action OnValueChanged;

    private void OnEnable()
    {
        Load();
    }

    public void Save()
    {
        PlayerPrefs.SetFloat(ID, value);
        OnValueChanged?.Invoke();
    }

    public void Load()
    {
        value = PlayerPrefs.GetFloat(ID, defaultValue);
    }

    public void SetValue(float value)
    {
        this.value = value;
        Save();
    }

    public float LatestValue
    {
        get { Load(); return value; }
    }
}
