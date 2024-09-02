using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "inventory", menuName = "SO/Items/Inventory")]
public class PlayerInventoryRS : RuntimeSet<InventoryItemRI>
{
    public bool Reset;
    
    private void OnDisable()
    {
        if (Reset) Items.Clear();
    }
}
