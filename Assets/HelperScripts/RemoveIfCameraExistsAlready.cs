using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveIfCameraExistsAlready : MonoBehaviour
{
    private void Awake()
    {
        if(FindObjectsOfType<Camera>().Length > 1)
        {
            Destroy(this.gameObject);
        }
    }
}
