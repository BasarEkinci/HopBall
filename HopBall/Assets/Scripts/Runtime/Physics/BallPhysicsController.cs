using System;
using Runtime.Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Physics
{
    public class BallPhysicsController : MonoBehaviour
    {
        [SerializeField] private float initialForceMagnitude = 5.0f;
        [SerializeField] private float maxSpeed = 10.0f;
        [SerializeField] private ParticleSystem impactEffect;
        
        private Collider _collider;
        private Rigidbody _rigidbody;
        private Vector3 _initialPosition;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }
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
            _initialPosition = transform.position;
            _rigidbody.useGravity = false;
        }
        
        private void FixedUpdate()
        {
            if (_rigidbody.velocity.magnitude > maxSpeed)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * maxSpeed;
            }
        }
        private void OnCollisionEnter(Collision other)
        {
            impactEffect.transform.position = other.GetContact(0).point;
            impactEffect.transform.rotation = Quaternion.LookRotation(-other.GetContact(0).normal);
            impactEffect.Play();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("LowerBound"))
            {
                CoreGameSignals.Instance.OnGameOver?.Invoke();
            }
            
            if(other.gameObject.CompareTag("Coin"))
            {
                CoreGameSignals.Instance.OnCollectCoin?.Invoke();
                Destroy(other.gameObject);
            }
        }
        private void OnGameOver()
        {
            _rigidbody.useGravity = false;
            _collider.enabled = false;
        }
        private void OnGameRestart()
        {
            _rigidbody.isKinematic = true;
            transform.position = _initialPosition;
            _collider.enabled = true;
        }
        private void OnGameStart()
        {
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
            float randomDirection = Random.Range(0, 2) == 0 ? -1.0f : 1.0f;
            Vector3 initialForce = new Vector3(randomDirection, 0.5f, 0) * initialForceMagnitude;
            _rigidbody.AddForce(initialForce);
        }
    }    
}


