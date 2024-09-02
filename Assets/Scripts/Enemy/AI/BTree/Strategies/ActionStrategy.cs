using System;
using UnityEngine;

namespace AI.Btree
{
    public class ActionStrategy : IStrategy
    {
        private Action action;
        private bool runForever;

        
        public ActionStrategy(Action action, bool runForever = false)
        {
            this.runForever = runForever;
            this.action = action;
        }

        public Node.Status Process()
        {
            action();
            return runForever ? Node.Status.Running : Node.Status.Success;
        }

        public void Reset()
        {
            
        }
    }
}