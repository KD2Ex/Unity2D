using Interfaces;
using UnityEngine;

public class ManaComponent : MonoBehaviour, IVisitable
{
    public float Value;
    
    public void Accept(IVisitor visitor)
    {
        // no op
    }

    public void Accept(IStatsVisitor visitor)
    {
        visitor.Visit(this);
    }
}