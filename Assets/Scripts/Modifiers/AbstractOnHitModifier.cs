using ChaosEffects;

namespace Modifiers
{
    public abstract class AbstractOnHitModifier : AbstractEventFilterModifier<OnExpireContext>
    {
        public AbstractOnHitModifier(int strength) : base(strength) {}

        protected override OnExpireContext OnFailure(OnExpireContext e)
        {
            ChaosPool.OnFail(e);
            return e;
        }
    }
}