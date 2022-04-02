using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Infrastructure;
using UnityEngine;
using UnityEngine.UI;

public class InvenotryDisplayer : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private float _duration;
    [SerializeField] private GameObject _inventory;

    private bool _opened;

    private void Awake()
    {
        _closeButton.onClick.AddListener(() =>
        {
            _inventory.transform.DOLocalMove(new Vector3(0, 1080, 0), _duration);
            Game.IsPaused = false;
        });
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (_inventory.activeSelf)
            {
                _inventory.transform.DOLocalMove(new Vector3(0, 1080, 0), _duration).OnComplete(() =>
                {
                    _inventory.SetActive(false);
                });
                Game.IsPaused = false;
            }
            else
            {
                _inventory.SetActive(true);
                _inventory.transform.DOLocalMove(new Vector3(0, 0, 0), _duration);
                Game.IsPaused = true;
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_inventory.activeSelf)
                _inventory.transform.DOLocalMove(new Vector3(0, 1080, 0), _duration).OnComplete(() =>
                {
                    _inventory.SetActive(false);
                });
            Game.IsPaused = false;
        }
    }
}