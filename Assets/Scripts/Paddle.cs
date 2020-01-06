using UnityEngine;

namespace Assets.Scripts
{
    public class Paddle : MonoBehaviour
    {
        [SerializeField] private float minX = 1f;
        [SerializeField] private float maxX = 15f;
        [SerializeField] private float screenWidthInUnits = 16f;

        private Ball _ball;
        private GameSession _gameSession;

        private void Start()
        {
            _ball = FindObjectOfType<Ball>();
            _gameSession = FindObjectOfType<GameSession>();
        }

        private void Update()
        {
            var paddlePosition = new Vector2(transform.position.x, transform.position.y);

            paddlePosition.x = Mathf.Clamp(GetXPos(), minX, maxX);

            transform.position = paddlePosition;
        }

        private float GetXPos()
        {
            if (_gameSession.IsAutoPlayEnabled())
            {
                return _ball.transform.position.x;
            }

            // Otherwise get mouse position
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
