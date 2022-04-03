using DG.Tweening;
using UnityEngine;

public class Equiper : MonoBehaviour
{
    [SerializeField] private Point _point;
    [SerializeField] private Spawner _spawner;

    private Throw _current; 
    public bool IsEquip { get; private set; }
    public void Deprive(bool fictive)
    {
        IsEquip = false;
        _current.Rigidbody.isKinematic = false;
        _current.BoxCollider.enabled = true;
        _current.transform.SetParent(null);
        _current = null;
    }
    
    public void Equip(IItem item)
    {
        Deprive();
        _current = _spawner.Spawn(item, _point.Position, _point.transform);
        _current.Rigidbody.isKinematic = true;
        _current.BoxCollider.enabled = false;
        IsEquip = true;
    }

    public void Deprive()
    {
        if (_current != null)
        {
            var current = _current;
            current.gameObject.transform.DOScale(new Vector3(0, 0, 0), 0.15f).OnComplete(() =>
            {
                Destroy(current.gameObject);
            });
        }
        IsEquip = false;
        _current = null;
    }
}