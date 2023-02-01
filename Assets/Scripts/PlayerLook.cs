using UnityEngine;

namespace Scripts
{
  public class PlayerLook : MonoBehaviour
    {
        private const string RotateSpeed = "RotateSpeed";

        [SerializeField] private Animator _playerAniman;
        [SerializeField] private float MouseSensitivity = 500f;

        private PlayerMove _playerMove;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;

            TryGetComponent<PlayerMove>(out _playerMove);
        }

        private void Update()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseTurn = mouseX * MouseSensitivity * Time.deltaTime;

            _playerAniman.SetFloat(RotateSpeed, Mathf.Round(mouseTurn));

            transform.Rotate(Vector3.up * mouseTurn);

            // Debug.DrawRay(transform.position, Vector3.up * mouseX, Color.green);
        }
    }
}

