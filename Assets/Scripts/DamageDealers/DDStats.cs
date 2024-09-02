using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDStats
{
    public DDStats(float damage, float knockbackForce, float stun)
    {
        Damage = damage;
        KnockbackForce = knockbackForce;
        Stun = stun;
    }

    public float Damage { get; private set; }
    public float KnockbackForce { get; private set; }
    public float Stun { get; private set; }
}
