using System;
using _Project.Runtime.Controllers.Physics;
using _Project.Runtime.Data;
using UnityEngine;

namespace _Project.Runtime.Controllers.Generators
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