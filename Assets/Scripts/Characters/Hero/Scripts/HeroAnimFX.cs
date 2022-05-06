using System.Collections;
using System.Collections.Generic;
using Infrastructure;
using UnityEngine;

public class HeroAnimFX : MonoBehaviour
{
    [SerializeField] private GameObject _heroLeftFoot;
    [SerializeField] private GameObject _heroRightFoot;
    [SerializeField] private GameObject _footStepFXPrefab;
    private GameObject _footStepFX;

    private bool isLeftFoot = true;
    
    public void StepFX()
    {
        _footStepFX = Instantiate(_footStepFXPrefab);
        print(_footStepFX.transform.childCount);
        _footStepFX.transform.position = isLeftFoot ? _heroLeftFoot.transform.position : _heroRightFoot.transform.position;
        _footStepFX.GetComponent<ParticleSystem>().Play();
        isLeftFoot = !isLeftFoot;
    }
    
}
