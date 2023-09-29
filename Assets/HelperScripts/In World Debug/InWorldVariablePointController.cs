using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InWorldVariablePointController : MonoBehaviour
{
    private new Renderer renderer = null;

    private void Awake()
    {
        renderer = GetComponentInChildren<Renderer>();
    }

    public void SetColor(Color color)
    {
        renderer.material.color = color;
    }
}
