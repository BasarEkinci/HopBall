using UnityEngine;
using Random = UnityEngine.Random;

public class BallMovementController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    internal Vector3 ballDirection;
    private Vector3[] _startDirections = new Vector3[2];
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _startDirections = new[] { Vector3.right, Vector3.left };
    }

    private void Start()
    {
        _rigidbody.AddForce(_startDirections[Random.Range(0,1)] * 150f);
    }
    public void Shoot(Vector3 direction, float speed)
    {
        _rigidbody.AddForce(new Vector2(direction.x,direction.y) * speed);
    }
}
