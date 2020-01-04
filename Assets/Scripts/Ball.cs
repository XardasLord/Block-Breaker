﻿using UnityEngine;

namespace Assets.Scripts
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Paddle paddle1;
        [SerializeField] private float xPush = 2f;
        [SerializeField] private float yPush = 15f;
        [SerializeField] private AudioClip[] ballSounds;

        // state
        private Vector2 paddleToBallVector;
        private bool hasStarted;
        
        // Cached component references
        private AudioSource myAudioSource;

        // Start is called before the first frame update
        void Start()
        {
            myAudioSource = GetComponent<AudioSource>();
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (hasStarted)
            {
                var randomAudioClip = ballSounds[Random.Range(0, ballSounds.Length)];
                myAudioSource.PlayOneShot(randomAudioClip);
            }
        }
    }
}