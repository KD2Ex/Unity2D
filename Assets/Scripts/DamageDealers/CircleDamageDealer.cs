using System;
using Interfaces;
using UnityEngine;

public class CircleDamageDealer : MonoBehaviour, IDamageDealer
{
    [SerializeField] private float _damage;
    [SerializeField] private float _stun;
    [SerializeField] private float _knockback;

    [SerializeField] private float radius;
    
    private CircleCollider2D range;
    
    private void Awake()
    {
        range = GetComponent<CircleCollider2D>();

        range.radius = radius;
    }

    float IDamageDealer.Damage
    {
        get => _damage;
        set => _damage = value;
    }

    float IDamageDealer.Stun
    {
        get => _stun;
        set => _stun = value;
    }

    float IDamageDealer.Knockback
    {
        get => _knockback;
        set => _knockback = value;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
