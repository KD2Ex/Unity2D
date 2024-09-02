using Interfaces;
using Structs;
using UnityEngine;

public class DamageblePart : MonoBehaviour
{
    [SerializeField] private Transform parent;
    private IDamageable damageable;
    
    private void Awake()
    {
        damageable = parent.GetComponent<IDamageable>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent<IDamageDealer>(out var dd);

        if (dd == null) return;

        Debug.Log(name + " " + dd.Damage);

        Vector2 direction = (other.transform.position - transform.position).normalized;

        var message = new DamageMessage(
            dd.Damage,
            dd.Stun,
            dd.Knockback,
            direction);
        
        damageable.TakeDamage(message);
    }
}
