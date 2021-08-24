using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviour
{
    public float Health;
    public float MaxHealth;
    public bool IsAlive = true;

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

    protected virtual void OnDeath() {}
}
