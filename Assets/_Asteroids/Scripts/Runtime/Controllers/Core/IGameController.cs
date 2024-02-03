using System;
using Asteroids.Runtime.Base;
using Asteroids.Runtime.Physics;
using UnityEngine;

namespace Asteroids.Runtime
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