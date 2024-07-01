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
        [SerializeField] private Transform previousHighScoreTextTransform;
        
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
            InitializeUI();
        }

        private void Update()
        {
            UpdateScore();
            UpdateScoreText();
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
            UpdatePreviousHighScoreTextPosition();
        }

        private void OnCollectCoin()
        {
            _coinAmount++;
            coinText.text = _coinAmount.ToString();
        }

        private void OnGameOver()
        {
            ShowEndGamePanel();
            UpdateEndGameScores();
        }

        private void OnGameRestart()
        {
            ShowGamePanel();
            ResetScores();
        }

        private void OnGameStart()
        {
            ShowGamePanel();
            coinText.text = _coinAmount.ToString();
        }

        private void InitializeUI()
        {
            gamePanel.SetActive(true);
            endGamePanel.SetActive(false);
            ResetScores();
        }

        private void UpdateScore()
        {
            _score = (int)ball.transform.position.y;
            if (_score > _currentScore)
            {
                _currentScore = _score;
            }
            SetHighScore();
        }

        private void UpdateScoreText()
        {
            scoreText.text = _currentScore + "m";
        }

        private void SetHighScore()
        {
            if (_score > _highScore)
            {
                _highScore = _score;
                highScoreText.text = _highScore.ToString();
            }
        }

        private void UpdatePreviousHighScoreTextPosition()
        {
            previousHighScoreTextTransform.position = new Vector3(-0.6f, _highScore, 0.8f);
        }

        private void ShowEndGamePanel()
        {
            gamePanel.SetActive(false);
            endGamePanel.SetActive(true);
        }

        private void ShowGamePanel()
        {
            gamePanel.SetActive(true);
            endGamePanel.SetActive(false);
        }

        private void UpdateEndGameScores()
        {
            currentScoreText.text = $"Skor : {_currentScore}m";
            highScoreText.text = $"Rekor :{_highScore}m";
        }

        private void ResetScores()
        {
            _score = 0;
            _currentScore = 0;
            previousHighScoreText.text = _highScore + "m";
        }
    }
}
