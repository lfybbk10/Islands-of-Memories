using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button _quit;
    public Button _reload;
    private void Awake()
    {
        _quit.onClick.AddListener(() =>
        {
            Application.Quit(0);
        });
        _reload.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });
    }
}
