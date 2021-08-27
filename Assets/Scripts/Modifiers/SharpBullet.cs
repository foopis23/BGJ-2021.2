namespace Modifiers
{
    public class SharpBullet : AbstractEventModifier<OnFireContext>
    {
        protected override void OnSuccess(OnFireContext e)
        {
            e.Projectile.pierces += 4;
        }

        protected override void OnFailure(OnFireContext e)
        {
            // TODO: Set this up with different pools for different event types
            throw new System.NotImplementedException();
        }
    }
}