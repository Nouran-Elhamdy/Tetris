using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using System.Collections;

namespace PuzzleGames
{
    public class Shape : MonoBehaviour
    {
        #region Public Variables

        public ShapeType shapeType = default;
        public GameObject shapePrefab = default;
        public bool canMoveInY = default;
        public RectInt rectInt = default;
        public bool isLocked = default;
        public List<Transform> tiles = default;

        #endregion
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && !isLocked)
            {
                MoveInX(-1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && !isLocked)
            {
                MoveInX(1);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && !isLocked)
            {
                Rotate();
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow) || canMoveInY)
            {
                MoveInY();
            }
        }

        //public int GetRandomRotation()
        //{
        //    int randomIndex = Random.Range(0, shapeValidations.Count);
        //    shapeRotation = shapeValidations[randomIndex].shapeRotation;
        //    return shapeRotation;
        //}

        public int GetRandomFlip()
        {
            int randomIndex = Random.Range(0, Manager.ShapeManager.shapeScale.Length);
            return Manager.ShapeManager.shapeScale[randomIndex];
        }
        bool IsValidPositionX(int direction)
        {
            foreach (Transform t in tiles) 
            { 
                var tilePosition = new Vector2(Mathf.RoundToInt(t.position.x), Mathf.RoundToInt(t.position.y));
                if (tilePosition.x + direction < rectInt.xMin || tilePosition.x + direction >= rectInt.xMax)
                {
                    return false;
                }
            }
            return true;
        }
        bool IsValidPositionY(int direction)
        {
            foreach (Transform t in tiles)
            {
                var tilePosition = new Vector2(Mathf.RoundToInt(t.position.x), Mathf.RoundToInt(t.position.y));
                if (tilePosition.y + direction <= rectInt.yMin)
                {
                    isLocked = true;
                    return false;
                }
            }
            return true;
        }
        bool IsValidRotation()
        {
            foreach (Transform t in tiles)
            {
                var tilePosition = new Vector2(Mathf.RoundToInt(t.position.x), Mathf.RoundToInt(t.position.y));
                if (tilePosition.y <= rectInt.yMin || tilePosition.x  < rectInt.xMin || tilePosition.x  >= rectInt.xMax)
                {
                    return false;
                }
            }
            return true;
        }
        public void MoveInX(int direction)
        {
            if (!IsValidPositionX(direction)) return;

            transform.position += new Vector3(direction, 0, 0);
        }
        public void MoveInY()
        {
            if (!IsValidPositionY(-1)) return;

            StartCoroutine(loopDelay());
            IEnumerator loopDelay()
            {
                transform.position += new Vector3(0, -1, 0);
                canMoveInY = false;
                yield return new WaitForSeconds(1f);
                canMoveInY = true;
            }
        }
        public void Rotate()
        {
            transform.Rotate(new Vector3(0, 0, -90));

            if(!IsValidRotation())
            {
                transform.Rotate(new Vector3(0, 0, 90));
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
