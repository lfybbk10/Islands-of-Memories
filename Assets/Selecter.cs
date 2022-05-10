using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Selecter : MonoBehaviour
{
    [SerializeField] private UIInventory _uiInventory;
    [SerializeField] private LayerMask _throwable;
    [SerializeField] private GameObject _hint;
    private Collider[] _colliders = new Collider[50];
    private Inventory _inventory;
    private Collider _current;

    private void Start()
    {
        _inventory = _uiInventory.GetInventory();
    }

    private void Update()
    {
        var count = Physics.OverlapSphereNonAlloc(transform.position, 3f, _colliders, _throwable);
        if (count > 0)
        {
            if (_current != _colliders[0])
                _hint.transform.DOScale(Vector3.zero, 0.15f).OnComplete(() =>
                {
                    _hint.transform.DOScale(new Vector3(1, 1, 1), 0.15f);
                });
            _current = _colliders[0];
            _hint.SetActive(true);
            _hint.transform.DOScale(new Vector3(1, 1, 1), 0.15f);
            if (Input.GetKeyDown(KeyCode.E))
            {
                var throwable = _current.GetComponent<Throw>();
                _inventory.Add(new Item(throwable.ItemInfo));
                throwable.Object.transform.DOScale(new Vector3(0, 0, 0), 0.15f).OnComplete(() =>
                {
                    Destroy(throwable.Object);
                });
                if (count > 1)
                {
                    _hint.transform.DOScale(Vector3.zero, 0.15f).OnComplete(() =>
                    {
                        _hint.gameObject.SetActive(false);
                    });
                }
                _current = null;
            }
        }
        else
        {
            _hint.transform.DOScale(Vector3.zero, 0.15f).OnComplete(() =>
            {
                _hint.gameObject.SetActive(false);
                _current = null;
            });
        }
    }
}