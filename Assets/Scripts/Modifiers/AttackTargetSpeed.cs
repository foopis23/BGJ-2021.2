using StatusEffects;

namespace Modifiers
{
    public class AttackTargetSpeed : AbstractOnHitModifier
    {
        protected override OnExpireContext OnSuccess(OnExpireContext e)
        {
            foreach (var entity in e.Projectile.AllHitEntities)
            {
                entity.ApplyStatusEffect(new SpeedStatusEffect(Strength), 10.0f);
            }

            return e;
        }

        public override string GetFlavorText()
        {
            return $"Attack Target Speed {Strength}: Sets movement speed for all hit targets.";
        }
    }
}