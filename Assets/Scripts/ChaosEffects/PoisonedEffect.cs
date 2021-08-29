using TickDamage;

namespace ChaosEffects
{
    public class PoisonedEffectOnShoot : IOnShootEffect
    {
        public void OnTrigger(OnFireContext ctx)
        {
            ctx.Projectile.Dad.ApplyTickStatus(TickType.Poison, 5.0f);
        }
    }
    
    public class PoisonedEffectOnPassive : IOnPassiveEffect
    {
        public void OnTrigger(OnPlayerPassiveModifierTick ctx)
        {
            ctx.Player.ApplyTickStatus(TickType.Poison, 5.0f);
        }
    }
    
    public class PoisonedEffectOnHit : IOnExpireEffect
    {
        public void OnTrigger(OnExpireContext ctx)
        {
            ctx.Projectile.Dad.ApplyTickStatus(TickType.Poison, 5.0f);
        }
    }
    
    public class PoisonedEffectBeforeShoot : IOnBeforeShootEffect
    {
        public void OnTrigger(BeforeFireContext ctx)
        {
            ctx.Shooter.ApplyTickStatus(TickType.Poison, 5.0f);
        }
    }
}