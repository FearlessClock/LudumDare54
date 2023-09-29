using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    private GUIStyle style;
    private float deltaTime = 0.0f;
    private float highestFPS = 0.0f;
    private float lowestFPS = float.MaxValue;
    private int frameCount = 0;
    private float totalFPS = 0.0f;

    private void Awake()
    {
        style = new GUIStyle();
        style.fontSize = 9;
        style.normal.textColor = Color.white;
        style.alignment = TextAnchor.UpperLeft;
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float currentFPS = 1.0f / deltaTime;

        // Update highest and lowest FPS
        highestFPS = Mathf.Max(highestFPS, currentFPS);
        lowestFPS = Mathf.Min(lowestFPS, currentFPS);

        // Update total FPS for average calculation
        frameCount++;
        totalFPS += currentFPS;
    }

    private void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        // Background
        GUI.Box(new Rect(0, 0, w, h * 2 / 100), GUIContent.none, style);

        // Current FPS
        float currentFPS = 1.0f / deltaTime;
        string fpsText = string.Format("FPS: {0:0.}", currentFPS);
        GUI.Label(new Rect(5, 0, w, h * 2 / 100), fpsText, style);

        // Average FPS
        float averageFPS = totalFPS / frameCount;
        string avgFPSText = string.Format("Avg FPS: {0:0.}", averageFPS);
        GUI.Label(new Rect(5, h * 2 / 100, w, h * 2 / 100), avgFPSText, style);

        // Highest FPS
        string highestFPSText = string.Format("Max FPS: {0:0.}", highestFPS);
        GUI.Label(new Rect(5, h * 4 / 100, w, h * 2 / 100), highestFPSText, style);

        // Lowest FPS
        string lowestFPSText = string.Format("Min FPS: {0:0.}", lowestFPS);
        GUI.Label(new Rect(5, h * 6 / 100, w, h * 2 / 100), lowestFPSText, style);
    }
}
