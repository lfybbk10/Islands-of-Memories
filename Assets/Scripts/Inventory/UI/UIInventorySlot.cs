using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private UIInventoryItem _uiInventoryItem;

    public IInventorySlot Slot { get; private set; }
    private Action<IInventorySlot, IInventorySlot> _transited;
    
    public void Set(IInventorySlot slot)
    {
        Slot = slot;
    }
    
    public virtual void OnDrop(PointerEventData eventData)
    {
        var otherSlotUI = eventData.pointerDrag.GetComponentInParent<UIInventorySlot>();
        var otherSlot = otherSlotUI.Slot;
        _transited.Invoke(otherSlot, Slot);
    }

    public void Render()
    {
        if (Slot != null)
            _uiInventoryItem.Render(Slot);
    }
    
    public void Init(Action<IInventorySlot, IInventorySlot> transited)
    {
        _transited = transited;
    }
}