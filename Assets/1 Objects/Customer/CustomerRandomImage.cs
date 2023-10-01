using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerRandomImage : MonoBehaviour
{
    [SerializeField] private Sprite[] customerSprites;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = customerSprites[Random.Range(0, customerSprites.Length)];
    }
}
