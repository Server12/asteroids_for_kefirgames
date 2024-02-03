using System;
using Asteroids.Runtime;
using Asteroids.Runtime.Data;
using UnityEngine;

namespace Asteroids.Runtime.Configs
{
    [CreateAssetMenu(fileName = "EnemiesData", menuName = "Create/EnemiesData", order = 0)]
    public class EnemiesData : ScriptableObject
    {
        public AsteroidsData AsteroidsData;

        public UfoData UfoData;
        
        [SerializeField] private ScoreData[] _scoresConfig;
        
        public ScoreData GetScoreData(GameEnemy enemy)
        {
            for (int i = 0; i < _scoresConfig.Length; i++)
            {
                if (_scoresConfig[i].GameEnemy == enemy)
                {
                    return _scoresConfig[i];
                }
            }

            throw new NotImplementedException($"{enemy}");
        }
    }
}