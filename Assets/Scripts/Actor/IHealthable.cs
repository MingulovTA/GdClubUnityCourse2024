using System;

public interface IHealthable
{
    int Value { get; }
    
    void TakeDamage(int delta, Actor attacker);
    void AddHealth(int delta);

    event Action OnValueChanged;
    event Action<Actor> OnTakeDamage;
    event Action OnAddHealth;
    event Action OnDie;
    event Action OnAlive;
}