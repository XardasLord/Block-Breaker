using UnityEngine;

namespace Assets.Scripts
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private AudioClip _blockDestroySound;

        private Level _level;

        private void Start()
        {
            _level = FindObjectOfType<Level>();
            _level.CountBreakableBlocks();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            DestroyBlock();
        }

        private void DestroyBlock()
        {
            AudioSource.PlayClipAtPoint(_blockDestroySound, Camera.main.transform.position);

            Destroy(gameObject);

            _level.BlockDestroyed();
        }
    }
}
