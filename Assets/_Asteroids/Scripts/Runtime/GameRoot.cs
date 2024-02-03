using Asteroids.Runtime.Data;
using Asteroids.Runtime.Physics;
using UnityEngine;

namespace Asteroids.Runtime
{
    public class GameRoot : MonoBehaviour
    {
        [SerializeField] private GameModel gameModel;

        private CollisionsManager _collisionsManager;

        private GameController _gameController;

        private void Awake()
        {
            _collisionsManager = new CollisionsManager();
            
            _gameController = new GameController(gameModel, _collisionsManager, this);
        }
        
        private void Start()
        {
            _gameController.Initialize();
        }

        private void Update()
        {
            _gameController.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _collisionsManager.FixedUpdate();
        }

        private void OnDestroy()
        {
            _gameController.Dispose();
        }
    }
}