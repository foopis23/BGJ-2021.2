using CallbackEvents;
using UnityEngine;

namespace ChaosEffects
{
    public interface IChaosEffect<T> where T : EventContext
    {
        public void OnTrigger(T ctx);
    }

    public interface IOnShootEffect : IChaosEffect<OnFireContext> {}
    public interface IOnExpireEffect : IChaosEffect<OnExpireContext> {}
    public interface IOnPassiveEffect : IChaosEffect<FailedStatusCheckContext> {}
    
    public class ExplodeInFaceEffect : IOnShootEffect
    {
        public void OnTrigger(OnFireContext ctx)
        {
            EventSystem.Current.FireEvent(new ExplosionEventContext(4){Pos = ctx.Projectile.transform.position});
        }
    }

    public static class ChaosPool
    {
        private static IOnShootEffect[] _onShootEffects;
        private static IOnExpireEffect[] _onHitEffects;
        private static IOnPassiveEffect[] _onPassiveEffects;


        public static void Init()
        {
            _onShootEffects = new IOnShootEffect[]{new ExplodeInFaceEffect()};
            _onHitEffects = new IOnExpireEffect[] { };
            _onPassiveEffects = new IOnPassiveEffect[]{ };
        }

        public static void OnEffectFail(OnFireContext epicFail)
        {
            var rand = Random.Range(0, _onShootEffects.Length);
            _onShootEffects[rand].OnTrigger(epicFail);
        }

        public static void OnEffectFail(OnExpireContext epicFail)
        {
            var rand = Random.Range(0, _onHitEffects.Length);
            _onHitEffects[rand].OnTrigger(epicFail);
        }

        public static void OnFail(FailedStatusCheckContext epicFail)
        {
            var rand = Random.Range(0, _onPassiveEffects.Length);
            _onPassiveEffects[rand].OnTrigger(epicFail);
        }
    }

    public class FailedStatusCheckContext : EventContext
    {
        public Player Player;
    }
}