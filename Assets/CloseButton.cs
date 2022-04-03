using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Infrastructure;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    [SerializeField] private GameObject[] _toClose;
    [SerializeField] private Button _button;

    private void Awake()
    {
        _button.onClick.AddListener(Close);
    }

    private void Close()
    {
        foreach (var obj in _toClose)
        {
            obj.transform.DOLocalMove(new Vector3(0, 1080, 0), 0.25f).OnComplete(() =>
            {
                obj.SetActive(false);
            });
            Game.IsPaused = false;
        }
    }
}
