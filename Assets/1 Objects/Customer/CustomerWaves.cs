using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerWaves : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] float timeBtwSpawn;
    float currentTimeBtwSpawn;
    [SerializeField] int maxNumberOfItem;

    [SerializeField] CustomerSpawner cs;

    private void Awake()
    {
        currentTimeBtwSpawn = timeBtwSpawn;
    }
    private void Update()
    {
        if(currentTimeBtwSpawn < 0)
        {
            currentTimeBtwSpawn = timeBtwSpawn;
            SpawnCustomer();

        }else currentTimeBtwSpawn -= Time.deltaTime;
    }

    [Button("Spawn Customer")]
    void SpawnSimpleCustomer()
    {
        StartCoroutine(cs.Spawn(maxNumberOfItem, Shipment.Instance.Targets));
    }

    void SpawnCustomer()
    {
        StartCoroutine(cs.Spawn(Random.Range(1, maxNumberOfItem + 1), Shipment.Instance.Targets));
    }
}
