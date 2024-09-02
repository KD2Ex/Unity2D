using UnityEngine;

[CreateAssetMenu(fileName = "Default Behaviour", menuName = "SO/Enemy AI/Goap Behaviour")]
public class GoapDefault : GoapBehavior
{
    public override void SetupBeliefs()
    {
        base.SetupBeliefs();
        
        beliefFactory.AddBelief("Nothing", () => false);
        
        beliefFactory.AddBelief("AgentIdle", () => !agent._navMeshAgent.hasPath);
        beliefFactory.AddBelief("AgentMoving", () => agent._navMeshAgent.hasPath);
        
        /*beliefFactory.AddBelief("AgentIdle", () => _rb.velocity.sqrMagnitude < .01f);
        beliefFactory.AddBelief("AgentMoving", () => _rb.velocity.sqrMagnitude >= .01f);*/
        beliefFactory.AddBelief("AgentLowHealth", () => agent.health < 30);
        beliefFactory.AddBelief("AgentHealthy", () => agent.health >= 50);
        beliefFactory.AddBelief("AgentStunned", () => agent._stunController.IsStunned);
        beliefFactory.AddBelief("AgentNotStunned", () => !agent._stunController.IsStunned);
        beliefFactory.AddBelief("AgentAttackReady", () => false);

        /*
        if (restPos)
        {
            beliefFactory.AddLocationBelief("AgentAtRestingPosition", 3f, restPos);
        }*/
        
        beliefFactory.AddBelief("AttackingPlayer", () => false);
    }

    public override void SetupActions()
    {
        base.SetupActions();
        
        actions.Add(new AgentAction.Builder("Relax")
            .WithStrategy(new IdleStrategy(1))
            .AddEffect(beliefs["Nothing"])
            .Build());
        
        actions.Add(new AgentAction.Builder("Wander Around")
            .WithStrategy(new WanderStrategy(agent._navMeshAgent, 5f))
            //.WithStrategy(new WanderRBStrategy(_rb, 5, 5f))
            .AddPrecondition(beliefs["AgentNotStunned"])
            .AddEffect(beliefs["AgentMoving"])
            .Build());
        
        actions.Add(new AgentAction.Builder("Chase")
            .WithStrategy(new MoveStrategyNM(agent._navMeshAgent, () => beliefs["PlayerInChaseRange"].Location))
            .AddPrecondition(beliefs["PlayerInChaseRange"])
            .AddPrecondition(beliefs["AgentNotStunned"])
            .AddEffect(beliefs["PlayerInAttackRange"])
            .Build());
        
        
    }

    public override void SetupGoals()
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

        /*goals.Add(new AgentGoal.Builder("KeepStaminaUp")
            .WithPriority(2)
            .WithDesiredEffect(beliefs["AgentStaminaHigh"])
            .Build());*/
        goals.Add(new AgentGoal.Builder("SeekAndDestroy")
            .WithPriority(2)
            .WithDesiredEffect(beliefs["AttackingPlayer"])
            .Build());
    }
}
