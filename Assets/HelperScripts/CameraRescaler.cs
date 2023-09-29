using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraRescaler : MonoBehaviour
{
    private Camera currentCamera = null;
    [SerializeField] private float refOrtographicSize = 21;
    [SerializeField] private Vector2 referenceSize; 
    private float defaultWidth = 0;
    private float defaultHeight = 0;
    private Vector3 cameraPos;

    [SerializeField] private Transform levelMiddle = null;
    [SerializeField] private Transform levelBottom = null;
    private float levelHalfHeight = 0;

    private void Awake()
    {
        levelHalfHeight = levelMiddle.position.y - levelBottom.position.y;
        currentCamera = GetComponent<Camera>();
        if (currentCamera == null)
        {
            Debug.Log("No camera on the object");
            return;
        }
    }

    private void Update()
    {
        ZoomLevel(refOrtographicSize);
    }

    public void ZoomLevel(float ortographicSize)
    {
        if(currentCamera == null)
        {
            currentCamera = GetComponent<Camera>();
        }
        levelHalfHeight = levelMiddle.position.y - levelBottom.position.y;
        cameraPos = currentCamera.transform.position;
        defaultHeight = ortographicSize;
        defaultWidth = ortographicSize * (referenceSize.x / referenceSize.y);
        currentCamera.orthographicSize = defaultWidth / currentCamera.aspect;
        currentCamera.transform.position = new Vector3(cameraPos.x, currentCamera.orthographicSize - levelHalfHeight, cameraPos.z);

    }
}
