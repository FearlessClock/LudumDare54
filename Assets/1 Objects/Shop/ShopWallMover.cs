using Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShopWallMover : MonoBehaviour
{
    [SerializeField] private GameObject wallRenderer = null;
    [SerializeField] private float cellSize = 1;

    private void Awake()
    {
        if (Application.isPlaying)
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (Application.isPlaying) return;

        float cellSizeX = Mathf.Sign(this.transform.position.x) * cellSize;
        float cellSizeY = Mathf.Sign(this.transform.position.y) * cellSize;
        wallRenderer.transform.localPosition = -new Vector3(this.transform.position.x- (int)(this.transform.position.x+ cellSizeX / 2), this.transform.position.y-(int)(this.transform.position.y + cellSizeY / 2), this.transform.position.z);
    }
} 
