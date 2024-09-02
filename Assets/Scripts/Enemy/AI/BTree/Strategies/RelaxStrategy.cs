using System;
using UnityEngine;

namespace AI.Btree
{
    public class RelaxStrategy : IStrategy
    {
        private float time;
        private float elapsed = 0f;
        private Action OnStop;

        public RelaxStrategy(float time)
        {
            this.time = time;
        }

        public Node.Status Process()
        {
            if (elapsed < time)
            {
                elapsed += Time.deltaTime;
                return Node.Status.Running;
            }

            return Node.Status.Success;
        }

        public void Reset()
        {
            elapsed = 0f;
        }
    }
}