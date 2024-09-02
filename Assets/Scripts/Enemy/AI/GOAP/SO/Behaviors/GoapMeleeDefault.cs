using UnityEngine;

public class GoapMeleeDefault : GoapAgent
{
    
    [Header("Sensor")] 
    [SerializeField] private Sensor chaseSensor;
    [SerializeField] private Sensor attackSensor;
 
    [SerializeField] private MeleeWeapon _melee;
    
    private void OnEnable()
    {
        chaseSensor.OnTargetChanged += HandleTargetChange;
    }

    private void OnDisable()
    {
        chaseSensor.OnTargetChanged -= HandleTargetChange;
    }
    
    protected override void SetupBeliefs()
    {
        base.SetupBeliefs();
        
        beliefFactory.AddBelief("Nothing", () => false);
        beliefFactory.AddBelief("AgentMoving", () => _navMeshAgent.hasPath);
        
        beliefFactory.AddBelief("AgentStunned", () => _stunController.IsStunned);
        beliefFactory.AddBelief("AgentNotStunned", () => !_stunController.IsStunned);
        beliefFactory.AddBelief("AgentAttackReady", () => false);
        
        beliefFactory.AddSensorBeliefs("PlayerInChaseRange", chaseSensor);
        beliefFactory.AddSensorBeliefs("PlayerInAttackRange", attackSensor);
        beliefFactory.AddBelief("AttackingPlayer", () => false);
    }

    protected override void SetupActions()
    {
        base.SetupActions();
        
        actions.Add(new AgentAction.Builder("Relax")
            .WithStrategy(new IdleStrategy(1))
            .AddEffect(beliefs["Nothing"])
            .Build());
        
        actions.Add(new AgentAction.Builder("Wander Around")
            .WithStrategy(new WanderStrategy(_navMeshAgent, 5f))
            .AddPrecondition(beliefs["AgentNotStunned"])
            .AddEffect(beliefs["AgentMoving"])
            .Build());
        
        actions.Add(new AgentAction.Builder("Chase")
            .WithStrategy(new MoveStrategyNM(_navMeshAgent, () => beliefs["PlayerInChaseRange"].Location))
            .AddPrecondition(beliefs["PlayerInChaseRange"])
            .AddPrecondition(beliefs["AgentNotStunned"])
            .AddEffect(beliefs["PlayerInAttackRange"])
            .Build());
        
        actions.Add(new AgentAction.Builder("Attack")
            .WithStrategy(new GOAPAttackStrategy(_animation, _animation.attackClip, _melee, attackSensor, _rb))
            .AddPrecondition(beliefs["PlayerInAttackRange"])
            .AddPrecondition(beliefs["AgentNotStunned"])
            .AddEffect(beliefs["AttackingPlayer"])
            .Build());
    }

    protected override void SetupGoals()
    {
        base.SetupGoals();
        
        goals.Add(new AgentGoal.Builder("CHillout")
            .WithPriority(1)
            .WithDesiredEffect(beliefs["Nothing"])
            .Build());
        
        goals.Add(new AgentGoal.Builder("Wander")
            .WithPriority(1)
            .WithDesiredEffect(beliefs["AgentMoving"])
            .Build());
        
        goals.Add(new AgentGoal.Builder("SeekAndDestroy")
            .WithPriority(2)
            .WithDesiredEffect(beliefs["AttackingPlayer"])
            .Build());
    }
}
