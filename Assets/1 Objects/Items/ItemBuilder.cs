using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            list[i].gameObject.SetActive(true);
        }
        return list;
    }
}
