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
    public class Laser : ICollisionAgent, IUpdate
    {
        private const float Length = 2;

        private readonly LaserView _laserView;
        private readonly Transform _spawner;
        private readonly Camera _camera;
        private readonly IObjectPool<Laser> _pool;

        private CollisionCheck _collisionCheck;

        private bool _isActive;

        private Vector3 _targetPos;
        private float _bulletSpeed;
        private Vector3 _direction;


        public Laser(LaserView laserView, Transform spawner, Camera camera, IObjectPool<Laser> pool)
        {
            _laserView = laserView;
            _spawner = spawner;
            _camera = camera;
            _pool = pool;
            Id = _laserView.GetInstanceID();
        }

        public void SetDirection(float bulletSpeed)
        {
            _collisionCheck = new CollisionCheck(CollisionCheckType.ByLineIntersection, false);
            
            _bulletSpeed = bulletSpeed;
            _direction = _spawner.up;

            _targetPos = _spawner.position + _direction * Length;

            _laserView.cachedTransform.position = _spawner.position;
            _laserView.LineRenderer.SetPosition(0,
                _laserView.cachedTransform.InverseTransformPoint(_spawner.position));
            _laserView.LineRenderer.SetPosition(1, _laserView.cachedTransform.InverseTransformPoint(_targetPos));
            _laserView.gameObject.SetActive(true);

            _collisionCheck.CurrentPosition = _spawner.position;
            _collisionCheck.EndPosition = _targetPos;
            _collisionCheck.ColliderRadius = _laserView.colliderRadius;


            _isActive = true;
        }


        public int Id { get; }


        public CollisionType Type => CollisionType.Laser;


        public GameEntityTag Tag => GameEntityTag.Laser;

        public CollisionType CanCollideWith => CollisionType.Enemy;

        public CollisionCheck CollisionCheck => _collisionCheck;

        public void OnCollidedWith(ICollisionEntity entity)
        {
         
        }

        public void Update(float deltaTime)
        {
            if (!_isActive) return;

            _laserView.cachedTransform.Translate(_direction * (_bulletSpeed * deltaTime), Space.World);

            var startPos = _laserView.cachedTransform.TransformPoint(_laserView.LineRenderer.GetPosition(0));
            var endPos = _laserView.cachedTransform.TransformPoint(_laserView.LineRenderer.GetPosition(1));

            _collisionCheck.CurrentPosition = startPos;
            _collisionCheck.EndPosition = endPos;
            _collisionCheck.ColliderRadius = _laserView.colliderRadius;
            
            _isActive = !_camera.IsOutOfViewPortView(_laserView.cachedTransform) || startPos.sqrMagnitude <= 0f;

            if (!_isActive)
            {
                _laserView.gameObject.SetActive(false);
                _pool.Release(this);
            }
        }
    }
}