using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CustomerSpawner : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private CustomerBrain customerPrefab = null;
    [SerializeField] private Transform entrancePoint = null;
    [SerializeField] private Transform exitPoint = null;


    [Header("Shipment")]
    [SerializeField] private Transform shipmentLocation;
    [SerializeField] private float shipmentOffset;
    [SerializeField] private int numberOfItemsAtStart;
    /*[SerializeField] */private List<Item> allTargets = new List<Item>();
    /*[SerializeField] */private List<Item> targets = new List<Item>();

    private void Start()
    {
        allTargets = ItemBuilder.GetAll();

        for (int i = 0; i < numberOfItemsAtStart; i++)
        {
            var item = allTargets[UnityEngine.Random.Range(0, allTargets.Count)];

            while (targets.Contains(item))
            {
                item = allTargets[UnityEngine.Random.Range(0, allTargets.Count)];
            }
            targets.Add(item);
            
            var spawnPos = shipmentLocation.position + new Vector3(UnityEngine.Random.Range(-shipmentOffset, shipmentOffset) + UnityEngine.Random.Range(-shipmentOffset, shipmentOffset), 0);
            item.MoveTo(spawnPos);
            item.gameObject.SetActive(true);
        }
    }

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
            customer.SetCustomerSpawner(this);
			customer.OnCustomerDone += CustomerDone;
        }
    }

    public void RecovObject(Item item)
    {
        Debug.Log("recov item");
        var spawnPos = shipmentLocation.position + new Vector3(UnityEngine.Random.Range(-shipmentOffset, shipmentOffset) + UnityEngine.Random.Range(-shipmentOffset, shipmentOffset), 0);
        item.gameObject.SetActive(true);
        targets.Add(item);
    }
	
	
    private void CustomerDone(CustomerBrain brain)
    {
        Destroy(brain.gameObject);
    }
}
