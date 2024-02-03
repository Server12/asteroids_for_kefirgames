namespace _Project.Runtime.Controllers.Physics
{
    public interface ICollisionsManager
    {
        void AddPhysicsAgent(ICollisionAgent agent);

        void RemovePhysicsAgent(ICollisionAgent agent);
    }
}