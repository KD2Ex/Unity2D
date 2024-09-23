using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Cooldown;

public class EnemyRangedWeapon : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float cooldown;

    public bool IsReady { get; private set; } = true;
    
    public void Execute(Vector2 direction)
    {
        var angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.Euler(0f,0f, angle);
        
        var instance = Instantiate(projectile, (Vector2)transform.position, Quaternion.identity);

        var mover = instance.AddComponent<ProjectileMover>();
        mover.Initialize(20f * Vector3.Normalize(direction));

        StartCoroutine(Cooldown.Start(cooldown, (value) => IsReady = value));
    }
}
