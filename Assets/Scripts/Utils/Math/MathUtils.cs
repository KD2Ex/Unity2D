using UnityEngine;

public static class MathUtils
{
    public static Vector2 GetPointOnCircle(Vector2 offset, float radius, float angle)
    {
        float x = offset.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = offset.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        var position = new Vector2(x, y);

        return position;
    }
}
