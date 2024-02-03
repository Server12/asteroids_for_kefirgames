using System;
using Asteroids.Runtime.Data;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Asteroids.Runtime
{
    [Serializable]
    public class AsteroidsData : BaseEnemyData
    {
        [SerializeField] private Sprite[] _asteroidSprites;

        [SerializeField] private float[] _depthScales;

        [SerializeField] private RandomRange speedRange;
        [SerializeField] private RandomRange rotationRange;
        [SerializeField] private int _shardsCount;

        [Range(1f, 4f)] public float radiusShardsSpawnPositionMultiplier;
        [Range(0.2f, 2f)] public float shardsSpeedMultiplier;
        public float shardsRotationSpeedMultiplier;

        public float[] DepthScales => _depthScales;

        public int ShardsCount => _shardsCount;

        public RandomRange SpeedRange => speedRange;

        public RandomRange RotationRange => rotationRange;

        public int GetRandomShardsCount()
        {
            return Random.Range(0, ShardsCount);
        }

        public float GetRandomScale(out int index)
        {
            index = Random.Range(0, _depthScales.Length);
            return _depthScales[index];
        }

        public Sprite GetRandomSprite()
        {
            return _asteroidSprites[Random.Range(0, _asteroidSprites.Length)];
        }
    }
}