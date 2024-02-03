using _Project.Runtime.Controllers.Weapons.Models;
using _Project.Runtime.Data;

namespace _Project.Runtime.Controllers.Weapons
{
    public interface IWeaponsManager
    {
        IWeaponModel GetWeaponModel(PlayerWeapon weapon);
    }
}