using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAutoInitHandler : MonoBehaviour
{
    private void Start()
    {
        Item item = GetComponent<Item>();

        if (item == null)
            throw new System.Exception("Object Requires Item Component");

        item.Init();
        item.ForceGridPlacement();
    }
}
