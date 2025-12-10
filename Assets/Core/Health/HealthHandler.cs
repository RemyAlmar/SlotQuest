using System;
using UnityEngine;
[Serializable]
public class HealthHandler : IHealthable
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    public int CurrentHealth => currentHealth;

    public int MaxHealth => maxHealth;

    public bool IsAlive => currentHealth > 0;

    public event Action OnHit;
    public event Action OnHeal;
    public event Action OnHealthChanged;

    public HealthHandler(int _maxHealth)
    {
        maxHealth = _maxHealth;
        currentHealth = _maxHealth;
    }

    public void Reset()
    {
        currentHealth = maxHealth;
    }

    public void RestoreHealth(int _heal)
    {
        if (!IsAlive) return;
        int _previousHealth = currentHealth;
        currentHealth += _heal;
        currentHealth = Mathf.Min(maxHealth, currentHealth);

        OnHeal?.Invoke();
        if (_previousHealth != currentHealth)
            OnHealthChanged?.Invoke();
    }

    public void TakeDamage(int _damage)
    {
        if (!IsAlive) return;
        int _previousHealth = currentHealth;
        currentHealth -= _damage;
        currentHealth = Mathf.Max(0, currentHealth);

        OnHit?.Invoke();
        if (_previousHealth != currentHealth)
            OnHealthChanged?.Invoke();
    }
}
