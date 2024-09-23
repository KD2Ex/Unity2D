using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private Enemy enemy;
    private StateNode current;
    private Dictionary<Type, StateNode> nodes = new();
    private HashSet<ITransition> anyTransitions = new();

    public StateMachine(Enemy enemy)
    {
        this.enemy = enemy;
    }
    
    public void Update()
    {
        var transition = GetTransition();
        if (transition != null)
        {
            ChangeState(transition.To);
        }
        
        current.State?.Update();

        Debug.Log(current.State?.GetType().ToString());
    }

    public void FixedUpdate()
    {
        current.State?.FixedUpdate();
    }

    public void SetState(IState state)
    {
        current = nodes[state.GetType()];
        current.State?.Enter();
    }
    
    private void ChangeState(IState state)
    {
        Debug.Log("Current: " + current.State);
        Debug.Log("New : " + state);
        if (current.State == state) return;

//      enemy.Behaviour.DebugText.text = state.ToString();
        
        var prevState = current.State;
        var nextState = nodes[state.GetType()].State;
        
        prevState?.Exit();
        nextState?.Enter();
        
        current = nodes[state.GetType()];
        
        /*CurrentState.Exit();
        CurrentState = newState;
        newState.Enter();*/
    }

    private ITransition GetTransition()
    {
        foreach (var transition in anyTransitions)
        {
            if (transition.Condition.Evaluate()) return transition;
        }

        foreach (var transition in current.Transitions)
        {
            if (transition.Condition.Evaluate()) return transition;
        }

        return null;
    }

    public void AddTransition(IState from, IState to, IPredicate condition)
    {
        GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
    }

    public void AddAnyTransition(IState to, IPredicate condition)
    {
        anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
    }
    
    StateNode GetOrAddNode(IState state)
    {
        var node = nodes.GetValueOrDefault(state.GetType());

        if (node == null)
        {
            node = new StateNode(state);
            nodes.Add(state.GetType(), node);
        }

        return node;
    }
    
}
