using System;

namespace _Project.Runtime.Controllers.Physics.Data
{
    [Flags]
    public enum CollisionType : Int64
    {
        None = 0,
        Player = 1 << 0,
        Enemy = 1 << 1,
        Bullet = 1 << 2,
        Laser = 1 << 3,
        Weapons = Bullet | Laser
    }
}