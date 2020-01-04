using UnityEngine;

namespace Assets.Scripts
{
    public class Block : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject);
        }
    }
}
