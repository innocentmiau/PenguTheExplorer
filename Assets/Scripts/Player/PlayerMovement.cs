using System;
using UnityEngine;

namespace Scripts.Player
{
    
    public class PlayerMovement : MonoBehaviour
    {
        
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 50f;
        [SerializeField] private float sprintSpeed = 100f;
        [SerializeField] private float acceleration = 10f;
        [SerializeField] private float deceleration = 8f;
        [SerializeField] private Vector2 jumpForces;
        [SerializeField] private float maxChargeTime = 2f;
        [SerializeField] private RotateFromMain[] rotateFromMain;
    
        [Header("Ground Check")]
        [SerializeField] private Transform[] groundChecks;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundCheckRadius = 1f;
    
        [Header("Components")]
        [SerializeField] private JumpPanelController jumpPanelController;
        
        private Rigidbody2D _rb;
        private bool _isGrounded;
        private float _horizontalInput;
    
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
    
        private bool _jumping = false;
        private bool _isSprinting = false;
        private float _jumped = 0f;
        private float _chargeTime = 0f;
        private float _currentSpeed = 0f;
        private void Update()
        {
            _horizontalInput = Input.GetAxisRaw("Horizontal");
            _isSprinting = Input.GetKey(KeyCode.LeftShift);
            HandleJumpInput();
            HandleSpriteFlip();
        }

        private void FixedUpdate()
        {
            Move();
            CheckGrounded();
        }

        void Move()
        {
            float targetSpeed = 0f;
            if (_horizontalInput != 0)
            {
                targetSpeed = _isSprinting ? sprintSpeed : moveSpeed;
                if (_isCharging) targetSpeed /= 2.5f;
                targetSpeed *= _horizontalInput;
            }

            float speedDifference = targetSpeed - _currentSpeed;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        
            _currentSpeed += speedDifference * accelRate * Time.fixedDeltaTime;
        
            _rb.linearVelocity = new Vector2(_currentSpeed, _rb.linearVelocity.y);
        }
    
        private bool _isCharging = false;
        void HandleJumpInput()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_isCharging)
                StartCharging();
        
            if (Input.GetKey(KeyCode.Space) && _isCharging)
                ContinueCharging();
        
            if (Input.GetKeyUp(KeyCode.Space) && _isCharging)
            {
                if (_isGrounded) ExecuteJump();
                _isCharging = false;
                _chargeTime = 0f;
                jumpPanelController.ResetBar();
            }

            if (_isCharging)
                jumpPanelController.UpdateBars(_chargeTime / maxChargeTime);
        }
        
        void ExecuteJump()
        {
            float chargeRatio = _chargeTime / maxChargeTime;
            Debug.Log($"chargeRatio: {chargeRatio}");
            float jumpForce = Mathf.Lerp(jumpForces.x, jumpForces.y, chargeRatio);
            
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
            _jumped = 0.1f;
            _jumping = true;
            
            Debug.Log($"Jump executed! Charge time: {_chargeTime:F2}s, Jump force: {jumpForce:F2}");
        }
    
        void StartCharging()
        {
            _isCharging = true;
            _chargeTime = 0f;
        }
    
        void ContinueCharging()
        {
            _chargeTime += Time.deltaTime;
        
            if (_chargeTime >= maxChargeTime)
                _chargeTime = maxChargeTime;
        }
        
        private void CheckGrounded()
        {
            foreach (Transform ground in groundChecks)
            {
                _isGrounded = Physics2D.OverlapCircle(ground.position, groundCheckRadius, groundLayer);
                if (_isGrounded) break;
            }
            if (_jumped <= 0f && _isGrounded)
            {
                _jumping = false;
                // see fall distance lol
            }
        }

        private bool _facingRight = true;
        private void HandleSpriteFlip()
        {
            if (_horizontalInput > 0 && !_facingRight)
            {
                Flip();
            }
            else if (_horizontalInput < 0 && _facingRight)
            {
                Flip();
            }
        }
        
        private void Flip()
        {
            _facingRight = !_facingRight;
            transform.Rotate(0, 180, 0);
            foreach (RotateFromMain rfm in rotateFromMain)
            {
                rfm.UpdateRotation(transform);
            }
        }
        
        /*
        void FixedUpdate()
        {
            _rb.linearVelocity = new Vector2(_horizontalInput * moveSpeed, _rb.linearVelocity.y);
        }
    
        void OnDrawGizmosSelected()
        {
            if (groundCheck != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            }
        }
        */
    }
    
}