using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerBuyItemHandler : MonoBehaviour
{
    private ItemAStarTargetPoints[] itemsToBuy = null;
    private int step = 0;

    public ItemAStarTargetPoints[] GetRemainingItems
    {
        get
        {
            List<ItemAStarTargetPoints> items = new List<ItemAStarTargetPoints>();
            for (int i = step; i < itemsToBuy.Length; i++)
            {
                items.Add(itemsToBuy[i]);
            }
            return items.ToArray();
        }
    }

    public void ClaimItem(ItemAStarTargetPoints target)
    {
        step++;
        target.gameObject.SetActive(false);
    }

    public ItemAStarTargetPoints GetItemNotBought()
    {
        return itemsToBuy[step];
    }

    public void Init(ItemAStarTargetPoints[] itemsToBuy)
    {
        this.itemsToBuy = itemsToBuy;
    }
}
