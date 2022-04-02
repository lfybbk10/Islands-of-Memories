using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Text))]
public class FPSCounter : MonoBehaviour
{
    private const float FPS_MEASURE_PERIOD = 0.5f;
    private const string DISPLAY = "{0} FPS";
    private TMP_Text _text;
    private int _fpsAccumulator;
    private float _fpsNextPeriod;
    private int _currentFps;

    private void Start()
    {
        _fpsNextPeriod = Time.realtimeSinceStartup + FPS_MEASURE_PERIOD;
        _text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _fpsAccumulator++;
        if (Time.realtimeSinceStartup > _fpsNextPeriod)
        {
            _currentFps = (int) (_fpsAccumulator / FPS_MEASURE_PERIOD);
            _fpsAccumulator = 0;
            _fpsNextPeriod += FPS_MEASURE_PERIOD;
            _text.text = string.Format(DISPLAY, _currentFps);
        }
    }
}