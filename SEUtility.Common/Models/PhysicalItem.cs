namespace SEUtility.Common.Models;
public class PhysicalItem : BaseItem
{
    public decimal Mass { get; init; }
    public decimal Volume { get; init; }
    public bool CanPlayerOrder { get; init; }
    internal PhysicalItem() { }

    public override string ToString()
    {
        return "PhysicalItem: " + base.ToString();
    }
}
