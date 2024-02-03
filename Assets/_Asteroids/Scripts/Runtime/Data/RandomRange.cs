using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.Runtime.Data
{
    [Serializable]
    public class RandomRange : ISerializationCallbackReceiver
    {
        [SerializeField] private float _min;
        [SerializeField] private float _max;

        public float GetRandomRange()
        {
            return Random.Range(_min, _max);
        }

        public int GetRandIntRange()
        {
            return Mathf.RoundToInt(GetRandomRange());
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (_min > _max)
            {
                _min = 0;
            }
        }
    }
}