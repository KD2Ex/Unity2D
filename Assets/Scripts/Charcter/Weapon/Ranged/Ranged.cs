using System;
using UnityEngine;

public class Ranged : DirectionalWeapon
{
    [SerializeField] private Transform parent;
    [SerializeField] private Transform lookAt;

    [SerializeField] private ProjectileAbility projectileSpawn;
    [SerializeField] private RangedInventory inventory;

    [SerializeField] private AudioClip attackSound;

    [SerializeField] private int ammoGeneratingThreshold;
    [SerializeField] private bool infiniteAmmo;

    private int ammoGeneratingProgress;
    
    private RangedWeaponData WeaponData => inventory.Equipped.Data;
    private Vector3 Pos => transform.position;
    public bool IsReady { get; private set; } = true;
    
    

    private void Equip()
    {
        Debug.Log(inventory.Equipped);

        if (inventory.Equipped)
        {
            projectileSpawn.weapon = WeaponData;
        }
        else
        {
            projectileSpawn.weapon = null;
        }
    }

    private void Awake()
    {
        WeaponData.CurrentAmmo = WeaponData.Ammo;
    }

    private void Start()
    {
        ammoGeneratingProgress = 0;
        Equip();
        
        if (lookAt == null)
        {
            lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        projectileSpawn.weapon = WeaponData;
    }

    private void FixedUpdate()
    {
        var dir = lookAt.position - Pos;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        parent.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void OnAim(bool active)
    {
        gameObject.SetActive(active);
    }
    
    public override void Attack(Vector2 dir, float cooldown = .5f)
    {
        Debug.Log(dir);
        RotateTowardsAttackDirection(dir);
        
        if (WeaponData.CurrentAmmo <= 0)
        {
            // play sound
            return;
        }


        if (!infiniteAmmo)
        {
            WeaponData.CurrentAmmo -= 1;
        }
        
        
        projectileSpawn
            .WithDirection(dir)
            .Execute(transform);
        
        SoundManager.instance.PlayClip(attackSound, transform);
    }

    private void RotateTowardsAttackDirection(Vector2 direction)
    {
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        parent.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    
    public void GenerateAmmo()
    {
        ammoGeneratingProgress++;

        if (ammoGeneratingProgress >= ammoGeneratingThreshold)
        {
            ammoGeneratingProgress = 0;
            WeaponData.CurrentAmmo++;
        }
    }
}
