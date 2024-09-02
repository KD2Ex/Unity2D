using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "SO/Character/Weapon/Melee Attack")]
public class SOMeleeAttack : ScriptableObject
{

    [SerializeField] private float damageValue;
    [SerializeField] private float stunValue;
    [SerializeField] private Vector2 size;
    [SerializeField] private Vector2 offset;

    public void Initialize()
    {
        
    }

    public void DealDamage()
    {
        
    }
}
