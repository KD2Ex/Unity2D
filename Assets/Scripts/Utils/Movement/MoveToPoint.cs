using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoveToPoint
{
    public static IEnumerator Execute(Rigidbody2D rb, Vector2 dir, float speed, float time)
    {
        var elapsed = 0f;

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            rb.MovePosition(rb.position + dir * (speed * Time.deltaTime));
            yield return null;
        }
    }
}
