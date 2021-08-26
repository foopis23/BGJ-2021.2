using CallbackEvents;
using UnityEngine;

namespace Modifiers
{
    public abstract class AbstractModifier<T> : IModifier where T:EventContext
    {
        public float Corruption = 0.1f;
        private float _instability = 0.0f;
        
        public void Activate()
        {
            EventSystem.Current.RegisterEventListener<T>(OnTrigger);
        }

        public void Deactivate()
        {
            if (EventSystem.Current != null)
            {
                EventSystem.Current.UnregisterEventListener<T>(OnTrigger);    
            }
        }

        public void Update()
        {
            if (Random.Range(0.0f, 1.0f) > Corruption) return;
            
            _instability = Mathf.Min(1.0f, _instability + 0.01f);
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