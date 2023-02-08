namespace SEUtility.Common.Models;

public class Blueprint : BaseItem
{
    public IReadOnlyList<BlueprintItem> Prerequisites { get; init; } = default!;
    public IReadOnlyList<BlueprintItem> Results { get; init; } = default!;
    public decimal BaseProductionTimeInSeconds { get; init; }
    public bool IsPrimary { get; init; }
    internal Blueprint() { }

    public override string ToString()
    {
        return "Blueprint: " + base.ToString();
    }
}
