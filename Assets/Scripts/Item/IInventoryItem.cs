using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class InventoryItem : ScriptableObject, IVisitable
{
    [field: SerializeField] public Texture2D Icon { get; protected set; }
    [field: SerializeField] public string Name { get; protected set; }
    public bool AutoEquip;
    public abstract void RegisterCallback(VisualElement button);
    
    public virtual void Accept(IVisitor visitor)
    {
    }

    public void Accept(IStatsVisitor visitor)
    {
        
    }
}
