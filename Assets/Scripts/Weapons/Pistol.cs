using System;
using UnityEngine;

namespace Weapons
{
    public class Pistol : MonoBehaviour, IWeapon
    {
        public GameObject bulletPrefab;
            
        private const float FireRate = 0.46f; // time between bullets
        private float _lastFire = 0.0f; // time the gun was fired last
        
        private const int MaxAmmo = 6; // How full the mag can be
        private float _currentAmmo = 6; // how much ammo currently

        private const float ReloadTime = 3.883f; // how long it takes to reload gun (animation length)
        private float _lastReload = 0.0f; // last time the gun was reloaded

        public bool Fire(Transform spawnPoint)
        {
            if (!CanFire()) return false;
            _lastFire = Time.time;
            _currentAmmo--;

            Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);

            return true;
        }

        public bool Reload()
        {
            if (!CanReload()) return false;
            _lastReload = Time.time;
            _currentAmmo = MaxAmmo;
            return true;
        }

        private bool CanFire()
        {
            return _currentAmmo > 0 && !IsReloading() && !IsShooting();
        }

        private bool CanReload()
        {
            return _currentAmmo < MaxAmmo && !IsReloading() && !IsShooting();
        }

        private bool IsReloading()
        {
            return Time.time - _lastReload < ReloadTime;
        }

        private bool IsShooting()
        {
            return Time.time - _lastFire < FireRate;
        }
    }
}