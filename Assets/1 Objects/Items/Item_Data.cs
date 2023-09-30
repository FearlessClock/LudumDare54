using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Item_Data
{
    public string Label;
    public string Description;
    public Sprite Sprite;
    public List<Vector2> ShapeLayout;
}
