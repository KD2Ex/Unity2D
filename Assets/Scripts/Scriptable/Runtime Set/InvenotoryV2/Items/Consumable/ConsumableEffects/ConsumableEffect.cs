using UnityEngine;
using UnityEngine.Events;

public abstract class ConsumableEffect : ScriptableObject
{
    public abstract void Execute();
    public UnityEvent OnUse;
}