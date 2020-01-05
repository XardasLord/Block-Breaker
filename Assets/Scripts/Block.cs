using UnityEngine;

namespace Assets.Scripts
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private AudioClip _blockDestroySound;
        [SerializeField] private GameObject _blockSparklesVFX;
        [SerializeField] private Sprite[] _hitSprites;

        private Level _level;

        private int _timesHit;

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
                HandleHit();
            }
        }

        private void HandleHit()
        {
            _timesHit++;
            var maxHits = _hitSprites.Length + 1;
            if (_timesHit >= maxHits)
            {
                DestroyBlock();
            }
            else
            {
                ShowNextHitSprite();
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
            GameObject sparkles = Instantiate(_blockSparklesVFX, transform.position, transform.rotation);
            Destroy(sparkles, 2f);
        }

        private void ShowNextHitSprite()
        {
            var spriteIndex = _timesHit -1;
            if (_hitSprites[spriteIndex] != null)
            {
                GetComponent<SpriteRenderer>().sprite = _hitSprites[spriteIndex];
            }
            else
            {
                Debug.LogError($"Block sprite is missing from array on the game object: '{gameObject.name}'");
            }
        }
    }
}
