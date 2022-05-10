using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class Narrator : MonoBehaviour
{
    [SerializeField] private Text _text;
    private int _index;
    private void Awake()
    {
        _index = 0;
        var fs = File.Create("1.txt", 2045);
        Debug.Log(fs.Name);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _text.DOText("",5f);
        }
    }
}
