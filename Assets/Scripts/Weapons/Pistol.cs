using System;
using UnityEngine;
using CallbackEvents;
using Weapons;

namespace Weapons
{
    public class Pistol : MonoBehaviour, IWeapon
    {
        public int maxAmmo = 6; // How full the mag can be
        public float fireRate = 0.46f; // time between bullets
        public float reloadTime = 3.883f; // how long it takes to reload gun (animation length)
        public float scatterOffset = 0.5f;
        public GameObject bulletPrefab;

        private float _currentAmmo; // how much ammo currently
        private float _lastFire = 0.0f; // time the gun was fired last
        private float _lastReload = 0.0f; // last time the gun was reloaded

        void Start()
        {
            _currentAmmo = maxAmmo;
        }

        public bool Fire(Transform spawnPoint)
        {
            if (!CanFire()) return false;
            _lastFire = Time.time;
            _currentAmmo--;

            var filtered = EventSystem.Current.FireFilter<BeforeFireContext>(new BeforeFireContext(this){Scatter =  0});

            for (var i=0; i < filtered.Scatter; i++)
            {
                var rotation = spawnPoint.rotation;
                var eulerAngles = rotation.eulerAngles;
                var position = spawnPoint.position;
                
                var bulletOffset = (i - ((float) i / 2)) * scatterOffset;
                var x = Mathf.Cos(eulerAngles.y) * bulletOffset;
                var z = Mathf.Sin(eulerAngles.y) * bulletOffset;
                
                Instantiate(bulletPrefab, new Vector3(position.x + x, position.y, position.z + z), rotation);
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
    public int Scatter;

    public BeforeFireContext(IWeapon weapon)
    {
        Weapon = weapon;
    }
}