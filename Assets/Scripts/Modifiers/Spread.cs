namespace Modifiers
{
    public class Spread : AbstractEventFilterModifier<BeforeFireContext>
    {
        public Spread(int strength) : base(strength) {}

        protected override BeforeFireContext OnSuccess(BeforeFireContext e)
        {
            e.Spread += _strength;
            return e;
        }

        protected override BeforeFireContext OnFailure(BeforeFireContext e)
        {
            throw new System.NotImplementedException();
        }
    }
}