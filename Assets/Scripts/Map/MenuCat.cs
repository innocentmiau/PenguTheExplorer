using System;
using Scripts.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Map
{
    public class MenuCat : MonoBehaviour
    {

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private int catNumber;
        [SerializeField] private Transform[] catBounds;

        [SerializeField] private AnimationClip idleAnimation;
        [SerializeField] private AnimationClip walkingAnimation;
        [SerializeField] private AnimationClip playingAnimation;
        [SerializeField] private AnimationClip sleepingAnimation;
        [SerializeField] private Animator animator;
        
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.linearVelocity = Vector2.zero;
        }

        private void Start()
        {
            StartIdle();
        }

        private void UpdateAnimator(AnimationClip clip)
        {
            if (animator != null)
                animator.Play(clip.name);
        }
        
        private void StartIdle()
        {
            float time = Random.Range(Constants.IdleTime, Constants.IdleTimeMax);
            UpdateAnimator(idleAnimation);
            _rb.linearVelocity = Vector2.zero;
            string movement = Random.Range(0f, 1f) <= 0.5f ? nameof(StartMovingLeft) : nameof(StartMovingRight);
            Invoke(movement, time);
        }

        private void StartMovingLeft()
        {
            spriteRenderer.flipX = true;
            Move(-1f);
        }
        
        private void StartMovingRight()
        {
            spriteRenderer.flipX = false;
            Move(1f);
        }

        private void Move(float value)
        {
            UpdateAnimator(walkingAnimation);
            float movingSpeed = Random.Range(Constants.MovingSpeed, Constants.MovingSpeedMax);
            _rb.linearVelocity = new Vector2(value, 0f) * movingSpeed;
            float time = Random.Range(Constants.MovingTime, Constants.MovingTimeMax);
            Invoke(nameof(PlayingOrSleep), time);
        }

        private void PlayingOrSleep()
        {
            float time = 0f;
            if (Random.Range(0f, 1f) <= 0.5f)
            {
                if (Random.Range(0f, 1f) <= 0.5f)
                {
                    UpdateAnimator(playingAnimation);
                    time = Random.Range(Constants.PlayingTime, Constants.PlayingTimeMax);
                }
                else
                {
                    UpdateAnimator(sleepingAnimation);
                    time = Random.Range(Constants.SleepTime, Constants.SleepTimeMax);
                }
            }
            Invoke(nameof(StartIdle), time);
        }
        
        
    }
}