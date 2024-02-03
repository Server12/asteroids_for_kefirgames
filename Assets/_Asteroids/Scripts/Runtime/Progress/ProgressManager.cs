using System;
using UnityEngine;

namespace Asteroids.Runtime.Saves
{
    public class ProgressManager : IProgressController, IDisposable, IInitializable
    {
        private const string TotalScorePrefKey = "totalScore";

        private int _totalScore;

        public void Initialize()
        {
            GameController.OnGameStarted += OnGameStartedHandler;
            GameController.OnGameOver += OnGameOverHandler;
            _totalScore = PlayerPrefs.GetInt(TotalScorePrefKey, 0);
        }

        private void OnGameStartedHandler()
        {
            CurrentScore = 0;
        }


        private void OnGameOverHandler()
        {
            if (CurrentScore > _totalScore)
            {
                _totalScore = CurrentScore;
                PlayerPrefs.SetInt(TotalScorePrefKey, _totalScore);
                PlayerPrefs.Save();
            }
        }

        public void Dispose()
        {
            GameController.OnGameStarted -= OnGameStartedHandler;
            GameController.OnGameOver -= OnGameOverHandler;
        }


        public int HighScore => _totalScore;


        public int CurrentScore { get; set; }

        public IGameController GameController { private get; set; }
    }
}