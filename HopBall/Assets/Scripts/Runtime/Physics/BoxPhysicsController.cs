using UnityEngine;

namespace Runtime.Physics
{
    public class BoxPhysicsController : MonoBehaviour
    {
        [SerializeField] private float forceMagnitude;
        [SerializeField] private float forceDegree;
        
        private Vector2 _startTouchPosition;
        private Vector2 _currentTouchPosition;
        private Vector2 _previousTouchPosition;
        private float _startTime;
        private float _previousTime;
        private const float SwipeVelocityScale = 0.0000001f;

        private Vector2 _swipeVelocity; 
        
        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        HandleTouchBegan(touch);
                        break;
                    case TouchPhase.Moved:
                        HandleTouchMoved(touch);
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        HandleTouchEnded();
                        break;
                }
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.CompareTag("Ball"))
            {
                other.gameObject.GetComponent<Rigidbody>().AddForce(
                    new Vector3(forceDegree, 90, 0) *
                    (forceMagnitude + Mathf.Abs(_swipeVelocity.y * SwipeVelocityScale)));
            }
        }

        private void HandleTouchBegan(Touch touch)
        {
            _startTouchPosition = touch.position;
            _previousTouchPosition = touch.position;
            _startTime = Time.time;
            _previousTime = Time.time;
        }

        private void HandleTouchMoved(Touch touch)
        {
            _currentTouchPosition = touch.position;
            float currentTime = Time.time;

            Vector2 displacement = _currentTouchPosition - _previousTouchPosition;
            float deltaTime = currentTime - _previousTime;

            _swipeVelocity = displacement / deltaTime;
            
            _previousTouchPosition = _currentTouchPosition;
            _previousTime = currentTime;
        }

        private void HandleTouchEnded()
        {
            _swipeVelocity = Vector2.zero;
        }
    }
}
