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
        
        private bool _isGameStarted;
        
        private void OnEnable()
        {
            CoreGameSignals.Instance.OnGameRestart += OnGameRestart;
            CoreGameSignals.Instance.OnGameOver += OnGameOver;
        }

        private void OnDisable()
        {
            CoreGameSignals.Instance.OnGameRestart -= OnGameRestart;
            CoreGameSignals.Instance.OnGameOver -= OnGameOver;
        }
        private void Start()
        {
            InvokeRepeating(nameof(SpawnCoin),0,10);
        }
        
        private void OnGameOver()
        {
            _isGameStarted = false;            
        }
        private void OnGameRestart()
        {
            foreach (Transform coin in coinsParent.transform)
            {
                Destroy(coin.gameObject);
            }
            _isGameStarted = true;
        }
        private void SpawnCoin()
        {
            int coinChance = Random.Range(0, 100);
            if (coinChance < 10)
            {
                Debug.Log("Spawning 10 coins");
                for (int i = 0; i < 18; i++)
                {
                    Instantiate(coin,coinSpawnPoints[i].position,Quaternion.identity,coinsParent.transform);
                }
            }
            else if(coinChance < 60)
            {
                Debug.Log("Spawning 1 coin");
                Instantiate(coin,coinSpawnPoints[Random.Range(0,coinSpawnPoints.Count)].position,Quaternion.identity,coinsParent.transform);
            }
        }
    }
}
