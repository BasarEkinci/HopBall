using Runtime.Physics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Controllers
{
    public class BallPhysicsController : MonoBehaviour
    {
        [SerializeField] private float bounceFactor = 1.5f;
        [SerializeField] private float initialForceMagnitude = 5.0f;
        [SerializeField] private float maxSpeed = 10.0f;

        private float _forceScale = 0.01f;

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

            if (collision.gameObject.TryGetComponent<BoxVelocityCalculator>(out BoxVelocityCalculator Box))
            {
                Vector3 collisionNormal = collision.contacts[0].normal;
                float collisionSpeed = Box.SwipeVelocity.magnitude + _rigidbody.velocity.magnitude;
                
                Vector3 bounceForce = collisionNormal * collisionSpeed * bounceFactor;
                _rigidbody.AddForce(bounceForce, ForceMode.Impulse);
                
                if (_rigidbody.velocity.magnitude > maxSpeed)
                {
                    _rigidbody.velocity = _rigidbody.velocity.normalized * maxSpeed;
                }
            }
        }
    }    
}


