using System;
using CallbackEvents;
using UnityEngine;
using UnityEngine.Serialization;

namespace TickDamage
{
    public enum TickType
    {
        Fire,
        Poison,
        Regeneration,
        StatusEffects
    }

    public class HealthTickManager : MonoBehaviour
    {
        public float fireTickSpeed;
        public float fireTickDamage;
        public float poisonTickSpeed;
        public float poisonTickDamage;
        public float regenerationTickSpeed;
        public float statusEffectTickSpeed;
        
        // private properties
        private float _lastFireTick;
        private float _lastPoisonTick;
        private float _lastRegenerationTick;
        private float _lastStatusEffectTick;

        public void Start()
        {
            _lastFireTick = 0;
            _lastPoisonTick = 0;
            _lastRegenerationTick = 0;
            _lastStatusEffectTick = 0;
        }

        public void Update()
        {
            // Fire Tick
            if (Time.time - _lastFireTick >= fireTickSpeed)
            {
                EventSystem.Current.FireEvent(new TickHealthContext(){Type = TickType.Fire,Damage = fireTickDamage, TickLength = fireTickSpeed});
                _lastFireTick = Time.time;
            }
        
            // Poison Tick
            if (Time.time - _lastPoisonTick > poisonTickSpeed)
            {
                EventSystem.Current.FireEvent(new TickHealthContext(){Type = TickType.Poison, Damage = poisonTickDamage, TickLength = poisonTickSpeed});
                _lastPoisonTick = Time.time;
            }

            // Regeneration Tick
            if (Time.time - _lastRegenerationTick >= regenerationTickSpeed)
            {
                EventSystem.Current.FireEvent(new TickHealthContext(){ Type = TickType.Regeneration, TickLength = regenerationTickSpeed});
                _lastRegenerationTick = Time.time;
            }

            if (Time.time - _lastStatusEffectTick >= statusEffectTickSpeed)
            {
                EventSystem.Current.FireEvent(new TickHealthContext(){ Type = TickType.StatusEffects, TickLength = statusEffectTickSpeed});
            }
        }
    }

    public class TickHealthContext : EventContext
    {
        public TickType Type;
        public float Damage;
        public float TickLength;
    }
}