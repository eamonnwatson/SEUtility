using SpaceEngineers.Data.Shared;

namespace SpaceEngineers.Data.Entities;
public class ProductionCubeBlock : CubeBlock
{
    private readonly List<Blueprint> _blueprints = [];
    public decimal RefineSpeed { get; private init; }
    public decimal MaterialEfficiency { get; private init; }
    public IReadOnlyList<Blueprint> Blueprints { get => _blueprints; }

    private ProductionCubeBlock(Guid id, string? type, string typeId, string subTypeId, string displayName, string fileName,
                      bool @public, CubeSize cubeSize, decimal buildTimeSeconds, int pCU, decimal refineSpeed, decimal materialEfficiency)
        : base(id, type, typeId, subTypeId, displayName, fileName, @public, cubeSize, buildTimeSeconds, pCU)
    {
        RefineSpeed = refineSpeed;
        MaterialEfficiency = materialEfficiency;
    }

    public void AddBlueprint(Blueprint item)
    {
        _blueprints.Add(item);
    }

    public static ProductionCubeBlock Create(string? type, string typeId, string subTypeId, string displayName, string fileName,
                               bool @public, CubeSize cubeSize, decimal buildTimeSeconds, int pCU, decimal refineSpeed, decimal materialEfficiency)
    {
        return new ProductionCubeBlock(Guid.NewGuid(), type, typeId, subTypeId, displayName, fileName, @public, cubeSize,
                             buildTimeSeconds, pCU, refineSpeed, materialEfficiency);
    }

    public bool CanCreateComponent(PhysicalItem component)
    {
        return _blueprints.Any(bp => bp.Results.Any(r => r.Component == component));
    }

    // Assume there should only ever be one blueprint to craft a single component...
    public Blueprint? GetBlueprintToCraftComponent(PhysicalItem component)
    {
        return _blueprints.FirstOrDefault(bp => bp.Results.Any(r => r.Component == component));
    }

    public IEnumerable<Blueprint> GetBlueprintsToCraftIngots(PhysicalItem ingot)
    {
        return _blueprints.Where(bp => bp.Results.Any(r => r.Component == ingot));
    }
}
