using System.Collections.Generic;
using Asteroids.Runtime.Data;
using Asteroids.Runtime.Configs;
using Asteroids.Runtime.Generators;
using Asteroids.Runtime.Physics;
using Asteroids.Runtime.Saves;
using UnityEngine;

namespace Asteroids.Runtime
{
    public class EnemiesManager : IEnemiesManager
    {
        private readonly EnemiesPrefabs _enemiesPrefabs;
        private readonly EnemiesData _enemiesData;
        private readonly IPlayerController _playerController;

        private ICollisionsManager _collisionsManager;

        private Camera _camera;

        private List<IEnemiesController> _controllers;

        private IProgressController _progressController;

        private bool _generateNow;

        public EnemiesManager(EnemiesPrefabs enemiesPrefabs, EnemiesData enemiesData)
        {
            _enemiesPrefabs = enemiesPrefabs;
            _enemiesData = enemiesData;
        }

        public void Initialize()
        {
            _controllers = new List<IEnemiesController>();

            _collisionsManager = GameController.CollisionsManager;
            _camera = GameController.Camera;

            _progressController = GameController.GetController<IProgressController>();

            if (_enemiesData.AsteroidsData.Enabled)
            {
                _controllers.Add(new AsteroidsController(_enemiesData.AsteroidsData, _enemiesPrefabs.AsteroidViewPrefab)
                {
                    Camera = _camera,
                    CollisionsManager = _collisionsManager
                });
            }

            if (_enemiesData.UfoData.Enabled)
            {
                _controllers.Add(new UfoController(GameController.GetController<IPlayerController>(),
                    _enemiesData.UfoData,
                    _enemiesPrefabs.UfoViewPrefab)
                {
                    Camera = _camera,
                    CollisionsManager = _collisionsManager
                });
            }

            foreach (var controller in _controllers)
            {
                controller.OnReadyToGenerate += OnReadyToGenerate;
                controller.OnEnemyHitted += OnEnemyHittedHandler;
            }

            GameController.OnGameStarted += OnGameStartedHandler;
        }

        private void OnGameStartedHandler()
        {
            foreach (var controller in _controllers)
            {
                controller.Generate(true);
            }
        }

        private void OnReadyToGenerate()
        {
            foreach (var controller in _controllers)
            {
                controller.Generate();
            }
        }

        private void OnEnemyHittedHandler(GameEnemy enemyType)
        {
            var scoreData = _enemiesData.GetScoreData(enemyType);
            _progressController.CurrentScore += scoreData.KillScore;
        }


        public void Update(float deltaTime)
        {
            foreach (var controller in _controllers)
            {
                controller.Update(deltaTime);
            }
        }

        public void Dispose()
        {
            foreach (var controller in _controllers)
            {
                controller.OnReadyToGenerate -= OnReadyToGenerate;
                controller.OnEnemyHitted -= OnEnemyHittedHandler;
                controller.Dispose();
            }

            GameController.OnGameStarted -= OnGameStartedHandler;
        }

        public IGameController GameController { private get; set; }
    }
}