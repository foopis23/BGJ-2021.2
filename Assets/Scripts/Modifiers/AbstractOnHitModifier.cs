using CallbackEvents;
using ChaosEffects;

namespace Modifiers
{
    public abstract class AbstractOnHitModifier : AbstractEventFilterModifier<OnExpireContext>
    {
        protected override OnExpireContext OnFailure(OnExpireContext e)
        {
            EventSystem.Current.FireEvent(new CardFailed(){CardObject = Card});
            ChaosPool.OnFail(e);
            return e;
        }
    }
}