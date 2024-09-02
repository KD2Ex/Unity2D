using System;
using System.Collections.Generic;
using MEC;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "SO/Character/Ability/Dash")]
public class DashAbilityStrategy : AbilityStrategy
{
    private Rigidbody2D rb;
    public float force;
    public float dashTime;
    public AnimationCurve curve;
    
    private Vector2 definedDirection = Vector2.zero;
    private bool running;
    private Vector2 position => rb.position;
    
    public override void Execute(Transform origin)
    {
        
    }

    public override void Execute(Rigidbody2D rb)
    {
        //Timing.KillCoroutines(Timing.CurrentCoroutine);
        
        OnStart?.Invoke();
        this.rb = rb;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction;
        if (definedDirection == Vector2.zero)
        {
            direction = ((Vector2)mousePos - position).normalized;
        }
        else
        {
            direction = definedDirection;
        }
        Timing.RunCoroutine(Dash(direction));
    }

    public override AbilityStrategy WithDirection(Vector2 direction)
    {
        definedDirection = direction;
        return this;
    }

    public override AbilityStrategy WithOffset(Vector2 offset)
    {
        return this;
    }

    private IEnumerator<float> Dash(Vector2 direction)
    {
        running = true;
        var time = 0f;
        FrameTime = 0f;

        var curveTime = curve[curve.length - 1].time;

        Debug.Log(curveTime);
        
        while (time < dashTime)
        {
            time += Time.deltaTime;
            
            //var curveValue = curve.Evaluate((curveTime * time) / dashTime);
            var targetPosition = position + (direction * (force * Time.deltaTime));
            rb.MovePosition(targetPosition);

            FrameTime += Time.deltaTime;
            
            yield return Timing.WaitForOneFrame;
        }

        definedDirection = Vector2.zero;
        FrameTime = -1f;

        OnStop?.Invoke();
        running = false;
    }
}