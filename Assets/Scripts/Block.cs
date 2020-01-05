using UnityEngine;

namespace Assets.Scripts
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private AudioClip blockDestroySound;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            AudioSource.PlayClipAtPoint(blockDestroySound, Camera.main.transform.position);

            Destroy(gameObject);
        }
    }
}
