using System.Collections.Generic;
using _Project.Runtime.Controllers.Physics;
using _Project.Runtime.Controllers.Weapons.Models;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Runtime.Controllers.Weapons
{
    public class LaserGunController : BaseGunController<LaserWeaponModel>
    {
        private readonly ICollisionsManager _collisionsManager;
        private readonly Camera _camera;

        private ObjectPool<Laser> _lasersPool;

        private HashSet<Laser> _shootedLasers;

        public LaserGunController(LaserWeaponModel model, ICollisionsManager collisionsManager, Camera camera) :
            base(model)
        {
            _collisionsManager = collisionsManager;
            _camera = camera;
        }

        public override void Initialize()
        {
            _shootedLasers = new HashSet<Laser>(Model.Data.MaxBullets);
            _lasersPool = new ObjectPool<Laser>(CreateFunc, ActionOnGet, ActionOnRelease, null, true,
                Model.Data.MaxBullets);
        }

        private void ActionOnRelease(Laser laser)
        {
            _collisionsManager.RemovePhysicsAgent(laser);
        }

        private void ActionOnGet(Laser laser)
        {
            laser.SetDirection(Model.Data.BulletSpeed);
            _collisionsManager.AddPhysicsAgent(laser);
        }

        private Laser CreateFunc()
        {
            var view = Object.Instantiate(Model.LaserViewPrefab);
            var laser = new Laser(view, Model.Spawner, _camera, _lasersPool);
            return laser;
        }

        protected override void ShootInternal()
        {
            _shootedLasers.Add(_lasersPool.Get());
        }


        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            foreach (var laser in _shootedLasers)
            {
                laser.Update(deltaTime);
            }
        }

        public override void Dispose()
        {
            _lasersPool?.Dispose();
        }
    }
}