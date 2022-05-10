using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health;
    [SerializeField] private HealthBar _healthBar;
    private float _startHealh;
    public Action Dead { get; set; }

    private void Awake()
    {
        RuntimeContext.Instance.health = this;
        _healthBar.SetHealth(_health);
        _startHealh = _health;
    }

    public float Value
    {
        get => _health;
        set => _health = value;
    }

    public void ApplyDamage(float value)
    {
        if (Value - value > 0)
            Value -= value;
        else Value = 0;
        _healthBar.Value = Value;
        if (Value < 0)
            Dead?.Invoke();
    }

    public void Heal(float value)
    {
        if (Value + value < _startHealh)
            Value += value;
        else Value = _startHealh;
        _healthBar.Value = Value;
    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ApplyDamage(5);
        }
    }
}