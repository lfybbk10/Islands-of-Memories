using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject _craft;
    [SerializeField] private GameObject _pause;
    [SerializeField] private Mover _mover;
    [SerializeField] private Button _close;
    [SerializeField] private Button _inventoryButton;
    [SerializeField] private Button _craftButton;
    private GameObject _moved;

    private void Awake()
    {
        _close.onClick.AddListener(
            () =>
            {
                _mover.Move(_menu);
            });
        _inventoryButton.onClick.AddListener(() =>
        {
            _pause.SetActive(false);
            _craft.SetActive(false);
            _inventory.SetActive(true);
        });
        _craftButton.onClick.AddListener(() =>
        {
            _inventory.SetActive(false);
            _pause.SetActive(false);
            _craft.SetActive(true);
        });
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_menu.activeSelf)
            {
                _inventory.SetActive(false);
                _craft.SetActive(false);
                _pause.SetActive(true);
            }
            _mover.Move(_menu);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!_menu.activeSelf)
            {
                _craft.SetActive(false);
                _pause.SetActive(false);
                _inventory.SetActive(true);
            }
            _mover.Move(_menu);
        }
    }
}