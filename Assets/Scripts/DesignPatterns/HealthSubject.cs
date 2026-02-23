using System;
using UnityEngine;

public class HealthSubject : MonoBehaviour
{
    public int maxHealth = 10;
    public int CurrentHealth { get; private set; }

    // Observers subscribe to this event
    public event Action<int, int> OnHealthChanged; // (current, max)

    private void Start()
    {
        CurrentHealth = maxHealth;
        Notify();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) TakeDamage(1);
        if (Input.GetKeyDown(KeyCode.J)) Heal(1);
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        Notify();
    }

    public void Heal(int amount)
    {
        CurrentHealth = Mathf.Min(maxHealth, CurrentHealth + amount);
        Notify();
    }

    private void Notify()
    {
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }
}