using UnityEngine;

namespace Runtime.Controllers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Transform target;
        [SerializeField] private float smoothSpeed = 0.125f;
        [SerializeField] private Vector3 offset;

        private float _initialY;

        private void Start()
        {
            if (target == null)
            {
                enabled = false;
                return;
            }

            _initialY = transform.position.y;
        }

        private void LateUpdate()
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 currentPosition = cameraTransform.position;

            if (desiredPosition.y > _initialY)
            {
                float newY = Mathf.Max(currentPosition.y, desiredPosition.y);
                Vector3 newPosition = new Vector3(currentPosition.x, newY, currentPosition.z);
                cameraTransform.position = Vector3.Lerp(currentPosition, newPosition, smoothSpeed);
            }
        }
    }
}
