using System;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField] private Point _point;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Equiper _equiper;

    private void Awake()
    {
        RuntimeContext.Instance.dropper = this;
    }
    
    public void Drop(Inventory inventory, UIInventorySlot uiSlot)
    {
        if (_equiper.IsEquip && uiSlot.Slot.Amount == 1)
            _equiper.Deprive(true);
        else
            _spawner.Spawn(uiSlot.Slot.Item, _point.Position);
        inventory.RemoveFromSlot(uiSlot.Slot);
    }
}