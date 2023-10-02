using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerWaves : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] float timeBeforeSpawn;
    [SerializeField] float timeBtwSpawn;
    [SerializeField] float miniTimeBtwSpawn;
    [SerializeField] float multiplier;
    float modifiedTimeBtwSpawn;
    float currentTimeBtwSpawn;
    [SerializeField] int maxNumberOfItem;

    [SerializeField] CustomerSpawner cs;

    private void Awake()
    {
        currentTimeBtwSpawn = timeBeforeSpawn;
        modifiedTimeBtwSpawn = timeBtwSpawn;
    }
    private void Update()
    {
        if(currentTimeBtwSpawn < 0)
        {
            modifiedTimeBtwSpawn *= multiplier;
            modifiedTimeBtwSpawn = Mathf.Clamp(modifiedTimeBtwSpawn, miniTimeBtwSpawn, timeBtwSpawn);
            currentTimeBtwSpawn = modifiedTimeBtwSpawn;
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
