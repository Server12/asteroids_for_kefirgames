using System;
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
            foreach (var scoreData in _scoresConfig)
            {
                if (scoreData.GameEnemy == enemy)
                {
                    return scoreData;
                }
            }

            throw new NotImplementedException($"{enemy}")
            {
                Source = nameof(EnemiesData)
            };
        }
    }
}