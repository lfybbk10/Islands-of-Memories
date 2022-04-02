using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button _newGame;
    [SerializeField] private Button _settings;
    [SerializeField] private Button _quit;
    [SerializeField] private SettingsMenu _settingsMenu;
    private void Awake()
    {
        _newGame.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });
        _quit.onClick.AddListener(Application.Quit);
        _settings.onClick.AddListener(() =>
        {
            _settingsMenu.gameObject.SetActive(true);
        });
    }
}