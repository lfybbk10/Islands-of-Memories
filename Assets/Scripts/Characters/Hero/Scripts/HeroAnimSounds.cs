using System;
using UnityEngine;
using Random = System.Random;

namespace Characters.Hero.Scripts
{
    public class HeroAnimSounds : MonoBehaviour
    {
        private AudioSource _audioSource;
        public AudioClip[] steps;
        public AudioClip hit;
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void StepSound()
        {
            _audioSource.PlayOneShot(steps[new Random().Next(0,steps.Length)]);
        }

        public void HitSound()
        {
            _audioSource.PlayOneShot(hit);
        }
    }
}