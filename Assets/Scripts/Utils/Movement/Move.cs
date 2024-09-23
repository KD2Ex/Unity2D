using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    
    private Rigidbody2D rb;

    private float elapsed = 0f;
    private float speed;
    private float time;
    private Vector2 dir;

    private bool execute;

    public bool IsRunning => execute;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        if (elapsed >= time)
        {
            elapsed = 0f;
            execute = false;
            return;
        }

        if (execute)
        {
            Execute();
        }
        
    }
    
    public void BySeconds(Vector2 dir, float speed, float time)
    {
        this.speed = speed;
        this.time = time;
        this.dir = dir;

        execute = true;
    }

    private void Execute()
    {
        elapsed += Time.deltaTime;
        //time * elapsed / totalTime
        var curveTime = curve[1].time;

        var eval = (curveTime * elapsed) / time;
        
        var totalSpeed = speed * curve.Evaluate(eval) * Time.deltaTime;
        rb.MovePosition(rb.position + dir * totalSpeed);
    }
}
