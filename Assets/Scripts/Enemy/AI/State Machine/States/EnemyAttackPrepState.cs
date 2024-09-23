using UnityEngine;

public class EnemyAttackPrepState : EnemyBaseState
{
    private Transform player;
    private Vector2 direction;
    
    public EnemyAttackPrepState(Enemy enemy, Animator animator, Transform player) : base(enemy, animator)
    {
        this.player = player;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.Agent.ResetPath();
        enemy.IsKnockable = false;
        
        direction = (player.position - enemy.transform.position).normalized;
        animator.SetTrigger("PrepAttack");
    }

    public override void Update()
    {
        base.Update();
        var prepDirection = (player.position - enemy.transform.position).normalized;
        var angle = Mathf.Atan2(prepDirection.y, prepDirection.x) * Mathf.Rad2Deg;
        angle = Mathf.Round(angle / 45) * 45;
        
        animator.SetFloat("Angle", angle);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.IsKnockable = true;
    }
}