using _Project.Runtime.Controllers.Physics.Data;

namespace _Project.Runtime.Controllers.Physics
{
    public interface ICollisionAgent : ICollisionEntity
    {
        CollisionType CanCollideWith { get; }

        CollisionCheck CollisionCheck { get; }
        
        void OnCollidedWith(ICollisionEntity entity);
    }
}