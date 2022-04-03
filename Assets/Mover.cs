using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Infrastructure;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _duration;

    public void Move(GameObject menu)
    {
        if (menu.activeSelf)
        {
            menu.transform.DOLocalMove(new Vector3(0, 1080, 0), _duration)
                .OnComplete(() => { menu.SetActive(false); });
            Game.IsPaused = false;
        }
        else
        {
            menu.SetActive(true);
            menu.transform.DOLocalMove(new Vector3(0, 0, 0), _duration);
            Game.IsPaused = true;
        }
    }
}