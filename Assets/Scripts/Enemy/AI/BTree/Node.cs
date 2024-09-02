using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils.List;

namespace AI.Btree
{

    public class Inverter : Node
    {
        public Inverter(string name, int priority = 0) : base(name, priority)
        {
        }

        public override Status Process()
        {
            switch (children[0].Process())
            {
                case Status.Running:
                    return Status.Running;
                case Status.Failure:
                    return Status.Success;
                default:
                    return Status.Failure;
            }
        }
    }
    
    public class RandomSelector : PrioritySelector
    {

        protected override List<Node> SortChildren() => children.Shuffle().ToList();

        public RandomSelector(string name) : base(name)
        {
        }
    }
    
    public class PrioritySelector : Selector
    {
        private List<Node> sortedChildren;
        private List<Node> SortedChildren => sortedChildren ??= SortChildren();

        protected virtual List<Node> SortChildren() => children.OrderByDescending(child => child.priority).ToList();
        
        public PrioritySelector(string name, int priority = 0) : base(name, priority)
        {
        }

        public override Status Process()
        {
            foreach (var child in SortedChildren)
            {
                switch (child.Process())
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Success:
                        return Status.Success;
                    default:
                        continue;
                }
            }

            return Status.Failure;
        }

        public override void Reset()
        {
            base.Reset();
            sortedChildren = null;
        }
    }
    
    public class Selector : Node
    {
        public Selector(string name, int priority = 0) : base(name, priority)
        {
        }

        public override Status Process()
        {
            if (currentChild < children.Count)
            {
                switch (children[currentChild].Process())
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Success:
                        return Status.Success;
                    default:
                        currentChild++;
                        return Status.Running;
                }
            }
            
            Reset();
            return Status.Failure;
        }
    }

    public class Sequence : Node
    {
        public Sequence(string name, int priority = 0) : base(name, priority)
        {
        }

        public override Status Process()
        {
            if (currentChild < children.Count)
            {
                switch (children[currentChild].Process()) 
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Failure:
                        Reset();
                        return Status.Failure;
                    default:
                        currentChild++;
                        return currentChild == children.Count ? Status.Success : Status.Running;
                }
            }
            
            Reset();
            return Status.Success;
        }
    }
    
    public class BehaviorTree : Node
    {
        public BehaviorTree(string name, int priority = 0) : base(name, priority)
        {
            
        }
        
        public override Status Process()
        {
            var status = children[currentChild].Process();
            // policy check

            currentChild = (currentChild + 1) % children.Count;
            return Status.Running;
        }
    }
    
    public class Leaf : Node
    {
        private readonly IStrategy strategy;

        public Leaf(string name, IStrategy strategy, int priority = 0) : base(name, priority)
        {
            this.strategy = strategy;
            this.priority = priority;
        }

        public override Status Process()
        {
            return strategy.Process();
        }

        public override void Reset() => strategy.Reset();
    }
    
    public class Node
    {
        public enum Status
        {
            Success,
            Failure,
            Running
        }

        public readonly string name;
        public int priority;

        public readonly List<Node> children = new();
        protected int currentChild;

        public Node(string name, int priority = 0)
        {
            this.name = name;
            this.priority = priority;
        }

        public void AddChild(Node child) => children.Add(child);

        public virtual Status Process()
        {
            return children[currentChild].Process();
        }

        public virtual void Reset()
        {
//            Debug.Log("Reset");
            currentChild = 0;
            foreach (var child in children)
            {
                child.Reset();
            }
        }
    }

}

