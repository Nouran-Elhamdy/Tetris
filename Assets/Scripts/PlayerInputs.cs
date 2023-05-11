using UnityEngine;

namespace PuzzleGames
{
   public class PlayerInputs : MonoBehaviour
    {
        [SerializeField] ShapeSpawner shapeSpawner;
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Debug.Log("here");
                shapeSpawner.currentShape.MoveOneUnit(-1);
            }
            else if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                shapeSpawner.currentShape.MoveOneUnit(1);
            }
        }
    }

}
