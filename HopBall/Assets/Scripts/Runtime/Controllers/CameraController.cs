using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; 
    public float smoothSpeed = 0.125f; 
    public Vector2 offset;

    private Rigidbody2D _targetRb;
    private Vector2 _targetLastPosition;
    private void Awake()
    {
        _targetRb = target.GetComponent<Rigidbody2D>();
        _targetLastPosition = target.transform.position;
    }

    private void LateUpdate()
    {
        Vector2 currentPosition = _targetRb.position;
        
        if (currentPosition.y < _targetLastPosition.y)
        {
            return;
        }
        
        Vector3 desiredPosition = target.position + (Vector3)offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, 0, 0);
        transform.position = smoothedPosition;
        _targetLastPosition = currentPosition;
    }
}
