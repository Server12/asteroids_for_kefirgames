using System;
using UnityEngine;

namespace Asteroids.Runtime.Data
{
    [Serializable]
    public class ScoreData
    {
        [SerializeField] private GameEnemy _enemyType;
        [SerializeField] private int _killScore;

        public int KillScore => _killScore;

        public GameEnemy GameEnemy => _enemyType;
    }
}