using Asteroids.Runtime.Views;
using Asteroids.Runtime.Physics;

namespace Asteroids.Runtime
{
    public interface IEnemy:IUpdate,ICollisionAgent
    {

        void Release();
        
        void Destroy();
    }
}