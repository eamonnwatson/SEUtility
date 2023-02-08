namespace SEUtility.Common.Models;

public struct ComponentItem
{
    public string Subtype { get; init; }
    public decimal Count { get; init; }
    public ComponentItem(string subtype, decimal count)
    {
        Subtype = subtype;
        Count = count;
    }
    public override string ToString()
    {
        return $"{Count} {Subtype}";
    }
}
