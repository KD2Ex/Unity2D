using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

public static class MoveToPoint
{
    public static IEnumerator<float> ExecuteTiming(Rigidbody2D rb, Vector2 dir, float speed, float time)
    {
        var elapsed = 0f;

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            rb.MovePosition(rb.position + dir * (speed * Time.deltaTime));
            yield return Timing.WaitForOneFrame;
        }
    }


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

    public static IEnumerator Translate(Transform origin, Vector2 dir, float speed, float time)
    {
        var elapsed = 0f;

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            var originPosition = (Vector2) origin.position;
            
            origin.Translate(originPosition + dir * (speed * Time.deltaTime));
            yield return null;
        }
    }
    
}
