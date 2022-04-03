using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Button _main;
    [SerializeField] private Button _graphics;
    [SerializeField] private Button _controls;
    [SerializeField] private Button _back;
    [SerializeField] private Top top;
    [SerializeField] private GameObject _notimplemented;

    private void Awake()
    {
        _main.onClick.AddListener(NotImplemented);
        _graphics.onClick.AddListener(NotImplemented);
        _controls.onClick.AddListener(NotImplemented);
        _back.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            top.gameObject.SetActive(true);
        });
    }

    private void NotImplemented()
    {
        _notimplemented.SetActive(true);
        Invoke(nameof(DisableText), 2.0f);
    }

    private void DisableText()
    {
        _notimplemented.SetActive(false);
    }
}

