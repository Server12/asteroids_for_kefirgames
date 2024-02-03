using System;
using Asteroids.Runtime.Base;

namespace Asteroids.Runtime.UI
{
    public interface IMainUIController:IController
    {
        event Action OnStartPlayGame;
        
    }
}