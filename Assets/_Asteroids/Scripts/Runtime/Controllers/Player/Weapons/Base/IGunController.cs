using System;
using Asteroids.Runtime.Weapons.Models;

namespace Asteroids.Runtime.Weapons
{
    public interface IGunController : IInitializable, IDisposable, IUpdate
    {
        IWeaponModel WeaponModel { get; }

        void ResetState();
        
        void Shoot();
    }
}