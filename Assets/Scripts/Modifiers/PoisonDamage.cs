using TickDamage;

namespace Modifiers
{
    public class PoisonDamage : AbstractOnHitModifier
    {
        private const float DurationPerLevel = 10;
        protected override OnExpireContext OnSuccess(OnExpireContext e)
        {
            foreach (var entity in e.Projectile.AllHitEntities)
            {
                entity.ApplyTickStatus(TickType.Poison, DurationPerLevel);
            }

            return e;
        }

        public override string GetFlavorText()
        {
            return $"Poison Damage {Strength}: Inflict poison on hit";
        }
    }
}