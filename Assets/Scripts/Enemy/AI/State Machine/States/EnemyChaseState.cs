using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    private readonly Transform player;
    private float speed;
    
    public EnemyChaseState(Enemy enemy, Animator animator, Transform player, float speed) : base(enemy, animator)
    {
        this.player = player;
        this.speed = speed;

    }
    public override void Enter()
    {
        base.Enter();
        enemy.Agent.speed = speed;
    }

    public override void Update()
    {
        base.Update();
        enemy.Agent.SetDestination((Vector2)player.position);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }
}