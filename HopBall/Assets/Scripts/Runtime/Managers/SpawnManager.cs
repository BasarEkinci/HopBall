using Runtime.Signals;
using Runtime.Utilities;
using UnityEngine;

namespace Runtime.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private ObstacleSpawner obstacleSpawner;
        [SerializeField] private CoinSpawner coinSpawner;

        private void OnEnable()
        {
            CoreGameSignals.Instance.OnGameOver += OnGameOver;
            CoreGameSignals.Instance.OnGameRestart += OnGameRestart;
            CoreGameSignals.Instance.OnGameStart += OnGameStart;
        }

        private void OnDisable()
        {
            CoreGameSignals.Instance.OnGameOver -= OnGameOver;
            CoreGameSignals.Instance.OnGameRestart -= OnGameRestart;
            CoreGameSignals.Instance.OnGameStart -= OnGameStart;
        }

        private void OnGameOver()
        {
            obstacleSpawner.SetInitialPositionToObstacle();
        }

        private void OnGameRestart()
        {
            obstacleSpawner.SetInitialPositionToObstacle();
            coinSpawner.ClearAllCoins();
        }
        private void OnGameStart()
        {
            obstacleSpawner.SetInitialPositionToObstacle();
        }
    }
}
