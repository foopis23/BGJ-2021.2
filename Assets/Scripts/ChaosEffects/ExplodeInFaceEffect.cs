using CallbackEvents;

namespace ChaosEffects
{
    public class ExplodeInFaceEffect : IOnShootEffect
    {
        public void OnTrigger(OnFireContext ctx)
        {
            EventSystem.Current.FireEvent(new ExplosionEventContext(1){Pos = ctx.Projectile.transform.position});
        }
    }
}