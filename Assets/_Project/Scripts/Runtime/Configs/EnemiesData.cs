using System;
using _Project.Runtime.Controllers;
using _Project.Runtime.Data;
using UnityEngine;

namespace _Project.Scripts.Runtime.Data
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