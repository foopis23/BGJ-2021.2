using ChaosEffects;
using UnityEngine.EventSystems;
using EventSystem = CallbackEvents.EventSystem;

namespace Modifiers
{
    public abstract class AbstractOnFireModifier : AbstractEventModifier<OnFireContext>
    {
        protected override void OnFailure(OnFireContext e)
        {
            EventSystem.Current.FireEvent(new CardFailed(){CardObject = Card});
            ChaosPool.OnFail(e);
        }
    }
}