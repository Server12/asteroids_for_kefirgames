using System;
using Asteroids.Runtime.Configs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Asteroids.Runtime.Data
{
    [Serializable]
    public class GameModel
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private GamePrefabs prefabs;
        [SerializeField] private EnemiesData enemiesData;

        [SerializeField] private PlayerData _playerData;
        
        public Camera Camera => _camera == null ? Camera.main : _camera;

        public GamePrefabs Prefabs => prefabs;
        
        public EnemiesData EnemiesData => enemiesData;

        public PlayerData PlayerData => _playerData;
    }
}