using System.Collections.Generic;
using UnityEngine;

public class GoapBehavior : ScriptableObject
{
    public Dictionary<string, AgentBelief> beliefs;
    public HashSet<AgentAction> actions;
    public HashSet<AgentGoal> goals;
    public BeliefFactory beliefFactory;
    public GoapAgent agent { get; private set; }

    public void Initialize(GoapAgent agent)
    {
        this.agent = agent;
        
        beliefs = new Dictionary<string, AgentBelief>();
        beliefFactory = new BeliefFactory(agent, beliefs);
        
        actions = new HashSet<AgentAction>();
        
        goals = new HashSet<AgentGoal>();

    }
    
    public virtual void SetupBeliefs()
    {
        
    }

    public virtual void SetupActions()
    {
        
    }

    public virtual void SetupGoals()
    {
        
    }
}
