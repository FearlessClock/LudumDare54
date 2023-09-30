using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Block
{
    [SerializeField] private Item_Data data;

    [SerializeField, Required] private SpriteRenderer spriteRenderer;
    [SerializeField, Required] private BoxCollider2D itemCollider;

    protected override void Start()
    {
        name = data.Label;
        blockLayoutOffset = data.ShapeLayout;

        base.Start();

        spriteRenderer.sprite = data.Sprite;
        spriteRenderer.transform.localPosition = BlockPivotOffset;

        itemCollider.offset = BlockPivotOffset;
        itemCollider.size = BlockLayoutSize;
    }

    void Update()
    {
        
    }
}
