using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameSession : MonoBehaviour
    {
        [Range(0.1f, 5f)]
        [SerializeField]
        private float _gameSpeed = 1f;

        [SerializeField] private int _pointsPerBlockDestroyed = 83;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private bool _isAutoPlayEnabled = false;

        // state variables
        [SerializeField] private int _currentScore = 0;

        private void Awake()
        {
            // Singleton
            var gameStatusCount = FindObjectsOfType<GameSession>().Length;
            if (gameStatusCount > 1)
            {
                gameObject.SetActive(false); // Just because of the Unity Execution Order https://docs.unity3d.com/Manual/ExecutionOrder.html
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Start()
        {
            _scoreText.text = _currentScore.ToString();
        }

        private void Update()
        {
            Time.timeScale = _gameSpeed;
        }

        public void AddToScore()
        {
            _currentScore += _pointsPerBlockDestroyed;
            _scoreText.text = _currentScore.ToString();
        }

        public void ResetGame()
        {
            Destroy(gameObject);
        }

        public bool IsAutoPlayEnabled()
        {
            return _isAutoPlayEnabled;
        }
    }
}
