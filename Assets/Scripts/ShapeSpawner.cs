using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;

namespace PuzzleGames
{
    public class ShapeSpawner : MonoBehaviour
    {
        #region Public Variables
        public List<Shape> shapes;
        public Shape currentShape { get; private set; }
        public Shape nextShape { get; private set; }
        public Transform[,] Grid = new Transform[10, 20];
        public static ShapeSpawner Instance;
        #endregion

        #region Public Methods
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            Spawn();
        }
        private void Update()
        {
            if (currentShape == null) return;

            if (currentShape.isLocked)
            {
                UpdateGrid();
                Spawn();
            }
        }
        public void Spawn()
        {
            int randomIndex = Random.Range(0, shapes.Count);
            currentShape = Instantiate(shapes[randomIndex], new Vector3(5, 20, 0), Quaternion.identity);
            currentShape.transform.SetParent(transform);
        }
        public void UpdateGrid()
        {
            foreach (Transform tile in currentShape.transform)
            {
                Grid[Mathf.RoundToInt(tile.transform.position.x), Mathf.RoundToInt(tile.transform.position.y)] = tile.transform;
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
        #endregion

        #region Private Methods
        #endregion

    }
}
