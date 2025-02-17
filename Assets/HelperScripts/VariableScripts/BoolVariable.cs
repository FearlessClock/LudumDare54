﻿using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "BoolVariable", menuName = "UnityHelperScripts/Variables/BoolVariable", order = 0)]
[InlineEditor]
public class BoolVariable : ScriptableObject
{
    public bool defaultValue = false;

    public void Reset()
    {
        value = defaultValue;
    }
    public bool value = false;
    public BoolEvent OnValueChanged;
    public static implicit operator bool(BoolVariable reference)
    {
        return reference.value;
    }
    [TextArea]
    public string description = "";

    public void SetValue(bool v)
    {
        this.value = v;
        OnValueChanged?.Invoke(v);
    }

    public void Inverse()
    {
        this.value = !this.value;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}