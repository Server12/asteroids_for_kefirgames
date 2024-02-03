using Asteroids.Runtime.Views;
using Asteroids.Runtime.Weapons.Data;
using UnityEngine;

namespace Asteroids.Runtime.Weapons.Models
{
    public class GunWeaponModel:BaseWeaponModel
    {
        public readonly BulletView BulletViewPrefab;
        public readonly Transform Spawner;

        public GunWeaponModel(BulletView bulletViewPrefab,WeaponData data,Transform spawner) : base(data)
        {
            BulletViewPrefab = bulletViewPrefab;
            Spawner = spawner;
        }
    }
}