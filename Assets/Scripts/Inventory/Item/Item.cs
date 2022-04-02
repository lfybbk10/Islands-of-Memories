using System;
using System.Collections.Generic;

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
}