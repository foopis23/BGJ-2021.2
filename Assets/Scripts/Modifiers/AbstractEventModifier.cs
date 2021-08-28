using CallbackEvents;
using UnityEngine;

namespace Modifiers
{
    public abstract class AbstractEventModifier<T> : AbstractModifier<T> where T:EventContext
    {
        public AbstractEventModifier(int strength) : base(strength) {}

        public override void Activate()
        {
            EventSystem.Current.RegisterEventListener<T>(OnTrigger);
        }

        public override void Deactivate()
        {
            if (EventSystem.Current != null)
            {
                EventSystem.Current.UnregisterEventListener<T>(OnTrigger);    
            }
        }

        private void OnTrigger(T e)
        {
            var rand = Random.Range(0.0f, 1.0f);

            if (rand > Instability)
            {
                OnSuccess(e);
            }
            else
            {
                OnFailure(e);
            }
        }
    
        protected abstract void OnSuccess(T e);
        protected abstract void OnFailure(T e);
    }
}