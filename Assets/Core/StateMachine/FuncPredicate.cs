using System;

public class FuncPredicate : IPredicate
{
    private readonly Func<bool> func;

    public FuncPredicate(Func<bool> _func)
    {
        func = _func;
    }
    public bool Evaluate() => func.Invoke();
}