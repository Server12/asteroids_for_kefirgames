using System;
using _Project.Runtime.Controllers.Base;
using _Project.Runtime.Controllers.Physics;
using _Project.Runtime.Controllers.Physics.Data;
using _Project.Runtime.Data;
using _Project.Runtime.Views;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace _Project.Runtime.Controllers
{
    public class Ufo : IEnemy
    {
        private readonly UfoView _view;
        private readonly IObjectPool<Ufo> _pool;
        private readonly IPlayerController _playerController;
        private CollisionCheck _collisionCheck = new(CollisionCheckType.ByRadius);

        private bool _isActive;
        private float _speed;
        private Vector2 _velocity;
        private Action<Ufo> _hitCallback;

        public Ufo(UfoView view, IObjectPool<Ufo> pool, IPlayerController playerController)
        {
            _view = view;
            _pool = pool;
            _playerController = playerController;
            Id = _view.GetInstanceID();
            _collisionCheck.ColliderRadius = _view.colliderRadius;
        }


        public void Setup(float speed, Vector2 spawnPos, Action<Ufo> hitCallback)
        {
            _hitCallback = hitCallback;
            _speed = speed;
            _view.cachedTransform.position = spawnPos;
            _view.gameObject.SetActive(true);
            _isActive = true;
        }

        public void Update(float deltaTime)
        {
            if (!_isActive) return;
            _collisionCheck.CurrentPosition = _view.cachedTransform.position;
            _collisionCheck.ColliderRadius = _view.colliderRadius;

            var moveDir = (_playerController.Position - _view.cachedTransform.position).normalized;

            _velocity = moveDir * (deltaTime * _speed);

            _view.cachedTransform.Translate(_velocity, Space.World);
        }

        public int Id { get; }

        public CollisionType Type => CollisionType.Enemy;


        public GameEntityTag Tag => GameEntityTag.Ufo;

        public CollisionType CanCollideWith => CollisionType.Weapons | CollisionType.Player;

        public CollisionCheck CollisionCheck => _collisionCheck;


        public void OnCollidedWith(ICollisionEntity entity)
        {
            if (entity.Type == CollisionType.Player)
            {
                _isActive = false;
            }
            else if ((entity.Type & CollisionType.Weapons) == entity.Type)
            {
                _isActive = false;
                _view.gameObject.SetActive(false);
                _pool.Release(this);
                _hitCallback?.Invoke(this);
            }
        }

        public void Destroy()
        {
            if (_view != null)
            {
                _isActive = false;
                Object.Destroy(_view.gameObject);
            }
        }

        public void Release()
        {
            _isActive = false;
            _view.gameObject.SetActive(false);
            _pool.Release(this);
        }
    }
}