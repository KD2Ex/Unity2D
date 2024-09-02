using System.Collections;
using UnityEngine;

public abstract class WorldEvent : ScriptableObject
{
    public bool InProgress { get; protected set; }
    public bool Finished { get; protected set; }
    
    private Transform origin;
    protected Vector2 Pos => origin.position;
    
    public void Initialize(Transform origin)
    {
        InProgress = false;
        Finished = false;
        
        this.origin = origin;
    }
    public abstract IEnumerator Event();
}
