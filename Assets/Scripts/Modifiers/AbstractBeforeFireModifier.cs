using ChaosEffects;

namespace Modifiers
{
    public abstract class AbstractBeforeFireModifier : AbstractEventFilterModifier<BeforeFireContext>
    {
        protected override BeforeFireContext OnFailure(BeforeFireContext e)
        {
            ChaosPool.OnFail(e);
            return e;
        }
    }
}