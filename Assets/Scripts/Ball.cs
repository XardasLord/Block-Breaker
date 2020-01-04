using UnityEngine;

namespace Assets.Scripts
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Paddle paddle1;
        [SerializeField] private float xPush = 2f;
        [SerializeField] private float yPush = 15f;

        // state
        private Vector2 paddleToBallVector;
        private bool hasStarted;

        // Start is called before the first frame update
        void Start()
        {
            paddleToBallVector = transform.position - paddle1.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (!hasStarted)
            {
                LockBallToPaddle();
                LaunchOnMouseClick();
            }
        }

        private void LockBallToPaddle()
        {
            var paddlePosition = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
            transform.position = paddlePosition + paddleToBallVector;
        }

        private void LaunchOnMouseClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                hasStarted = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);
            }
        }
    }
}
