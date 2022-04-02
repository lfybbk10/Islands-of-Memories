
using System.Collections.Generic;

public interface IInventory
{
    bool IsFull { get; }

    void Add(IItem item);

    void Remove(ItemType type, int amount = 1);
    
    IItem GetItem(string name);
    
    IEnumerable<IItem> GetItems();
}