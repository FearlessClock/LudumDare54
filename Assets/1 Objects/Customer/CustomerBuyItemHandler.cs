using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerBuyItemHandler : MonoBehaviour
{
    private Item[] itemsToBuy = null;
    private int step = 0;

    public Item[] GetRemainingItems
    {
        get
        {
            List<Item> items = new List<Item>();
            for (int i = step; i < itemsToBuy.Length; i++)
            {
                items.Add(itemsToBuy[i]);
            }
            return items.ToArray();
        }
    }

    public void ClaimItem(Item target)
    {
        step++;
        target.ItemBought();
        target.gameObject.SetActive(false);

    }

    public Item GetItemNotBought()
    {
        return itemsToBuy[step];
    }

    public void Init(Item[] itemsToBuy)
    {
        this.itemsToBuy = itemsToBuy;
    }

    public void ReturnItems()
    {
        for (int i = 0;i < itemsToBuy.Length; i++)
        {
            Shipment.Instance.RecovObject(itemsToBuy[i]);
            //itemsToBuy[i].gameObject.SetActive(true);
        }
    }
}
