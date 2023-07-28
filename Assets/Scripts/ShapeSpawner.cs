using UnityEngine;
using System;
using System.Linq;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using System.Collections;

namespace PuzzleGames
{
   public class ShapeSpawner : MonoBehaviour
    {
        #region Public Variables
        public List<Shape> shapes;
        public Shape currentShape;
        public Shape nextShape;
        bool isSpawned;
        #endregion

        #region Public Methods

        private void Update()
        {
            if(!isSpawned)
            {
                StartCoroutine(LoopSpwan());
                isSpawned = true;
            }
        }
        public void Spawn()
        {
            int randomIndex = Random.Range(0, shapes.Count);
            currentShape = Instantiate(shapes[randomIndex]);
            currentShape.transform.Rotate(0f, 0f, currentShape.GetRandomRotation());
            currentShape.transform.localScale = new Vector3(currentShape.GetRandomFlip(), 1, 1);
           
            var shapeValidation = shapes[randomIndex].shapeValidations.Find(x => x.shapeRotation == currentShape.shapeRotation);
            currentShape.transform.localPosition = shapeValidation.spawnPosition;   
            currentShape.step = shapeValidation.step; 
            currentShape.layoutType = shapeValidation.shapeLayoutType;       
        }
        #endregion

        #region Private Methods
        #endregion

        IEnumerator LoopSpwan()
        {
            Spawn();
            yield return new WaitUntil(()=> currentShape.isGround);
            isSpawned = false;
        }
    }
}
