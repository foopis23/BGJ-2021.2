using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviour
{
    // editor fields
    public float MaxHealth;
    public bool IsAlive = true;

    // public properties
    public float Health { get; private set; }

    public void Damage(float damage)
    {
        if(IsAlive)
        {
            Health -= damage;
            if(Health <= 0f)
            {
                Health = 0f;
                IsAlive = false;
                OnDeath();
            }
        }
    }

    public void Heal(float healing)
    {
        if(IsAlive)
        {
            Health = Mathf.Min(MaxHealth, Health + healing);
        }
    }

    protected virtual void OnDeath() {}
}
