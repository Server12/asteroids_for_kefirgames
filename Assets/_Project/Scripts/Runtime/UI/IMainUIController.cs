using System;
using _Project.Runtime.Controllers.Base;

namespace _Project.Runtime.UI
{
    public interface IMainUIController:IController
    {
        event Action OnStartPlayGame;
        
    }
}