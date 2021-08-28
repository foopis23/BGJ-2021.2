using CallbackEvents;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;

namespace Weapons
{
    public class WeaponController : MonoBehaviour
    {
        // editor fields
        public float inputBufferDuration;
        public Animator shootAnimator;
        public Animator reloadAnimator;
        public Animator equipAnimator;
        public Transform bulletSpawnPoint;
        public GameObject weaponObject;
        public TextMeshProUGUI ammoText;

        // private fields
        private IWeapon _weapon;
        private bool _enabled = true;
        private float _queuedFireTime;
        private float _queuedReloadTime;

        public void Start()
        {
            _weapon = weaponObject.GetComponent<IWeapon>();
            reloadAnimator.gameObject.SetActive(false);
            shootAnimator.gameObject.SetActive(true);
            equipAnimator.gameObject.SetActive(false);
            ammoText.text = _weapon.currentAmmo.ToString();
            
            EventSystem.Current.RegisterEventListener<OnPlayerDeathContext>((e) => _enabled = false);
        }

        public void Update()
        {
            if (!_enabled) return;
            if (_weapon == null) return;

            if ((Input.GetButtonDown("Fire1") || Time.time - _queuedFireTime < inputBufferDuration) && _weapon.CanFire())
            {
                if(_weapon.IsBusy())
                {
                    if(Input.GetButtonDown("Fire1"))
                    {
                        _queuedFireTime = Time.time;
                    }
                }
                else
                {
                    _weapon.Fire(bulletSpawnPoint);
                    reloadAnimator.gameObject.SetActive(false);
                    shootAnimator.gameObject.SetActive(true);
                    equipAnimator.gameObject.SetActive(false);
                    shootAnimator.Play("shoot");
                    ammoText.text = _weapon.currentAmmo.ToString();
                    if(!_weapon.CanFire())
                    {
                        _queuedReloadTime = 1000000;
                    }

                    _queuedFireTime = 0;
                }
            }

            if ((Input.GetButtonDown("Reload") || Time.time - _queuedReloadTime < inputBufferDuration) && _weapon.CanReload())
            {
                if(_weapon.IsBusy())
                {
                    if(Input.GetButtonDown("Reload"))
                    {
                        _queuedReloadTime = Time.time;
                    }
                }
                else
                {
                    _weapon.Reload();
                    reloadAnimator.gameObject.SetActive(true);
                    shootAnimator.gameObject.SetActive(false);
                    equipAnimator.gameObject.SetActive(false);
                    reloadAnimator.Play("reload");
                    EventSystem.Current.CallbackAfter(() => {
                        ammoText.text = _weapon.currentAmmo.ToString();
                    }, 2600);

                    _queuedReloadTime = 0;
                }
            }
        }
    }
}