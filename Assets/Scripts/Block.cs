using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private AudioClip _blockDestroySound;
        [SerializeField] private GameObject _blockSparklesVFX;
        [SerializeField] private Sprite[] _hitSprites;
        [SerializeField] private GameObject[] _effects;
        [Range(0, 1)]
        [SerializeField] 
        private float _chanceForEffect = 0.2f;

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
            GenerateSpecialEffect();
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

        private void GenerateSpecialEffect()
        {
            if (Math.Abs(_chanceForEffect) > 0f && Random.value < _chanceForEffect)
            {
                // TODO: To improve performance we can destroy this game object if it triggers the loose collider for example
                Instantiate(
                    _effects[Random.Range(0, _effects.Length - 1)],
                    transform.position,
                    transform.rotation);
            }
        }
    }
}
