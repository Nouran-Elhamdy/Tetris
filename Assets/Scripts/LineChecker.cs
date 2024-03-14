using PuzzleGames;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KAlabs
{
    public class LineChecker : MonoBehaviour
    {
        public bool isFinishLine;
        public List<Tile> tiles = new List<Tile>();
        public ShapeSpawner spawner;

        private void Update()
        {
            CheckIfTouched();
        }
        public void CheckIfTouched()
        {
            if (isFinishLine) 
            {
                bool isOccupied = tiles.Any(tile => tile.isOccupied == true);

                if(isOccupied) 
                {
                  spawner.canSpawn = false;    
                }
                else
                {
                    spawner.canSpawn = true;
                }
            }
          
        }
    }

}
