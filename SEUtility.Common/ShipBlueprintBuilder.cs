using SEUtility.Common.Interfaces;
using SEUtility.Common.Models;

namespace SEUtility.Common;

public class ShipBlueprintBuilder : IShipBlueprintBuilder
{
    private string name = string.Empty;
    private CubeSize size;
    private readonly List<(string, string)> items = new();

    internal ShipBlueprintBuilder() { }

    public IShipBlueprintBuilder AddItem(string typeID, string subTypeID)
    {
        items.Add((typeID, subTypeID));
        return this;
    }

    public ShipBlueprint Build()
    {
        var bp = new ShipBlueprint(name, size);

        var bpItems = items.GroupBy(a => (a.Item1, a.Item2)).Select(a => new BlueprintItem(a.Key.Item1, a.Key.Item2, a.Count())).ToList();
        bp.AddItems(bpItems);
        return bp;
    }

    public IShipBlueprintBuilder SetName(string name)
    {
        this.name = name;
        return this;
    }

    public IShipBlueprintBuilder SetSize(CubeSize size)
    {
        this.size = size;
        return this;
    }
}
