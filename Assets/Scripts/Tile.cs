using PuzzleGames;
using UnityEngine;

namespace KAlabs
{
    public class Tile : MonoBehaviour
    {
        public bool isOccupied;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Tile tile))
            {
                isOccupied = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            isOccupied = false;
        }
    }

}
