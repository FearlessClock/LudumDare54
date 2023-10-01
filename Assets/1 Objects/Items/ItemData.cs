using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemData
{
    public string Label;
    public string Description;
    public Sprite Sprite;
    public ItemLayout layout;
    [Space(50)]public List<Vector2> ShapeLayout;
}

[Serializable]
public struct ItemLayout
{
    public List<Vector2> positions;
}
