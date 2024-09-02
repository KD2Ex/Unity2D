using System;
using UnityEngine;

public class PickupRI : MonoBehaviour
{
    [SerializeField] private InventoryItemRI item;
    [SerializeField] private int amount;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (item.Stackable)
        {
            if (!item.set.Items.Contains(item))
            {
                item.set.Add(item);
            }

            item.Amount += amount;
        }
        else
        {
            item.set.Add(item);
        }
        gameObject.SetActive(false);
    }
}