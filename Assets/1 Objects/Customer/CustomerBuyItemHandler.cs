using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerBuyItemHandler : MonoBehaviour
{
    private CustomerMovementHandler movementHandler = null;

    private void Awake()
    {
        movementHandler = GetComponent<CustomerMovementHandler>();
        movementHandler.OnArriveAtSpot += OnArrive;
    }

    private void OnArrive()
    {

    }
}
