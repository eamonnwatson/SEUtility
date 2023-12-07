namespace SpaceEngineers.Data.Entities;
public class Blueprint : BaseItem
{
    private readonly List<PhysicalItemListItem> _prerequisites = [];
    private readonly List<PhysicalItemListItem> _results = [];
    public decimal BaseProductionTimeInSeconds { get; private init; }
    public IReadOnlyList<PhysicalItemListItem> Prerequisites { get => _prerequisites; }
    public IReadOnlyList<PhysicalItemListItem> Results { get => _results; }
    public List<ProductionCubeBlock> ProductionCubeBlocks { get; private set; } = [];
    private Blueprint(Guid id, string? type, string typeId, string subTypeId, string displayName, string fileName,
                      decimal baseProductionTimeInSeconds)
        : base(id, type, typeId, subTypeId, displayName, fileName)
    {
        BaseProductionTimeInSeconds = baseProductionTimeInSeconds;
    }
    public void AddPrerequisite(decimal quantity, PhysicalItem item)
    {
        //var newListItem = new PhysicalItemListItem(Guid.NewGuid(), quantity, item);
        var newListItem = new PhysicalItemListItem() { Quantity = quantity, Component = item };
        _prerequisites.Add(newListItem);
    }
    public void AddResult(decimal quantity, PhysicalItem item)
    {
        //var newListItem = new PhysicalItemListItem(Guid.NewGuid(), quantity, item);
        var newListItem = new PhysicalItemListItem() { Quantity = quantity, Component = item };
        _results.Add(newListItem);
    }

    public static Blueprint Create(string? type, string typeId, string subTypeId, string displayName, string fileName,
                                   decimal baseProductionTimeInSeconds)
    {
        return new Blueprint(Guid.NewGuid(), type, typeId, subTypeId, displayName, fileName, baseProductionTimeInSeconds);
    }

}
