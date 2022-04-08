using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _new;
    [SerializeField] private Button _setting;
    [SerializeField] private Button _quit;


    private void Awake()
    {
        _new.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });
        _quit.onClick.AddListener(() =>
        {
            Application.Quit(0);
        });
    }
}
