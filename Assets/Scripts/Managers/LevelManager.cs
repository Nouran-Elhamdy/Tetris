using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PuzzleGames
{
    public class LevelManager : Manager
    {
        public static Action OnLevelStarted;
        public static Action OnLevelCompleted;

        public int currentLevelNumber;
        public GameObject currentLevel;
        public List<LevelData> gameLevels = new List<LevelData>();
        public bool isGameRunning;

        private void Start()
        {
            isGameRunning = true;
        }
        private void OnEnable()
        {
            OnLevelStarted += InitLevel;
            OnLevelCompleted += DisposeOldLevel;    
        }
        private void OnDisable()
        {
            OnLevelStarted -= InitLevel;
            OnLevelCompleted -= DisposeOldLevel;
        }

        public void InitLevel()
        {
            currentLevelNumber++;
            if(currentLevelNumber <= gameLevels.Count)
            {
                var levelData =  gameLevels.Find(x => x.levelNumber == currentLevelNumber);
                currentLevel  =  Instantiate(levelData.LevelPrefab, transform);
                isGameRunning = true;
            }
            else
            {
                Debug.Log("No More Levels To Load");
            }
        }

        private void DisposeOldLevel()
        {
            Destroy(currentLevel);
            isGameRunning = false;
            InitLevel();
        }
        public bool IsGameCompleted()
        {
            return currentLevelNumber == gameLevels.Count;
        }
    }
}

