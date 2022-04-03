using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Infrastructure;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private float _duration;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_menu.activeSelf)
            {
                _menu.transform.DOLocalMove(new Vector3(0, 1080, 0), _duration).OnComplete(() =>
                {
                    _menu.SetActive(false);
                });
                Game.IsPaused = false;
            }
            else
            {
                _menu.SetActive(true);
                _menu.transform.DOLocalMove(new Vector3(0, 0, 0), _duration);
                Game.IsPaused = true;
            }
        }   
    }
}