using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private KeyItemRI requiredItem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (requiredItem.set.Items.Contains(requiredItem))
        {
            requiredItem.set.Remove(requiredItem);
            gameObject.SetActive(false);
        }
    }
}
