using Asteroids.Runtime.Data;
using Asteroids.Runtime.Weapons.Models;

namespace Asteroids.Runtime.Weapons
{
    public interface IWeaponsManager
    {
        IWeaponModel GetWeaponModel(PlayerWeapon weapon);
    }
}