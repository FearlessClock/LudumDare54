using UnityEngine;

public static class ExtensionVector3
{
    public static Vector3 FlattenVector(this Vector3 vec)
    {
        return new Vector3(vec.x, vec.y, 0);
    }
}
public static class ExtensionVector2
{
    public static Vector2 IntifyRound(this Vector2 vec)
    {
        return new Vector2(Mathf.RoundToInt(vec.x), Mathf.RoundToInt(vec.y));
    }
}
