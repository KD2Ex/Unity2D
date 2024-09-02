using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RangedInventory : ScriptableObject
{
    public List<RangedRI> items;
    public bool Reset;
    public RangedRI Equipped;

    private void OnEnable()
    {
        if (Reset)
        {
            Equipped = null;
            items.Clear();
        }
    }
    
}
