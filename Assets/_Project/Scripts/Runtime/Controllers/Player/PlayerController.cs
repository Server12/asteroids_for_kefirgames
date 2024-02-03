using System;
using _Project.Runtime.Controllers.Extensions;
using _Project.Runtime.Controllers.Physics;
using _Project.Runtime.Controllers.Physics.Data;
using _Project.Runtime.Controllers.Weapons;
using _Project.Runtime.Controllers.Weapons.Data;
using _Project.Runtime.Data;
using _Project.Runtime.Views;
using _Project.Scripts.Runtime.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Runtime.Controllers
{
    public class PlayerController : IPlayerController, ICollisionAgent, IInitializable, IUpdate, IDisposable
    {
        public event Action OnKilled;

        private readonly PlayerView _playerViewPrefab;
        private readonly WeaponPrefabs _weaponPrefabs;

        private Camera _camera;
        private ICollisionsManager _collisionsManager;

        private CollisionCheck _collisionCheck = new(CollisionCheckType.ByRadius);

        private PlayerView _player;

        private readonly PlayerInputController _inputController = new PlayerInputController();

        private readonly PlayerData _playerData;

        private Vector2 _velocity;
        private Vector2 _inertia;
        private float _acceleration;

        private bool _isKilled;

        private WeaponsManager _weaponsManager;

        public int Id { get; private set; }

        public CollisionType Type => CollisionType.Player;


        public GameEntityTag Tag => GameEntityTag.Player;

        public CollisionCheck CollisionCheck => _collisionCheck;

        public bool IsKilled => _isKilled;
        public Vector3 Position => _player.cachedTransform.position;

        public float CurrentRotation { get; set; }

        public float MaxSpeed => _playerData.maxSpeed;

        public Vector2 Velocity => _velocity;

        public IGameController GameController { private get; set; }

        public PlayerController(PlayerData playerData,
            PlayerView playerViewPrefab, WeaponPrefabs weaponPrefabs)
        {
            _playerViewPrefab = playerViewPrefab;
            _weaponPrefabs = weaponPrefabs;
            _playerData = playerData;
        }


        public void Initialize()
        {
            _collisionsManager = GameController.CollisionsManager;
            _camera = GameController.Camera;

            _player = Object.Instantiate(_playerViewPrefab);
            _player.gameObject.SetActive(false);

            Id = _player.GetInstanceID();

            _weaponsManager =
                new WeaponsManager(GameController.CollisionsManager, GameController.Camera, GameController);
            _weaponsManager.Initialize(_playerData.weapons, _weaponPrefabs, _player.shootSpawner);

            GameController.OnGameStarted += OnGameStartedHandler;
        }

        private void OnGameStartedHandler()
        {
            _collisionCheck = new CollisionCheck(CollisionCheckType.ByRadius);

            _inputController.Enable();

            _player.cachedTransform.SetPositionAndRotation(Vector2.zero, Quaternion.identity);
            _player.gameObject.SetActive(true);

            _weaponsManager.SwitchWeapon(_inputController.WeaponIndex);

            _collisionsManager.AddPhysicsAgent(this);
            _isKilled = false;
        }


        public void Update(float deltaTime)
        {
            if (_isKilled)
            {
                _velocity = Vector2.zero;
                _inertia = Vector2.zero;
                _acceleration = 0f;
            }

            //physics update
            _collisionCheck.CurrentPosition = _player.cachedTransform.position;
            _collisionCheck.ColliderRadius = _player.colliderRadius;

            _weaponsManager.Update(deltaTime);

            var rotation = _playerData.rotateAngle * (deltaTime * _playerData.rotateSpeed) * _inputController.Rotation;
            _player.cachedTransform.Rotate(Vector3.forward, rotation);

            CurrentRotation = _player.cachedTransform.eulerAngles.z;

            if (_inputController.IsMoving)
            {
                _acceleration += deltaTime * _playerData.accelerationSpeed;
                _acceleration = Mathf.Clamp(_acceleration, 0, _playerData.maxSpeed);
                _velocity = (Vector2)_player.cachedTransform.up * _acceleration + _inertia;
                _inertia = Vector2.Lerp(_inertia, Vector2.zero, deltaTime);
            }
            else
            {
                _acceleration = 0f;
                _velocity *= Mathf.Clamp01(1f - _playerData.drag * deltaTime);
                _inertia = _velocity;
            }

            if (_inputController.IsWeaponChanged)
            {
                _weaponsManager.SwitchWeapon(_inputController.WeaponIndex);
            }


            if (_inputController.isShooting)
            {
                _weaponsManager.Shoot();
            }


            _player.cachedTransform.Translate(_velocity * deltaTime, Space.World);

            _camera.RestrictViewPort2DMovement(_player.cachedTransform);
        }

        public CollisionType CanCollideWith => CollisionType.Enemy;

        public IWeaponsManager WeaponsManager => _weaponsManager;

        public void OnCollidedWith(ICollisionEntity entity)
        {
            if (entity.Type == CollisionType.Enemy)
            {
                PlayerKilled();
            }
        }

        private void PlayerKilled()
        {
            if (_isKilled) return;
            _isKilled = true;
            _inputController.Disable();
            _player.gameObject.SetActive(false);
            _collisionsManager.RemovePhysicsAgent(this);
            OnKilled?.Invoke();
        }

        public void Dispose()
        {
            GameController.OnGameStarted -= OnGameStartedHandler;
            _inputController.Dispose();
            _weaponsManager.Dispose();
        }
    }
}