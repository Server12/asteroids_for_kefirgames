using _Project.Runtime.Controllers.Physics.Data;
using _Project.Runtime.Data;

namespace _Project.Runtime.Controllers.Physics
{
    public interface ICollisionEntity
    {
        int Id { get; }

        CollisionType Type { get; }

        GameEntityTag Tag { get; }
    }
}