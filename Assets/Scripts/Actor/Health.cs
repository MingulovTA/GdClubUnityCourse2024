using System;
using UnityEngine;

public class Health : MonoBehaviour, IHealthable
{
    [SerializeField] private int _value = 100;
    
    public int Value => _value;
    
    public event Action OnValueChanged;
    public event Action<Actor> OnTakeDamage;
    public event Action OnAddHealth;
    public event Action OnDie;
    public event Action OnAlive;
    
    public void TakeDamage(int delta, Actor attacker)
    {
        if (_value <= 0) return;
        _value -= delta;
        OnTakeDamage?.Invoke(attacker);
        OnValueChanged?.Invoke();
        
        if (_value<=0)
            OnDie?.Invoke();
    }

    public void AddHealth(int delta)
    {
        int lastValue = _value;
        _value += delta;
        OnAddHealth?.Invoke();
        OnValueChanged?.Invoke();
        
        if (lastValue<=0&&_value>0)
            OnAlive?.Invoke();
    }
}