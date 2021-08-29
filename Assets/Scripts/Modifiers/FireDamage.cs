using TickDamage;

namespace Modifiers
{
    public class FireDamage : AbstractOnHitModifier
    {
        private const float DurationPerLevel = 5.0f;
        protected override OnExpireContext OnSuccess(OnExpireContext e)
        {
            foreach (var entity in e.Projectile.AllHitEntities)
            {
                entity.ApplyTickStatus(TickType.Fire, DurationPerLevel);
            }

            return e;
        }

        public override string GetFlavorText()
        {
            return $"Fire Damage {Strength}: Ignite target on hit.";
        }
    }
}