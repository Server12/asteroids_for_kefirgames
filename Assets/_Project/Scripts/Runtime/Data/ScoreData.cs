using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Runtime.Data
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