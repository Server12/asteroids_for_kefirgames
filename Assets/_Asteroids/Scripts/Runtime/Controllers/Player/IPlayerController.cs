using System;
using Asteroids.Runtime.Base;
using Asteroids.Runtime.Weapons;
using UnityEngine;

namespace Asteroids.Runtime
{
    public interface IPlayerController:IController
    {
        event Action OnKilled;
        
        bool IsKilled { get; }
        
        Vector3 Position { get; }
        
        float CurrentRotation { get; }
        
        float MaxSpeed { get; }
        
        Vector2 Velocity { get; }
        
        IWeaponsManager WeaponsManager { get; }
    }
}