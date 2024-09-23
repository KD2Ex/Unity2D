using UnityEngine;

[CreateAssetMenu]
public class RangedWeaponData : ScriptableObject
{ 
    public Projectile Projectile;
    public float FireRate;
    public float Speed;
    public float SizeScale;

    public int Ammo;

    private int currentAmmo;
    public int CurrentAmmo
    {
        get => currentAmmo;
        set
        {
            currentAmmo = value;
            if (currentAmmo > Ammo) currentAmmo = Ammo;
        }
    }

    //ammo type
}