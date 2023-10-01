using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private CustomerBrain customerPrefab = null;
    [SerializeField] private Transform entrancePoint = null;
    [SerializeField] private Transform exitPoint = null;
    [SerializeField] private List<Item> targets = new List<Item>();
   

    public IEnumerator Spawn(int itemToTake)
    {
        yield return new WaitForEndOfFrame();

        if (targets.Count > 0)
        {
            CustomerBrain customer = Instantiate<CustomerBrain>(customerPrefab, entrancePoint.position, entrancePoint.rotation, transform);

            if (itemToTake > targets.Count) itemToTake = targets.Count;

            Item[] _targets = new Item[itemToTake];

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
			customer.OnCustomerDone += CustomerDone;
        }
    }
	
	
    private void CustomerDone(CustomerBrain brain)
    {
        Destroy(brain.gameObject);
    }
}
