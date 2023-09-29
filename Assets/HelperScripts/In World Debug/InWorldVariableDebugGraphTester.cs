using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InWorldVariableDebugGraphTester : MonoBehaviour
{
    [SerializeField] private InWorldVariableDebugGraphController controller = null;
    [SerializeField] private Transform debuggedTransform  = null;
    private Color[] colors = new Color[5] {Color.red, Color.green, Color.blue, Color.white, Color.magenta}; 

    private void Update()
    {
        controller.AddPoint(debuggedTransform.position.y, colors[Random.Range(0, colors.Length)]);
    }
}
