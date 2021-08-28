using ChaosEffects;

namespace Modifiers
{
    public abstract class AbstractOnPassiveModifier : AbstractEventModifier<OnPlayerPassiveModifierTick>
    {
        protected override void OnFailure(OnPlayerPassiveModifierTick e)
        {
            ChaosPool.OnFail(e);
        }
    }
}