using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Throw : MonoBehaviour, IThrowable
{
    [SerializeField] private ItemInfo _info;
    public IItemInfo ItemInfo => _info;
    private Rigidbody _rigidbody;
    private BoxCollider _collider;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();
    }
    
    public Rigidbody Rigidbody => _rigidbody;
    public BoxCollider BoxCollider => _collider;
    public GameObject Object => gameObject;
}