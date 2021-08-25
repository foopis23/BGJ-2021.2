using UnityEngine;

namespace Weapons
{
    public interface IWeapon
    {
        // attempt to fire the weapon, the weapon will handle the fire rate logic and shit
        public bool Fire(Transform spawnPoint);
        
        // attempt a reload, the gun will handle if the reload is started
        public bool Reload();
    }
}