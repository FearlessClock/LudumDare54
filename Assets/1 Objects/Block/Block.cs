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

    private void Start()
    {
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

    private void OnDestroy()
    {
        OnClick.RemoveAllListeners();
        OnDrag.RemoveAllListeners();
        OnRelease.RemoveAllListeners();
    }
}
