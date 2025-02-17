using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif

public class ItemBuilder : MonoBehaviour
{
    static ItemBuilder instance;
    public static ItemBuilder Instance { get => instance; }

    [SerializeField] private ItemDatabase database;
    [SerializeField] private GameObject itemBasePrefab;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public static List<Item> GetAll() => Instance?.GetAllItems();

    private Item CreateItem() => Instantiate(itemBasePrefab, transform.position, Quaternion.identity).GetComponent<Item>();

    private List<Item> GetAllItems()
    {
        List<Item> list = new List<Item>();
        for (int i = 0; i < database.Count; ++i)
        {
            list.Add(CreateItem());
            list[i].SetData(database.GetItemData(i));
            list[i].Init();
            list[i].gameObject.SetActive(false);
        }
        return list;
    }

#if UNITY_EDITOR
    [Button]
    private void SpawnAllItems()
    {
        var items = GetAllItems();
        foreach (var item in items)
        {
            item.gameObject.SetActive(true);
        }
    }
#endif
}
