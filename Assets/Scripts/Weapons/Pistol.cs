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

        private float _currentAmmo; // how much ammo currently
        private float _lastFire = 0.0f; // time the gun was fired last
        private float _lastReload = 0.0f; // last time the gun was reloaded

        private void Start()
        {
            _currentAmmo = maxAmmo;
        }

        public bool Fire(Transform spawnPoint)
        {
            if (!CanFire()) return false;
            _lastFire = Time.time;
            _currentAmmo--;

            var filtered = EventSystem.Current.FireFilter<BeforeFireContext>(
                new BeforeFireContext(this){BulletCount =  baseBulletCount, Spread = baseSpread}
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
            }

            return true;
        }

        public bool Reload()
        {
            if (!CanReload()) return false;
            _lastReload = Time.time;
            _currentAmmo = maxAmmo;
            return true;
        }

        private bool CanFire()
        {
            return _currentAmmo > 0 && !IsReloading() && !IsShooting();
        }

        private bool CanReload()
        {
            return _currentAmmo < maxAmmo && !IsReloading() && !IsShooting();
        }

        private bool IsReloading()
        {
            return Time.time - _lastReload < reloadTime;
        }

        private bool IsShooting()
        {
            return Time.time - _lastFire < fireRate;
        }
    }
}

public class BeforeFireContext : EventContext
{
    public readonly IWeapon Weapon;
    public int BulletCount;
    public float Spread;

    public BeforeFireContext(IWeapon weapon)
    {
        Weapon = weapon;
    }
}