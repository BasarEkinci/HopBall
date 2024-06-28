using UnityEngine;

namespace Runtime.Physics
{
    public class BoxVelocityCalculator : MonoBehaviour
    {
        private Vector2 _startTouchPosition;
        private Vector2 _currentTouchPosition;
        private Vector2 _previousTouchPosition;
        private float _startTime;
        private float _previousTime;
        private const float _swipeVelocityScale = 0.0001f;

        public Vector2 SwipeVelocity { get; private set; }

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

            SwipeVelocity = displacement / deltaTime * _swipeVelocityScale;

            _previousTouchPosition = _currentTouchPosition;
            _previousTime = currentTime;
        }

        private void HandleTouchEnded()
        {
            SwipeVelocity = Vector2.zero;
        }
    }
}
