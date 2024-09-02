using System.Collections;
using Structs;
using UnityEngine;

namespace Interfaces
{
    public interface IDamageable
    {
        HealthComponent Health { get;  set; }

        void TakeDamage(DamageMessage message);

        void Die();
    }
}