using ChaosEffects;

namespace Modifiers
{
    public abstract class AbstractOnFireModifier : AbstractEventModifier<OnFireContext>
    {
        protected override void OnFailure(OnFireContext e)
        {
            ChaosPool.OnFail(e);
        }
    }
}