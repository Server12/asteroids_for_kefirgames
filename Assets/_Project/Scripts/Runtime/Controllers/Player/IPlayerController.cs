using System;
using _Project.Runtime.Controllers.Base;
using _Project.Runtime.Controllers.Weapons;
using UnityEngine;

namespace _Project.Runtime.Controllers
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