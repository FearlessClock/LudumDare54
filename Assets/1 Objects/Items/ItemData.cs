using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemData
{
    public string Label;
    public string Description;
    public Sprite Sprite;
    public List<Vector2> ShapeLayout;
}
