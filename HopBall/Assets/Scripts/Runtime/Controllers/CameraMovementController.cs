using UnityEngine;

namespace Runtime.Controllers
{
    public class CameraMovementController : MonoBehaviour
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

            _initialY = cameraTransform.position.y;
        }

        private void LateUpdate()
        {
            if (target == null)
            {
                return;
            }

            Vector3 desiredPosition = GetDesiredPosition();
            Vector3 currentPosition = cameraTransform.position;

            if (desiredPosition.y > _initialY)
            {
                Vector3 newPosition = GetNewPosition(desiredPosition, currentPosition);
                cameraTransform.position = Vector3.Lerp(currentPosition, newPosition, smoothSpeed);
            }
        }

        private Vector3 GetDesiredPosition()
        {
            return target.position + offset;
        }

        private Vector3 GetNewPosition(Vector3 desiredPosition, Vector3 currentPosition)
        {
            float newY = Mathf.Max(currentPosition.y, desiredPosition.y);
            return new Vector3(currentPosition.x, newY, currentPosition.z);
        }
    }
}
