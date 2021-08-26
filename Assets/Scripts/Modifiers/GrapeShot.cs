namespace Modifiers
{
    public class GrapeShot : AbstractEventFilterModifier<BeforeFireContext>
    {
        protected override BeforeFireContext OnSuccess(BeforeFireContext e)
        {
            e.Scatter += 8;
            return e;
        }

        protected override BeforeFireContext OnFailure(BeforeFireContext e)
        {
            throw new System.NotImplementedException();
        }
    }
}