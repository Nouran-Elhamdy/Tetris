using UnityEngine;
using System.Collections;

namespace PuzzleGames
{
    public class Shape : MonoBehaviour
    {
        #region Public Variables

        public bool canMoveInY = default;
        public bool isLocked = default;
        public ShapeType shapeType = default;
        public ShapeState shapeState = default;
        public Color liveColor;
        public Color ghostColor;
        #endregion
        private void Update()
        {
            if(!isLocked)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    MoveInX(-1);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    MoveInX(1);
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Rotate();
                }

                else if (Input.GetKeyDown(KeyCode.DownArrow) || canMoveInY)
                {
                    if(shapeState == ShapeState.Live && Manager.GameStatus.IsGameStarted)
                    { 
                        MoveInY();
                    }
                }
                if(shapeState == ShapeState.Ghost)
                {
                    MoveGhostShape();
                }
            }
        }
        private bool IsValidPositionX(int direction)
        {
            foreach (Transform t in transform)
            {
                var tilePosition = new Vector2(Mathf.RoundToInt(t.position.x + direction), Mathf.RoundToInt(t.position.y));
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        if (ShapeSpawner.Instance.Grid[i, j] != null)
                        {
                            if ((Vector2)ShapeSpawner.Instance.Grid[i, j].position == tilePosition)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            foreach (Transform t in transform)
            {
                var tilePosition = new Vector2(Mathf.RoundToInt(t.position.x), Mathf.RoundToInt(t.position.y));
                if (tilePosition.x + direction < ShapeSpawner.Instance.RectInt.xMin || tilePosition.x + direction >= ShapeSpawner.Instance.RectInt.xMax)
                {
                    return false;
                }
            }
            return true;
        }
        private bool IsValidPositionY(int direction)
        {
            foreach (Transform t in transform)
            {
                var tilePosition = new Vector2(Mathf.RoundToInt(t.position.x), Mathf.RoundToInt(t.position.y + direction));
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        if (ShapeSpawner.Instance.Grid[i, j] != null)
                        {
                            if ((Vector2)ShapeSpawner.Instance.Grid[i, j].position == tilePosition)
                            {
                                isLocked = true;
                                return false;
                            }
                        }
                    }
                }
                if (tilePosition.y + direction < ShapeSpawner.Instance.RectInt.yMin)
                {
                    isLocked = true;
                    return false;
                }
            }
            return true;
        }
        private bool IsValidPositionForGhostShape()
        {
            foreach (Transform t in transform)
            {
                var tilePosition = new Vector2(Mathf.RoundToInt(t.position.x), Mathf.RoundToInt(t.position.y));
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        if (ShapeSpawner.Instance.Grid[i, j] != null)
                        {
                            if ((Vector2)ShapeSpawner.Instance.Grid[i, j].position == tilePosition)
                            {
                                return false;
                            }
                        }
                    }
                }
                if (tilePosition.y <= ShapeSpawner.Instance.RectInt.yMin)
                {
                    return false;
                }
            }
            return true;
        }
        private bool IsValidRotation()
        {
            bool isRotating;
            foreach (Transform t in transform)
            {
                var tilePosition = new Vector2(Mathf.RoundToInt(t.position.x), Mathf.RoundToInt(t.position.y));
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        if (ShapeSpawner.Instance.Grid[i, j] != null)
                        {
                            if ((Vector2)ShapeSpawner.Instance.Grid[i, j].position == tilePosition)
                            {
                                isRotating = false;
                                return isRotating;
                            }
                        }
                    }
                }
            }

            foreach (Transform t in transform)
            {
                var tilePosition = new Vector2(Mathf.RoundToInt(t.position.x), Mathf.RoundToInt(t.position.y));
                if (tilePosition.y <= ShapeSpawner.Instance.RectInt.yMin ||
                    tilePosition.x < ShapeSpawner.Instance.RectInt.xMin ||
                    tilePosition.x >= ShapeSpawner.Instance.RectInt.xMax ||
                    tilePosition.y >= ShapeSpawner.Instance.RectInt.yMax)
                {
                    isRotating = false;
                    return isRotating;
                }
            }
            isRotating = true;
            return isRotating;
        }
        private void MoveInX(int direction)
        {
            if (!IsValidPositionX(direction)) return;

            transform.position += new Vector3(direction, 0, 0);
        }
        private void MoveInY()
        {
            if (!IsValidPositionY(-1))
            {
                return;
            }

            StartCoroutine(loopDelay());
            IEnumerator loopDelay()
            {
                transform.position += new Vector3(0, -1, 0);
                canMoveInY = false;
                yield return new WaitForSeconds(1f);
                canMoveInY = true;
            }
        }
        private void Rotate()
        {
            if (shapeType == ShapeType.O_Shaped) return;

            transform.Rotate(new Vector3(0, 0, -90));

            if (!IsValidRotation())
            {
                transform.Rotate(new Vector3(0, 0, 90));
            }
        }
        private void MoveGhostShape()
        {
            while (IsValidPositionForGhostShape())
            {
                transform.position += new Vector3(0, -1, 0);
            }
            if (!IsValidPositionForGhostShape())
            {
                transform.position += new Vector3(0, 1, 0);
            }
        }
    }
}
public enum ShapeType
{
    I_Shaped,
    L_Shaped,
    T_Shaped,
    O_Shaped,
    S_Shaped,
    Z_Shaped,
    J_Shaped
}
public enum ShapeState
{
    Live,
    Ghost
}