namespace Modifiers
{
    public class SharpBullet : AbstractOnFireModifier
    {
        protected override void OnSuccess(OnFireContext e)
        {
            e.Projectile.pierces += 4;
        }
    }
}