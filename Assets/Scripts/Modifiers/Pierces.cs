namespace Modifiers
{
    public class Pierces : AbstractOnFireModifier
    {
        protected override void OnSuccess(OnFireContext e)
        {
            e.Projectile.pierces += Strength;
        }
    }
}