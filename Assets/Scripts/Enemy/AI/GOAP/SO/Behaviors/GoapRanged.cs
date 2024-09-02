using UnityEngine;

public class GoapRanged : GoapAgent
{
    [Header("Sensor")] 
    [SerializeField] private Sensor attackSensor;
    [SerializeField] private Sensor retreatSensor;

    [SerializeField] private Ranged _ranged;
    [SerializeField] private RaycastDetection _detection;
    private bool IsAttackReady => _ranged.IsReady;
    public bool movedRecently;

    
    private CountDownTimer _timer;
    
    private void OnEnable()
    {
        retreatSensor.OnTargetChanged += HandleTargetChange;
    }

    private void OnDisable()
    {
        retreatSensor.OnTargetChanged -= HandleTargetChange;
    }
    
    protected override void SetupBeliefs()
    {
        base.SetupBeliefs();
        
        beliefFactory.AddBelief("Nothing", () => false);
        beliefFactory.AddBelief("AgentMoving", () => _navMeshAgent.hasPath);
        
        beliefFactory.AddBelief("AgentStunned", () => _stunController.IsStunned);
        beliefFactory.AddBelief("AgentNotStunned", () => !_stunController.IsStunned);
        beliefFactory.AddBelief("AgentAttackReady", () => IsAttackReady);
        
        beliefFactory.AddSensorBeliefs("PlayerInRetreatRange", retreatSensor);
        beliefFactory.AddSensorBeliefs("PlayerInAttackRange", attackSensor);
        beliefFactory.AddBelief("AttackingPlayer", () => false);
        beliefFactory.AddBelief("KeepDistance", () => false);
        beliefFactory.AddBelief("KeepStrafing", () => movedRecently);
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

        actions.Add(new AgentAction.Builder("Reload")
            .WithStrategy(new IdleStrategy(.5f))
            .AddEffect(beliefs["AgentAttackReady"])
            .Build());
        
        actions.Add(new AgentAction.Builder("Attack")
            .WithStrategy(new RangedStrategy(_ranged, attackSensor, _rb))
            .AddPrecondition(beliefs["PlayerInAttackRange"])
            .AddPrecondition(beliefs["AgentNotStunned"])
            .AddPrecondition(beliefs["AgentAttackReady"])
            .AddEffect(beliefs["AttackingPlayer"])
            .Build());

      
        
        /*actions.Add(new AgentAction.Builder("Strafe")
            .WithStrategy(new MoveStrategyNM(_navMeshAgent, () => _detection.GetRetreatDirection() * 2f, () => movedRecently = true))
            .AddPrecondition(beliefs["AgentNotStunned"])
            .AddEffect(beliefs["KeepStrafing"])
            .Build());*/
      
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
            .WithPriority(3)
            .WithDesiredEffect(beliefs["AttackingPlayer"])
            .Build());
        
        goals.Add(new AgentGoal.Builder("Dash away")
            .WithPriority(4)
            .WithDesiredEffect(beliefs["KeepDistance"])
            .Build());
        
        /*goals.Add(new AgentGoal.Builder("Strafe")
            .WithPriority(4)
            .WithDesiredEffect(beliefs["KeepStrafing"])
            .Build());*/
        
        goals.Add(new AgentGoal.Builder("Reload")
            .WithPriority(2)
            .WithDesiredEffect(beliefs["AgentAttackReady"])
            .Build());
    }

    protected override void SetupTimers()
    {
        MovedRecentlyTimer();
        _timers.Add(_timer);
        
        base.SetupTimers();
    }

    private void MovedRecentlyTimer()
    {
        _timer = new CountDownTimer(1.5f);

        _timer.OnTimerStart += () => Debug.Log("movedRecently timer");
        _timer.OnTimerStop += () =>
        {
            movedRecently = false;
            _timer.Start();
        };
        
        _timer.Start();
    }
}
