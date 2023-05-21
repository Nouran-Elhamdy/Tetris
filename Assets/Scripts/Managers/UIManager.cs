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
        #endregion

        #region Public Methods
        public void SwitchToView(ViewType view1)
        {
            gameViews.ForEach(x => x.baseView.HideView());

            var viewToEnable = gameViews.Find(x => x.viewType == view1);
            viewToEnable.baseView.ShowView();
        }
        #endregion
    }
}

