using UnityEngine;

public abstract class DirectionalWeapon : MonoBehaviour
{
    public abstract void Attack(Vector2 direction, float cooldown = .5f);
    
}
