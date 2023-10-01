using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemData
{
    public string Label;
    [TextArea] public string Description;
    public Sprite Sprite;
    public ItemLayout Layout;

    public ItemData(List<Vector2> layout)
    {
        Label = "Item Name";
        Description = "...";
        Sprite = null;
        Layout = new ItemLayout(layout);
    }
}

[Serializable]
public struct ItemLayout
{
    [SerializeField] private List<Vector2> positions;
    public List<Vector2> Positions { get => positions; }

    public ItemLayout(List<Vector2> content)
    {
        positions = content;
    }

    public bool Contains(Vector2 position) => positions.Contains(position);
    public void AddCenterBlock() => positions.Add(Vector2.zero);
}
