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

    protected List<Vector2Int> blockLayoutOffset;

    public Vector2 BlockLayoutSize { get; private set; }
    public Vector2 BlockPivotOffset { get; private set; }

    protected virtual void Start()
    {
        ComputeBlockLayout();
        if (pickable)
            OnDrag.AddListener(() => MoveTo(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
    }

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

        BlockPivotOffset = (botLeft + topRight) / 2f;

        botLeft.x = Mathf.Abs(botLeft.x);
        botLeft.y = Mathf.Abs(botLeft.y);
        BlockLayoutSize = botLeft + topRight + Vector2.one;

        if (GridManager.Instance == null)
            return;
        
        BlockPivotOffset *= GridManager.Instance.CellSize;
        BlockLayoutSize *= GridManager.Instance.CellSize;
    }

    private void OnDestroy()
    {
        OnClick.RemoveAllListeners();
        OnDrag.RemoveAllListeners();
        OnRelease.RemoveAllListeners();
    }
}
