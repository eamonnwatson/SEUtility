namespace SpaceEngineers.Data.Entities.Recipies;
public class RefineryRecipe(ProductionCubeBlock refinery)
{
    public ProductionCubeBlock Refinery { get; } = refinery;
    public IReadOnlyCollection<PhysicalItemListItem> Ores { get; internal set; } = default!;
    public PhysicalItemListItem Stone { get; internal set; } = default!;
    public bool Distinct { get; internal set; }
}
