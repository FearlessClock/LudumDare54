using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPoolReturner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SendToPool stp = other.GetComponent<SendToPool>();
        if (stp)
        {
            stp.SendBackToPool();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        SendToPool stp = collision.gameObject.GetComponent<SendToPool>();
        if (stp)
        {
            stp.SendBackToPool();
        }
    }
}
