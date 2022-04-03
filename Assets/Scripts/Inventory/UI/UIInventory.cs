using System;
using System.Collections.Generic;
using System.Linq;
using Craft;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private ItemInfo _info;
    [SerializeField] private ItemInfo _info2;
    [SerializeField] private Dropper _dropper;
    [SerializeField] private UIInventorySlot[] _uiSlots;
    [SerializeField] private Equiper _equiper;
    private Inventory _inventory;
    private IList<UIInventoryFastSlot> _fast;
    private UIInventoryFastSlot _selected;
    public static Action InventoryChanged;
    private Action<IInventorySlot, IInventorySlot> _transited;

    private void Awake()
    {
        InventoryChanged += OnInventoryChanged;
        _fast = GetComponentsInChildren<UIInventoryFastSlot>();
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            _inventory.Add(new Item(_info));
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            _inventory.Add(new Item(_info2));
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_selected != null && !_selected.Slot.IsEmpty)
            {
               
                _dropper.Drop(_inventory, _selected);
                if (_selected.Slot.IsEmpty)
                {
                    _selected.ToggleSelect();
                    _selected = null;
                }
            }
        }

        for (int i = 1; i <= _fast.Count; i++)
        {
            if (Input.GetKeyDown(i.ToString())) //TODO поправить сделать черещ string.Format
            {
                var slot = _fast[i - 1];
                if (slot.Slot.IsEmpty)
                    return;
                if (_selected == slot)
                {
                    _selected.ToggleSelect();
                    if (!_selected.Slot.IsEmpty)
                        _equiper.Deprive();
                    _selected = null;
                    return;
                }

                if (_selected != null)
                    _selected.ToggleSelect();
                slot.ToggleSelect();
                _selected = slot;
                if (!_selected.Slot.IsEmpty)
                    _equiper.Equip(_selected.Slot.Item);
            }
        }
    }

    private void Setup()
    {
        _inventory = new Inventory(_uiSlots.Length, InventoryChanged);
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