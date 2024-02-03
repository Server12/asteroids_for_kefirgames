using System;
using _Project.Runtime.Data;
using _Project.Runtime.Views;
using UnityEngine;

namespace _Project.Runtime.Controllers.Weapons.Data
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