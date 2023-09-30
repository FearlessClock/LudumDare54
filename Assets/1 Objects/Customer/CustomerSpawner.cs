using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private CustomerMovementHandler customerPrefab = null;
    [SerializeField] private Transform entrancePoint = null;
    [SerializeField] private Transform exitPoint = null;
    [SerializeField] private ItemAStarTargetPoints[] targets = null;

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();

        CustomerMovementHandler customer = Instantiate<CustomerMovementHandler>(customerPrefab, entrancePoint.position, entrancePoint.rotation, transform);

        customer.Init(targets, exitPoint.position);
    }
}
