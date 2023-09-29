using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InWorldVariableDebugGraphController : MonoBehaviour
{
    private List<Point> points = new List<Point>();


    [SerializeField] private float spacing = 0.02f;
    [SerializeField] private float scale = 1f;
    [SerializeField] private int maxPoints = 100;

    [SerializeField] private InWorldVariablePointController pointPrefab = null;
    private InWorldVariablePointController[] pointInstances = null;

    private bool insertedThisFrame = false;

    private new Camera camera = null;
    [SerializeField] private bool faceCamera = true;

    private void Start()
    {
        camera = FindObjectOfType<Camera>();
        pointInstances = new InWorldVariablePointController[maxPoints];
        for (int i = 0; i < pointInstances.Length; i++)
        {
            pointInstances[i] = Instantiate<InWorldVariablePointController>(pointPrefab, this.transform.position + Vector3.right * i* spacing, Quaternion.identity, this.transform);
        }
    }

    public void AddPoint(float value, Color color)
    {
        points.Insert(0,new Point(value, color));
        insertedThisFrame = true;
        if(points.Count > maxPoints)
        {
            points.RemoveAt(maxPoints - 1);
        }
    }

    private void FixedUpdate()
    {
        if (!insertedThisFrame)
        {
            points.Insert(0, null);
            if (points.Count > maxPoints)
            {
                points.RemoveAt(maxPoints - 1);
            }
        }
        TurnToCamera();
        PrintGraph();
        insertedThisFrame=false;
    }

    private void TurnToCamera()
    {
        if (faceCamera)
        {
            this.transform.forward = (camera.transform.position - this.transform.position).normalized ;
        }
    }

    private void PrintGraph()
    {
        for (int i = 0; i < pointInstances.Length; i++)
        {
            if(i < points.Count)
            {
                float value = points[i] == null ? 0 : points[i].value;
                pointInstances[i].transform.localPosition = new Vector3(pointInstances[i].transform.localPosition.x, value* scale, pointInstances[i].transform.localPosition.z);
                pointInstances[i].SetColor(points[i] == null ? Color.black : points[i].color);
            }
        }
    }
}

public class Point
{
    public float value;
    public Color color;

    public Point(float value, Color color)
    {
        this.value = value;
        this.color = color;
    }
}
