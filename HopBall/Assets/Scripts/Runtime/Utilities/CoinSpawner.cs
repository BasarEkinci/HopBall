using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Utilities
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject coin;
        [SerializeField] private GameObject coinsParent;
        [SerializeField] private List<Transform> coinSpawnPoints;
        private void Start()
        {
            InvokeRepeating(nameof(SpawnCoin), 0, 5f);
        }        
        internal void ClearAllCoins()
        {
            foreach (Transform coinChild in coinsParent.transform)
            {
                Destroy(coinChild.gameObject);
            }
        }
        internal void SpawnCoin()
        {
            int coinChance = Random.Range(0, 100);
            if (coinChance < 10)
            {
                for (int i = 0; i < 18; i++)
                {
                    Instantiate(coin,coinSpawnPoints[i].position,Quaternion.identity,coinsParent.transform);
                }
            }
            else if(coinChance < 60)
            {
                Instantiate(coin,coinSpawnPoints[Random.Range(0,coinSpawnPoints.Count)].position,Quaternion.identity,coinsParent.transform);
            }
        }
    }
}
