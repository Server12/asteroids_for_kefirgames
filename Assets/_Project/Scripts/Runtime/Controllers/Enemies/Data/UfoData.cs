using System;
using UnityEngine;

namespace _Project.Runtime.Controllers
{
    [Serializable]
    public class UfoData : BaseEnemyData
    {
        [SerializeField] private float _generateByOneDelaySeconds = 1;

        [Range(0, 0.8f)] [SerializeField] private float _playerSpeedMultiplier;

        public float GenerateByOneDelaySeconds => _generateByOneDelaySeconds;

        public float PlayerSpeedMultiplier => _playerSpeedMultiplier;

        protected override void AfterSerialize()
        {
            _generateByOneDelaySeconds = Mathf.Clamp(_generateByOneDelaySeconds, 10, 100);
            _maxOnScreen = Mathf.Clamp(_maxOnScreen, 1, 3);
        }
    }
}