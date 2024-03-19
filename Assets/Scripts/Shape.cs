using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;

namespace PuzzleGames
{
    [Serializable]
    public class Shape : MonoBehaviour
    {
        #region Public Variables

        public ShapeType shapeType;
        public ShapeLayoutType layoutType;
        public GameObject shapePrefab;
        public int shapeRotation = 0;
        public bool canMoveInY;
        public bool canRotate;
        public bool canMoveRight;
        public bool canMoveLeft;
        public bool isGrounded;
        public float step;
        public List<ShapeValidation> shapeValidations = new List<ShapeValidation>();
        int index;
        public BoxCollider2D topCollider;
        public BoxCollider2D botCollider;
        public BoxCollider2D leftCollider;
        public BoxCollider2D rightCollider;
        #endregion
        private void Start()
        {
            index = (int)layoutType;
        }
        public int GetRandomRotation()
        {
            int randomIndex = Random.Range(0, shapeValidations.Count);
            shapeRotation = shapeValidations[randomIndex].shapeRotation;
            return shapeRotation;
        }

        public int GetRandomFlip()
        {
            int randomIndex = Random.Range(0, Manager.ShapeManager.shapeScale.Length);
            return Manager.ShapeManager.shapeScale[randomIndex];
        }

        public void MoveInX(int sign)
        {
            transform.position = new Vector3(transform.position.x + (step * sign), transform.position.y, transform.position.z);
        }
        public void MoveInY()
        {
            StartCoroutine(loopDelay());
            IEnumerator loopDelay()
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
                canMoveInY = false;
                yield return new WaitForSeconds(0.5f);
                canMoveInY = true;
            }
        }
        public void Rotate()
        {
            if (index == 1)
                index = 0;
            else
                index = 1;

            layoutType = (ShapeLayoutType)index;
            switch (layoutType)
            {
                case ShapeLayoutType.Horizontal:
                    shapeRotation = 0;
                    break;
                case ShapeLayoutType.Vertical:
                    shapeRotation = 90;
                    break;
                default:
                    break;
            }

            switch (shapeType)
            {
                case ShapeType.I_Shaped:
                    transform.localEulerAngles = new Vector3(transform.position.x, transform.position.y, shapeRotation);
                    break;
                case ShapeType.L_Shaped:
                case ShapeType.T_Shaped:
                case ShapeType.S_Shaped:
                    transform.Rotate(new Vector3(transform.position.x, transform.position.y, transform.position.z + 90));
                    break;
                case ShapeType.O_Shaped:
                    break;
                default:
                    break;
            }
            StartCoroutine(RefreshTrigger());
        }
        IEnumerator RefreshTrigger()
        {
            BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
            boxCollider2D.isTrigger = false;
            yield return new WaitForSeconds(0.05f);
            boxCollider2D.isTrigger = true;
        }
        //TODO transfer this code in another script for edge colliding
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.otherCollider.gameObject.CompareTag("Top"))
            {
                if (collision.gameObject.GetComponent<Shape>())
                {
                    collision.gameObject.GetComponent<Shape>().isGrounded = true;
                    canMoveRight = false;
                    canMoveLeft = false;
                }
            }
            else if (collision.otherCollider.gameObject.CompareTag("Right"))
            {
                canMoveRight = false;
                canMoveLeft = true;
            }
            else if (collision.otherCollider.gameObject.CompareTag("Left"))
            {
                canMoveLeft = false;
                canMoveRight = true;
            }
            else if (collision.otherCollider.gameObject.CompareTag("Bottom"))
            {
                canMoveLeft = false;
                canMoveRight = true;
            }

        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.otherCollider.gameObject.CompareTag("Right"))
            {
                canMoveRight = true;
            }
            else if (collision.otherCollider.gameObject.CompareTag("Left"))
            {
                canMoveLeft = true;
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
    S_Shaped
}
public enum ShapeLayoutType
{
    Horizontal,
    Vertical,
}
[Serializable]
public class ShapeValidation
{
    public ShapeLayoutType shapeLayoutType;
    public int shapeRotation;
    public Vector2 spawnPosition;
    public float step;
}


