using Grid;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Item : Block
{
    [SerializeField] private ItemData data;

    [SerializeField, Required] private SpriteRenderer spriteRenderer;
    [SerializeField, Required] private BoxCollider2D itemCollider;
    [SerializeField] private bool isAlreadySpawn;

    private Vector3 lastPosition;
    private bool wasPlacedOnce;
    private List<Vector2> adjacentBlockLayoutOffset = new List<Vector2>();

    public override void Init()
    {
        name = data.Label;

        if (!data.Layout.Contains(Vector3.zero))
            data.Layout.AddCenterBlock();
        blockLayoutOffset = data.Layout.Positions;

        base.Init();
        ComputeAdjacentBlocks();

        spriteRenderer.sprite = data.Sprite;
        spriteRenderer.transform.localPosition = BlockPivotOffset;

        itemCollider.offset = BlockPivotOffset;
        itemCollider.size = BlockLayoutSize;

        lastPosition = transform.position;

        OnRelease.AddListener(MouseDropItem);

        //if (isAlreadySpawn) MouseDropItem();
    }

    public Vector2[] GetAdjacentLayoutPositions()
    {
        Vector2 currentPos = transform.position;
        var positions = adjacentBlockLayoutOffset.ToArray();
        for (int i = 0; i < positions.Length; ++i)
            positions[i] += currentPos;

        return positions;
    }

    public void SetData(ItemData data) => this.data = data;

    public void ForceGridPlacement() => MouseDropItem();
    
    private void ComputeAdjacentBlocks()
    {
        void AddToAdjacentBlock(Vector2 pos)
        {
            if (blockLayoutOffset.Contains(pos) || adjacentBlockLayoutOffset.Contains(pos))
                return;
            adjacentBlockLayoutOffset.Add(pos);
        }

        Vector2 currentOffset;
        for (int i = 0; i < blockLayoutOffset.Count; ++i)
        {
            currentOffset = blockLayoutOffset[i];
            AddToAdjacentBlock(currentOffset + Vector2.up);
            AddToAdjacentBlock(currentOffset + Vector2.down);
            AddToAdjacentBlock(currentOffset + Vector2.left);
            AddToAdjacentBlock(currentOffset + Vector2.right);
        }
    }

    public void MouseDropItem()
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

        float cellSize = GridManager.Instance.CellSize;
        
        // Check if any targeted Cell is already Blocked
        for (int i = 0; i < blockLayoutOffset.Count; ++i)
        {
            if (!GridManager.Instance.GetAtWorldLocation((Vector3)blockLayoutOffset[i] * cellSize + transform.position).isBlocked)
                continue;

            ReturnToLastPosition();
            return;
        }

        Vector3 newPos = GridManager.Instance.GetAtWorldLocation(transform.position).worldPosition;
        UpdateBlockCells(newPos);
        
        // Snap Position to Grid
        lastPosition = newPos;
        transform.position = newPos;

        if (!wasPlacedOnce) wasPlacedOnce = true;
    }

    private void ReturnToLastPosition() => transform.position = lastPosition;

    private void UpdateBlockCells(Vector3 newPos)
    {
        float cellSize = GridManager.Instance.CellSize;
        Vector3 offset;

        if (wasPlacedOnce) // Only if Item was Blocking Grid cells
        {
            // Empty All old positions
            for (int i = 0; i < blockLayoutOffset.Count; ++i)
            {
                offset = blockLayoutOffset[i] * cellSize;
                GridManager.Instance.UpdateGridAtWorldPosition(lastPosition + offset, GridInformation.GridType.Empty);
            }
        }

        // Update New positions
        for (int i = 0; i < blockLayoutOffset.Count; ++i)
        {
            offset = blockLayoutOffset[i] * cellSize;
            GridManager.Instance.UpdateGridAtWorldPosition(newPos + offset, GridInformation.GridType.Item);
        }
    }

    public void ItemBought()
    {
        float cellSize = GridManager.Instance.CellSize;
        Vector3 offset;

        // Empty All old positions
        for (int i = 0; i < blockLayoutOffset.Count; ++i)
        {
            offset = blockLayoutOffset[i] * cellSize;
            GridManager.Instance.UpdateGridAtWorldPosition(lastPosition + offset, GridInformation.GridType.Empty);
        }
    }
}
