using UnityEngine;

public class EnemyRangedAttackState : EnemyBaseState
{
    private Transform player;
    private EnemyRangedWeapon ranged;
    private Vector2 direction;
    
    public EnemyRangedAttackState(Enemy enemy, Animator animator, Transform player, EnemyRangedWeapon ranged) : base(enemy, animator)
    {
        this.player = player;
        this.ranged = ranged;
    }

    public override void Enter()
    {
        base.Enter();

        direction = (player.position - enemy.transform.position).normalized;
        
        ranged.Execute(direction);
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