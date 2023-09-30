using UnityEngine;

public static class ExtensionVector3
{
    public static Vector3 FlattenVector(this Vector3 vec)
    {
        return new Vector3(vec.x, vec.y, 0);
    }
}
