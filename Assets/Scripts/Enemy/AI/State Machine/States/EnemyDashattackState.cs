using Unity.VisualScripting;
using UnityEngine;

public class EnemyDashattackState : EnemyBaseState
{
    private float time;
    private float speed;
    private Transform player;

    private Vector2 direction;
    
    public EnemyDashattackState(Enemy enemy, Animator animator, Transform player, float speed, float time) : base(enemy, animator)
    {
        this.speed = speed;
        this.time = time;
        this.player = player;
    }

    public override void Enter()
    {
        base.Enter();
        direction = (player.position - enemy.transform.position).normalized;

        enemy.Agent.ResetPath();
        enemy.Behaviour.StartDash();

        enemy.Melee.Attack(direction, 0.5f);
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