using System;
using Interfaces;
using Structs;
using UnityEngine;

public class Projectile : MonoBehaviour, IDamageDealer
{

    [SerializeField] private string Tag;
    
    [Header("DD Stats")]
    [SerializeField] private float damage;
    [SerializeField] private float stun;
    [SerializeField] private float knockback;

    float IDamageDealer.Damage
    {
        get => damage;
        set => damage = value;
    }

    float IDamageDealer.Stun
    {
        get => stun;
        set => stun = value;
    }

    float IDamageDealer.Knockback
    {
        get => knockback;
        set => knockback = value;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag(Tag)) return;
        
        other.gameObject.TryGetComponent<IDamageable>(out var enemy);
        var direction = (other.transform.position - transform.position).normalized;
        
        var damageMessage = new DamageMessage(
            damage,
            stun,
            knockback,
            direction);

        enemy.TakeDamage(damageMessage);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
