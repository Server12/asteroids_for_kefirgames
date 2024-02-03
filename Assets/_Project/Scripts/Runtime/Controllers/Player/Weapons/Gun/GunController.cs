using System.Collections.Generic;
using _Project.Runtime.Controllers.Physics;
using _Project.Runtime.Controllers.Weapons.Models;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Runtime.Controllers.Weapons
{
    public class GunController : BaseGunController<GunWeaponModel>
    {
        private readonly ICollisionsManager _collisionsManager;
        private readonly Camera _camera;
        private readonly HashSet<Bullet> _bullets;

        private ObjectPool<Bullet> _bulletsPool;

        public GunController(GunWeaponModel weaponModel, ICollisionsManager collisionsManager, Camera camera) : base(
            weaponModel)
        {
            _collisionsManager = collisionsManager;
            _camera = camera;
            _bullets = new HashSet<Bullet>(100);
        }


        public override void Initialize()
        {
            _bulletsPool = new ObjectPool<Bullet>(CreateBullet, GetBullet, ReleaseBullet, null, false, 100);
        }

        private void ReleaseBullet(Bullet bullet)
        {
            _collisionsManager.RemovePhysicsAgent(bullet);
        }

        private void GetBullet(Bullet bullet)
        {
            bullet.Speed = Model.Data.BulletSpeed;
            bullet.ShootDirection = Model.Spawner.up;
            bullet.SetSpawnPos(Model.Spawner.position);
            _collisionsManager.AddPhysicsAgent(bullet);
        }

        private Bullet CreateBullet()
        {
            var bulletView = Object.Instantiate(Model.BulletViewPrefab);
            return new Bullet(bulletView, _bulletsPool, _camera);
        }

        protected override void ShootInternal()
        {
            _bullets.Add(_bulletsPool.Get());
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            foreach (var bullet in _bullets)
            {
                bullet.Update(deltaTime);
            }
        }

        public override void Dispose()
        {
            _bulletsPool?.Dispose();
        }
    }
}