using System;
using System.Collections.Generic;
using UnityEngine;

public class BeliefFactory
{
    private readonly GoapAgent agent;
    private readonly Dictionary<string, AgentBelief> beliefs;

    public BeliefFactory(GoapAgent agent, Dictionary<string, AgentBelief> beliefs)
    {
        this.agent = agent;
        this.beliefs = beliefs;
    }

    public void AddBelief(string key, Func<bool> condition)
    {
        beliefs.Add(key, new AgentBelief.Builder(key)
            .WithCondition(condition)
            .Build());
    }

    public void AddLocationBelief(string key, float distance, Transform locationCondition)
    {
        AddLocationBelief(key, distance, locationCondition.position);
    }
    
    public void AddLocationBelief(string key, float distance, Vector2 locationCondition)
    {
        beliefs.Add(key, new AgentBelief.Builder(key)
            .WithCondition(() => InRangeOf(locationCondition, distance))
            .WithLocation(() => locationCondition)
            .Build());
    }

    public void AddSensorBeliefs(string key, Sensor sensor)
    {
        beliefs.Add(key, new AgentBelief.Builder(key)
            .WithCondition(() => sensor.IsTargetInRange)
            .WithLocation(() => sensor.TargetTransform.position)
            .Build());
    }
    
    private bool InRangeOf(Vector2 pos, float range) => 
        Vector2.Distance(agent.transform.position, pos) < range;
    
    
}

public class AgentBelief
{
    public string Name { get; }

    private Func<bool> condition = () => false;
    private Func<Vector3> observedLocation = () => Vector3.zero;

    public Vector3 Location => observedLocation();

    private AgentBelief(string name)
    {
        Name = name;
    }

    public bool Evaluate() => condition();

    public class Builder
    {
        private readonly AgentBelief belief;

        public Builder(string name)
        {
            belief = new AgentBelief(name);
        }

        public Builder WithCondition(Func<bool> condition)
        {
            belief.condition = condition;
            return this;
        }
        
        public Builder WithLocation(Func<Vector3> location)
        {
            belief.observedLocation = location;
            return this;
        }

        public AgentBelief Build()
        {
            return belief;
        }
    }
}
