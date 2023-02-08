namespace SEUtility.Common.Models;

public class RecipeItem
{
    public decimal Quantity { get; internal set; }
    public BaseItem Item { get; }
    public ItemType ItemType { get; }
    internal RecipeItem(decimal qty, BaseItem item)
    {
        Quantity = qty;
        Item = item;
        ItemType = item.Type;
    }
    public override string ToString()
    {
        return $"{Quantity} - {Item}";
    }
}
