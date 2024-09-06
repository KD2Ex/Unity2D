using UnityEngine;

public static class MathUtils
{
    public static Vector2 GetPointOnCircle(Vector2 origin, float radius, float angle)
    {
        float x = origin.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = origin.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        var position = new Vector2(x, y);

        return position;
    }
}
