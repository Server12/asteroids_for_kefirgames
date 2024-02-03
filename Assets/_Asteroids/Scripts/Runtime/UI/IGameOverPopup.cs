using System;
using Asteroids.Runtime.Base;

namespace Asteroids.Runtime.UI
{
    public interface IGameOverPopup:IController
    {
        event Action OnPlayAgain;
    }
}