using System.Collections.Generic;
using MEC;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


namespace AI.Btree
{
    public class Wandering : IStrategy
    {
        private NavMeshAgent agent;

        private bool started;
        private bool cooldownStarted;
        private float time;

        private float cooldown;
        
        public bool IsReady { get; private set; } = true;
        
        public Wandering(NavMeshAgent agent, float cooldown)
        {
            this.agent = agent;
            this.cooldown = cooldown;
        }

        public Node.Status Process()
        {
            if (!IsReady) return Node.Status.Failure;
            
            if (!started)
            {
                for (int i = 0; i < 5; i++)
                {
                    var randomDir = Random.insideUnitCircle * 5f;
                    NavMeshHit hit;
                
                    if (NavMesh.SamplePosition((Vector2)agent.transform.position + randomDir, out hit, 5f, 1))
                    {
                        agent.SetDestination(hit.position);
                    }
                }

                started = true;
                return Node.Status.Running;
            }
            
            if (agent.remainingDistance < .1f)
            {

                Timing.RunCoroutine(Cooldown());
                /*if (!cooldownStarted)
                {
                    cooldownStarted = true;
                    time = 0f;
                    return Node.Status.Running;
                }
                
                time += Time.deltaTime;
                if (time < .5f) return Node.Status.Running;
                */
                
                started = false;
                cooldownStarted = false;
                return Node.Status.Success;
            }
            
            return Node.Status.Running;
        }

        private IEnumerator<float> Cooldown()
        {
            IsReady = false;

            yield return Timing.WaitForSeconds(cooldown);

            IsReady = true;
        }
    }
}