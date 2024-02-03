using _Project.Runtime.Data;
using _Project.Runtime.Views;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Runtime.Controllers.Generators
{
    public class UfoController : BaseEnemiesGroupController<Ufo, UfoView, UfoData>
    {
        private readonly IPlayerController _playerController;

        private float _generateCount = 0;

        private float _delayByEachUfoTimer;

        public UfoController(IPlayerController playerController, UfoData data, UfoView enemyViewPrefab) : base(
            enemyViewPrefab, data)
        {
            _playerController = playerController;
        }

        protected override Ufo CreateEnemyInternal(UfoView view, IObjectPool<Ufo> pool)
        {
            return new Ufo(view, pool, _playerController);
        }

        protected override void ClearInternal()
        {
            _generateCount = 0;
            _delayByEachUfoTimer = _data.GenerateByOneDelaySeconds;
        }

        protected override void UpdateInternal(float deltaTime)
        {
            if (_generateCount > 0 && !_playerController.IsKilled)
            {
                _delayByEachUfoTimer -= deltaTime;
                if (_delayByEachUfoTimer <= 0)
                {
                    _generateCount--;
                    _delayByEachUfoTimer = _data.GenerateByOneDelaySeconds;

                    var ufo = GetOrCreate();
                    var randomViewPortPoint = _data.SpawnSides[Random.Range(0, _data.SpawnSides.Length)];

                    var colliderRadius = ufo.CollisionCheck.ColliderRadius;

                    var distance = Vector2.Distance(_playerController.Position, randomViewPortPoint);
                    while (distance <= colliderRadius)
                    {
                        randomViewPortPoint = _data.SpawnSides[Random.Range(0, _data.SpawnSides.Length)];
                        distance = Vector2.Distance(_playerController.Position, randomViewPortPoint);
                    }

                    var spawnPos = Camera.ViewportToWorldPoint(randomViewPortPoint);
                    ufo.Setup(_playerController.MaxSpeed * _data.PlayerSpeedMultiplier, spawnPos, OnHitUfo);
                }
            }
        }

        private void OnHitUfo(Ufo ufo)
        {
            InvokeEnemyHit(GameEnemy.Ufo);
        }

        protected override void GenerateInternal(int maxCount)
        {
            if (_generateCount < maxCount)
            {
                _generateCount = maxCount;
            }
        }
    }
}