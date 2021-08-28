namespace Modifiers
{
    public class BulletCount : AbstractEventFilterModifier<BeforeFireContext>
    {
        public BulletCount(int strength) : base(strength) {}

        protected override BeforeFireContext OnSuccess(BeforeFireContext e)
        {
            e.BulletCount += _strength;
            return e;
        }

        protected override BeforeFireContext OnFailure(BeforeFireContext e)
        {
            throw new System.NotImplementedException();
        }
    }
}