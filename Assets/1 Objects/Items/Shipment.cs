using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shipment : MonoBehaviour
{
    static Shipment instance;
    public static Shipment Instance { get => instance; }

    [Header("Start spawn")]
    [SerializeField] private int numberOfItemsAtStart; 
    [SerializeField] private float startSpawnOffset;

    [Header("Shipment")]
    [SerializeField] private float shipmentOffset;
    private List<Item> allTargets = new List<Item>();
    private List<Item> targets = new List<Item>();
    public List<Item> Targets { get => targets;}

    [Header("Spawn item")]
    [SerializeField] private float timeBtwSpawn;
    private float currentTimeBetwSpawn;



    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        SetItems();
        currentTimeBetwSpawn = timeBtwSpawn;
    }

    private void Update()
    {
        if (currentTimeBetwSpawn < 0)
        {
            currentTimeBetwSpawn = timeBtwSpawn;
            SpawnItem();
        }
        else currentTimeBetwSpawn -= Time.deltaTime;
    }

    void SetItems()
    {
        allTargets = ItemBuilder.GetAll();

        for (int i = 0; i < numberOfItemsAtStart; i++)
        {
            var item = allTargets[Random.Range(0, allTargets.Count)];

            targets.Add(item);

            var spawnPos = new Vector3(Random.Range(-startSpawnOffset, startSpawnOffset) + Random.Range(-startSpawnOffset, startSpawnOffset), 0);
            targets[i].MoveTo(spawnPos);
            targets[i].gameObject.SetActive(true);
            //targets[i].MouseDropItem();
            
        }
    }

    public void RecovObject(Item item)
    {
        var spawnPos = transform.position + new Vector3(UnityEngine.Random.Range(-shipmentOffset, shipmentOffset) + UnityEngine.Random.Range(-shipmentOffset, shipmentOffset), 0);
        item.gameObject.SetActive(true);
        item.gameObject.transform.position = spawnPos;
        targets.Add(item);
    }

    public void SpawnItem()
    {
        var item = Instantiate(allTargets[Random.Range(0, allTargets.Count)].gameObject, transform.position, transform.rotation);
        item.GetComponent<Item>()?.Init();
        RecovObject(item.GetComponent<Item>());
    }
}
