using System;
using Asteroids.Runtime.Extensions;
using Asteroids.Runtime.Data;
using Asteroids.Runtime.Views;
using Asteroids.Runtime.Generators;
using Asteroids.Runtime.Physics;
using Asteroids.Runtime.Physics.Data;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Asteroids.Runtime
{
    public class Asteroid : IEnemy
    {
        private readonly AsteroidView _asteroidView;
        private readonly Camera _camera;
        private readonly IObjectPool<Asteroid> _pool;

        private CollisionCheck _collisionCheck = new(CollisionCheckType.ByRadius);

        private Vector2 _moveDir;

        private readonly float _colliderRadius;

        private bool isActive = false;

        private AsteroidSetupInfo _setupInfo;

        private Action<Asteroid> _hitCallback;

        public Asteroid(AsteroidView asteroidView, Camera camera, IObjectPool<Asteroid> pool)
        {
            _asteroidView = asteroidView;
            _camera = camera;
            _pool = pool;
            _colliderRadius = asteroidView.colliderRadius;
            Id = _asteroidView.GetInstanceID();
        }


        public void Setup(Sprite sprite, Vector2 spawnPos, AsteroidSetupInfo setupInfo, Action<Asteroid> hitCallback)
        {
            _collisionCheck = new CollisionCheck(CollisionCheckType.ByRadius);
            _hitCallback = hitCallback;
            _asteroidView.SpriteRenderer.sprite = sprite;
            _asteroidView.cachedTransform.localScale = Vector3.one * setupInfo.Scale;
            _asteroidView.colliderRadius = _colliderRadius * setupInfo.Scale;
            _setupInfo = setupInfo;

            _asteroidView.cachedTransform.position = spawnPos;
            _asteroidView.cachedTransform.rotation = Quaternion.Euler(0, 0, SetupInfo.RotationSpeed);
            _moveDir = _asteroidView.cachedTransform.up;
            _asteroidView.gameObject.SetActive(true);
            isActive = true;
        }


        public void Update(float deltaTime)
        {
            if (!isActive) return;
            _collisionCheck.CurrentPosition = _asteroidView.transform.position;
            _collisionCheck.ColliderRadius = _colliderRadius * SetupInfo.Scale;

            _asteroidView.cachedTransform.Translate(_moveDir * (SetupInfo.Speed * deltaTime), Space.World);
            _asteroidView.cachedTransform.Rotate(Vector3.forward, SetupInfo.RotationSpeed * deltaTime);

            _camera.RestrictViewPort2DMovement(_asteroidView.cachedTransform);
        }

        public int Id { get; }

        public CollisionType Type => CollisionType.Enemy;


        public GameEntityTag Tag => _setupInfo.Tag;

        public CollisionType CanCollideWith => CollisionType.Weapons | CollisionType.Player;

        public CollisionCheck CollisionCheck => _collisionCheck;

        public Vector2 Position => _asteroidView.cachedTransform.position;

        public AsteroidSetupInfo SetupInfo => _setupInfo;


        public void OnCollidedWith(ICollisionEntity entity)
        {
            Release();
            _hitCallback?.Invoke(this);
        }

        public void Release()
        {
            if(!isActive)return;
            isActive = false;
            _asteroidView.gameObject.SetActive(false);
            _pool.Release(this);
        }

        public void Destroy()
        {
            isActive = false;
            if (_asteroidView != null)
            {
                Object.Destroy(_asteroidView.gameObject);
            }
        }
    }
}