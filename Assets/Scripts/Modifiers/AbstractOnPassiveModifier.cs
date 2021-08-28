using ChaosEffects;

namespace Modifiers
{
    public abstract class AbstractOnPassiveModifier : AbstractEventFilterModifier<PlayerStatusEffectContext>
    {
        public AbstractOnPassiveModifier(int strength) : base(strength) {}

        protected override PlayerStatusEffectContext OnFailure(PlayerStatusEffectContext e)
        {
            ChaosPool.OnFail(e);
            return e;
        }
    }
}