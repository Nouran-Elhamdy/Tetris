using UnityEngine;
using System;
using Random = UnityEngine.Random;

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

        #endregion

        #region Private Variables

        private readonly int[] shapeRotations = { 0, 90, 180, 270, -90, -180, -270};
        private readonly int[] shapeScale = { -1, 1 };

        #endregion
        public int GetRandomRotation()
        {
            int randomIndex = Random.Range(0, shapeRotations.Length);
            shapeRotation = shapeRotations[randomIndex];
            return shapeRotation;
        }

        public int GetRandomFlip()
        {
            int randomIndex = Random.Range(0, shapeScale.Length);
            return shapeScale[randomIndex];
        }

        public void MoveOneUnit(int sign)
        {
            step += (sign) * 0.5f;
            transform.position = new Vector3(step , transform.position.y, transform.position.z);
            Debug.Log(transform.position);
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
