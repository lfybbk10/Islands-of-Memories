using System;

public interface IDamageable
{ 
    public Action Dead { get; set; }
    public float Value { get; set; }

    void ApplyDamage(float value);
}