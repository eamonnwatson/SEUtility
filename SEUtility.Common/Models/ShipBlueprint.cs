namespace SEUtility.Common.Models;

public class ShipBlueprint
{
    private readonly List<BlueprintItem> items = new();
    public IEnumerable<BlueprintItem> Items { get => items; }
    public string Name { get; }
    public CubeSize CubeSize { get; }
    internal ShipBlueprint(string name, CubeSize size)
    {
        Name = name;
        CubeSize = size;
    }

    internal void AddItems(IEnumerable<BlueprintItem> items)
    {
        this.items.AddRange(items);
    }
}
