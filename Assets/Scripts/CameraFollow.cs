using UnityEngine;

namespace Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTargetPoint;
        [SerializeField] private float _followSpeed;
        [SerializeField] private float _rotateSpeed;

        private void FixedUpdate()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _cameraTargetPoint.rotation, Time.fixedDeltaTime * _rotateSpeed);

            // if ((transform.position - _cameraTargetPoint.position).magnitude > 0.02f)
            // {
                transform.position = Vector3.Lerp(transform.position, _cameraTargetPoint.position, Time.fixedDeltaTime * _followSpeed);
            // }
        }
    }
}

