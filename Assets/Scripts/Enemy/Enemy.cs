using System.Collections;
using Interfaces;
using Structs;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : BaseDamagableCharacter, IDamageable
{
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip deathSound;
    
    private MeleeWeapon _melee;
    private StunController _stunController;
    private SpriteRenderer _sprite;
    private Rigidbody2D _rb;
    private Animator _animator;
    private AnimationController animationController;
    private NavMeshAgent agent;
    
    private int _flashAmountProperty = Shader.PropertyToID("_Amount");
    private float _flashTime = .3f;
    private bool _isBeingKnocked;
    public HealthComponent Health { get; set; }

    [SerializeField] private UnityEvent OnDeath;
    public UnityEvent<float, float> OnDamage = new ();
    public UnityEvent AfterSpawn;

    private bool isDead;
    public bool IsDead => isDead;

    public bool IsKnockable = true;
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        _melee = GetComponentInChildren<MeleeWeapon>();
        _stunController = GetComponent<StunController>();
        _sprite = GetComponent<SpriteRenderer>();
        //_rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        animationController = GetComponent<AnimationController>();

        Health = GetComponent<HealthComponent>();

        
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
        
        //OnDeath = new UnityEvent<Enemy>();
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {

    }
    
    // Start is called before the first frame update
    void Start()
    {
            
    }

    public void OnSpawnAnimationEnd()
    {
        AfterSpawn.Invoke();
    }
    
    // Update is called once per frame
    void Update()
    {
        animationController.SetSpeed(agent.velocity.magnitude, agent.speed);
        animationController.SetDirection(agent.velocity.normalized);
        if (agent.velocity.magnitude > .1f)
        {
            animationController.SetLastDirection(agent.velocity.normalized);
        }
    }

    public void OnTakingDamage()
    {
        _melee.DisableWeaponCollision();
    }
    
    public void TakeDamage(DamageMessage message)
    {
        if (Health.Value <= 0) return;
        
        // Update health bar
        OnDamage?.Invoke(Health.Value - message.Value, Health.MaxValue);
        //healthBar.UpdateBar(Health.Value / Health.MaxValue);

        
        //Sound
        SoundManager.instance.PlayClip(damageSound, transform);
        
        // Flash on damage
        if (_isFlashing) StopCoroutine(nameof(Flash));
        StartCoroutine(Flash(_sprite, _flashAmountProperty, _flashTime));


        if (IsKnockable)
        {
            if (_isBeingKnocked) StopCoroutine(nameof(TakeKnockback));
            StartCoroutine(TakeKnockback(message.KnockbackDirection, message.KnockbackForce));
            _animator.SetTrigger("Hit");
        }

        Health.Add(-message.Value);
        
        
        if (_stunController)
        {
            _stunController.AddValue(message.StunValue);
            if (_stunController._stunValue >= _stunController.StunThreshold)
            {
                _melee?.DisableWeaponCollision();
            }
        }

        if (Health.Value <= 0)
        {
            Die();
        }

    }

    private IEnumerator StopParticles(ParticleSystem particles, float time, float lifeTime)
    {
        yield return new WaitForSeconds(time);
        particles.Pause();
        yield return new WaitForSeconds(lifeTime);
        Destroy(particles.gameObject);
    }


    private IEnumerator TakeKnockback(Vector2 dir, float force)
    {
        _isBeingKnocked = true;
        
        var knockbackTime = .25f;
        
        agent.velocity = dir * (force);
      
        yield return new WaitForSeconds(knockbackTime);
        //_rb.velocity = Vector2.zero;
        _isBeingKnocked = false;
        _animator.ResetTrigger("Hit");
    }
    
    public void Die()
    {
        isDead = true;
        OnDeath?.Invoke();
        
        DisableWeaponCollision();
        
        SoundManager.instance.PlayClip(deathSound, transform);
        _animator.SetTrigger("Death");

        agent.ResetPath();

        StartCoroutine(DestroyCorpse(0f));
    }

    private IEnumerator DestroyCorpse(float sec)
    {
        yield return new WaitForSeconds(sec);
        gameObject.SetActive(false);
    }

    public void EnableWeaponCollision()
    {
        if (isDead) return;
        
        _melee.EnableWeaponCollision();
    }
    public void DisableWeaponCollision()
    {
        _melee.DisableWeaponCollision();
    }
}
