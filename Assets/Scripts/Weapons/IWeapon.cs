using UnityEngine;

namespace Weapons
{
    public interface IWeapon
    {
        // attempt to fire the weapon, the weapon will handle the fire rate logic and shit
        public bool Fire(Transform spawnPoint, LivingEntity shooter);
        
        // attempt a reload, the gun will handle if the reload is started
        public bool Reload();

        public bool CanFire(); // returns true if the gun is able to fire (after any cooldowns end)
        public bool CanReload(); // returns true if the gun is able to reload (after an cooldowns end)
        public bool IsBusy(); // returns true if a cooldown is currently happening

        public float currentAmmo { get; }
    }
}