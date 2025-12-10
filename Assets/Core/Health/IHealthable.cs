using System;

public interface IHealthable
{
    public int CurrentHealth { get; }
    public int MaxHealth { get; }
    public bool IsAlive { get; }
    public void TakeDamage(int _damage);
    public void Reset();
    public void RestoreHealth(int _heal);
    public event Action OnHit;
    public event Action OnHeal;
    public event Action OnHealthChanged;
}
