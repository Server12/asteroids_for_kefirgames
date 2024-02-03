using System;

namespace _Project.Runtime.Controllers.Base
{
    public interface IController
    {
       IGameController GameController { set; }
    }
}