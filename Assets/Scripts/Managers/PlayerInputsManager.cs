using System;
using UnityEngine;

namespace PuzzleGames
{
   public class PlayerInputsManager : Manager
    {
        [SerializeField] ShapeSpawner shapeSpawner;
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                shapeSpawner.currentShape.MoveInX(-1);
            }
            else if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                shapeSpawner.currentShape.MoveInX(1);
            }
            else if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                shapeSpawner.currentShape.Rotate(-90);
            }

            if(shapeSpawner.currentShape != null && shapeSpawner.currentShape.canMove && !shapeSpawner.currentShape.isGround)
            {
                shapeSpawner.currentShape.MoveInY();

            }
        }
    }

}
