using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Runtime.Data;
using Asteroids.Runtime.Views;
using Asteroids.Runtime.Generators;
using Asteroids.Runtime.Physics;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Asteroids.Runtime
{
    public abstract class BaseEnemiesGroupController<T, TView, TData> : IEnemiesController
        where TView : BaseView where T : class, IEnemy where TData : BaseEnemyData
    {
        public event Action OnReadyToGenerate;

        public event Action<GameEnemy> OnEnemyHitted;

        private readonly TView _enemyViewPrefab;

        public ICollisionsManager CollisionsManager { set; protected get; }

        public Camera Camera { set; protected get; }
        
        protected readonly TData _data;

        private readonly HashSet<T> _updateEnemiesList;
        private readonly ObjectPool<T> _enemiesPool;

        private bool _isGenerated;
        private float _generationDelayTimer;

        protected BaseEnemiesGroupController(TView enemyViewPrefab, TData data)
        {
            _data = data;
            _enemyViewPrefab = enemyViewPrefab;
            _updateEnemiesList = new HashSet<T>(20);
            _enemiesPool = new ObjectPool<T>(CreateEnemy, ActionOnGet, ActionOnRelease, null, false);
        }


        private void ActionOnRelease(T enemy)
        {
            CollisionsManager.RemovePhysicsAgent(enemy);
            _updateEnemiesList.Remove(enemy);
        }

        private void ActionOnGet(T enemy)
        {
            CollisionsManager.AddPhysicsAgent(enemy);
            _updateEnemiesList.Add(enemy);
        }

        private T CreateEnemy()
        {
            return CreateEnemyInternal(Object.Instantiate(_enemyViewPrefab), _enemiesPool);
        }

        protected T GetOrCreate()
        {
            return _enemiesPool.Get();
        }

        protected abstract T CreateEnemyInternal(TView view, IObjectPool<T> pool);

        protected virtual void ClearInternal()
        {
        }

        public void Generate(bool clear = false)
        {
            if (clear)
            {
                _isGenerated = false;
                _updateEnemiesList.ToList().ForEach(enemy => enemy.Release());
                ClearInternal();
            }

            if (_isGenerated) return;
            _isGenerated = true;

            var countToGenerate = _updateEnemiesList.Count < _data.MaxOnScreen
                ? _data.MaxOnScreen - _updateEnemiesList.Count
                : 0;

            GenerateInternal(countToGenerate);

            _generationDelayTimer = _data.GenerateDelaySeconds;
        }

        protected abstract void GenerateInternal(int maxCount);

        protected virtual void UpdateInternal(float deltaTime)
        {
        }

        public void Update(float deltaTime)
        {
            if (_isGenerated)
            {
                _generationDelayTimer -= deltaTime;
                _isGenerated = _generationDelayTimer > 0;

                if (!_isGenerated)
                {
                    OnReadyToGenerate?.Invoke();
                }
            }

            foreach (var enemy in _updateEnemiesList)
            {
                enemy.Update(deltaTime);
            }

            UpdateInternal(deltaTime);
        }

        public virtual void Dispose()
        {
            _enemiesPool?.Dispose();
        }

        protected void InvokeEnemyHit(GameEnemy enemyType)
        {
            OnEnemyHitted?.Invoke(enemyType);
        }
    }
}