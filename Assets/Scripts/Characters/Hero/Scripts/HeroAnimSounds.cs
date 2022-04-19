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
        public AudioClip[] crounch;
        public AudioClip hit;
        public AudioClip[] start_jump;
        public AudioClip[] end_jump;
        public AudioClip[] roll;

        
        //TO DO разобраться с багом на кол-во шагов 
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

        public void HitSound()
        {
            _audioSource.PlayOneShot(hit);
        }

        public void CrouchSound()
        {
            _audioSource.PlayOneShot(crounch[new Random().Next(0, crounch.Length)]);
        }

        public void StartJump()
        {
            _audioSource.PlayOneShot(start_jump[new Random().Next(0, start_jump.Length)]);
        }
        
        public void EndJump()
        {
            _audioSource.PlayOneShot(end_jump[new Random().Next(0, end_jump.Length)]);
        }
        
        public void RollSound()
        {
            _audioSource.PlayOneShot(roll[new Random().Next(0, roll.Length)]);
        }
        
    }
    
}