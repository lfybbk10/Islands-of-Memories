using System;
using System.Collections.Generic;
using System.Linq;
using Debug = UnityEngine.Debug;

public class Inventory : IInventory
{
    public bool IsFull => _slots.All(x => x.IsFull);
    public bool IsEmpty => _slots.All(x => x.IsEmpty);

    private List<IInventorySlot> _slots;

    private Action _inventoryStateChanged;

    public Inventory(int initial, Action inventoryChanged)
    {
        _inventoryStateChanged = inventoryChanged;
        _slots = new List<IInventorySlot>();
        for (var i = 0; i < initial; i++)
            _slots.Add(new InventorySlot());
    }


    public bool Contains(ItemType type, int amount) => CountByType(type) >= amount;

    private int CountByType(ItemType type) => _slots.FindAll(x => x.Item.Info.Type == type).Count;


    public void Add(IItem item)
    {
        //Если инвентарь полон. Нельзя ничего добавить, следовательно выходим.
        if (IsFull)
            return;
        if (IsEmpty)
        {
            AddItem(_slots[0], item);
            return;
        }

        //Сначала находим неполный слот, в котором лежит Item такого же типа, как подаваемый, если такого нет, то находим пустой слот.
        var suitable = _slots.Find(x => x.Item != null && x.Item.Equal(item) && !x.IsFull) ??
                       _slots.Find(x => x.IsEmpty);
        if (suitable != null)
            AddItem(suitable, item);
    }

    private void AddItem(IInventorySlot slot, IItem item)
    {
        //Проверяет, влезает ли в ячейку количество предметов, которые мы хотим положить.
        var isFits = slot.Amount + item.State.Amount <= item.Info.MaxQuantity;
        var amountToAdd = isFits ? item.State.Amount : item.Info.MaxQuantity - slot.Amount;
        if (slot.IsEmpty)
            slot.Set(item.Clone(amountToAdd));
        else slot.Item.State.Amount += amountToAdd;
        OnInventoryStateChanged();
        var amountLeft = item.State.Amount - amountToAdd;
        if (amountLeft > 0)
        {
            item.State.Amount = amountLeft;
            Add(item);
        }
    }

    public void RemoveFromSlot(IInventorySlot inventorySlot, int amount = 1)
    {
        if (!inventorySlot.IsEmpty)
        {
            inventorySlot.Item.State.Amount -= amount;
            if (inventorySlot.Amount <= 0)
                inventorySlot.Clear();
            OnInventoryStateChanged();
        }
    }
    public void Remove(ItemType type, int amount = 1)
    {
        if (IsEmpty)
            return;
        var itemsSlots = _slots.FindAll(x => !x.IsEmpty && x.Item.Info.Type == type);
        if (itemsSlots.Count != 0)
        {
            var count = itemsSlots.Count;
            for (var i = count - 1; i >= 0; ++i)
            {
                var slot = itemsSlots[i];
                if (slot.Amount >= amount)
                {
                    slot.Item.State.Amount -= amount;
                    if (slot.Amount <= 0)
                        slot.Clear();
                    OnInventoryStateChanged();
                    return;
                }
                var amountLeft = amount - slot.Amount;
                slot.Clear();
                OnInventoryStateChanged();
                Remove(type, amountLeft);
            }
        }
    }
    
    //Возможно можно сделать через Add
    //Выполняет тразит предметема с одного слота (from) в другой (to)
    public void Transit(IInventorySlot from, IInventorySlot to)
    {
        //Для того, чтобы при резком бросании элемента инвентаря предмет пропадал.
        if (from == to)
            return;
        //Если слот в который мы переносим полный, выходим
        if (to.IsFull)
            return;
        // //Если слот в который мы переносим неполный, но предмет по типу не подходит, выходим
        if (!to.IsEmpty && from.Item != to.Item)
            return;

        //Проверяет, влезает ли в слот количество предметов, которые мы хотим положить.
        var isFits = from.Amount + to.Amount <= from.Item.Info.MaxQuantity;
        var amountToAdd = isFits ? from.Amount : from.Item.Info.MaxQuantity - to.Amount;
        var amountLeft = from.Amount - amountToAdd;

        if (to.IsEmpty)
        {
            to.Set(from.Item);
            from.Clear();
            to.Item.State.Amount += amountToAdd;
        }
        else
        {
            to.Item.State.Amount += amountToAdd;
            if (isFits)
                from.Clear();
            else from.Item.State.Amount = amountLeft;
        }

        OnInventoryStateChanged();
    }

    public IItem GetItem(string name)
    {
        return _slots.Find(x => x.Item.Info.Name == name).Item;
    }

    public IReadOnlyList<IInventorySlot> GetSlots()
    {
        return _slots;
    }

    public IEnumerable<IItem> GetItems()
    {
        foreach (var slot in _slots)
            yield return slot.Item;
    }

    private void OnInventoryStateChanged()
    {
        _inventoryStateChanged?.Invoke();
    }
}