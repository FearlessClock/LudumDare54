﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobile60FPS : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
