using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Selecter : MonoBehaviour
{
    [SerializeField] private UIInventory _uiInventory;
    [SerializeField] private LayerMask _throwable;
    private Collider[] _colliders = new Collider[50];
    private Inventory _inventory;

    private void Start()
    {
        _inventory = _uiInventory.GetInventory();
    }
    
    private void Update()
    {
        var count = Physics.OverlapSphereNonAlloc(transform.position, 3f, _colliders, _throwable);
        if (count > 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                var throwable = _colliders[0].GetComponent<Throw>();
                _inventory.Add(new Item(throwable.ItemInfo));
                throwable.Object.transform.DOScale(new Vector3(0, 0, 0), 0.15f).OnComplete(() =>
                {
                    Destroy(throwable.Object);
                });
            }
        }
    }   
}
