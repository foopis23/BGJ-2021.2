using CallbackEvents;
using UnityEngine;

namespace Modifiers
{
    public abstract class AbstractModifier<T> : IModifier where T:EventContext
    {
        private float _instability = 0.1f;
        private readonly float _timeEquipped;
        public float InstabilityMultiplier = 1.0f;

        protected AbstractModifier()
        {
            EventSystem.Current.RegisterEventListener<T>(OnTrigger);
            _timeEquipped = Time.time;
        }

        ~AbstractModifier()
        {
            if (EventSystem.Current != null)
            {
                EventSystem.Current.UnregisterEventListener<T>(OnTrigger);    
            }
        }

        public void Update()
        {
            _instability = Mathf.Min((Time.time - _timeEquipped) / (100 * InstabilityMultiplier), 1);
        }

        private void OnTrigger(T e)
        {
            var rand = Random.Range(0.0f, 1.0f);

            if (rand > _instability)
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