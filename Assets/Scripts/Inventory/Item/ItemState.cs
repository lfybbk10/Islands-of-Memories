

public class ItemState : IItemState
{
    public int Amount { get; set; }

    public ItemState(int amount)
    {
        Amount = amount;
    }
}