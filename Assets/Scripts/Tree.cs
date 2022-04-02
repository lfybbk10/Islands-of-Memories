using System;
using DG.Tweening;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float _heath;

    public void Damage(float value)
    {
        _heath -= value;
        if (_heath + float.Epsilon < 0) 
            Kill();
    }
    
    private void Kill()
    {
        //TODO выпадение дров.
        Destroy(gameObject);
    }
}