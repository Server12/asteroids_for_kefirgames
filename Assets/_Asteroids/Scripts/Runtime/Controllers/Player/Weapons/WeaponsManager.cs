using System;
using Asteroids.Runtime.Saves;
using Asteroids.Runtime.Data;
using Asteroids.Runtime.UI;
using Asteroids.Runtime.Physics;
using Asteroids.Runtime.Weapons.Data;
using Asteroids.Runtime.Weapons.Models;
using UnityEngine;

namespace Asteroids.Runtime.Weapons
{
    public class WeaponsManager : IWeaponsManager, IInitializable<WeaponData[], WeaponPrefabs, Transform>, IUpdate
    {
        private readonly Camera _camera;
        private readonly IGameController _gameController;
        private readonly ICollisionsManager _collisionsManager;
        private IGunController[] _guns;
        private PlayerWeapon _currentWeapon;

        public WeaponsManager(ICollisionsManager collisionsManager, Camera camera, IGameController gameController)
        {
            _collisionsManager = collisionsManager;
            _camera = camera;
            _gameController = gameController;
        }

        public void Initialize(WeaponData[] weapons, WeaponPrefabs weaponPrefabs, Transform shootSpawner)
        {
            _gameController.OnGameOver += OnGameOverHandler;

            _currentWeapon = PlayerWeapon.BulletGun;

            _guns = new IGunController[Enum.GetNames(typeof(PlayerWeapon)).Length];

            foreach (var weaponData in weapons)
            {
                var index = (int)weaponData.WeaponType;
                _guns[index] = CreateGunController(weaponData, weaponPrefabs, shootSpawner);
            }

            foreach (var gunController in _guns)
            {
                gunController.Initialize();
            }
        }

        private void OnGameOverHandler()
        {
            foreach (var gunController in _guns)
            {
                gunController.ResetState();
            }
        }

        private IGunController CreateGunController(WeaponData weaponData, WeaponPrefabs prefabs,
            Transform shootSpawner)
        {
            switch (weaponData.WeaponType)
            {
                case PlayerWeapon.BulletGun:
                    return new GunController(new GunWeaponModel(prefabs.BulletViewPrefab, weaponData, shootSpawner),
                        _collisionsManager, _camera);
                case PlayerWeapon.LaserGun:
                    return new LaserGunController(
                        new LaserWeaponModel(prefabs.LaserViewPrefab, weaponData, shootSpawner), _collisionsManager,
                        _camera);
            }

            throw new NotImplementedException($"For weapon:{weaponData.WeaponType}")
            {
                Source = nameof(WeaponsManager)
            };
        }

        public void Update(float deltaTime)
        {
            foreach (var gun in _guns)
            {
                gun.Update(deltaTime);
            }
        }


        public IWeaponModel GetWeaponModel(PlayerWeapon weapon)
        {
            return _guns[(int)weapon]?.WeaponModel;
        }

        public void SwitchWeapon(int weaponIndex)
        {
            if (weaponIndex >= 0 && weaponIndex < _guns.Length &&
                _guns[weaponIndex].WeaponModel.State == GunState.Ready)
            {
                _currentWeapon = (PlayerWeapon)weaponIndex;
            }
        }

        public void Shoot()
        {
            _guns[(int)_currentWeapon]?.Shoot();
        }

        public void Dispose()
        {
            _gameController.OnGameOver -= OnGameOverHandler;
            foreach (var gunController in _guns)
            {
                gunController.Dispose();
            }
        }
    }
}