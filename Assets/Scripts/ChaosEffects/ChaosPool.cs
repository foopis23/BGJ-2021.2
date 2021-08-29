using UnityEngine;

namespace ChaosEffects
{
    public static class ChaosPool
    {
        private static IOnShootEffect[] _onShootEffects;
        private static IOnExpireEffect[] _onHitEffects;
        private static IOnPassiveEffect[] _onPassiveEffects;
        private static IOnBeforeShootEffect[] _onBeforeShootEffects;
        private static bool _inited = false;

        private static void Init()
        {
            _inited = true;
            _onShootEffects = new IOnShootEffect[]{new ExplodeInFaceEffect(), new BurstIntoFlames(), new SlownessEffectOnShoot(), new PoisonedEffectOnShoot(), new ExtraBounceEffect()};
            _onHitEffects = new IOnExpireEffect[] { new BurstIntoFlamesOnHit(), new SlownessEffectOnExpire(), new PoisonedEffectOnHit()};
            _onPassiveEffects = new IOnPassiveEffect[]{ new BurstIntoFlamesPassive(), new SlownessEffectOnPassive(), new PoisonedEffectOnPassive()};
            _onBeforeShootEffects = new IOnBeforeShootEffect[] { new ExplodeInFaceBeforeShoot(), new SlownessEffectBeforeShoot(), new BurstIntoFlameBeforeShoot(), new PoisonedEffectBeforeShoot() };
        }

        public static void OnFail(OnFireContext epicFail)
        {
            if (!_inited) Init();
            if (_onShootEffects.Length < 1) return;
            
            var rand = Random.Range(0, _onShootEffects.Length);
            _onShootEffects[rand].OnTrigger(epicFail);
        }

        public static void OnFail(OnExpireContext epicFail)
        {
            if (!_inited) Init();
            if (_onHitEffects.Length < 1) return;
            
            var rand = Random.Range(0, _onHitEffects.Length);
            _onHitEffects[rand].OnTrigger(epicFail);
        }

        public static void OnFail(OnPlayerPassiveModifierTick epicFail)
        {
            if (!_inited) Init();
            if (_onPassiveEffects.Length < 1) return;
            
            var rand = Random.Range(0, _onPassiveEffects.Length);
            _onPassiveEffects[rand].OnTrigger(epicFail);
        }

        public static void OnFail(BeforeFireContext epicFail)
        {
            if (!_inited) Init();
            if (_onBeforeShootEffects.Length < 1) return;

            var rand = Random.Range(0, _onBeforeShootEffects.Length);
        } 
    }
}