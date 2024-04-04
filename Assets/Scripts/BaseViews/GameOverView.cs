using System;
using UnityEngine;
using UnityEngine.UI;

namespace PuzzleGames
{
    public class GameOverView : BaseView
    {
        #region Private Variables
        [SerializeField] Button retryButton;
        [SerializeField] Button quitGame;
        #endregion

        #region Unity Callbacks
        private void OnEnable()
        {
            retryButton.onClick.AddListener(OnClickRetryButton);
            quitGame.onClick.AddListener(OnClickQuitButton);
        }
        private void OnDisable()
        {
            retryButton.onClick.RemoveListener(OnClickRetryButton);
            quitGame.onClick.RemoveListener(OnClickQuitButton);
        }
        #endregion

        #region Unity GUI Callbacks
        public void OnClickRetryButton()
        {
            GameStatus.StartGame?.Invoke();
        }
        private void OnClickQuitButton()
        {
            Application.Quit();
        }
        #endregion
    }
}

