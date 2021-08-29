using UnityEngine;
using CallbackEvents;
using Weapons;
using Random = UnityEngine.Random;

namespace Weapons
{
    public class Pistol : MonoBehaviour, IWeapon
    {
        public int maxAmmo = 6; // How full the mag can be
        public float fireRate = 0.46f; // time between bullets
        public float reloadTime = 3.883f; // how long it takes to reload gun (animation length)
        public int baseBulletCount = 1;
        public float baseSpread = 0.0f;

        public GameObject bulletPrefab;

        private float _lastFire = 0.0f; // time the gun was fired last
        private float _lastReload = 0.0f; // last time the gun was reloaded

        public float currentAmmo { get; private set; } // how much ammo currently

        private void Start()
        {
            currentAmmo = maxAmmo;
        }

        public bool Fire(Transform spawnPoint, LivingEntity shooter)
        {
            if (!CanFire() || IsBusy()) return false;
            _lastFire = Time.time;
            currentAmmo--;

            var filtered = EventSystem.Current.FireFilter<BeforeFireContext>(
                new BeforeFireContext(this){BulletCount =  baseBulletCount, Spread = baseSpread, Shooter = shooter}
                );

            var bulletCount = Mathf.Max(baseBulletCount, filtered.BulletCount);
            var spread = Mathf.Max(0, filtered.Spread);

            for (var i=0; i < bulletCount; i++)
            {
                var randomSpread = Quaternion.Euler(
                    Random.value * spread - (spread * 0.5f),
                    Random.value * spread - (spread * 0.5f),
                    Random.value * spread - (spread * 0.5f)
                );
                var forward = randomSpread * spawnPoint.forward;
                
                var obj = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
                obj.transform.forward = forward;
                obj.GetComponent<Projectile>().Dad = shooter;
            }

            return true;
        }

        public bool Reload()
        {
            if (!CanReload() || IsBusy()) return false;
            _lastReload = Time.time;
            currentAmmo = maxAmmo;
            return true;
        }

        public bool CanFire()
        {
            return currentAmmo > 0;
        }

        public bool CanReload()
        {
            return currentAmmo < maxAmmo;
        }

        public bool IsBusy()
        {
            return Time.time - _lastReload < reloadTime || Time.time - _lastFire < fireRate;
        }
    }
}

public class BeforeFireContext : EventContext
{
    public LivingEntity Shooter;
    public readonly IWeapon Weapon;
    public int BulletCount;
    public float Spread;

    public BeforeFireContext(IWeapon weapon)
    {
        Weapon = weapon;
    }
}