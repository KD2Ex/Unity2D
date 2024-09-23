using System;

public class ActionPredicate : IPredicate
{
    private Action action;
    private Func<bool> func;


    public ActionPredicate(Func<bool> func, Action action)
    {
        this.action = action;
        this.func = func;
    }

    public bool Evaluate()
    {
        var success = func.Invoke();
        
        if (success) action.Invoke();

        return success;
    }
}