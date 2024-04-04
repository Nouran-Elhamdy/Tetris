using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using System;

namespace PuzzleGames
{
    public class ShapeSpawner : MonoBehaviour
    {
        #region Public Variables
        public RectInt RectInt = default;
        public List<Shape> shapes;
        public Shape CurrentShape { get; private set; }
        public Shape CurrentGhostShape { get; private set; }

        public Transform[,] Grid = new Transform[10, 20];
        public static ShapeSpawner Instance;
        #endregion

        #region Public Methods
        private void Awake()
        {
            Instance = this;
        }
        private void OnEnable()
        {
            GameStatus.StartGame += OnGameStarted;
        }

        private void OnDisable()
        {
            GameStatus.StartGame -= OnGameStarted;
        }
        private void Update()
        {
            if (CurrentShape == null) return;

            CurrentGhostShape.transform.rotation = CurrentShape.transform.rotation;
            CurrentGhostShape.transform.position = CurrentShape.transform.position;

            if (CurrentShape.isLocked && Manager.GameStatus.IsGameStarted)
            {
                Destroy(CurrentGhostShape.gameObject);
                UpdateGrid();
                SpawnShape();
                SpawnGhostShape();
                ClearHierarchy();
            }
            DetectCompleteLines();
        }

        private void OnGameStarted()
        {
            ClearGrid();
            SpawnShape();
            SpawnGhostShape();
        }

        private void SpawnShape()
        {
            int randomIndex = Random.Range(0, shapes.Count);
            CurrentShape = Instantiate(shapes[randomIndex], new Vector3(5, 20, 0), Quaternion.identity);
            CurrentShape.transform.SetParent(transform);
            CurrentShape.shapeState = ShapeState.Live;
        }
        private void SpawnGhostShape()
        {
            CurrentGhostShape = Instantiate(CurrentShape, new Vector3(5, 20, 0), Quaternion.identity);
            CurrentGhostShape.transform.SetParent(transform);
            CurrentGhostShape.shapeState = ShapeState.Ghost;
            CurrentGhostShape.canMoveInY = false;

            foreach (Transform tile in CurrentGhostShape.transform)
            {
                tile.gameObject.GetComponent<SpriteRenderer>().color = CurrentShape.ghostColor;
            }
        }
        public void UpdateGrid()
        {
            foreach (Transform tile in CurrentShape.transform)
            {
                var tilePosition = new Vector2(Mathf.RoundToInt(tile.position.x), Mathf.RoundToInt(tile.position.y));
                if (tilePosition.y > RectInt.yMax - 1)
                {
                    Debug.Log("GAME OVER");
                    GameStatus.GameOver?.Invoke();
                    break;
                }
                else
                {
                    Grid[Mathf.RoundToInt(tile.transform.position.x), Mathf.RoundToInt(tile.transform.position.y)] = tile.transform;
                }
            }
        }
        public void DetectCompleteLines()
        {
            bool isCompleteLine;
            for (int i = 0; i < 20; i++)
            {
                isCompleteLine = true;
                for (int j = 0; j < 10; j++)
                {
                    if (Grid[j, i] == null)
                    {
                        isCompleteLine = false;
                        break;
                    }
                }
                if (isCompleteLine)
                {
                    Debug.Log("Complete line found at row: " + i);
                    ClearLine(i);
                }
            }
        }
        private void ClearLine(int row)
        {
            for (int i = 0; i < 10; i++)
            {
                Destroy(Grid[i, row].gameObject);
                Grid[i, row] = null;
            }
            ShiftAllRows(row);
        }
        private void ShiftRow(int row)
        {
            for (int i = 0; i < 10; i++)
            {
                if (Grid[i, row] != null)
                {
                    Grid[i, row - 1] = Grid[i, row];
                    Grid[i, row] = null;
                    Grid[i, row - 1].position += new Vector3(0, -1, 0);
                }
            }
        }
        private void ShiftAllRows(int row)
        {
            for (int i = row + 1; i < 20; i++)
            {
                ShiftRow(i);
            }
        }
        private void ClearHierarchy()
        {
            Shape[] shapes = FindObjectsOfType<Shape>();

            foreach (Shape shape in shapes)
            {
                if (shape.transform.childCount == 0)
                {
                    Destroy(shape.gameObject);
                }
            }
        }
        private void ClearGrid()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            //for (int i = 0; i < 20; i++)
            //{
            //    for (int j = 0; j < 10; j++)
            //    {
            //        if (Grid[j, i] != null)
            //        {
            //            Grid[i, j] = null;    
            //        }
            //    }
            //}
            CurrentShape = null;
            CurrentGhostShape = null;
        }
        #endregion

        #region Private Methods
        #endregion

    }
}
