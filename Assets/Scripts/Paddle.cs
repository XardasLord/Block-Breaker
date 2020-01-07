using UnityEngine;

namespace Assets.Scripts
{
    public class Paddle : MonoBehaviour
    {
        [SerializeField] private float _minX = 1f;
        [SerializeField] private float _maxX = 15f;
        [SerializeField] private float _screenWidthInUnits = 16f;

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

            paddlePosition.x = Mathf.Clamp(GetXPos(), _minX, _maxX);

            transform.position = paddlePosition;
        }

        private float GetXPos()
        {
            if (_gameSession.IsAutoPlayEnabled())
            {
                return _ball.transform.position.x;
            }

            // Otherwise get mouse position
            return Input.mousePosition.x / Screen.width * _screenWidthInUnits;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.tag)
            {
                case "EffectExpandPaddle":
                    if (transform.localScale.x < 2f)
                    {
                        transform.localScale += new Vector3(0.5f, 0);
                        _minX += 0.5f;
                        _maxX -= 0.5f;
                    }
                    break;
                case "EffectShrinkPaddle":
                    if (transform.localScale.x > 0.5f)
                    {
                        transform.localScale -= new Vector3(0.5f, 0);
                        _minX -= 0.5f;
                        _maxX += 0.5f;
                    }
                    break;
                case "EffectFastGame":
                    _gameSession.SpeedUpGame();
                    break;
                case "EffectSlowGame":
                    _gameSession.SpeedDownGame();
                    break;
            }
        }
    }
}
