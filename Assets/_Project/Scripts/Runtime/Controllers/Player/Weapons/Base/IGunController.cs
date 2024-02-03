using System;
using _Project.Runtime.Controllers.Weapons.Models;

namespace _Project.Runtime.Controllers.Weapons
{
    public interface IGunController : IInitializable, IDisposable, IUpdate
    {
        IWeaponModel WeaponModel { get; }

        void ResetState();
        
        void Shoot();
    }
}