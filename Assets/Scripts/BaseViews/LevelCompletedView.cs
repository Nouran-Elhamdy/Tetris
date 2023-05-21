using System;
using UnityEngine;
using UnityEngine.UI;

namespace PuzzleGames
{
    public class LevelCompletedView : BaseView
    {
        #region Private Variables
        [SerializeField] Button nextLevelButton;
        [SerializeField] Button quitGameButton;

        [SerializeField] GameObject gameCompletedRect;
        [SerializeField] GameObject nextLevelRect;

        [SerializeField] GameObject confetti;
        #endregion

        #region Unity Callbacks
        private void Start()
        {
            confetti.gameObject.SetActive(true);   
        }
        private void OnEnable()
        {
            Manager.LevelManager.isGameRunning = false;
            confetti.gameObject.SetActive(true);
            nextLevelButton.onClick.AddListener(OnClickNextLevelButton);
            quitGameButton.onClick.AddListener(OnClicQuitGameButton);
        }
        private void OnDisable()
        {
           if(confetti) confetti.gameObject.SetActive(false);
            nextLevelButton.onClick.RemoveListener(OnClickNextLevelButton);
            quitGameButton.onClick.RemoveListener(OnClicQuitGameButton);
        }
        #endregion

        #region Unity GUI Callbacks
        private void OnClickNextLevelButton()
        {
            if (Manager.LevelManager.IsGameCompleted())
            {
                nextLevelRect.SetActive(false);
                gameCompletedRect.SetActive(true);
            }
            else
            {
                Manager.UIManager.SwitchToView(ViewType.GameView);
                LevelManager.OnLevelCompleted?.Invoke();
            }
        }
        private void OnClicQuitGameButton()
        {
            Application.Quit();
            Debug.Log("Quit Game.");
        }
        #endregion
    }
}

