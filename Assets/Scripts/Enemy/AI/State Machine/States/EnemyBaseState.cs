using UnityEngine;

public abstract class EnemyBaseState : IState
{
    protected Enemy enemy;
    protected Animator animator;
    
    protected EnemyBaseState(Enemy enemy, Animator animator)
    {
        this.enemy = enemy;
        this.animator = animator;
    }

    public virtual void Enter()
    {
    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}