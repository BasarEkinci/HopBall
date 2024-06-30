using System.Collections.Generic;
using Runtime.Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Utilities
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject coin;
        [SerializeField] private GameObject coinsParent;
        [SerializeField] private List<Transform> coinSpawnPoints;
        
        
        private void OnEnable()
        {
            CoreGameSignals.Instance.OnGameRestart += OnGameRestart;
        }

        private void OnDisable()
        {
            CoreGameSignals.Instance.OnGameRestart -= OnGameRestart;
        }
        private void Start()
        {
            InvokeRepeating(nameof(SpawnCoin),0,10);
        }
        
        private void OnGameRestart()
        {
            foreach (Transform coinChild in coinsParent.transform)
            {
                Destroy(coinChild.gameObject);
            }
        }
        private void SpawnCoin()
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
