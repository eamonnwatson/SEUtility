using System.Diagnostics;

namespace SpaceEngineers.Data.Entities.Recipies;
[DebuggerDisplay("Assemblers = {Assemblers.Count}, Ingots = {Ingots.Count}, Refineries = {RefineriesThatCanCraftIngots.Count}")]
public class AssemblerRecipe(List<ProductionCubeBlock> assembers)
{
    public IReadOnlyCollection<ProductionCubeBlock> Assemblers { get; internal init; } = assembers;
    public IReadOnlyCollection<PhysicalItemListItem> Ingots { get; internal set; } = default!;
    public IReadOnlyCollection<ProductionCubeBlock> RefineriesThatCanCraftIngots { get; internal set; } = default!;
}
