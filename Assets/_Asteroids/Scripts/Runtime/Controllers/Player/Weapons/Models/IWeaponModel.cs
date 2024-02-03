using Asteroids.Runtime.Weapons.Data;

namespace Asteroids.Runtime.Weapons.Models
{
    public interface IWeaponModel
    {
        GunState State { get; }

        int BulletsCounter { get; }

        float BulletsChargeTimer { get; }

        WeaponData Data { get; }
    }
}