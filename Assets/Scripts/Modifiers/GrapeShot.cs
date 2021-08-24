namespace Modifiers
{
    public class GrapeShot : AbstractModifier<OnFireContext>
    {
        protected override void OnSuccess(OnFireContext e)
        {
            // TODO: set pierce value higher
            throw new System.NotImplementedException();
        }

        protected override void OnFailure(OnFireContext e)
        {
            // TODO: Set this up with different pools for different event types
            throw new System.NotImplementedException();
        }
    }
}