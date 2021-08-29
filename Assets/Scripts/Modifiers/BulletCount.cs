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
            return $"Bullet Count {Strength}: Changes the amount of bullets fired.";
        }
    }
}