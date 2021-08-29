using CallbackEvents;
using ChaosEffects;

namespace Modifiers
{
    public abstract class AbstractOnPassiveModifier : AbstractEventModifier<OnPlayerPassiveModifierTick>
    {
        protected override void OnFailure(OnPlayerPassiveModifierTick e)
        {
            EventSystem.Current.FireEvent(new CardFailed(){CardObject = Card});
            ChaosPool.OnFail(e);
        }
    }
}