namespace Modifiers
{
    public class StoppingPower : AbstractOnFireModifier
    {
        protected override void OnSuccess(OnFireContext e)
        {
            e.Projectile.pierces -= 4;
        }
    }
}