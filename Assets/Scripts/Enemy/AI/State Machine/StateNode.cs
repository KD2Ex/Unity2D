using System.Collections.Generic;

public class StateNode
{
    public StateNode(IState state)
    {
        State = state;
        Transitions = new HashSet<ITransition>();
    }

    public IState State { get; }
    public HashSet<ITransition> Transitions { get; }

    public void AddTransition(IState to, IPredicate condition)
    {
        Transitions.Add(new Transition(to, condition));
    }
}