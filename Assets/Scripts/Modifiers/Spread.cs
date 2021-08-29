namespace Modifiers
{
    public class Spread : AbstractEventFilterModifier<BeforeFireContext>
    {
        private const float SpreadAmount = 2f;
        protected override BeforeFireContext OnSuccess(BeforeFireContext e)
        {
            e.Spread += SpreadAmount * Strength;
            return e;
        }

        protected override BeforeFireContext OnFailure(BeforeFireContext e)
        {
            throw new System.NotImplementedException();
        }

        public override string GetFlavorText()
        {
            return $"Spread {Strength}: Bullet Spread Angle.";
        }
    }
}