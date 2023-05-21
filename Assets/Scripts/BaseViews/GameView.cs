using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PuzzleGames
{
    public class GameView : BaseView
    {
        #region Private Variables
        [SerializeField] Text levelNumberText;
        #endregion

        #region Unity Callbacks
        private void Start()
        {
            UpdateLevelNumber();
        }
        private void OnEnable()
        {
            LevelManager.OnLevelCompleted += UpdateLevelNumber;
        }
        private void OnDisable()
        {
            LevelManager.OnLevelCompleted -= UpdateLevelNumber;
        }
        #endregion

        #region Event Callbacks
        private void UpdateLevelNumber()
        {
           levelNumberText.text = Manager.LevelManager.currentLevelNumber.ToString() + " / " + Manager.LevelManager.gameLevels.Count;
        }
        #endregion
    }
}

