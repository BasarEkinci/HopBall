using UnityEngine;

namespace Runtime.Controllers
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] private Vector3 initialForce;

        private Rigidbody _rigidbody;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            _rigidbody.AddForce(initialForce,ForceMode.Impulse);
        }
    }    
}


