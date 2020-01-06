using UnityEngine;

namespace Assets.Scripts
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Paddle _paddle1;
        [SerializeField] private float _xPush = 2f;
        [SerializeField] private float _yPush = 15f;
        [SerializeField] private AudioClip[] _ballSounds;
        [SerializeField] private float _randomFactor = 0.5f;

        // state
        private Vector2 _paddleToBallVector;
        private bool _hasStarted;

        // Cached component references
        private AudioSource _myAudioSource;
        private Rigidbody2D _myRigidBody2D;

        // Start is called before the first frame update
        private void Start()
        {
            _paddleToBallVector = transform.position - _paddle1.transform.position;

            _myAudioSource = GetComponent<AudioSource>();
            _myRigidBody2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (!_hasStarted)
            {
                LockBallToPaddle();
                LaunchOnMouseClick();
            }
        }

        private void LockBallToPaddle()
        {
            var paddlePosition = new Vector2(_paddle1.transform.position.x, _paddle1.transform.position.y);
            transform.position = paddlePosition + _paddleToBallVector;
        }

        private void LaunchOnMouseClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _hasStarted = true;
                _myRigidBody2D.velocity = new Vector2(_xPush, _yPush);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_hasStarted)
            {
                PlayBallSFX();
                AddVelocityTweak();
            }
        }

        private void PlayBallSFX()
        {
            var randomAudioClip = _ballSounds[Random.Range(0, _ballSounds.Length)];
            _myAudioSource.PlayOneShot(randomAudioClip);
        }

        private void AddVelocityTweak()
        {
            var velocityTweak = new Vector2(
                Random.Range(0f, _randomFactor),
                Random.Range(0f, _randomFactor));
            _myRigidBody2D.velocity += velocityTweak;
        }
    }
}
