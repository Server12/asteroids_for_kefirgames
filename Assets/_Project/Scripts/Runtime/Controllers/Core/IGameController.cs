using System;
using _Project.Runtime.Controllers.Base;
using _Project.Runtime.Controllers.Physics;
using UnityEngine;

namespace _Project.Runtime.Controllers
{
    public interface IGameController
    {
        event Action OnGameStarted;

        event Action OnGameOver;
        
        Camera Camera { get; }
        
        ICollisionsManager CollisionsManager { get; }
        
        T GetController<T>() where T : IController;
    }
}