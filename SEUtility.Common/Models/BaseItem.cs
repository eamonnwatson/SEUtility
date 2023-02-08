namespace SEUtility.Common.Models;

public abstract class BaseItem
{
    public ItemType Type { get; init; }
    public string TypeId { get; init; } = default!;
    public string SubTypeId { get; init; } = default!;
    public string DisplayName { get; internal set; } = default!;
    public string FileName { get; init; } = default!;
    internal BaseItem() { }
    public override string ToString()
    {
        return $"[{TypeId}/{SubTypeId}]";
    }
}
