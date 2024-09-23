using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EnemyDashState : EnemyBaseState
{
    private Transform player;
    private RangedSMBehavior ranged;
    private Vector2 direction;
    
    private const float speed = 15f;
    
    
    public EnemyDashState(Enemy enemy, Animator animator, Transform player, RangedSMBehavior ranged) : base(enemy, animator)
    {
        this.player = player;
        this.ranged = ranged;
    }

    public override void Enter()
    {
        base.Enter();
        direction = (enemy.transform.position - player.transform.position).normalized;
        enemy.Agent.ResetPath();
        enemy.Behaviour.StartDash();
    }

    public override void Update()
    {
        base.Update();
        enemy.Agent.velocity = direction * speed;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.Agent.velocity = Vector2.zero;
        enemy.Behaviour.StartDashCooldown();
    }
}