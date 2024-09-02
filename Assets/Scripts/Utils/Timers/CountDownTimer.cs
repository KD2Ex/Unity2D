using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CountDownTimer : Timer
{
    public CountDownTimer(float value) : base(value)
    {
    }

    public override void Tick(float deltaTime)
    {

        if (IsRunning && Time > 0)
        {
            Time -= deltaTime;
        }

        if (IsRunning && Time <= 0)
        {
            Stop();
        }
    }

    public bool IsFinished => Time <= 0;

    public void Reset() => Time = initialTime;

    public void Reset(float newValue)
    {
        initialTime = newValue;
        Reset();
    }
}
