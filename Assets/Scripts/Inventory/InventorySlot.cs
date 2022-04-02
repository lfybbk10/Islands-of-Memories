public class InventorySlot : IInventorySlot
{
    public bool IsFull => !IsEmpty && Item.Info.MaxQuantity == Item.State.Amount;

    public bool IsEmpty => Item == null;
    public IItem Item { get; private set; }

    public int Amount => IsEmpty ? 0 : Item.State.Amount;

    public void Set(IItem item) => Item = item;

    public void Clear()
    {
        if (IsEmpty)
            return;
        Item.State.Amount = 0;
        Item = null;
    }
}