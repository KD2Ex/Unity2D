using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public interface IGoapPlanner
{
   ActionPlan Plan(GoapAgent agent, HashSet<AgentGoal> goals, AgentGoal mostRecentGoal = null);
}

public class GoapPlanner : IGoapPlanner
{
   public ActionPlan Plan(GoapAgent agent, HashSet<AgentGoal> goals, AgentGoal mostRecentGoal = null)
   {

//      Debug.Log($"Most recent goal: {mostRecentGoal?.Name}");
      List<AgentGoal> orderedGoals = goals
         .Where(g => g.DesiredEffects.Any(b => !b.Evaluate()))
         .OrderByDescending(g => g == mostRecentGoal ? g.Priority - 0.01 : g.Priority)
         .ToList();

      foreach (var goal in orderedGoals)
      {
         Node goalNode = new Node(null, null, goal.DesiredEffects, 0);

         if (FindPath(goalNode, agent.actions))
         {
            if (goalNode.IsDead) continue;

            Stack<AgentAction> actionStack = new Stack<AgentAction>();

            int i = 0;
            
            while (goalNode.Leaves.Count > 0)
            {
               
               //Debug.Log($"Leaf: {goalNode.Leaves[i].Action.Name}");
               
               var cheapest = goalNode.Leaves.OrderBy(leaf => leaf.Cost).First();
               goalNode = cheapest;
               actionStack.Push(cheapest.Action);

               ++i;
            }

            //Debug.Log(actionStack.Peek().Name);
            
            return new ActionPlan(goal, actionStack, goalNode.Cost);
         }
      }

      Debug.Log(orderedGoals.Count);
      Debug.LogWarning("No plan found");
      return null;
   }

   private bool FindPath(Node parent, HashSet<AgentAction> actions)
   {
      var orderedActions = actions.OrderBy(a => a.Cost);
      
      foreach (var action in orderedActions)
      {
         var requiredEffects = parent.RequiredEffects;

         requiredEffects.RemoveWhere(b => b.Evaluate());

         if (requiredEffects.Count == 0)
         {
            return true;
         }

         if (action.Effects.Any(requiredEffects.Contains))
         {
            var newRequiredEffects = new HashSet<AgentBelief>(requiredEffects);
            newRequiredEffects.ExceptWith(action.Effects);
            newRequiredEffects.UnionWith(action.Preconditions);

            var newAvailableActions = new HashSet<AgentAction>(actions);
            newAvailableActions.Remove(action);

            var newNode = new Node(parent, action, newRequiredEffects, parent.Cost + action.Cost);

            if (FindPath(newNode, newAvailableActions))
            {
               parent.Leaves.Add(newNode);
               newRequiredEffects.ExceptWith(newNode.Action.Preconditions);
            }

            if (newRequiredEffects.Count == 0)
            {
               return true;
            }
         }
      }

      return false;
   }
}



public class Node
{
   public Node Parent { get; }
   public AgentAction Action { get; }
   public HashSet<AgentBelief> RequiredEffects { get; }
   public List<Node> Leaves { get; } // Neighbors?
   public float Cost { get; }

   public bool IsDead => Leaves.Count == 0 && Action == null;

   public Node(Node parent, AgentAction action, HashSet<AgentBelief> effects, float cost)
   {
      Parent = parent;
      Action = action;
      RequiredEffects = new HashSet<AgentBelief>(effects);
      Leaves = new List<Node>();
      Cost = cost;
   }
}


public class ActionPlan
{
   public AgentGoal AgentGoal { get; }
   public Stack<AgentAction> Actions { get; }
   public float TotalCost { get; set; }

   public ActionPlan(AgentGoal goal, Stack<AgentAction> actions, float cost)
   {
      AgentGoal = goal;
      Actions = actions;
      TotalCost = cost;
   }
}
