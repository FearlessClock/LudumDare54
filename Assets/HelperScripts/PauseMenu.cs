using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    private float lastTimeScale = 0;

    private void Awake()
    {
        lastTimeScale = Time.timeScale ;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = lastTimeScale;
    }
}
