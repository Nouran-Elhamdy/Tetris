using UnityEngine;
using System;
using Random = UnityEngine.Random;
using DG.Tweening;
using System.Collections;

namespace PuzzleGames
{
    [Serializable]
   public class Shape : MonoBehaviour
    {
        #region Public Variables

        public ShapeType shapeType;
        public GameObject shapePrefab;
        public int shapeRotation = 0;
        float step;
        float currentRot;
        public bool canMove;
        public bool isGround;
        #endregion
        public int GetRandomRotation()
        {
            int randomIndex = Random.Range(0, Manager.ShapeManager.shapeRotations.Length);
            shapeRotation = Manager.ShapeManager.shapeRotations[randomIndex];
            return shapeRotation;
        }

        public int GetRandomFlip()
        {
            int randomIndex = Random.Range(0, Manager.ShapeManager.shapeScale.Length);
            return Manager.ShapeManager.shapeScale[randomIndex];
        }

        public void MoveInX(int sign)
        {
            step += (sign) * 0.5f;
            transform.position = new Vector3(step , transform.position.y, transform.position.z);
        }
        public void MoveInY()
        {
            StartCoroutine(loopDelay());
            IEnumerator loopDelay()
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z);
                canMove = false;
                yield return new WaitForSeconds(1);
                canMove = true;
            }
        }
        public void Rotate(float rotation)
        {
            currentRot += rotation;
            transform.Rotate(new Vector3(transform.position.x, transform.position.y, currentRot));
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
}
