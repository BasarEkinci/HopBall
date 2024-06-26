using System;
using UnityEngine;

public class BlockMovementController : MonoBehaviour
{
    [SerializeField] private float blockMass;
    
    private Vector3 offset;
    private bool dragging = false;
    private int touchId = -1;

    private float _slideSpeed;
    private Vector2 _lastTouchPosition;

    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            if (touch.phase == TouchPhase.Began)
            {
                Collider2D collider = Physics2D.OverlapPoint(touchPosition);
                if (collider != null && collider.gameObject == gameObject)
                {
                    dragging = true;
                    touchId = touch.fingerId;
                    offset = gameObject.transform.position - touchPosition;
                }
            }
            else if (dragging && touch.fingerId == touchId)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    Vector3 newPosition = touchPosition + offset;
                    newPosition.x = transform.position.x;
                    transform.position = newPosition;
                    
                    Vector2 currentPosition = touch.position;
                    _slideSpeed = ((currentPosition - _lastTouchPosition).magnitude / 10f) / Time.deltaTime;
                    _lastTouchPosition = currentPosition;
                    Debug.Log(_slideSpeed);

                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    dragging = false;
                    touchId = -1;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.TryGetComponent(out BallMovementController ball))
        {
            ball.Shoot(ball.ballDirection,_slideSpeed);
        }
    }
}
