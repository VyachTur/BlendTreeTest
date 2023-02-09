using System;
using UnityEngine;

namespace Scripts
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerAttack))]
    public class PlayerMove : MonoBehaviour
    {
        public bool IsPlayerJump => _isPlayerJump;
        private const string Jump = "DoJump";
        private const string Walk = "Walk";
        private const string Run = "Run";
        private const string Speed = "Speed";
        private const string IsRunning = "IsRunning";

        [SerializeField] private float _jumpForce;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private float _gravity = Physics.gravity.y;

        [SerializeField] private Animator _playerAnimator;

        private CharacterController _characterController;
        private PlayerAttack _playerAttack;
        private float _verticalInput;
        private float _jumpSpeed;
        private bool _isPlayerJump;
        private float _speedLerp;
        private Vector3 _dir;

        private Vector3 _playerLookPoint;

        private void Start()
        {
            TryGetComponent(out _characterController);

            TryGetComponent(out _playerAttack);
            _playerAttack.OnPlayerAttackEvent += StopMove;
        }

        private void StopMove() 
        {
            _verticalInput = 0f;
            _speedLerp = 0f;
        }  

        private void Update()
        {
            if (!_characterController.isGrounded) 
            {
                _isPlayerJump = true;
                return;
            }

            _isPlayerJump = false;

            _verticalInput = Input.GetAxis("Vertical");
            _speedLerp = Mathf.Lerp(_speedLerp, _verticalInput, Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _jumpSpeed = _jumpForce;

                _playerAnimator.SetTrigger(Jump);
            }
        }

        private void FixedUpdate()
        {
            _jumpSpeed += _gravity * Time.fixedDeltaTime;

            _dir = transform.forward * _speedLerp * _moveSpeed * Time.fixedDeltaTime;

            _dir = new Vector3(_dir.x, _jumpSpeed * Time.fixedDeltaTime, _dir.z);

            _characterController.Move(_dir); // player move forward fith gravity

            // Debug.DrawRay(transform.position, new Vector3(_dir.x, 0f, _dir.z) * 10f, Color.red);

            var speed = _characterController.velocity.magnitude * _speedLerp;
            
            _playerAnimator.SetFloat(Speed, speed);
        }
    }
}

