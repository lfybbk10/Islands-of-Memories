using System;
using DG.Tweening;
using UnityEngine;
using Random = System.Random;

namespace Characters.Hero.Scripts
{
    public class HeroAnimSounds : MonoBehaviour
    {
        private AudioSource _audioSource;
        public AudioClip[] running;
        public AudioClip[] walk;
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void StepSound()
        {
            _audioSource.PlayOneShot(running[new Random().Next(0,running.Length)]);
        }

        public void WalkSound()
        {
            _audioSource.PlayOneShot(walk[new Random().Next(0,walk.Length)]);
        }
    }
}