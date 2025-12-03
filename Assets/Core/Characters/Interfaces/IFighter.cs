using System;

public interface IFighter
{
    public event Action OnStartTurn;
    public event Action OnEndTurn;
    public void Initialize();
    public void StartTurn();
    public void EndTurn();
}
