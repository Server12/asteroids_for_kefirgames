using _Project.Runtime.Data;
using _Project.Runtime.Views;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Runtime.Controllers.Generators
{
    public readonly struct AsteroidSetupInfo
    {
        public readonly float Scale;
        public readonly float Speed;
        public readonly float RotationSpeed;
        public readonly int ScaleIndex;
        public readonly int ShardsCount;
        public readonly GameEntityTag Tag;

        public AsteroidSetupInfo(float scale, float speed, float rotationSpeed, int scaleIndex,
            GameEntityTag tag = GameEntityTag.Asteroid, int shardsCount = 0)
        {
            Scale = scale;
            Speed = speed;
            RotationSpeed = rotationSpeed;
            ScaleIndex = scaleIndex;
            ShardsCount = shardsCount;
            Tag = tag;
        }
    }


    public class AsteroidsController : BaseEnemiesGroupController<Asteroid, AsteroidView, AsteroidsData>
    {
        public AsteroidsController(AsteroidsData data, AsteroidView enemyViewPrefab) : base(
            enemyViewPrefab, data)
        {
        }

        
        private AsteroidSetupInfo CreateInfo(int scaleIndex, float speed, float rotationSpeed, int shardsCount,
            GameEntityTag tag)
        {
            return new AsteroidSetupInfo(_data.DepthScales[scaleIndex], speed, rotationSpeed, scaleIndex, tag,
                shardsCount);
        }


        protected override Asteroid CreateEnemyInternal(AsteroidView view, IObjectPool<Asteroid> pool)
        {
            return new Asteroid(view, Camera, pool);
        }

        protected override void GenerateInternal(int maxCount)
        {
            for (int i = 0; i < maxCount; i++)
            {
                var scaleIndex = Random.Range(0, _data.DepthScales.Length);
                GameEntityTag tag = scaleIndex <= 1 ? GameEntityTag.Asteroid : GameEntityTag.AsteroidShard;
                var info = CreateInfo(scaleIndex,
                    _data.SpeedRange.GetRandomRange(),
                    _data.RotationRange.GetRandomRange(),
                    scaleIndex == 0 ? _data.ShardsCount : _data.ShardsCount - scaleIndex, tag);

                GenerateRandomSpawnAsteroid(info);
            }
        }


        private void GenerateShards(Asteroid asteroid)
        {
            var scaleIndex = asteroid.SetupInfo.ScaleIndex;
            var depthIndex = scaleIndex + 1;
            if (depthIndex < _data.DepthScales.Length)
            {
                for (int i = 0; i < asteroid.SetupInfo.ShardsCount; i++)
                {
                    var info = CreateInfo(depthIndex, asteroid.SetupInfo.Speed * _data.shardsSpeedMultiplier,
                        _data.RotationRange.GetRandomRange() * _data.shardsRotationSpeedMultiplier,
                        _data.GetRandomShardsCount(),
                        depthIndex > 1 ? GameEntityTag.AsteroidShard : GameEntityTag.Asteroid);

                    var shardAsteroid = GetOrCreate();

                    var radiusPosition = asteroid.Position +
                                         Random.insideUnitCircle * _data.radiusShardsSpawnPositionMultiplier;

                    shardAsteroid.Setup(_data.GetRandomSprite(), radiusPosition, info, OnHitCallback);
                }
            }
        }

        private void OnHitCallback(Asteroid asteroid)
        {
            GenerateShards(asteroid);
            var enemyType = (GameEnemy)asteroid.SetupInfo.ScaleIndex;
            InvokeEnemyHit(enemyType);
        }

        private void GenerateRandomSpawnAsteroid(AsteroidSetupInfo setupInfo)
        {
            var asteroid = GetOrCreate();
            var randomViewPortPoint = _data.SpawnSides[Random.Range(0, _data.SpawnSides.Length)];
            var spawnPos = Camera.ViewportToWorldPoint(randomViewPortPoint);

            asteroid.Setup(_data.GetRandomSprite(), spawnPos, setupInfo, OnHitCallback);
        }
    }
}