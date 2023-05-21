using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace PuzzleGames
{
    public class StartView : BaseView
    {
        #region Private Variables
        [SerializeField] Button startButton;
        #endregion

        #region Unity Callbacks
        private void OnEnable()
        {
            startButton.onClick.AddListener(OnClickStartButton);
        }
        private void OnDisable()
        {
            startButton.onClick.RemoveListener(OnClickStartButton);
        }
        #endregion

        #region Unity GUI Callbacks
        public void OnClickStartButton()
        {
            Manager.UIManager.SwitchToView(ViewType.GameView);
            LevelManager.OnLevelStarted?.Invoke();
        }
        #endregion

    }
}

