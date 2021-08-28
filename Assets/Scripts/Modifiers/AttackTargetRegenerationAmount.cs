namespace Modifiers
{
    public class AttackTargetRegenerationAmount : AbstractOnHitModifier
    {
        protected override OnExpireContext OnSuccess(OnExpireContext e)
        {
            foreach (var entity in e.Projectile.AllHitEntities)
            {
                entity.ApplyStatusEffect(new StatusEffects.RegenerationAmount(Strength), 10.0f);
            }

            return e;
        }
    }
}