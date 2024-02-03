using _Project.Runtime.Controllers.Weapons.Data;

namespace _Project.Runtime.Controllers.Weapons.Models
{
    public interface IWeaponModel
    {
        GunState State { get; }

        int BulletsCounter { get; }

        float BulletsChargeTimer { get; }

        WeaponData Data { get; }
    }
}