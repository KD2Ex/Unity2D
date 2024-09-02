using System;
using UnityEngine;

public abstract class Timer
{
    protected float initialTime;
    public float Time { get; set; }
    public bool IsRunning { get; protected set; }

    public float Progress => Time / initialTime;
    
    public Action OnTimerStart = delegate {  };
    public Action OnTimerStop = delegate {  };

    protected Timer(float value)
    {
        initialTime = value;
        IsRunning = false;
    }

    public void Start()
    {
        Time = initialTime;
        
        if (IsRunning) return;
        
        IsRunning = true;
        OnTimerStart.Invoke();
    }
    
    public void Stop()
    {
        Debug.Log("Timer stop");

        
        if (!IsRunning) return;
        
        IsRunning = false;
        OnTimerStop.Invoke();
    }

    public void Resume() => IsRunning = true;
    public void Pause() => IsRunning = false;

    public abstract void Tick(float deltaTime);
}
