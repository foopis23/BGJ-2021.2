namespace Modifiers
{
    public class ExplosionPower : AbstractEventFilterModifier<OnExpireContext>
    {
        public ExplosionPower(int strength) : base(strength) {}

        protected override OnExpireContext OnSuccess(OnExpireContext e)
        {
            e.ExplosionPower += _strength;
            return e;
        }

        protected override OnExpireContext OnFailure(OnExpireContext e)
        {
            throw new System.NotImplementedException();
        }
    }   
}