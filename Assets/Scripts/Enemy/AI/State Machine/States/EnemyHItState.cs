using UnityEngine;

public class EnemyHItState : EnemyBaseState
{
    public EnemyHItState(Enemy enemy, Animator animator) : base(enemy, animator)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetTrigger("Hit");
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