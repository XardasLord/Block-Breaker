using UnityEngine;

namespace Assets.Scripts
{
    public class GameStatus : MonoBehaviour
    {
        [Range(0.1f, 5f)] 
        [SerializeField] 
        private float _gameSpeed = 1f;

        private void Update()
        {
            Time.timeScale = _gameSpeed;
        }
    }
}
