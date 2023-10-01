using Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
{
    /// <summary>
    /// Triggered On Mouse Left Button Down
    /// </summary>
    public UnityEvent OnClick;
    /// <summary>
    /// Triggered On Mouse Left Button Hold
    /// </summary>
    public UnityEvent OnDrag;
    /// <summary>
    /// Triggered On Mouse Left Button Up
    /// </summary>
    public UnityEvent OnRelease;

    [SerializeField] private bool pickable = true;

    protected List<Vector2> blockLayoutOffset = new List<Vector2>();

    /// <summary>
    /// Distance from pivot to Lowest corner position
    /// </summary>
    protected Vector3 BottomLeftBound;
    /// <summary>
    /// Distance from pivot to Greatest corner position
    /// </summary>
    protected Vector3 TopRightBound;

    public Vector2 BlockLayoutSize { get; private set; }
    public Vector2 BlockPivotOffset { get; private set; }

    private void OnMouseDown()
    {
        OnClick?.Invoke();
    }

    private void OnMouseDrag()
    {
        OnDrag?.Invoke();
    }

    private void OnMouseUp()
    {
        OnRelease?.Invoke();
    }
    public virtual void Init()
    {
        ComputeBlockLayout();
        if (pickable)
            OnDrag.AddListener(() => MoveTo(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
    }

    public Vector2[] GetLayoutPositions()
    {
        Vector2 currentPos = transform.position;
        Vector2[] layoutPosition = blockLayoutOffset.ToArray();
        for (int i = 0; i < layoutPosition.Length; i++)
            layoutPosition[i] += currentPos;
        
        return layoutPosition; 
    }

    public void MoveTo(Vector2 pos)
    {
        transform.position = pos;
    }

    private void ComputeBlockLayout()
    {
        Vector2 offset;
        Vector2 botLeft = Vector2.zero;
        Vector2 topRight = Vector2.zero;
        for (int i = 0; i < blockLayoutOffset.Count; ++i)
        {
            offset = blockLayoutOffset[i];
            if (offset.x < botLeft.x) botLeft.x = offset.x;
            if (offset.y < botLeft.y) botLeft.y = offset.y;
            if (offset.x > topRight.x) topRight.x = offset.x;
            if (offset.y > topRight.y) topRight.y = offset.y;
        }

        // Set Bounds and Pivot
        BottomLeftBound = botLeft;
        TopRightBound = topRight;
        BlockPivotOffset = (botLeft + topRight) / 2f;

        // Absolute the negative values to compute the full size
        botLeft.x = Mathf.Abs(botLeft.x);
        botLeft.y = Mathf.Abs(botLeft.y);
        BlockLayoutSize = botLeft + topRight + Vector2.one;

        if (GridManager.Instance == null)
            return;

        // Take into account the size of a grid cell
        float cellSize = GridManager.Instance.CellSize;
        BottomLeftBound *= cellSize;
        TopRightBound *= cellSize;
        BlockPivotOffset *= cellSize;
        BlockLayoutSize *= cellSize;
    }

    private void OnDestroy()
    {
        OnClick.RemoveAllListeners();
        OnDrag.RemoveAllListeners();
        OnRelease.RemoveAllListeners();
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        float size = GridManager.Instance.CellSize;
        Gizmos.color = Color.cyan;
        for (int i = 0; i < blockLayoutOffset.Count; ++i)
            Gizmos.DrawWireCube(blockLayoutOffset[i] * size + (Vector2)transform.position, Vector2.one * (size - .2f));
    }
}
