using UnityEngine;

namespace Runtime.Controllers
{
    public class BallPhysicsController : MonoBehaviour
    {
        [SerializeField] private float bounceFactor = 1.5f;
        [SerializeField] private float initialForceMagnitude = 5.0f;
        [SerializeField] private float maxSpeed = 10.0f;

        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();

            float randomDirection = Random.Range(0, 2) == 0 ? -1.0f : 1.0f;
            Vector3 initialForce = new Vector3(randomDirection, 0.5f, 0) * initialForceMagnitude;
            _rigidbody.AddForce(initialForce, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Vector3 collisionNormal = collision.contacts[0].normal;
            float collisionSpeed = collision.relativeVelocity.magnitude;

            Vector3 bounceForce = collisionNormal * Mathf.Min(collisionSpeed, maxSpeed) * bounceFactor;
            _rigidbody.AddForce(bounceForce, ForceMode.Impulse);
            
            if (_rigidbody.velocity.magnitude > maxSpeed)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * maxSpeed;
            }
        }
    }    
}


