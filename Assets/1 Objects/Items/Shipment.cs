using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Shipment : MonoBehaviour
{
    static Shipment instance;
    public static Shipment Instance { get => instance; }

    [SerializeField] private int numberOfItemsAtStart; 
    [SerializeField] private float startSpawnOffset; 

    [SerializeField] private float shipmentOffset;
    private List<Item> allTargets = new List<Item>();
    private List<Item> targets = new List<Item>();
    public List<Item> Targets { get => targets;}

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
    }

    void SetItems()
    {
        allTargets = ItemBuilder.GetAll();

        for (int i = 0; i < numberOfItemsAtStart; i++)
        {
            var item = allTargets[Random.Range(0, allTargets.Count)];

            while (targets.Contains(item))
            {
                item = allTargets[UnityEngine.Random.Range(0, allTargets.Count)];
            }
            targets.Add(item);

            var spawnPos = new Vector3(UnityEngine.Random.Range(-startSpawnOffset, startSpawnOffset) + UnityEngine.Random.Range(-startSpawnOffset, startSpawnOffset), 0);
            item.MoveTo(spawnPos);
            item.gameObject.SetActive(true);
            item.MouseDropItem();
        }
    }

    public void RecovObject(Item item)
    {
        var spawnPos = transform.position + new Vector3(UnityEngine.Random.Range(-shipmentOffset, shipmentOffset) + UnityEngine.Random.Range(-shipmentOffset, shipmentOffset), 0);
        item.gameObject.SetActive(true);
        targets.Add(item);
    }
}
