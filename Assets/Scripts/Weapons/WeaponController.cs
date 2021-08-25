using UnityEngine;
using UnityEngine.Serialization;

namespace Weapons
{
    public class WeaponController : MonoBehaviour
    {
        public Transform bulletSpawnPoint;
        public GameObject weaponObject;
        public Animator weaponAnimator;
        private IWeapon _weapon;

        public void Start()
        {
            _weapon = weaponObject.GetComponent<IWeapon>();

            if (weaponAnimator == null)
            {
                weaponAnimator = weaponObject.GetComponent<Animator>();
            }
        }

        public void Update()
        {
            if (_weapon == null)
            {
                return;
            }
            
            if (Input.GetButtonDown("Fire1") && _weapon.Fire(bulletSpawnPoint))
            {
                weaponAnimator.Play("Fire");
            }

            if (Input.GetButtonDown("Reload") && _weapon.Reload())
            {
                weaponAnimator.Play("Reload");
            }
        }
    }
}