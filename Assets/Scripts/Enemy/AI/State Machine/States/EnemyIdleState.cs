using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(Enemy enemy, Animator animator) : base(enemy, animator)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.Agent.ResetPath();
    }

    public override void Update()
    {
        base.Update();
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