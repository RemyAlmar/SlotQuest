public class GenericTransition<T> : IGenericTransition<T>
{
    public IGenericState<T> To { get; }
    public IPredicate Condition { get; }
    public GenericTransition(IGenericState<T> to, IPredicate condition)
    {
        To = to;
        Condition = condition;
    }
}