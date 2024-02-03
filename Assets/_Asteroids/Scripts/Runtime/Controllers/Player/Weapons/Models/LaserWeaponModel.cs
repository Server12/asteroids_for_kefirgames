using Asteroids.Runtime.Views;
using Asteroids.Runtime.Weapons.Data;
using UnityEngine;

namespace Asteroids.Runtime.Weapons.Models
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