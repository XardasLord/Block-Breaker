using UnityEngine;

namespace Assets.Scripts
{
    public class Paddle : MonoBehaviour
    {
        [SerializeField] private float minX = 1f;
        [SerializeField] private float maxX = 15f;
        [SerializeField] private float screenWidthInUnits = 16f; 

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            var mouseXPositionInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
            var paddlePosition = new Vector2(transform.position.x, transform.position.y);

            paddlePosition.x = Mathf.Clamp(mouseXPositionInUnits, minX, maxX);

            transform.position = paddlePosition;
        }
    }
}
