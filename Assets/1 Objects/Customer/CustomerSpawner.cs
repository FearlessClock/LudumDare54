using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private CustomerMovementHandler customerPrefab = null;
    [SerializeField] private Transform entrancePoint = null;
    [SerializeField] private Transform exitPoint = null;
    [SerializeField] private ItemAStarTargetPoints[] targets = null;
    

    /*private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();

        CustomerMovementHandler customer = Instantiate<CustomerMovementHandler>(customerPrefab, entrancePoint.position, entrancePoint.rotation, transform);

        customer.Init(targets, exitPoint.position);
    }*/

    public IEnumerator Spawn(int itemToTake)
    {
        yield return new WaitForEndOfFrame();

        CustomerMovementHandler customer = Instantiate<CustomerMovementHandler>(customerPrefab, entrancePoint.position, entrancePoint.rotation, transform);

        ItemAStarTargetPoints[] _targets = new ItemAStarTargetPoints[itemToTake];

        for (int i = 0; i < _targets.Length; i++)
        {
            var item = targets[UnityEngine.Random.Range(1, targets.Length)];

            while(_targets.Contains(item))
            {
                item = targets[UnityEngine.Random.Range(1, targets.Length)];
            }

            _targets[i] = item;
        }

        customer.Init(_targets, exitPoint.position);
    }
}
