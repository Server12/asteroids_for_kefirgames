using System;
using Asteroids.Runtime.Data;
using Asteroids.Runtime.Views;
using UnityEngine;

namespace Asteroids.Runtime.Weapons.Data
{
    [Serializable]
    public class WeaponData
    {
        [SerializeField] private float shootCoolDown;
        [SerializeField] private float bulletSpeed;

        [SerializeField] private int _maxBullets;
        [SerializeField] private float _bulletChargeCooldown;
        
        [SerializeField] private PlayerWeapon _weaponType;
        public PlayerWeapon WeaponType => _weaponType;
        
        public int MaxBullets => _maxBullets;

        public float ShootCoolDown => shootCoolDown;
        
        
        public float BulletSpeed => bulletSpeed;

        public float BulletChargeCooldown => _bulletChargeCooldown;
        
    }
}