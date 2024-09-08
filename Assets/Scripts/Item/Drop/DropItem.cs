using System;
using UnityEngine;

[Serializable]
public class DropItem
{
    public DropItem(GameObject item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    [field: SerializeField] public GameObject item { get; private set; }
    [field: SerializeField] public int amount { get; private set; }
    
}