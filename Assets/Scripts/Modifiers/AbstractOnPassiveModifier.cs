using ChaosEffects;

namespace Modifiers
{
    public abstract class AbstractOnPassiveModifier : AbstractEventFilterModifier<PlayerStatusEffectContext>
    {
        protected override PlayerStatusEffectContext OnFailure(PlayerStatusEffectContext e)
        {
            ChaosPool.OnFail(e);
            return e;
        }
    }
}