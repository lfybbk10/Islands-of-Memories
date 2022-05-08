using System;

public interface IDamageable
{ 
    public float Health { get; set; }

    void ApplyDamage(float value);
}