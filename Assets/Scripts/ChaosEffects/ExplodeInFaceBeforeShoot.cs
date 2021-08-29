using CallbackEvents;
using Weapons;

namespace ChaosEffects
{
    public class ExplodeInFaceBeforeShoot : IOnBeforeShootEffect
    {
        public void OnTrigger(BeforeFireContext ctx)
        {
            var obj = ((Pistol) ctx.Weapon).gameObject;
            EventSystem.Current.FireEvent(new ExplosionEventContext(1){Pos = obj.transform.position});
        }
    }
}