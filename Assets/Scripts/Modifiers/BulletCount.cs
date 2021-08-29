namespace Modifiers
{
    public class BulletCount : AbstractBeforeFireModifier
    {
        protected override BeforeFireContext OnSuccess(BeforeFireContext e)
        {
            e.BulletCount += Strength;
            return e;
        }

        public override string GetFlavorText()
        {
            return $"Bullet Count {Strength}: Increases the amount of bullets fired.";
        }
    }
}