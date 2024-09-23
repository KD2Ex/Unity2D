using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class StateBehaviour : MonoBehaviour
{
    protected Enemy enemy;
    private Player player;
    protected StateMachine stateMachine;
    private TextMeshProUGUI text;
    protected Transform playerTransform => player.transform;
    protected float distanceToPlayer => (playerTransform.position - transform.position).magnitude;

    [SerializeField] private float chaseSpeed;
    
    [Header("dash stats")]
    [SerializeField] private float dashSpeed;
    [SerializeField] protected float dashTime;
    [SerializeField] protected float dashCooldown;

    protected bool isDashRunning;
    protected bool isDashReady = true;

    protected bool startAttack;

    public TextMeshProUGUI DebugText => text;
    public UnityEvent<string> OnStateChanged;
    // States

    protected virtual void InitializeBehavior()
    {
        var chaseState = new EnemyChaseState(enemy, enemy.Animator, playerTransform, chaseSpeed);
        var dashattackState = new EnemyDashattackState(enemy, enemy.Animator, playerTransform, 15f, .15f);
        var idleState = new EnemyIdleState(enemy, enemy.Animator);
        var prepState = new EnemyAttackPrepState(enemy, enemy.Animator, playerTransform);
        var hitState = new EnemyHItState(enemy, enemy.Animator);
        
        At(chaseState, prepState, new FuncPredicate(() => distanceToPlayer < 3f && isDashReady));
        At(prepState, dashattackState, new FuncPredicate(() => startAttack));
        At(dashattackState, idleState, new FuncPredicate(() => !isDashRunning));
        
        At(chaseState, idleState, new FuncPredicate(() => distanceToPlayer < 3f && !isDashReady));
        At(idleState, chaseState, new FuncPredicate(() => distanceToPlayer >= 3f));
        At(idleState, prepState, new FuncPredicate(() => isDashReady));
        
        AtAny(hitState, new FuncPredicate(() => enemy.IsBeingKnocked));
        At(hitState, idleState, new ActionPredicate(() => !enemy.IsBeingKnocked, () => startAttack = false));
        
        stateMachine.SetState(chaseState);
    }
    
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Start()
    {

        player = FindObjectOfType<Player>();
        text = FindObjectsOfType<TextMeshProUGUI>().First((item) => item.tag == "DebugText");
        /*Debug.Log(result.Length);

        foreach (var item in result)
        {
            Debug.Log(item.name);
        }*/
        
        Debug.Log(text);
        
        stateMachine = new StateMachine(enemy);
        
        InitializeBehavior();
    }

    private void Update()
    {
        stateMachine.Update();

        Debug.Log("Dash running: " + isDashRunning);
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    protected void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
    protected void AtAny(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

    public void StartAttack() => startAttack = true;
    public void StopAttack() => startAttack = false;
    
    public void StartDash()
    {
        StartCoroutine(Cooldown(dashTime, (value) => isDashRunning = !value));
    }

    public void StartDashCooldown()
    {
        StartCoroutine(Cooldown(dashCooldown, value => isDashReady = value));
    }

    /*
    private IEnumerator Dash()
    {
        
    }*/

    private IEnumerator Cooldown(float time, Action<bool> func)
    {
        Debug.Log(time);
        
        func(false);

        var elapsed = 0f;

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        func(true);
        Debug.Log("Coroutine ended");
    }
    
}