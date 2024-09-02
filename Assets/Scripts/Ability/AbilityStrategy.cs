using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbilityStrategy : ScriptableObject
{
    [SerializeField] private float cooldown;
    public float Cooldown => cooldown;
    public Action OnStop;
    public Action OnStart;
    
    public float FrameTime { get; protected set; }
    
    public virtual void Execute(Transform origin, Transform target)
    {
    }

    public abstract void Execute(Transform origin);

    public virtual void Execute(Rigidbody2D rb)
    {
    }

    public abstract AbilityStrategy WithDirection(Vector2 direction);
    public abstract AbilityStrategy WithOffset(Vector2 offset);
}

