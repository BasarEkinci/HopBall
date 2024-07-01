using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Utilities
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject coin;
        [SerializeField] private List<GameObject> obstacles;
        
        
        private float _initialY;
        private float _initialX;
        private float _obstacleXpositionBound = 0.85f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                float newY = other.gameObject.transform.position.y + Random.Range(15, 30);
                float newX = Random.Range(-_obstacleXpositionBound, _obstacleXpositionBound);
                Vector3 newPosition = new Vector3(newX, newY, 0);
                other.gameObject.transform.position = newPosition;
            }

            if (other.gameObject.CompareTag("Coin"))
            {
                Destroy(other.gameObject);
            }
        }
        internal void SetInitialPositionToObstacle()
        {
            _initialY = Random.Range(20, 35);
            _initialX = Random.Range(-_obstacleXpositionBound, _obstacleXpositionBound);
            obstacles[0].transform.position = new Vector3(_initialX, _initialY, 0);   
            obstacles[1].transform.position = new Vector3(_initialX, _initialY + Random.Range(15,25), 0);
        }
    }
}
