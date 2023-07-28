using System;
using UnityEngine;

namespace PuzzleGames
{
   public class PlayerInputsManager : Manager
    {
        [SerializeField] ShapeSpawner shapeSpawner;
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow) && shapeSpawner.currentShape.canMoveLeft)
            {
                shapeSpawner.currentShape.MoveInX(-1);
            }
            else if(Input.GetKeyDown(KeyCode.RightArrow) && shapeSpawner.currentShape.canMoveRight)
            {
                shapeSpawner.currentShape.MoveInX(1);
            }
            else if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                SnapShapePositionOnHitWallAndRotate();
                shapeSpawner.currentShape.Rotate();
            }

            if(shapeSpawner.currentShape != null && shapeSpawner.currentShape.canMoveInY && !shapeSpawner.currentShape.isGround)
            {
                shapeSpawner.currentShape.MoveInY();
            }
        }

        private void SnapShapePositionOnHitWallAndRotate()
        {
            switch (shapeSpawner.currentShape.shapeType)
            {
                case ShapeType.I_Shaped:
                case ShapeType.T_Shaped:

                    if (shapeSpawner.currentShape.layoutType == ShapeLayoutType.Vertical)
                    {
                        if (!shapeSpawner.currentShape.canMoveRight)
                        {
                             shapeSpawner.currentShape.MoveInX(-1);
                        }
                        if (!shapeSpawner.currentShape.canMoveLeft)
                        {
                            shapeSpawner.currentShape.MoveInX(1);
                            shapeSpawner.currentShape.MoveInX(1);
                        }
                    }
                    break;
                case ShapeType.L_Shaped:
                    if (shapeSpawner.currentShape.layoutType == ShapeLayoutType.Horizontal)
                    {
                        if (!shapeSpawner.currentShape.canMoveLeft)
                        {
                            shapeSpawner.currentShape.MoveInX(1);
                        }
                    }
                    break;
                case ShapeType.O_Shaped:
                    break;
                case ShapeType.S_Shaped:
                    break;
                default:
                    break;
            }
        }
    }

}
