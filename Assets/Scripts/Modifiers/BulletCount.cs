namespace Modifiers
{
    public class BulletCount : AbstractEventFilterModifier<BeforeFireContext>
    {
        protected override BeforeFireContext OnSuccess(BeforeFireContext e)
        {
            e.BulletCount += Strength;
            return e;
        }

        protected override BeforeFireContext OnFailure(BeforeFireContext e)
        {
            throw new System.NotImplementedException();
        }

        public override string GetFlavorText()
        {
            return $"Bullet Count {Strength}: Increases the amount of bullets fired.";
        }
    }
}