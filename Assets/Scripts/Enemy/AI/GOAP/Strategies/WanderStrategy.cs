using UnityEngine;
using UnityEngine.AI;

public class WanderStrategy : IActionStrategy
{
    private readonly NavMeshAgent _agent;
    private readonly float _wanderRadius;

    public bool CanPerform => !Complete;
    public bool Complete => _agent.remainingDistance <= 2f && !_agent.pathPending;

    public WanderStrategy(NavMeshAgent agent, float wanderRadius)
    {
        _agent = agent;
        _wanderRadius = wanderRadius;
    }

    public void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector2 randomDirection = (UnityEngine.Random.insideUnitCircle * _wanderRadius);
            NavMeshHit hit;

            if (NavMesh.SamplePosition((Vector2)_agent.transform.position + randomDirection, out hit, _wanderRadius, 1))
            {
                _agent.SetDestination(hit.position);
                return;
            }
        }
    }
}
