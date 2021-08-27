namespace Modifiers
{
    public class Tnt : AbstractEventFilterModifier<ExplosionPowerFilterContext>
    {
        protected override ExplosionPowerFilterContext OnSuccess(ExplosionPowerFilterContext e)
        {
            e.ExplosionPower++;
            return e;
        }

        protected override ExplosionPowerFilterContext OnFailure(ExplosionPowerFilterContext e)
        {
            throw new System.NotImplementedException();
        }
    }
}