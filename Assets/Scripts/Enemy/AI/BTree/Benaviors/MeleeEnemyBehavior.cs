using System.Collections;
using AI.Btree;
using UnityEngine;
using AttackStrategy = AI.Btree.AttackStrategy;

public class MeleeEnemyBehavior : EnemyBehavior
{
    [SerializeField] private Sensor attackSensor;
    [SerializeField] private Sensor chaseSensor;

    [SerializeField] private MeleeWeapon weapon;
    
    private bool attackReady = true;

    private bool restFromFight;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        var prioritySelector = new PrioritySelector("Prior");

        var chaseSequence = new Sequence("Chase", 10);
        chaseSequence.AddChild(new Leaf("Is not attcking", new Condition(() => !weapon.Attacking)));
        chaseSequence.AddChild(new Leaf("Is player in range", new Condition(() => chaseSensor.IsTargetInRange)));
        chaseSequence.AddChild(new Leaf("Chase", new ActionStrategy(() =>
        {
            if (weapon.Attacking) return;
            agent.SetDestination(chaseSensor.TargetTransform.position);
        })));

        var attackSequence = new Sequence("Attack", 15);
        attackSequence.AddChild(new Leaf("Is Player in range", new Condition(() => attackSensor.IsTargetInRange)));
        attackSequence.AddChild(new Leaf("Attack ready", new Condition(() => attackReady)));
        attackSequence.AddChild(new Leaf("Before attack", new ActionStrategy(() =>
        {
            agent.ResetPath();
            BecomeUnstoppable();
        })));
        attackSequence.AddChild(new Leaf("Attack", new AttackStrategy(weapon, transform, attackSensor)));
        attackSequence.AddChild(new Leaf("cooldown", new ActionStrategy(() =>
        {
            RevertUnstoppable();
            StartCoroutine(AbilityCooldown(.2f, () => attackReady = false, () => attackReady = true));
        })));

        var wanderingSequence = new Sequence("Wandering", 5);
        wanderingSequence.AddChild(new Leaf("Wanders", new Wandering(agent, 1f)));
        
        prioritySelector.AddChild(chaseSequence);
        prioritySelector.AddChild(attackSequence);
        
        
        tree.AddChild(prioritySelector);
    }

    private IEnumerator Attacking()
    {
        yield return new WaitUntil(() => !weapon.Attacking);
    }
    
    // Update is called once per frame
    void Update()
    {
        tree.Process();
    }

    public void BecomeUnstoppable()
    {
        owner.IsKnockable = false;
    }
    
    public void RevertUnstoppable()
    {
        owner.IsKnockable = true;
    }
}
