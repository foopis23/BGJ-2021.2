using TickDamage;
using Weapons;

namespace ChaosEffects
{
    public class BurstIntoFlames : IOnShootEffect
    {
        public void OnTrigger(OnFireContext ctx)
        {
            ctx.Projectile.Dad.ApplyTickStatus(TickType.Fire, 5.0f);
        }
    }

    public class BurstIntoFlamesPassive : IOnPassiveEffect
    {
        public void OnTrigger(OnPlayerPassiveModifierTick ctx)
        {
            ctx.Player.ApplyTickStatus(TickType.Fire, 5.0f);
        }
    }

    public class BurstIntoFlamesOnHit : IOnExpireEffect
    {
        public void OnTrigger(OnExpireContext ctx)
        {
            ctx.Projectile.Dad.ApplyTickStatus(TickType.Fire, 5.0f);
        }
    }

    public class BurstIntoFlameBeforeShoot : IOnBeforeShootEffect
    {
        public void OnTrigger(BeforeFireContext ctx)
        {
            ctx.Shooter.ApplyTickStatus(TickType.Fire, 5.0f);
        }
    }
}