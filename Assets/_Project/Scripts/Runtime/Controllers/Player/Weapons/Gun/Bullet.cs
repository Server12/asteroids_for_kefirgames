using System;
using _Project.Runtime.Controllers.Extensions;
using _Project.Runtime.Controllers.Physics;
using _Project.Runtime.Controllers.Physics.Data;
using _Project.Runtime.Data;
using _Project.Runtime.Views;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Runtime.Controllers.Weapons
{
    public class Bullet : ICollisionAgent, IUpdate
    {
        private readonly BulletView _bulletView;
        private readonly IObjectPool<Bullet> _pool;
        private readonly Camera _camera;

        private CollisionCheck _collisionCheck;

        private float _speed;
        private Vector2 _shootDirection;

        private bool _isVisible;

        public Bullet(BulletView bulletView, IObjectPool<Bullet> pool, Camera camera)
        {
            _bulletView = bulletView;
            _pool = pool;
            _camera = camera;
            _collisionCheck = new CollisionCheck(CollisionCheckType.ByRadius);
        }

        public int Id => _bulletView.GetInstanceID();

        public CollisionType Type => CollisionType.Bullet;


        public GameEntityTag Tag => GameEntityTag.Bullet;

        public CollisionType CanCollideWith => CollisionType.Enemy;


        public CollisionCheck CollisionCheck => _collisionCheck;

        public Vector2 ShootDirection
        {
            set => _shootDirection = value;
        }

        public float Speed
        {
            set => _speed = value;
        }

        public void SetSpawnPos(Vector3 position)
        {
            _collisionCheck = new CollisionCheck(CollisionCheckType.ByRadius);
            _bulletView.cachedTransform.position = position;
            _bulletView.gameObject.SetActive(true);
            _isVisible = true;
        }


        public void OnCollidedWith(ICollisionEntity entity)
        {
            _isVisible = false;
            _bulletView.gameObject.SetActive(false);
            _pool.Release(this);
        }


        public void Update(float deltaTime)
        {
            if (!_isVisible) return;
            _collisionCheck.CurrentPosition = _bulletView.cachedTransform.position;
            _collisionCheck.ColliderRadius = _bulletView.colliderRadius;


            _bulletView.cachedTransform.Translate(_shootDirection * (_speed * deltaTime), Space.World);

            _isVisible = !_camera.IsOutOfViewPortView(_bulletView.cachedTransform);
            if (!_isVisible)
            {
                _bulletView.gameObject.SetActive(false);
                _pool.Release(this);
            }
        }
    }
}