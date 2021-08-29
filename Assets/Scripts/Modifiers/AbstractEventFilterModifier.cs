using CallbackEvents;
using UnityEngine;

namespace Modifiers
{
    public abstract class AbstractEventFilterModifier<T> : AbstractModifier<T> where T : EventContext
    {
        public override void Activate()
        {
            EventSystem.Current.RegisterFilterListener<T>(OnTrigger);
        }

        public override void Deactivate()
        {
            if (EventSystem.Current != null)
            {
                EventSystem.Current.UnregisterFilterListener<T>(OnTrigger);    
            }
        }

        private T OnTrigger(T e)
        {
            var rand = Random.Range(0.0f, 1.0f);
            return rand > (Card.ChaosLevel / Card.Modifiers.Length) ? OnSuccess(e) : OnFailure(e);
        }
    
        protected abstract T OnSuccess(T e);
        protected abstract T OnFailure(T e);
    }
}