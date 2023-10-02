using HelperScripts.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerBuyItemHandler : MonoBehaviour
{
    private Item[] itemsToBuy = null;
    private int step = 0;
    [SerializeField] private EventObjectScriptable onGainScore;
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
        Shipment.Instance.RemoveFromAvailableTargets(target);
        target.ItemBought();
        target.gameObject.SetActive(false);
        onGainScore.Call(1);
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
        for (int i = 0; i < itemsToBuy.Length - step; i++)
            Shipment.Instance.AddToAvailableTargets(itemsToBuy[i]);
    }
}
