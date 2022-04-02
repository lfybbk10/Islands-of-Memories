using DG.Tweening;
using UnityEngine;

public class UIInventoryFastSlot : UIInventorySlot
{
    private bool _isSelected;
    public void ToggleSelect()
    {
        if (!_isSelected)
        {
            transform.DOScale(new Vector3(1, 1, 1) * 1.2f, 0.15f);
            _isSelected = true;
        }
        else
        {
            transform.DOScale(new Vector3(1, 1, 1), 0.15f);
            _isSelected = false;
        }
    }
}