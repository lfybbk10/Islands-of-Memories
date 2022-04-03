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
                    var pos = _colliders[0].transform.position;
                    pos.y += 2f;
                    _hint.gameObject.transform.position = pos;
                    _hint.transform.DOScale(new Vector3(1, 1, 1) * .12f, 0.15f);
                });
            if (_current == null)
            {
                _hint.SetActive(true);
                var pos = _colliders[0].transform.position;
                pos.y += 2f;
                _hint.gameObject.transform.position = pos;
                _hint.transform.DOScale(new Vector3(1, 1, 1) * .12f, 0.15f);
            }
            _current = _colliders[0];
            if (Input.GetKeyDown(KeyCode.E))
            {
                var throwable = _current.GetComponent<Throw>();
                _inventory.Add(new Item(throwable.ItemInfo));
                throwable.Object.transform.DOScale(new Vector3(0, 0, 0), 0.15f).OnComplete(() =>
                {
                    Destroy(throwable.Object);
                });
                _hint.transform.DOScale(Vector3.zero, 0.15f).OnComplete(() => { _hint.gameObject.SetActive(false); _current = null; });
            }
        }
        else
        {
            if (_current != null)
            {
                _hint.transform.DOScale(Vector3.zero, 0.15f).OnComplete(() => { _hint.gameObject.SetActive(false); _current = null; });
            }
               
        }
    }
}