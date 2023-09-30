using Grid;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Block
{
    [SerializeField] private Item_Data data;

    [SerializeField, Required] private SpriteRenderer spriteRenderer;
    [SerializeField, Required] private BoxCollider2D itemCollider;

    private Vector3 lastPosition;

    protected override void Start()
    {
        name = data.Label;
        blockLayoutOffset = data.ShapeLayout;

        base.Start();

        spriteRenderer.sprite = data.Sprite;
        spriteRenderer.transform.localPosition = BlockPivotOffset;

        itemCollider.offset = BlockPivotOffset;
        itemCollider.size = BlockLayoutSize;

        lastPosition = transform.position;

        OnRelease.AddListener(MouseDropItem);
    }

    private void MouseDropItem()
    {
        // Check if Corner Bounds are Inside the Grid
        // better than checking block's every cells
        bool inside = GridManager.Instance.IsInsideGrid(transform.position + BottomLeftBound)
            && GridManager.Instance.IsInsideGrid(transform.position + TopRightBound);

        if(!inside)
        {
            ReturnToLastPosition();
            return;
        }

        lastPosition = transform.position;
    }

    private void ReturnToLastPosition() => transform.position = lastPosition;
}
