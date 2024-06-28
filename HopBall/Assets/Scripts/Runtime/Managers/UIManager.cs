using Runtime.Signals;
using TMPro;
using UnityEngine;

namespace Runtime.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject ball;
        [SerializeField] private GameObject gamePanel;
        [SerializeField] private GameObject endGamePanel;
        
        [SerializeField] private TextMeshPro previousHighScoreText;

        [SerializeField] private TMP_Text coinText;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text highScoreText;
        [SerializeField] private TMP_Text currentScoreText;

        private int _coinAmount;
        private int _score;
        private int _highScore;
        private int _currentScore = 0;
        
        private void OnEnable()
        {
            CoreGameSignals.Instance.OnGameStart += OnGameStart;
            CoreGameSignals.Instance.OnGameRestart += OnGameRestart;
            CoreGameSignals.Instance.OnGameOver += OnGameOver;
            CoreGameSignals.Instance.OnCollectCoin += OnCollectCoin;
        }

        private void Start()
        {
            if(!gamePanel.activeSelf)
                gamePanel.SetActive(true);
            if(endGamePanel.activeSelf)
                endGamePanel.SetActive(false);
            _currentScore = 0;
            _score = 0;
            _coinAmount = 0;
        }

        private void Update()
        {
            SetHighScore();
            _score = (int)ball.transform.position.y;
            if (_score > _currentScore)
            {
                _currentScore = _score;
            }
            scoreText.text = _currentScore.ToString();
        }

        private void OnDisable()
        {
            CoreGameSignals.Instance.OnGameStart -= OnGameStart;
            CoreGameSignals.Instance.OnGameRestart -= OnGameRestart;
            CoreGameSignals.Instance.OnGameOver -= OnGameOver;
            CoreGameSignals.Instance.OnCollectCoin -= OnCollectCoin;
        }

        public void RestartButton()
        {
            CoreGameSignals.Instance.OnGameRestart?.Invoke();
        }

        private void OnCollectCoin()
        {
            _coinAmount++;
            coinText.text = _coinAmount.ToString();
        }

        private void OnGameOver()
        {
            gamePanel.SetActive(false);
            endGamePanel.SetActive(true);
            currentScoreText.text = "Skor : " + _currentScore;
            highScoreText.text = "Rekor : " + _highScore;
        }
        private void OnGameRestart()
        {
            if(!gamePanel.activeSelf)
                gamePanel.SetActive(true);
            
            if(endGamePanel.activeSelf)
                endGamePanel.SetActive(false);
            
            previousHighScoreText.text = _highScore + "m";
            _score = 0;
            _currentScore = 0;
        }
        private void OnGameStart() 
        {
            if(!gamePanel.activeSelf)
                gamePanel.SetActive(true);
            if(endGamePanel.activeSelf)
                endGamePanel.SetActive(false);

            coinText.text = _coinAmount.ToString();
        }
        private void SetHighScore()
        {
            if (_score > _highScore)
            {
                _highScore = _score;
                highScoreText.text = _highScore.ToString();
            }
        }
    }
}
