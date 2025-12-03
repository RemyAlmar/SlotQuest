using System;
using UnityEngine;

public class Character : MonoBehaviour, IFighter
{
    public event Action OnStartTurn;
    public event Action OnEndTurn;

    public virtual void Initialize()
    {
        Debug.Log("Initialisation");
    }
    public void StartTurn()
    {
        Debug.Log("StartTurn");
        OnStartTurn?.Invoke();
    }
    public void EndTurn()
    {
        Debug.Log("EndTurn");
        OnEndTurn?.Invoke();
    }
}