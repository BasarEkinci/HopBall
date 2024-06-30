using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Runtime.Controllers
{
    public class BallSoundController : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> ballSounds;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnCollisionEnter(Collision other)
        {
            switch (other.gameObject.tag)
            {
                case "Obstacle":
                    _audioSource.PlayOneShot(ballSounds[2]);
                    break;
                case "Box":
                    _audioSource.PlayOneShot(ballSounds[0]);
                    break;
                case "Wall":
                    _audioSource.PlayOneShot(ballSounds[3]);
                    break;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Coin"))
            {
                _audioSource.PlayOneShot(ballSounds[1]);
            }
        }
    }
}
