using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile Ability", menuName = "SO/Character/Ability/Projectile")] 
public class ProjectileAbility : AbilityStrategy
{
    public RangedWeaponData weapon;
    
    /*public GameObject projectile;
    public float speed;
    public float sizeScale;*/
    private GameObject projectile => weapon.Projectile.gameObject;
    private float sizeScale => weapon.SizeScale;
    private float speed => weapon.Speed;
    private float fireRate => weapon.FireRate;
    
    private Vector2 definedDirection;
    private Vector2 offset;

    private bool isReady = true;

    private void OnDisable()
    {
        weapon = null;
    }

    public override void Execute(Transform origin)
    {
        if (!weapon) return;
        if (!isReady) return;
        
        var position = offset != Vector2.zero 
            ? (Vector2) origin.position + offset 
            : (Vector2) origin.position; 
        
        var proj = Instantiate(projectile, position, Quaternion.identity);
        proj.transform.localScale *= sizeScale;

        ProjectileMover mover = proj.AddComponent<ProjectileMover>();
        mover.Initialize(speed * Vector3.Normalize(definedDirection));

        
        Timing.RunCoroutine(AbilityCooldown());
    }

    public override AbilityStrategy WithDirection(Vector2 direction)
    {
        definedDirection = direction;
        return this;
    }

    public override AbilityStrategy WithOffset(Vector2 offset)
    {
        this.offset = offset;
        return this;
    }

    private IEnumerator<float> AbilityCooldown()
    {
        isReady = false;
        yield return Timing.WaitForSeconds(1 / fireRate);
        isReady = true;
    }
}
