public interface IStatus
{
    public int Id { get; }
    public bool CanStack { get; }
    public void Execute();
    public void StackWith(IStatus _status);
    public void Add();
    public void Remove();
    public void Reset();
}