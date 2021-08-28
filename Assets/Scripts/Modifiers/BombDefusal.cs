namespace Modifiers
{
    public class BombDefusal : AbstractEventFilterModifier<OnExpireContext>
    {
        protected override OnExpireContext OnSuccess(OnExpireContext e)
        {
            e.ExplosionPower--;
            return e;
        }

        protected override OnExpireContext OnFailure(OnExpireContext e)
        {
            throw new System.NotImplementedException();
        }
    }   
}