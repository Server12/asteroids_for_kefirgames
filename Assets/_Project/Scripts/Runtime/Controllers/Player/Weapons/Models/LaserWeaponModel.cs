using _Project.Runtime.Controllers.Weapons.Data;
using _Project.Runtime.Views;
using UnityEngine;

namespace _Project.Runtime.Controllers.Weapons.Models
{
    public class LaserWeaponModel:BaseWeaponModel
    {
        public readonly LaserView LaserViewPrefab;
        public readonly Transform Spawner;

        public LaserWeaponModel(LaserView laserViewPrefab, WeaponData data,Transform spawner) : base(data)
        {
            LaserViewPrefab = laserViewPrefab;
            Spawner = spawner;
        }
    }
}