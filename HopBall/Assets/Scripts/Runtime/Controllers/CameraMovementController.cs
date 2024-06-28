using System.Collections.Generic;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers
{
    public class CameraMovementController : MonoBehaviour
    {
        [SerializeField] private GameObject camera;
        [SerializeField] private Transform target;
        [SerializeField] private float smoothSpeed = 0.125f;
        [SerializeField] private Vector3 offset;
        [SerializeField] private List<GameObject> boxes;
        
        private Vector3 _leftBoxStartPosition;
        private Vector3 _rightBoxStartPosition;
        private Vector3 _startPosition;
        private float _initialY;


        private void OnEnable()
        {
            CoreGameSignals.Instance.OnGameStart += OnGameStart;
            CoreGameSignals.Instance.OnGameRestart += OnGameRestart;
        }

        private void OnDisable()
        {
            CoreGameSignals.Instance.OnGameStart -= OnGameStart;
            CoreGameSignals.Instance.OnGameRestart -= OnGameRestart;            
        }

        private void Start()
        {
            _leftBoxStartPosition = boxes[0].transform.position;
            _rightBoxStartPosition = boxes[1].transform.position;
            _startPosition = camera.transform.position;
            if (target == null)
            {
                enabled = false;
                return;
            }

            _initialY = camera.transform.position.y;
        }

        private void LateUpdate()
        {
            if (target == null)
            {
                return;
            }

            Vector3 desiredPosition = GetDesiredPosition();
            Vector3 currentPosition = camera.transform.position;

            if (desiredPosition.y > _initialY)
            {
                Vector3 newPosition = GetNewPosition(desiredPosition, currentPosition);
                camera.transform.position = Vector3.Lerp(currentPosition, newPosition, smoothSpeed);
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
        
        private void OnGameRestart()
        {
            camera.transform.position = _startPosition;
            _leftBoxStartPosition = boxes[0].transform.position ;
            _rightBoxStartPosition = boxes[1].transform.position;
        }

        private void OnGameStart()
        {
        }
    }
}
