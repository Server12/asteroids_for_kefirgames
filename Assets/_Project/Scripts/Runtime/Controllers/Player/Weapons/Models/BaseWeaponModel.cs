using _Project.Runtime.Controllers.Weapons.Data;
using UnityEngine;

namespace _Project.Runtime.Controllers.Weapons.Models
{
    public class BaseWeaponModel : IWeaponModel, IUpdate
    {
        public WeaponData Data { get; set; }

        public GunState State => _state;

        public float BulletsChargeTimer => _bulletsChargeTimer;

        public int BulletsCounter => _bulletsCounter;

        private GunState _state = GunState.Ready;

        private float _coolDownTimer;

        private int _bulletsCounter;
        private float _bulletsChargeTimer;
        private readonly int _maxBullets;

        private readonly float _maxBulletsChargeCooldown;

        protected BaseWeaponModel(WeaponData data)
        {
            Data = data;
            _maxBullets = data.MaxBullets <= 0 ? -1 : data.MaxBullets;
            _bulletsCounter = _maxBullets;
            _maxBulletsChargeCooldown = data.BulletChargeCooldown;
        }

        public void Reset()
        {
            _coolDownTimer = 0;
            _state = GunState.Ready;
        }

        public void Shoot()
        {
            if (_bulletsCounter <= 0) return;
            _state = GunState.Charging;
            _bulletsCounter--;
        }

        public bool CanShoot()
        {
            if (_coolDownTimer > 0 || _bulletsCounter == 0) return false;
            _coolDownTimer = Data.ShootCoolDown;
            return true;
        }

        private void UpdateChargeTimer(float deltaTime)
        {
            if (_bulletsCounter < _maxBullets)
            {
                _bulletsChargeTimer += deltaTime;
                if (_bulletsChargeTimer >= _maxBulletsChargeCooldown)
                {
                    _state = GunState.Ready;
                    _bulletsCounter++;
                }

                _bulletsChargeTimer = Mathf.Repeat(_bulletsChargeTimer, _maxBulletsChargeCooldown);
            }
        }

        public void Update(float deltaTime)
        {
            if (_coolDownTimer > 0)
            {
                _coolDownTimer -= deltaTime;
            }

            UpdateChargeTimer(deltaTime);
        }
    }
}