using Asteroids.Runtime.Data;
using Asteroids.Runtime.Physics.Data;

namespace Asteroids.Runtime.Physics
{
    public interface ICollisionEntity
    {
        int Id { get; }

        CollisionType Type { get; }

        GameEntityTag Tag { get; }
    }
}