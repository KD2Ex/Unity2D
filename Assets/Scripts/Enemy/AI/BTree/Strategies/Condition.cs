using System;

namespace AI.Btree
{
    public class Condition : IStrategy
    {
        private Func<bool> predicate;
        
        public Condition(Func<bool> predicate)
        {
            this.predicate = predicate;
        }

        public Node.Status Process() => predicate() ? Node.Status.Success : Node.Status.Failure;
    }
}