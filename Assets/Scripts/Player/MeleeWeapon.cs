using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeWeapon : DirectionalWeapon
{
    [SerializeField] private GameObject attack;
    [SerializeField] private string enemyTag;

    public UnityEvent OnDamaging;
    
    private Animator _animator;
    private List<GameObject> _attacks;

    private bool canUse = true;
    private float angle;
    private Vector2 _direction;
    public string EnemyTag => enemyTag;
    
    public bool Attacking { get; private set; }
    
    private void Awake()
    {
        _attacks = new List<GameObject>
        {
            attack
        };
        
        _animator = GetComponentInParent<Animator>();
    }

    private void OnEnable()
    {
        DisableAllAttacks();
    }

    public override void Attack(Vector2 direction, float cooldown)
    {
        if (!canUse) return;
        Debug.Log(OnDamaging.GetPersistentEventCount());

        OnDamaging.Invoke();
        
        Attacking = true;
        _animator.SetTrigger("Attack");
        
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Round(angle / 45) * 45;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        
        _animator.SetFloat("Angle", angle);
        
        //StartCoroutine(Attacking(direction));
    }
    
    public void EnableWeaponCollision()
    {
        canUse = false;
        //Debug.Log(angle);
        
        attack.SetActive(true);
    }
    public void DisableWeaponCollision()
    {
        canUse = true;
        Attacking = false;
        DisableAllAttacks();
        _animator.ResetTrigger("Attack");
        //_maskAnimator.ResetTrigger("Attack");
    }

    private void DisableAllAttacks()
    {
        transform.rotation = Quaternion.identity;
        foreach (var attack in _attacks)
        {
            attack.SetActive(false);
        }
    }
}
