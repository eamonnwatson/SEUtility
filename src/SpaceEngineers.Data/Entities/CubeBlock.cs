using SpaceEngineers.Data.Shared;

namespace SpaceEngineers.Data.Entities;
public class CubeBlock : BaseItem
{
    private readonly List<PhysicalItemListItem> _components = [];
    public bool Public { get; private init; }
    public bool IsAirTight { get; private init; }
    public CubeSize CubeSize { get; private init; }
    public decimal BuildTimeSeconds { get; private init; }
    public int PCU { get; private init; }
    public IReadOnlyList<PhysicalItemListItem> Components { get => _components; }
    public int CriticalComponentIndex { get; private set; }
    protected CubeBlock(Guid id, string? type, string typeId, string subTypeId, string displayName, string fileName,
                      bool @public, CubeSize cubeSize, decimal buildTimeSeconds, int pCU)
        : base(id, type, typeId, subTypeId, displayName, fileName)
    {
        Public = @public;
        CubeSize = cubeSize;
        BuildTimeSeconds = buildTimeSeconds;
        PCU = pCU;
    }

    public void AddComponent(int quantity, PhysicalItem item, bool criticalComponent = false)
    {
        //var newListItem = new PhysicalItemListItem(Guid.NewGuid(), quantity, item);
        var newListItem = new PhysicalItemListItem() { Quantity = quantity, Component = item };
        _components.Add(newListItem);
        if (criticalComponent)
            CriticalComponentIndex = Components.Count - 1;
    }

    public static CubeBlock Create(string? type, string typeId, string subTypeId, string displayName, string fileName,
                                   bool @public, CubeSize cubeSize, decimal buildTimeSeconds, int pCU)
    {
        return new CubeBlock(Guid.NewGuid(), type, typeId, subTypeId, displayName, fileName, @public, cubeSize,
                             buildTimeSeconds, pCU);
    }

}
