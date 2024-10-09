using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Interfaces;
using Structs;
using UnityEngine;
using UnityEngine.Events;

public class Player : BaseDamagableCharacter, IDamageable
{
    public InputReader inputReader;

    [Header("Cameras")]
    [SerializeField] private CinemachineVirtualCamera mainCamera;
    [SerializeField] private CinemachineVirtualCamera aimCamera;
    
    [Header("Abilities")]
    [SerializeField] private Aiming _aiming;
    [SerializeField] private Ranged _ranged;
    [SerializeField] private Dash dashTest;
    [SerializeField] private List<AbilityStrategy> abilities = new();
    [SerializeField] private Transform crosshair;
    
    [Header("Inventory")] 
    [SerializeField] private PlayerInteractableItem itemInRadius;
    
    private bool dashReady = true;
    private bool invincible;
    
    [Header("VFX")]

    [Header("Events")] public UnityEvent OnDeath;

    private Rigidbody2D _rb;
    private Movement _movementComponent;
    private List<MonoBehaviour> _stateComponents;
    private MeleeWeapon _melee;
    private Animator _animator;
    private SpriteRenderer _sprite;

    private Vector2 lookDirection;
    
    public HealthComponent Health { get; set; }
    
    // [SerializeField] private PlayerInventory inventory;
    // [SerializeField] private RangedInventory rangedInventory;
    
    private int _flashAmountProperty = Shader.PropertyToID("_Amount");
    private float _flashTime = .3f;
    private delegate void AttackAction(Vector2 direction, float cooldown = 0f);
    private AttackAction attackAction;
    
    public bool IsAiming { get; private set; }
    public bool IsDead { get; set; }
    
    private void Awake()
    {
        dashTest = GetComponent<Dash>();
        
        _rb = GetComponent<Rigidbody2D>();
        _movementComponent = GetComponent<Movement>();
        _melee = GetComponentInChildren<MeleeWeapon>();
        _animator = GetComponent<Animator>();

        _sprite = GetComponent<SpriteRenderer>();

        Health = GetComponent<HealthComponent>();
    }

    private void OnEnable()
    {
        inputReader.PrimaryEvent += Attack;
        inputReader.MoveEvent += Move;
        inputReader.InteractEvent += Interact;
        inputReader.DashEvent += Dash;
        inputReader.AimEvent += Aim;
        inputReader.LookEvent += OnLook;
        inputReader.ShootEvent += OnShoot;
        
        attackAction = MeleeAttack;
    }

    private void OnDisable()
    {
        inputReader.PrimaryEvent -= Attack;
        inputReader.MoveEvent -= Move;
        inputReader.InteractEvent -= Interact;
        inputReader.DashEvent -= Dash;
        inputReader.AimEvent -= Aim;
        inputReader.LookEvent -= OnLook;
        inputReader.ShootEvent -= OnShoot;
        
        
        
        Debug.Log("Disable");
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator.SetFloat("Speed", 1f);
        //_maskAnimator.SetFloat("Speed", 1f);
        Application.targetFrameRate = -1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnShoot()
    {
        var dir = (crosshair.position - transform.position).normalized;
        _ranged.Attack(CalculateDirection());
    }
    
    private void OnLook(Vector2 dir)
    {
        lookDirection = dir;
    }
    
    public void OnSpawnAnimationEnd()
    {
        
    }
    
    private void Aim(bool aiming)
    {
        IsAiming = aiming;
        _aiming.OnAim(aiming);
        
        if (!IsAiming)
        {
            aimCamera.m_Priority = 9;

            attackAction = MeleeAttack;
            _ranged.gameObject.SetActive(false);
            return;
        }

        if (!GameController.connected)
        {
            aimCamera.m_Priority = 11;
        }
        
        attackAction = _ranged.Attack;

        _ranged.gameObject.SetActive(true);
        
        // TODO: invoke GameEvent instead of hard referencing _ranged
    }

    private void MeleeAttack(Vector2 direction, float cooldown)
    {
        _melee.Attack(direction, cooldown);
        dashTest.Execute(direction, 10f, .1f);
    }
    
    private void Interact()
    {
        if (itemInRadius.Interactable == null) return;
        
        itemInRadius.Interactable.OnInteraction();
    }

    private Vector2 CalculateDirection()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction;

        if (GameController.connected)
        {
            direction = IsAiming ? lookDirection : _movementComponent.LastMovementDirection;
        }
        else
        {
            direction = (mousePos - transform.position).normalized;
        }

        return direction;
    }

    private Vector2 DetectEnemyInCone(Vector2 initialDirection)
    {
        var closestPoint = Vector2.zero;
        var offset = -45;
        
        
        for (int i = 0; i < 5; i++)
        {
            var hitDirection = Quaternion.AngleAxis(offset + i * 15, Vector3.back) * initialDirection;
            
            var hit = Physics2D.Raycast(transform.position, hitDirection, 10f, LayerMask.NameToLayer("Enemy"));
            if (hit)
            {
                var distance = Vector2.Distance(transform.position, hit.point);

                if (distance < Vector2.Distance(closestPoint, hit.point))
                {
                    closestPoint = hit.point;
                }
            }
        }
        
        return closestPoint;
    }
    
    private void Attack()
    {
        attackAction(CalculateDirection());
    }

    private void Move(Vector2 direction)
    {
        _movementComponent.OnMove(direction);
    }

    private void Dash()
    {
        /*var timing = abilities[0].FrameTime / .8f;
        if (timing is > .3f and < 1f)
        {
            StopCoroutine(Cooldown());
            ExecuteDash();
            return;
        }*/
        if (!dashReady) return;
        
        ExecuteDash();
        
        //curveDash.TriggerAbility(direction);
    }

    private void ExecuteDash()
    {
        /*abilities[0]
            .Execute(_rb);*/
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction;

        if (GameController.connected)
        {
            direction = _movementComponent.LastMovementDirection;
        }
        else
        {
            direction = (mousePos - transform.position).normalized;
        }
        
        dashTest.Execute(direction);
        StartCoroutine(Cooldown(dashTest.Cooldown.Value));
    }
    
    private IEnumerator Cooldown(float s)
    {
        dashReady = false;
        yield return new WaitForSeconds(s);
        dashReady = true;
    }
    
    public void TakeDamage(DamageMessage message)
    {
        if (IsDead) return;
        if (invincible) return;
        if (_isFlashing) StopCoroutine(nameof(Flash));
        StartCoroutine(Flash(_sprite, _flashAmountProperty, _flashTime));
        
        Health.Add(-message.Value);

        if (Health.Value <= 0)
        {
            Die();
            return;
        }
        
        BecomeInvincible(.3f);
    }

    private void BecomeInvincible(float time)
    {
        StartCoroutine(Utils.Cooldown.Cooldown.Start(time, value => invincible = !value));
    }

    public void Die()
    {
        _sprite.sortingLayerName = "Dead";

        OnDeath?.Invoke();
        _animator.SetTrigger("Death");

        _movementComponent.enabled = false;
        _rb.velocity = Vector2.zero;
        enabled = false;
        IsDead = true;
    }
    
    public void EnableWeaponCollision()
    {
        _movementComponent.BlockMovement = true;
        _melee.EnableWeaponCollision();
    }
    public void DisableWeaponCollision()
    {
        _movementComponent.BlockMovement = false;
        _melee.DisableWeaponCollision();
    }
}
