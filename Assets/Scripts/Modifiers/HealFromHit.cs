namespace Modifiers
{
    public class HealFromHit : AbstractOnHitModifier
    {
        private const float DamagePercentHealedPerLevel = 0.1f;
        protected override OnExpireContext OnSuccess(OnExpireContext e)
        {
            var healthGain = e.Projectile.AllHitEntities.Count * e.Projectile.damage * DamagePercentHealedPerLevel * Strength;
            e.Projectile.Dad.Heal(healthGain);
            return e;
        }
    }
}