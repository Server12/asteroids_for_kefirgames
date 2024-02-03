using System;
using Asteroids.Runtime;
using Asteroids.Runtime.Saves;
using Object = UnityEngine.Object;

namespace Asteroids.Runtime.UI
{
    public class GameOverPopupController : IGameOverPopup, IInitializable, IDisposable
    {
        public event Action OnPlayAgain;

        private readonly GameOverPopup _gameOverPopupPrefab;

        private GameOverPopup _popup;

        private IProgressController _progressController;

        public GameOverPopupController(GameOverPopup gameOverPopupPrefab)
        {
            _gameOverPopupPrefab = gameOverPopupPrefab;
        }

        public void Initialize()
        {
            _progressController = GameController.GetController<IProgressController>();
            GameController.OnGameOver += OnGameOverHandler;
        }

        private void OnGameOverHandler()
        {
         
            _popup = Object.Instantiate(_gameOverPopupPrefab);
            _popup.playButton.onClick.AddListener(ClosePopupHandler);

            _popup.scoreText.text = $"SCORE: {_progressController.CurrentScore}";
            _popup.highscoreText.text = $"HIGHSCORE: {_progressController.HighScore}";
        }

        private void ClosePopupHandler()
        {
            _popup.playButton.onClick.RemoveAllListeners();
            Object.Destroy(_popup.gameObject);
            OnPlayAgain?.Invoke();
        }


        public IGameController GameController { private get; set; }


        public void Dispose()
        {
            GameController.OnGameOver -= OnGameOverHandler;
        }
    }
}