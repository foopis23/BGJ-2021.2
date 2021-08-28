using ChaosEffects;

namespace Modifiers
{
    public abstract class AbstractOnFireModifier : AbstractEventModifier<OnFireContext>
    {
        public AbstractOnFireModifier(int strength) : base(strength) {}

        protected override void OnFailure(OnFireContext e)
        {
            ChaosPool.OnFail(e);
        }
    }
}