using UnityEngine;

namespace Assets.Scripts
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private AudioClip _blockDestroySound;
        [SerializeField] private GameObject blockSparklesVFX;

        private Level _level;

        private void Start()
        {
            _level = FindObjectOfType<Level>();

            if (tag == "Breakable")
            {
                _level.CountBlocks();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (tag == "Breakable")
            {
                DestroyBlock();
            }
        }

        private void DestroyBlock()
        {
            PlayBlockDestroySFX();
            Destroy(gameObject);
            _level.BlockDestroyed();
            TriggerSparklesVFX();
        }

        private void PlayBlockDestroySFX()
        {
            FindObjectOfType<GameSession>().AddToScore();
            AudioSource.PlayClipAtPoint(_blockDestroySound, Camera.main.transform.position);
        }

        private void TriggerSparklesVFX()
        {
            GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
            Destroy(sparkles, 2f);
        }
    }
}
