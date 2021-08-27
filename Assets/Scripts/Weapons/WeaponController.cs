using CallbackEvents;
using UnityEngine;
using UnityEngine.Serialization;

namespace Weapons
{
    public class WeaponController : MonoBehaviour
    {
        public Animator shootAnimator;
        public Animator reloadAnimator;
        public Animator equipAnimator;
        
        public Transform bulletSpawnPoint;
        public GameObject weaponObject;
        private IWeapon _weapon;

        private bool _enabled = true;
        private bool _queuedFire = false;
        private bool _queuedReload = false;

        public void Start()
        {
            _weapon = weaponObject.GetComponent<IWeapon>();
            reloadAnimator.gameObject.SetActive(false);
            shootAnimator.gameObject.SetActive(true);
            equipAnimator.gameObject.SetActive(false);
            
            EventSystem.Current.RegisterEventListener<OnPlayerDeathContext>((e) => _enabled = false);
        }

        public void Update()
        {
            if (!_enabled) return;
            if (_weapon == null) return;

            if ((Input.GetButtonDown("Fire1") || _queuedFire) && _weapon.CanFire())
            {
                if(_weapon.IsBusy())
                {
                    _queuedFire = true;
                }
                else
                {
                    _weapon.Fire(bulletSpawnPoint);
                    reloadAnimator.gameObject.SetActive(false);
                    shootAnimator.gameObject.SetActive(true);
                    equipAnimator.gameObject.SetActive(false);
                    shootAnimator.Play("shoot");
                    _queuedFire = false;
                }
            }

            if ((Input.GetButtonDown("Reload") || _queuedReload) && _weapon.CanReload())
            {
                if(_weapon.IsBusy())
                {
                    _queuedReload = true;
                }
                else
                {
                    _weapon.Reload();
                    reloadAnimator.gameObject.SetActive(true);
                    shootAnimator.gameObject.SetActive(false);
                    equipAnimator.gameObject.SetActive(false);
                    reloadAnimator.Play("reload");
                    _queuedReload = false;
                }
            }
        }
    }
}