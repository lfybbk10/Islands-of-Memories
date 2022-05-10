using System;
using System.Collections.Generic;
using UnityEngine;

public class Item : IItem
{
    public IItemInfo Info { get; }
    public IItemState State { get; }

    public Item(IItemInfo info, int amount = 1)
    {
        Info = info;
        State = new ItemState(amount);
    }

    public IItem Clone(int amount)
    {
        var cloned = new Item(Info)
        {
            State = {Amount = amount},
        };
        return cloned;
    }

    public bool Equal(IItem item)
    {
        return Info.Type == item.Info.Type;
    }

    public void Use()
    {
        RuntimeContext.Instance.health.Heal(Info.UsableEffect.Health); 
    }
}