using System;
using UnityEngine;

public abstract class Character : MonoBehaviour, IFighter
{
    [SerializeField] protected int maxHealth = 3;
    protected HealthHandler health;
    protected bool isPlaying;
    public bool IsPlaying { get => isPlaying; protected set => isPlaying = value; }
    public HealthHandler Health => health;

    public event Action OnStartTurn;
    public event Action OnEndTurn;

    public virtual void Initialize()
    {
        health ??= new(maxHealth);
    }
    public virtual void StartTurn()
    {
        Debug.Log("Fighter : Start Turn");
        isPlaying = true;
        OnStartTurn?.Invoke();
        PlayTurn();
    }

    protected abstract void PlayTurn();
    public virtual void EndTurn()
    {
        Debug.Log("Fighter : End Turn");
        isPlaying = false;
        OnEndTurn?.Invoke();
        Debug.Log("Fighter : End Turn Exit Function");
    }
}