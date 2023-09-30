using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerBuyItemHandler : MonoBehaviour
{

    public void ClaimItem(ItemAStarTargetPoints target)
    {
        target.gameObject.SetActive(false);
    }
}
