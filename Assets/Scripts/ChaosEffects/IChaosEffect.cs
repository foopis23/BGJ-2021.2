using CallbackEvents;

namespace ChaosEffects
{
    public interface IChaosEffect<in T> where T : EventContext
    {
        public void OnTrigger(T ctx);
    }
}