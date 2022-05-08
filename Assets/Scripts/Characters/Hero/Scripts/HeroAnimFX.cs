using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Infrastructure;
using UnityEngine;

public class HeroAnimFX : MonoBehaviour
{
    [SerializeField] private GameObject _heroLeftFoot;
    [SerializeField] private GameObject _heroRightFoot;
    [SerializeField] private GameObject _footStepFXPrefab;
    private GameObject _footStepFX;

    private bool isLeftFoot = true;

    private void StepFX()
    {
        _footStepFX = Instantiate(_footStepFXPrefab);
        _footStepFX.transform.position =
            isLeftFoot ? _heroLeftFoot.transform.position : _heroRightFoot.transform.position;
        _footStepFX.GetComponent<ParticleSystem>().Play();
        isLeftFoot = !isLeftFoot;
        Destroy(_footStepFX, _footStepFX.GetComponent<ParticleSystem>().main.duration);
    }
}
