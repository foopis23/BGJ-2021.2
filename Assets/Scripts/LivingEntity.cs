using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CallbackEvents;
using StatusEffects;
using TickDamage;
using UnityEngine.Serialization;

public abstract class LivingEntity : MonoBehaviour
{
    // editor fields
    public float MaxHealth;
    public bool IsAlive = true;
    [SerializeField] protected float baseDamageResistance;
    [SerializeField] protected float baseWalkSpeed;
    [SerializeField] protected float baseStrafeSpeed;
    [SerializeField] protected float baseMoveAcceleration;
    [SerializeField] protected float baseHealthRegeneration;
    [SerializeField] protected float baseHealthRegenerationTickSpeed;
    [SerializeField] protected float baseHealthRegenerationDelay;

    // public properties
    public float BaseDamageResistance => baseDamageResistance;
    public float BaseWalkSpeed => baseWalkSpeed;
    public float BaseHealthRegeneration => baseHealthRegeneration;
    public float BaseHealthRegenerationTickSpeed => baseHealthRegenerationTickSpeed;
    public float BaseHealthRegenerationDelay => baseHealthRegenerationDelay;
    public float BaseStrafeSpeed => baseStrafeSpeed;
    public float BaseMoveAcceleration => baseMoveAcceleration;
    public float Health { get; private set; }
    [NonSerialized] public float DamageResistance;
    [NonSerialized] public float WalkSpeed;
    [NonSerialized] public float HealthRegeneration;
    [NonSerialized] public float HealthRegenerationTickSpeed;
    [NonSerialized] public float HealthRegenerationDelay;
    [NonSerialized] public float StrafeSpeed;
    [NonSerialized] public float MoveAcceleration;

    // private properties
    private Dictionary<TickType, float> _tickDamage;
    private Dictionary<AbstractStatusEffect, float> _statusEffects;
    private float _lastRegeneration;
    private float _lastDamage;

    public void ApplyStatusEffect(AbstractStatusEffect effect, float duration)
    {
        _statusEffects[effect] = duration;
    }

    public void ApplyTickStatus(TickType type, float duration)
    {
        if (_tickDamage.ContainsKey(type))
        {
            if (_tickDamage[type] < duration)
            {
                _tickDamage[type] = duration;
            }
        }
        else
        {
            _tickDamage[type] = duration;
        }
    }
    
    public virtual void Damage(float damage)
    {
        if (!IsAlive) return;

        if (damage > 0)
        {
            damage = Mathf.Max(1, damage - DamageResistance);
            _lastDamage = Time.time;
        }
        
        Health -= damage;
            
        if(Health <= 0f)
        {
            Health = 0f;
            IsAlive = false;
            OnDeath();
        }

        Health = Mathf.Min(MaxHealth, Health);
    }

    public void Heal(float healing)
    {
        Damage(-healing);
    }

    protected void Init()
    {
        _statusEffects = new Dictionary<AbstractStatusEffect, float>();
        _tickDamage = new Dictionary<TickType, float>();
        Heal(MaxHealth);
        
        // set base shit
        SetStats(0);

        // add event listeners
        EventSystem.Current.RegisterEventListener<ExplosionEventContext>(ExplosionListener);
        EventSystem.Current.RegisterEventListener<TickHealthContext>(OnTickDamage);
    }

    protected void DeInit()
    {
        EventSystem.Current.UnregisterEventListener<ExplosionEventContext>(ExplosionListener);
        EventSystem.Current.UnregisterEventListener<TickHealthContext>(OnTickDamage);
    }
    protected virtual void OnDeath() {}

    private void SetStats(float timeSinceLastTick)
    {
        DamageResistance = baseDamageResistance;
        WalkSpeed = baseWalkSpeed;
        HealthRegeneration = baseHealthRegeneration;
        HealthRegenerationDelay = baseHealthRegenerationDelay;
        HealthRegenerationTickSpeed = baseHealthRegenerationTickSpeed;
        StrafeSpeed = baseStrafeSpeed;
        MoveAcceleration = baseMoveAcceleration;

        foreach (var effect in _statusEffects.Keys)
        {
            _statusEffects[effect] -= timeSinceLastTick;
            if (_statusEffects[effect] <= 0)
            {
                _statusEffects.Remove(effect);
                continue;
            }
            
            effect.StatFilter(this);
        }
    }
    
    private void ExplosionListener(ExplosionEventContext e)
    {
        Damage(e.GetDamage(transform.position));
    }

    private void OnTickDamage(TickHealthContext e)
    {
        switch (e.Type)
        {
            case TickType.Regeneration:
                OnRegenerationTick(e);
                break;
            case TickType.StatusEffects:
                SetStats(e.TickLength);
                break;
            case TickType.Fire:
            case TickType.Poison:
                if (!_tickDamage.ContainsKey(e.Type)) return;

                _tickDamage[e.Type] = Mathf.Max(_tickDamage[e.Type] - e.TickLength, 0);
        
                if (_tickDamage[e.Type] > 0)
                {
                    Damage(e.Damage);
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnRegenerationTick(TickHealthContext e)
    {
        if (!(Time.time - _lastRegeneration > HealthRegenerationTickSpeed) ||
            !(Time.time - _lastDamage > HealthRegenerationDelay)) return;
        
        Heal(HealthRegeneration);
        _lastRegeneration = Time.time;
    }
}
