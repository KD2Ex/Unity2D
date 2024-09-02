using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveStrategyNM : IActionStrategy
{
    private readonly NavMeshAgent _agent;
    private readonly Func<Vector2> _destination;
    private readonly Action OnStop;

    public bool CanPerform => !Complete;
    public bool Complete => _agent.remainingDistance <= 1f && !_agent.pathPending;

    public MoveStrategyNM(NavMeshAgent agent, Func<Vector2> destination, Action onStop = null)
    {
        _agent = agent;
        _destination = destination;

        OnStop = onStop;
    }

    public void Start()
    {
        _agent.SetDestination(_destination());
    }

    public void Stop()
    {
        _agent.ResetPath();
        OnStop?.Invoke();
    }
}
