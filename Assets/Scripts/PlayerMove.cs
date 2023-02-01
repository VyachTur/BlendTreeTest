using UnityEngine;

namespace Scripts
{
  [RequireComponent(typeof(CharacterController))]
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
        private float _verticalInput;
        private float _jumpSpeed;
        private bool _isPlayerJump;
        private float _speedLerp;


        private Vector3 _playerLookPoint;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
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

            Vector3 dir = transform.forward * _speedLerp * _moveSpeed * Time.fixedDeltaTime;

            dir = new Vector3(dir.x, _jumpSpeed * Time.fixedDeltaTime, dir.z);

            _characterController.Move(dir); // player move forward fith gravity

            // Debug.DrawRay(transform.position, new Vector3(dir.x, 0f, dir.z) * 10f, Color.red);

            var speed = _characterController.velocity.magnitude * _speedLerp;
            
            _playerAnimator.SetFloat(Speed, speed);
        }
    }
}

