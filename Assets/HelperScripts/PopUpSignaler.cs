using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpSignaler : MonoBehaviour
{
    [SerializeField] private BoolVariable isPopUpOpen = null;

    private void OnEnable()
    {
        isPopUpOpen.SetValue(true);
    }

    private void OnDisable()
    {
        isPopUpOpen.SetValue(false);
    }
}
