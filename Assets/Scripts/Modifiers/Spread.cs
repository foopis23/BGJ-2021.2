namespace Modifiers
{
    public class Spread : AbstractBeforeFireModifier
    {
        private const float SpreadAmount = 2f;
        protected override BeforeFireContext OnSuccess(BeforeFireContext e)
        {
            e.Spread += SpreadAmount * Strength;
            return e;
        }

        public override string GetFlavorText()
        {
            return $"Spread {Strength}: Bullet spread angle.";
        }
    }
}