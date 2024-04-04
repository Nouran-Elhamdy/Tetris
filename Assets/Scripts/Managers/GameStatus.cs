using System;

namespace PuzzleGames
{
    public class GameStatus : Manager
    {
        public static Action StartGame;
        public static Action GameOver;

        public bool IsGameStarted { get; private set;}

        private void OnEnable()
        {
            StartGame += OnGameStarted;
            GameOver += OnGameOver;
        }
        private void OnDisable()
        {
            StartGame -= OnGameStarted;
            GameOver -= OnGameOver;
        }

        public void OnGameStarted()
        {
            IsGameStarted = true;
        }
        private void OnGameOver()
        {
            IsGameStarted = false;
        }
    }
}

