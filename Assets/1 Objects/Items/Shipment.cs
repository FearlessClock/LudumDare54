using Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shipment : MonoBehaviour
{
    static Shipment instance;
    public static Shipment Instance { get => instance; }

    [Header("Start spawn")]
    [SerializeField] private int numberOfItemsAtStart; 

    [Header("Storage")]
    [SerializeField] private Vector2 storageAreaSize;
    private List<Item> allItems = new List<Item>();

    private List<Item> availableTargets = new List<Item>();
    public List<Item> Targets { get => availableTargets;}

    [Header("Spawn item")]
    [SerializeField] private float timeBetweenSpawn;
    private float currentTimeBetweenSpawn;



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
        currentTimeBetweenSpawn = timeBetweenSpawn;
    }

    private void Update()
    {
        if (currentTimeBetweenSpawn < 0)
        {
            currentTimeBetweenSpawn = timeBetweenSpawn;
            SpawnItemInStorage();
        }
        else currentTimeBetweenSpawn -= Time.deltaTime;
    }

    private void SetItems()
    {
        allItems = ItemBuilder.GetAll();

        for (int i = 0; i < numberOfItemsAtStart; i++)
        {
            var item = GetRandomItem();
            Debug.Log("Spawned " + item.name);
            var spawnPos = GridManager.Instance.GetRandomAvailablePosition();
            item.MoveTo(spawnPos);

            item.gameObject.SetActive(true);
            item.ForceGridPlacement();
        }
    }

    private Item GetRandomItem()
    {
        var item = Instantiate(allItems[Random.Range(0, allItems.Count)]);
        item.Init();
        return item;
    }

    private void SpawnItemInStorage()
    {
        SoundTransmitter.Instance.Play("NewItem");

        var spawnPos = transform.position + new Vector3(Random.Range(-storageAreaSize.x, storageAreaSize.x), Random.Range(-storageAreaSize.y, storageAreaSize.y), 0) / 2f;
        
        var item = GetRandomItem();
        item.MoveTo(spawnPos);

        item.gameObject.SetActive(true);
    }

    public void AddToAvailableTargets(Item item)
    {
        if (availableTargets.Contains(item))
            return;

        availableTargets.Add(item);
    }

    public void RemoveFromAvailableTargets(Item item)
    {
        if (!availableTargets.Contains(item))
            return;

        availableTargets.Remove(item);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(.7f, .72f, .87f);
        Gizmos.DrawWireCube(transform.position, storageAreaSize);
    }
}
