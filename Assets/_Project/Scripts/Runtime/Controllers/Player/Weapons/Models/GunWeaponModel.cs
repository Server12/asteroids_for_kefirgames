using _Project.Runtime.Controllers.Weapons.Data;
using _Project.Runtime.Views;
using UnityEngine;

namespace _Project.Runtime.Controllers.Weapons.Models
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