using System;
using _Project.Runtime.Controllers.Base;

namespace _Project.Runtime.UI
{
    public interface IGameOverPopup:IController
    {
        event Action OnPlayAgain;
    }
}