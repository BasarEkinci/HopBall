using UnityEngine;

public class BallMovementController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector3 lastVelocity;
    public Vector3 ballDirection;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        lastVelocity = _rigidbody.velocity;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        var speed = lastVelocity.magnitude;
        ballDirection = Vector3.Reflect(lastVelocity.normalized, coll.contacts[0].normal);
       // _rigidbody.velocity = ballDirection * Mathf.Max(speed, 0f);        
    }

    public void Shoot(Vector3 direction, float speed)
    {
        _rigidbody.AddForce(new Vector2(direction.x,direction.y) * speed);
    }
}
