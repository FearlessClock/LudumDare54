using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewManager : MonoBehaviour
{
    public static ReviewManager Instance;


    private void Awake()
    {
        Instance = this;
    }


}
