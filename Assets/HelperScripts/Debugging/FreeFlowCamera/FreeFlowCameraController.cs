using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFlowCameraController : MonoBehaviour
{
    private float yaw;
    private float pitch;
    [SerializeField] private float speed = 5;
    [SerializeField] private float rotationSpeed = 10;


    void Update()
    {
        CameraTranslate();
        CameraRotate();
        CameraSpeed();
    }

    public void CameraTranslate()
    {

        Vector3 nexPosition = transform.position;

        if (Input.GetKey(KeyCode.Keypad6))
        {
            nexPosition += transform.right * speed * Time.unscaledDeltaTime;
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            nexPosition += -1 * transform.right * speed * Time.unscaledDeltaTime;
        }

        if (Input.GetKey(KeyCode.Keypad8))
        {
            nexPosition += transform.forward * speed * Time.unscaledDeltaTime;
        }
        if (Input.GetKey(KeyCode.Keypad5))
        {
            nexPosition += -1 * transform.forward * speed * Time.unscaledDeltaTime;
        }

        if (Input.GetKey(KeyCode.Keypad9))
        {
            nexPosition += Vector3.up * speed * Time.unscaledDeltaTime;
        }
        if (Input.GetKey(KeyCode.Keypad3))
        {
            nexPosition += -1 * Vector3.up * speed * Time.unscaledDeltaTime;
        }

        transform.position = nexPosition;
    }

    public void CameraRotate()
    {
        if (Input.GetMouseButton(1))
        {
            if (Input.GetAxis("Mouse X") != 0)
                yaw += rotationSpeed * Time.unscaledDeltaTime * Input.GetAxis("Mouse X");

            if (Input.GetAxis("Mouse Y") != 0)
                pitch += rotationSpeed * Time.unscaledDeltaTime * Input.GetAxis("Mouse Y") * -1;

            transform.rotation = Quaternion.Euler(pitch, yaw, 0);
        }
    }

    public void CameraSpeed()
    {
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            speed -= 0.2f;
            if( speed < 0) speed = 0;
        }
        
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            speed += 0.2f;
        }
    }
}
