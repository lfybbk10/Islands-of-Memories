using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _craft;
    
    private void Awake()
    {
        _button.onClick.AddListener(Show);
    }
    
    private void Show()
    {
        _craft.transform.localPosition = new Vector3(0, 0, 0);
    }
}
