using UnityEngine;

public static class ExtensionMethods
{
    public static Vector2 AsVector2(this Vector3 _v)
    {
        return new Vector2(_v.x, _v.y);
    }
}