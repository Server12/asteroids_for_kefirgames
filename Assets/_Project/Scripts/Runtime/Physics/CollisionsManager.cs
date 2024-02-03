using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using _Project.Runtime.Controllers.Physics.Data;
using UnityEngine;

namespace _Project.Runtime.Controllers.Physics
{
    public class CollisionsManager : ICollisionsManager
    {
        private readonly List<ICollisionAgent> _collisionCheckAgents = new(50);

        private readonly Queue<ICollisionAgent> _toRemove = new(5);

        private readonly Queue<ICollisionAgent> _toAdd = new(5);

        private readonly HashSet<int> _uniqueAgents = new(50);

        private readonly HashSet<int> _alreadyCollided = new HashSet<int>(50);

        public void AddPhysicsAgent(ICollisionAgent agent)
        {
            if (agent.CanCollideWith == CollisionType.None || agent.Type == CollisionType.None)
            {
                Debug.LogError($"{nameof(CollisionsManager)} wrong agent type or collideWith mask");
                return;
            }

            if (!_uniqueAgents.Add(agent.Id)) return;
            _toAdd.Enqueue(agent);
        }

        public void RemovePhysicsAgent(ICollisionAgent agent)
        {
            _alreadyCollided.Remove(agent.Id);
            _uniqueAgents.Remove(agent.Id);
            _toRemove.Enqueue(agent);
        }

        public void FixedUpdate()
        {
            while (_toAdd.Count > 0)
            {
                _collisionCheckAgents.Add(_toAdd.Dequeue());
            }

            int len = _collisionCheckAgents.Count;
            for (int i = 0; i < len; i++)
            {
                var checkAgent = _collisionCheckAgents[i];

                if (_alreadyCollided.Contains(checkAgent.Id)) continue;
                var type = checkAgent.Type;

                for (var j = 0; j < len; j++)
                {
                    var colidableAgent = _collisionCheckAgents[j];

                    if ((type & colidableAgent.CanCollideWith) != type || checkAgent.Id == colidableAgent.Id) continue;

                    if (checkAgent.CollisionCheck.IsCollided(colidableAgent.CollisionCheck))
                    {
                        if (checkAgent.CollisionCheck.CollideOnce)
                        {
                            _alreadyCollided.Add(checkAgent.Id);
                        }

                        checkAgent.OnCollidedWith(colidableAgent);
                    }
                }
            }

            while (_toRemove.Count > 0)
            {
                _collisionCheckAgents.Remove(_toRemove.Dequeue());
            }
        }
    }
}