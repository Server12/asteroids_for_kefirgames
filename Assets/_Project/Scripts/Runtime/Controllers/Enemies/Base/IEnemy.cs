using _Project.Runtime.Controllers.Physics;
using _Project.Runtime.Views;

namespace _Project.Runtime.Controllers
{
    public interface IEnemy:IUpdate,ICollisionAgent
    {

        void Release();
        
        void Destroy();
    }
}