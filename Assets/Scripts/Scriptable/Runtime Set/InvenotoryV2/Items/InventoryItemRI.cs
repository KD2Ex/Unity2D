using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class InventoryItemRI : ScriptableObject
{
    public Texture2D Icon;
    public string Name;
    //public ItemType Type;

    public int Amount;
    [FormerlySerializedAs("ShowAmount")] public bool Stackable;

    public PlayerInventoryRS set;

    private void OnDisable()
    {
        Amount = 0;
    }

    public virtual void OnClick(ClickEvent e)
    {
        
    }
    
    public virtual void Drop()
    {
        set.Remove(this);
    }
}
