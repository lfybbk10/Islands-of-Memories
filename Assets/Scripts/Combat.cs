using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;


public class Combat : MonoBehaviour
{
    [SerializeField] private Point _point;
    [SerializeField] private LayerMask _damagableMask;
    private readonly Collider[] _colliders = new Collider[30];

    private void Awake()
    {
        RuntimeContext.Instance.combat = this;
    }
    //Используется в аниматоре AxeHit
    private void ApplyDamage()
    {
        var count = Physics.OverlapSphereNonAlloc(_point.Position, 2.5f, _colliders, _damagableMask);
        if (count == 0)
            return;
        _colliders[0].GetComponent<IDamageable>().ApplyDamage(RuntimeContext.Instance.pickedSlot.Slot.Item.Info.Damage);
    }
}