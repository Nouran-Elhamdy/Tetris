using System;
using UnityEngine;

public class BaseView : MonoBehaviour
{
    #region Public Methods
    public virtual void ShowView()
    {
        gameObject.SetActive(true);    
    }
    public virtual void HideView()
    {
        gameObject.SetActive(false);
    }
    #endregion

}
[Serializable]
public struct View
{
    public ViewType viewType;
    public BaseView baseView;
}

public enum ViewType
{
    StartView,
    GameView,
    LevelCompletedView
}