using System;
using Asteroids.Runtime.Base;
using Asteroids.Runtime.UI;
using Asteroids.Runtime.Configs;
using Asteroids.Runtime.Saves;

namespace Asteroids.Runtime.Factory
{
    public enum GameControllerType
    {
        PlayerController,
        EnemiesController,
        MainUIController,
        GameOverPopupController,
        ProgressManager,
    }


    public class GameControllersFactory
    {
        private readonly PlayerData _playerData;
        private readonly EnemiesData _enemiesData;
        private readonly GamePrefabs _gamePrefabs;

        public GameControllersFactory(PlayerData playerData, EnemiesData enemiesData, GamePrefabs gamePrefabs)
        {
            _playerData = playerData;
            _enemiesData = enemiesData;
            _gamePrefabs = gamePrefabs;
        }

        public IController Create(GameControllerType type)
        {
            switch (type)
            {
                case GameControllerType.PlayerController:
                    return new PlayerController(_playerData, _gamePrefabs.PlayerViewPrefab, _gamePrefabs.WeaponPrefabs);
                case GameControllerType.EnemiesController:
                    return new EnemiesManager(_gamePrefabs.EnemiesPrefabs, _enemiesData);
                case GameControllerType.MainUIController:
                    return new MainUIController(_gamePrefabs.MainUIPrefab);
                case GameControllerType.GameOverPopupController:
                    return new GameOverPopupController(_gamePrefabs.GameOverPopupPrefab);
                case GameControllerType.ProgressManager:
                    return new ProgressManager();
                default:
                    throw new NotImplementedException($"Controller for {type}")
                    {
                        Source = nameof(GameControllersFactory)
                    };
            }
        }
    }
}