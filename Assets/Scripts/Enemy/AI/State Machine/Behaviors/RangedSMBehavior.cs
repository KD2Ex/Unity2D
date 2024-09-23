using UnityEngine;

public class RangedSMBehavior : StateBehaviour
{
    [SerializeField] private EnemyRangedWeapon ranged;
    
    protected override void InitializeBehavior()
    {
        Debug.Log(name);
        Debug.Log(dashTime);
        Debug.Log(dashCooldown);

        //ranged = ScriptableObject.CreateInstance<ProjectileAbility>();
        
        var idleState = new EnemyIdleState(enemy, enemy.Animator);
        var hitState = new EnemyHItState(enemy, enemy.Animator);
        var dashState = new EnemyDashState(enemy, enemy.Animator, playerTransform, this);
        var rangedAttackState = new EnemyRangedAttackState(enemy, enemy.Animator, playerTransform, ranged);
        
        At(idleState, dashState, new FuncPredicate(() => distanceToPlayer < 5f && isDashReady));
        At(dashState, idleState, new FuncPredicate(() => !isDashRunning));
        At(hitState, idleState, new ActionPredicate(() => !enemy.IsBeingKnocked, () => startAttack = false));
        AtAny(hitState, new FuncPredicate(() => enemy.IsBeingKnocked));
        
        At(idleState, rangedAttackState, new FuncPredicate(() => distanceToPlayer < 10f && ranged.IsReady));
        At(rangedAttackState, idleState, new FuncPredicate(() => !ranged.IsReady));
        stateMachine.SetState(idleState);
    }
}