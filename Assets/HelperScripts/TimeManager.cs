using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static float DeltaTime => Time.deltaTime * timeScale;
    public static float UnscaledDeltaTime => Time.deltaTime;
    public static float FixedDeltaTime => Time.fixedDeltaTime * timeScale;
    private static float timeScale = 1;

    public static float TimeScale { get { return timeScale; }  set { timeScale = value; } }
}
