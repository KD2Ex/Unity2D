using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerInventory : ScriptableObject
{
    [SerializeField] private List<InventoryItem> items = new();

    public List<InventoryItem> Items => items;
    
    public void Add(InventoryItem item)
    {
        items.Add(item);
    }

    public void Remove(InventoryItem item)
    {
        if (items.Contains(item)) items.Remove(item);
    }
}
