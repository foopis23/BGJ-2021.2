using CallbackEvents;
using ChaosEffects;

namespace Modifiers
{
    public abstract class AbstractBeforeFireModifier : AbstractEventFilterModifier<BeforeFireContext>
    {
        protected override BeforeFireContext OnFailure(BeforeFireContext e)
        {
            EventSystem.Current.FireEvent(new CardFailed(){CardObject = Card});
            ChaosPool.OnFail(e);
            return e;
        }
    }
}