namespace Asteroids.Runtime.Physics
{
    public interface ICollisionsManager
    {
        void AddPhysicsAgent(ICollisionAgent agent);

        void RemovePhysicsAgent(ICollisionAgent agent);
    }
}