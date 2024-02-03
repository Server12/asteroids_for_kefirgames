using System;
using Asteroids.Runtime.Data;
using Asteroids.Runtime.Physics;
using UnityEngine;

namespace Asteroids.Runtime.Generators
{
    public interface IEnemiesController : IUpdate, IDisposable
    {
        event Action OnReadyToGenerate;
        event Action<GameEnemy> OnEnemyHitted;

        ICollisionsManager CollisionsManager { set; }
        Camera Camera { set; }

        void Generate(bool clear = false);
        
    }
}