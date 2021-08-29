using StatusEffects;

namespace ChaosEffects
{
    public class SlownessEffectOnShoot : IOnShootEffect
    {
        public void OnTrigger(OnFireContext ctx)
        {
            ctx.Projectile.Dad.ApplyStatusEffect(new SpeedStatusEffect(-1), 5.0f);
        }
    }

    public class SlownessEffectOnPassive : IOnPassiveEffect
    {
        public void OnTrigger(OnPlayerPassiveModifierTick ctx)
        {
            ctx.Player.ApplyStatusEffect(new SpeedStatusEffect(-1), 5.0f);
        }
    }

    public class SlownessEffectOnExpire : IOnExpireEffect
    {
        public void OnTrigger(OnExpireContext ctx)
        {
            ctx.Projectile.Dad.ApplyStatusEffect(new SpeedStatusEffect(-1), 5.0f);
        }
    }

    public class SlownessEffectBeforeShoot : IOnBeforeShootEffect
    {
        public void OnTrigger(BeforeFireContext ctx)
        {
            ctx.Shooter.ApplyStatusEffect(new SpeedStatusEffect(-1), 5.0f);
        }
    }
}