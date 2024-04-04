using System;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGames
{
   public class Manager : MonoBehaviour
    {
        #region Properties
        private static Dictionary<Type, Manager> m_Managers;
        private static Dictionary<Type, Manager> Managers
        {
            get
            {
                if (m_Managers == null)
                    m_Managers = new Dictionary<Type, Manager>();
                return m_Managers;
            }
        }

        private static UIManager m_UIManager;
        public static UIManager UIManager
        {
            get
            {
                if (!m_UIManager)
                    m_UIManager = GetManager<UIManager>();
                return m_UIManager;
            }
        }
        private static GameStatus m_GameStatus;
        public static GameStatus GameStatus
        {
            get
            {
                if (!m_GameStatus)
                    m_GameStatus = GetManager<GameStatus>();
                return m_GameStatus;
            }
        }
        private static PlayerInputsManager m_PlayerInputsManager;
        public static PlayerInputsManager PlayerInputsManager
        {
            get
            {
                if (!m_PlayerInputsManager)
                    m_PlayerInputsManager = GetManager<PlayerInputsManager>();
                return m_PlayerInputsManager;
            }
        }
        #endregion

        #region Unity Callbacks
        public virtual void Awake()
        {
            if (!Managers.ContainsKey(this.GetType()))
                Managers.Add(this.GetType(), this);
        }
        #endregion

        #region Private Methods
        private static T GetManager<T>()
        {
            if (m_Managers.ContainsKey(typeof(T)))
                return (T)Convert.ChangeType(m_Managers[typeof(T)], typeof(T));
            return (T)Convert.ChangeType(null, typeof(T));
        }
        #endregion
    }

}
