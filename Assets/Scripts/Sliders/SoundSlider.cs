using System;
using UnityEngine;
using UnityEngine.UI;


public class SoundSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private AudioSource _source;

    private void Awake()
    {
        _slider.onValueChanged.AddListener(value => { _source.volume = value; });
    }
}