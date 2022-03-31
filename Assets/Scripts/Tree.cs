using DG.Tweening;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float _heath;

    public void Damage(float value)
    {
      
        _heath -= value;
        print(_heath);
        if (_heath < 0)
            Kill();
    }
    
    private void Kill()
    {
        Destroy(gameObject);
        //TODO выпадение дров.
    }
}