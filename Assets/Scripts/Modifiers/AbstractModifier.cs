using CallbackEvents;
using UnityEngine;

namespace Modifiers
{
    public abstract class AbstractModifier<T> : IModifier where T : EventContext
    {
        public int Strength;
        public float Corruption = 0.1f;
        protected float Instability = 0.0f;

        public void SetStrength(int strength)
        {
            Strength = strength;
        }
        
        public abstract void Activate();

        public abstract void Deactivate();

        public virtual void Update()
        {
            if (Random.Range(0.0f, 1.0f) > Corruption) return;
            
            Instability = Mathf.Min(1.0f, Instability + 0.01f);
        }
    }
}