using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CallbackEvents;

public abstract class LivingEntity : MonoBehaviour
{
    // editor fields
    public float MaxHealth;
    public bool IsAlive = true;

    // public properties
    public float Health { get; private set; }

    private void ExplosionListener(ExplosionEventContext e)
    {
        Damage(e.GetDamage(transform.position));
    }

    public void InitEvent()
    {
        EventSystem.Current.RegisterEventListener<ExplosionEventContext>(ExplosionListener);
    }

    public void RemoveEvents()
    {
        EventSystem.Current.UnregisterEventListener<ExplosionEventContext>(ExplosionListener);
    }

    public virtual void Damage(float damage)
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

            Health = Mathf.Min(MaxHealth, Health);
        }
    }

    public void Heal(float healing)
    {
        Damage(-healing);
    }

    protected virtual void OnDeath() {}
}
