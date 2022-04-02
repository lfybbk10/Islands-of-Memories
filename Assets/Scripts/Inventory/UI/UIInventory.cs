using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private ItemInfo _info;
    [SerializeField] private UIInventorySlot[] _uiSlots;
    private Action _inventoryChanged;
    private Action<IInventorySlot, IInventorySlot> _transited;
    private Inventory _inventory;

    private void Awake()
    {
        _inventoryChanged += OnInventoryChanged;
        _transited += OnTransited;
        Setup();
    }

    public Inventory GetInventory() => _inventory;
    
    private void OnTransited(IInventorySlot slot1, IInventorySlot slot2)
    {
        _inventory.Transit(slot1, slot2);
    }
    
    //TODO передавать в качестве параметра обновляемые слоты
    private void OnInventoryChanged()
    {
        foreach (var slot in _uiSlots)
            slot.Render();
    }

    private void Setup()
    {
        _inventory = new Inventory(_uiSlots.Length, _inventoryChanged);
        var allSlots = _inventory.GetSlots();
        for (int i = 0; i < _uiSlots.Length; ++i)
        {
            var slot = allSlots[i];
            var uiSlot = _uiSlots[i];
            uiSlot.Init(_transited);
            uiSlot.Set(slot);
            uiSlot.Render();
        }
    }
}