using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class UIInventory : MonoBehaviour
{
    [SerializeField] private ItemInfo _item1;
    [SerializeField] private ItemInfo _item2;
    [SerializeField] private GameObject _vecticalInventory;
    public UIInventorySlot[] _uiSlots;
    public Inventory Inventory;
    
    private void Awake()
    {
        _uiSlots = GetComponentsInChildren<UIInventorySlot>();
        Inventory = new Inventory(9);
        Inventory.InventoryStateChanged += OnInventoryStateChanged;
        SetupInventoryUI(Inventory);
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
            Inventory.TryToAdd(new Item(_item1));
        if(Input.GetKeyDown(KeyCode.P))
            Inventory.TryToAdd(new Item(_item2));

        //Выбор предметов на q, одну штуку
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var selectedSlot = _uiSlots?.FirstOrDefault(x => x.IsSelected);
            if (selectedSlot != null && !selectedSlot.Slot.IsEmpty)
            {
                var item = selectedSlot.Slot.Item;
                var obj = Instantiate(item.Info.GameObject);    
                //TODO Изменить куда спавниться предмет.
                obj.transform.position = new Vector3(1, 1, 1);
                Inventory.RemoveFromSlot(item.Info.Type,selectedSlot.Slot);
            }
        }
        
    }
    
    private void OnInventoryStateChanged()
    {
        foreach (var slot in _uiSlots)
            slot.Render();
    }

    private void SetupInventoryUI(Inventory inventory)
    {
        var allSlots = inventory.GetSlots()?.ToList();
        var allSlotsCount = allSlots?.Count;
        for (int i = 0; i < allSlotsCount; ++i)
        {
            var slot = allSlots[i];
            var uiSlot = _uiSlots[i];
            uiSlot.Set(slot);
            uiSlot.Render();
        }
    }
}