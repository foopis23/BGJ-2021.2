namespace Modifiers
{
    public class ExplosionPower : AbstractEventFilterModifier<OnExpireContext>
    {
        protected override OnExpireContext OnSuccess(OnExpireContext e)
        {
            e.ExplosionPower += Strength;
            return e;
        }

        protected override OnExpireContext OnFailure(OnExpireContext e)
        {
            throw new System.NotImplementedException();
        }
    }   
}