using Interfaces;
using Structs;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float stunValue;
    [SerializeField] private float knockbackForce;

    private MeleeWeapon owner;
    
    private void Awake()
    {
        owner = GetComponentInParent<MeleeWeapon>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if ((_owner.layer.value & (1 << other.gameObject.layer)) == 0) return;
        if (!other.CompareTag(owner.EnemyTag)) return;
        
        Debug.Log("hited");
        
        other.TryGetComponent<IDamageable>(out var enemy);
        if (enemy == null) return;
        
        DealDamage(enemy, other.transform.position);
    }

    private void DealDamage(IDamageable enemy, Vector2 position)
    {
        owner.OnDamaging.Invoke();
        
        var dir = (position - (Vector2)owner.transform.position).normalized;

        DamageMessage message = new DamageMessage(
            damage,
            stunValue,
            knockbackForce,
            dir);
            
        enemy.TakeDamage(message);
    }
}
