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
    //[SerializeField] private ItemAStarTargetPoints[] targets = null;
    [SerializeField] private List<ItemAStarTargetPoints> targets = new List<ItemAStarTargetPoints>();
    

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

        if (targets.Count > 0)
        {
            if (itemToTake > targets.Count) itemToTake = targets.Count;

            ItemAStarTargetPoints[] _targets = new ItemAStarTargetPoints[itemToTake];

            for (int i = 0; i < _targets.Length; i++)
            {
                var item = targets[UnityEngine.Random.Range(0, targets.Count)];

                while (_targets.Contains(item))
                {
                    item = targets[UnityEngine.Random.Range(0, targets.Count)];
                }

                _targets[i] = item;
                targets.Remove(item);

            }

            customer.Init(_targets, exitPoint.position);
        }
    }
}
