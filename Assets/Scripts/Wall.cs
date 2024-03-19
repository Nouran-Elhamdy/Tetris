using Unity.VisualScripting;
using UnityEngine;

namespace PuzzleGames
{
    public class Wall : MonoBehaviour
    {
        [SerializeField] ShapeSpawner shapeSpawner;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Shape shape))
            {
                if (shapeSpawner.currentShape == shape)
                {
                    if (tag == "Bottom")
                    {
                        shape.isGrounded = true;
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
        private void OnCollisionExit2D(Collision2D collision)
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
