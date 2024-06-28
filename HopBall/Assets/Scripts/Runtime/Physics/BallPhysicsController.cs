using System;
using Runtime.Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Physics
{
    public class BallPhysicsController : MonoBehaviour
    {
        [SerializeField] private float bounceFactor = 1.5f;
        [SerializeField] private float initialForceMagnitude = 5.0f;
        [SerializeField] private float maxSpeed = 10.0f;

        private Vector3 _initialForce;
        private Rigidbody _rigidbody;

        private void OnEnable()
        {
            CoreGameSignals.Instance.OnGameStart += OnGameStart;
            CoreGameSignals.Instance.OnGameRestart += OnGameRestart;
            CoreGameSignals.Instance.OnGameOver += OnGameOver;
        }
        private void OnDisable()
        {
            CoreGameSignals.Instance.OnGameStart -= OnGameStart;
            CoreGameSignals.Instance.OnGameRestart -= OnGameRestart;
            CoreGameSignals.Instance.OnGameOver -= OnGameOver;
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.AddForce(_initialForce,ForceMode.Impulse);
            _rigidbody.useGravity = false;
        }

        private void OnCollisionEnter(Collision collision)
        {

            if (collision.gameObject.TryGetComponent<BoxVelocityCalculator>(out BoxVelocityCalculator box))
            {
                Vector3 collisionNormal = collision.contacts[0].normal;
                float collisionSpeed = box.SwipeVelocity.magnitude + _rigidbody.velocity.magnitude;
                
                Vector3 bounceForce = collisionNormal * collisionSpeed * bounceFactor;
                _rigidbody.AddForce(bounceForce, ForceMode.Impulse);
                
                if (_rigidbody.velocity.magnitude > maxSpeed)
                {
                    _rigidbody.velocity = _rigidbody.velocity.normalized * maxSpeed;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("LowerBound"))
            {
                CoreGameSignals.Instance.OnGameOver?.Invoke();
            }
        }
        
        private void OnGameOver()
        {
            _rigidbody.useGravity = false;
        }

        private void OnGameRestart()
        {
            _rigidbody.useGravity = false;
            transform.position = new Vector3(0, 3, 0);
        }

        private void OnGameStart()
        {
            _rigidbody.useGravity = true;
            float randomDirection = Random.Range(0, 2) == 0 ? -1.0f : 1.0f;
            _initialForce = new Vector3(randomDirection, 0.5f, 0) * initialForceMagnitude;
        }
    }    
}


