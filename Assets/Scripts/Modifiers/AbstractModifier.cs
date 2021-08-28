using CallbackEvents;
using UnityEngine;

namespace Modifiers
{
    public abstract class AbstractModifier<T> : IModifier where T : EventContext
    {
        public CardObject Card;
        public int Strength;

        public void SetCard(CardObject card)
        {
            Card = card;
        }

        public void SetStrength(int strength)
        {
            Strength = strength;
        }
        
        public abstract void Activate();

        public abstract void Deactivate();

        public virtual void Update() {}
    }
}