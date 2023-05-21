using UnityEngine;
using System;
using Random = UnityEngine.Random;
using DG.Tweening;
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
        public bool canMove;
        public bool isGround;
        public float step; 
        public List<ShapeValidation> shapeValidations = new List<ShapeValidation>();

        #endregion
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
                canMove = false;
                yield return new WaitForSeconds(1);
                canMove = true;
            }
        }
        public void Rotate(float rotation)
        {
            transform.Rotate(new Vector3(transform.position.x, transform.position.y, transform.position.z + rotation));
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            isGround = true; 
            if(collision.gameObject.TryGetComponent(out Wall wall)) { }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            isGround = false;
            if (collision.gameObject.TryGetComponent(out Wall wall)) { }
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
        None,
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

}
