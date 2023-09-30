using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private CustomerBrain customerPrefab = null;
    [SerializeField] private Transform entrancePoint = null;
    [SerializeField] private Transform exitPoint = null;
    [SerializeField] private ItemAStarTargetPoints[] targets = null;

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();

        CustomerBrain customer = Instantiate<CustomerBrain>(customerPrefab, entrancePoint.position, entrancePoint.rotation, transform);

        customer.Init(targets, exitPoint.position);
        customer.OnCustomerDone += CustomerDone;
    }

    private void CustomerDone(CustomerBrain brain)
    {
        Destroy(brain.gameObject);
    }
}
