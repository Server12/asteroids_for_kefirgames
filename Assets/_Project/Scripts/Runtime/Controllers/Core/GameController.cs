using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using _Project.Runtime.Controllers.Base;
using _Project.Runtime.Controllers.Factory;
using _Project.Runtime.Controllers.Physics;
using _Project.Runtime.Data;
using _Project.Runtime.UI;
using UnityEngine;

namespace _Project.Runtime.Controllers
{
    public sealed class GameController : IGameController, IUpdate, IInitializable, IDisposable
    {
        public event Action OnGameStarted;

        public event Action OnGameOver;


        public Camera Camera => _model.Camera;

        public ICollisionsManager CollisionsManager => _collisionsManager;


        private readonly GameModel _model;
        private readonly ICollisionsManager _collisionsManager;
        private readonly MonoBehaviour _behaviour;

        private readonly List<IController> _controllers = new List<IController>(4);
        private readonly List<IUpdate> _updateables = new List<IUpdate>(4);

        private IPlayerController _player;
        private IGameOverPopup _gameOverPopup;
        private IMainUIController _mainUIController;

        public GameController(GameModel model, ICollisionsManager collisionsManager, MonoBehaviour behaviour)
        {
            _model = model;
            _collisionsManager = collisionsManager;
            _behaviour = behaviour;
        }

        public void Initialize()
        {
            var factory = new GameControllersFactory(_model.PlayerData, _model.EnemiesData, _model.Prefabs);

            _controllers.Add(factory.Create(GameControllerType.PlayerController));
            _controllers.Add(factory.Create(GameControllerType.EnemiesController));
            _controllers.Add(factory.Create(GameControllerType.MainUIController));
            _controllers.Add(factory.Create(GameControllerType.GameOverPopupController));
            _controllers.Add(factory.Create(GameControllerType.ProgressManager));


            foreach (var controller in _controllers)
            {
                controller.GameController = this;

                if (controller is IInitializable initialization)
                {
                    initialization.Initialize();
                }

                if (controller is IUpdate updateable)
                {
                    _updateables.Add(updateable);
                }
            }

            _player = GetController<IPlayerController>();
            _mainUIController = GetController<IMainUIController>();
            _gameOverPopup = GetController<IGameOverPopup>();

            _mainUIController.OnStartPlayGame += StartGame;
        }

        private void StartGame()
        {
            _mainUIController.OnStartPlayGame -= StartGame;
            _player.OnKilled += OnGameOverHandler;
            OnGameStarted?.Invoke();
        }

        public T GetController<T>() where T : IController
        {
            var type = typeof(T);
            foreach (var controller in _controllers)
            {
                if (controller is T controllerInstance)
                {
                    return controllerInstance;
                }
            }

            return default;
        }

        private void OnGameOverHandler()
        {
            _player.OnKilled -= OnGameOverHandler;
            _gameOverPopup.OnPlayAgain += OnPlayAgainHandler;
            Debug.Log("Game Over");
            OnGameOver?.Invoke();
        }

        private void OnPlayAgainHandler()
        {
            _gameOverPopup.OnPlayAgain -= OnPlayAgainHandler;
            StartGame();
        }

        public void Update(float deltaTime)
        {
            foreach (var updateable in _updateables)
            {
                updateable.Update(deltaTime);
            }
        }

        public void Dispose()
        {
            foreach (var controller in _controllers)
            {
                if (controller is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}