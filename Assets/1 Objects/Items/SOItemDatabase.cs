using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Database", menuName = "Item Database")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] private List<ItemData> items;

    public int Count => items.Count;

    public ItemData GetItemData(int index) => items[index];
}
