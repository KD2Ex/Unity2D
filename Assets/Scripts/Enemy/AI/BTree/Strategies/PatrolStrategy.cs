using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Btree
{
    public class PatrolStrategy : IStrategy
    {
        private NavMeshAgent agent;
        private List<Transform> pts;
        private float speed;
        private int index;

        private bool isPathCalculated;

        public PatrolStrategy(NavMeshAgent navMeshAgent, List<Transform> pts, float speed)
        {
            agent = navMeshAgent;
            this.pts = pts;
            this.speed = speed;
        }
        
        public Node.Status Process()
        {
            Debug.Log(index);
            if (index == pts.Count)
            {
                Reset();
                return Node.Status.Running;
            }
            
            var target = pts[index];
            agent.SetDestination(target.position);

            if (agent.hasPath && agent.remainingDistance < .1f)
            {
                index++;
                //isPathCalculated = false;
            }

            /*if (agent.pathPending)
            {
                isPathCalculated = true;
            }
            */

            return Node.Status.Running;
        }

        public void Reset()
        {
            index = 0;
        }
    }
}