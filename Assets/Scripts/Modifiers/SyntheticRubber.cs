namespace Modifiers
{
    public class SyntheticRubber : AbstractModifier<OnFireContext>
    {
        protected override void OnSuccess(OnFireContext e)
        {
            e.Projectile.bounces -= 3;
        }

        protected override void OnFailure(OnFireContext e)
        {
            // TODO: Set this up with different pools for different event types
            throw new System.NotImplementedException();
        }
    }
}