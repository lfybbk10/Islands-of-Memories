using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tree : MonoBehaviour
{
    [SerializeField] private Point _point;
    [SerializeField] private float _heath;
    [SerializeField] private GameObject _load;
    public void Damage(float value)
    {
        _heath -= value;
        
        if (_heath + float.Epsilon < 0) 
            Kill();
    }
    
    private void Kill()
    {
        for (var i = 0; i < Random.Range(2, 5); i++)
        {
            var l = Instantiate(_load);
            var scale = l.transform.localScale;
            l.transform.localScale = Vector3.zero;
            l.transform.position = _point.Position;
            l.transform.position += Vector3.up;
            l.transform.DOScale(scale, 0.15f);
        }
        gameObject.SetActive(false);
    }
}