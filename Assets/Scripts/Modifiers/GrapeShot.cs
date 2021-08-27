namespace Modifiers
{
    public class GrapeShot : AbstractEventFilterModifier<BeforeFireContext>
    {
        protected override BeforeFireContext OnSuccess(BeforeFireContext e)
        {
            e.BulletCount += 2;
            e.Spread += 5;
            return e;
        }

        protected override BeforeFireContext OnFailure(BeforeFireContext e)
        {
            throw new System.NotImplementedException();
        }
    }
}