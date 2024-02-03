using Asteroids.Runtime.Physics.Data;

namespace Asteroids.Runtime.Physics
{
    public interface ICollisionAgent : ICollisionEntity
    {
        CollisionType CanCollideWith { get; }

        CollisionCheck CollisionCheck { get; }
        
        void OnCollidedWith(ICollisionEntity entity);
    }
}