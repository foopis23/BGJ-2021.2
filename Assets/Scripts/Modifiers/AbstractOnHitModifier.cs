using ChaosEffects;

namespace Modifiers
{
    public abstract class AbstractOnHitModifier : AbstractEventFilterModifier<OnExpireContext>
    {
        protected override OnExpireContext OnFailure(OnExpireContext e)
        {
            ChaosPool.OnFail(e);
            return e;
        }
    }
}