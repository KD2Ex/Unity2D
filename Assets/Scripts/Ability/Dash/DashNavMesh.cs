using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DashNavMesh : MonoBehaviour
{
    [SerializeField] private DashStats stats;
    
    private NavMeshAgent agent;
    
    public FloatReference Cooldown => stats.cooldown;
    public FloatReference DashTime => stats.dashTime;

    private void Awake()
    {

        Debug.Log("nav mesh dash awake");
    }

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Execute(Vector2 direction)
    {
        agent.ResetPath();
        StartCoroutine(Dashing(direction));
        Debug.Log("nav mesh dash execute");
    }

    private IEnumerator Dashing(Vector2 direction)
    {
        var velocity = (direction * (stats.force.Value));
        agent.velocity = velocity;
        Debug.Log(velocity);
        Debug.Log(stats.force.Value);
        Debug.Log(agent.velocity);
        Debug.Log(agent.desiredVelocity);
        yield return new WaitForSeconds(stats.dashTime.Value);

        agent.velocity = Vector3.zero;
    }
}
