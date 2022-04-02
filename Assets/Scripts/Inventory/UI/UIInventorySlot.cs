using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private UIInventoryItem _uiInventoryItem;

    private IInventorySlot _slot { get; set; }
    private Action<IInventorySlot, IInventorySlot> _transited;
    
    public void Set(IInventorySlot slot)
    {
        _slot = slot;
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        var otherSlotUI = eventData.pointerDrag.GetComponentInParent<UIInventorySlot>();
        var otherSlot = otherSlotUI._slot;
        _transited.Invoke(otherSlot, _slot);
    }

    public void Render()
    {
        if (_slot != null)
            _uiInventoryItem.Render(_slot);
    }
    
    public void Init(Action<IInventorySlot, IInventorySlot> transited)
    {
        _transited = transited;
    }
}