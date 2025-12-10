using System;
public interface IGenericState<T>
{
    public T StateType { get; }
    public void OnEnter();
    public void Update();
    public void FixedUpdate();
    public void OnExit();

    public Action EnterBehaviour { get; set; }
    public Action UpdateBehaviour { get; set; }
    public Action FixedUpdateBehaviour { get; set; }
    public Action ExitBehaviour { get; set; }
}
