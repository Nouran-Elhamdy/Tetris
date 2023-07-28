using Unity.VisualScripting;
using UnityEngine;

namespace PuzzleGames
{
   public class Wall : MonoBehaviour
    {
        [SerializeField] ShapeSpawner shapeSpawner;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Shape shape))
            {
                if(shapeSpawner.currentShape == shape)
                {
                    if (tag == "Bottom")
                    {
                        shape.isGround = true;
                    }
                    else if (tag == "Right")
                    {
                        shape.canMoveRight = false;
                        shape.canMoveLeft = true;
                    }
                    else if (tag == "Left")
                    {
                        shape.canMoveLeft = false;
                        shape.canMoveRight = true;

                    }
                        
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Shape shape))
            {
                if (shapeSpawner.currentShape == shape)
                {
                    if (tag == "Right")
                    {
                        shape.canMoveRight = true;
                    }
                    else if (tag == "Left")
                    {
                        shape.canMoveLeft = true;
                    }
                }
            }
        }
       
    }

}
