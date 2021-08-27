namespace Modifiers
{
    public class BombDefusal : AbstractEventFilterModifier<ExplosionPowerFilterContext>
    {
        protected override ExplosionPowerFilterContext OnSuccess(ExplosionPowerFilterContext e)
        {
            e.ExplosionPower--;
            return e;
        }

        protected override ExplosionPowerFilterContext OnFailure(ExplosionPowerFilterContext e)
        {
            throw new System.NotImplementedException();
        }
    }   
}