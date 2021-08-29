namespace ChaosEffects
{
    public class ExtraBounceEffect : IOnShootEffect
    {
        public void OnTrigger(OnFireContext ctx)
        {
            ctx.Projectile.bounces += 1;
        }
    }
}