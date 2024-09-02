using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Structs;
using UnityEngine;

public class DamageableTEST : MonoBehaviour, IDamageable
{
    public HealthComponent Health { get; set; }
    public void TakeDamage(DamageMessage message)
    {
        Debug.Log("damage taken");
    }

    public void Die()
    {
    }
}
