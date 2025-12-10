public interface IGenericTransition<T>
{
    public IGenericState<T> To { get; }
    public IPredicate Condition { get; }
}
