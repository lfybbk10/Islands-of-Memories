using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;


public class Combat : MonoBehaviour
{
    [SerializeField] private Point _point;
    [SerializeField] private LayerMask _trees;
    private readonly Collider[] _colliders = new Collider[30];
    
    private void Damage() //Используется в аниматоре Axe Hit
    {
        var count = Physics.OverlapSphereNonAlloc(_point.Position, 2.5f, _colliders, _trees);
        for (int i = 0; i < count; ++i)
        {
            var tree = _colliders[i].GetComponent<Tree>();
            tree.transform.DOShakePosition(0.1f, 0.05f, 3);
            tree.Damage(0.1f);
        }
    }
}