using System;
using System.Collections.Generic;

namespace PuzzleGames
{
    public class UIManager : Manager
    {
        #region Public Variables
        public List<View> gameViews;
        #endregion

        #region Unity Callbacks
        private void Start()
        {
            SwitchToView(ViewType.StartView);
        }
        private void OnEnable()
        {
            GameStatus.StartGame += OnGameStated;
            GameStatus.GameOver += OnGameOver;
        }
        private void OnDisable()
        {
            GameStatus.GameOver -= OnGameOver;
        }     
        #endregion

        #region Public Methods
        public void SwitchToView(ViewType view1)
        {
            gameViews.ForEach(x => x.baseView.HideView());

            var viewToEnable = gameViews.Find(x => x.viewType == view1);
            viewToEnable.baseView.ShowView();
        }

        #endregion
        private void OnGameOver()
        {
           SwitchToView(ViewType.GameOverView);
        }
        private void OnGameStated()
        {
            SwitchToView(ViewType.GameView);
        }
    }
}

