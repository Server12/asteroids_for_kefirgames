using System;
using System.Linq;
using Asteroids.Runtime.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Asteroids.Runtime
{
    [Serializable]
    public abstract class BaseEnemyData : ISerializationCallbackReceiver
    {
        [SerializeField] private bool _enabled = true;
        
        [SerializeField] private float _generateDelaySeconds;

        [SerializeField] protected int _maxOnScreen;

        [SerializeField] private Vector2[] _spawnSides;

        public float GenerateDelaySeconds => _generateDelaySeconds;

        public int MaxOnScreen => _maxOnScreen;

        public Vector2[] SpawnSides => _spawnSides;

        public bool Enabled => _enabled;

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            AfterSerialize();
        }

        protected virtual void AfterSerialize()
        {
        }
    }
}