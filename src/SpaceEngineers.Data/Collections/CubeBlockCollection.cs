using SpaceEngineers.Data.Entities;
using SpaceEngineers.Data.Entities.Blueprints;
using SpaceEngineers.Data.Shared;
using System.Collections;
using System.Diagnostics;

namespace SpaceEngineers.Data.Collections;
[DebuggerDisplay("Count = {Count}")]
public class CubeBlockCollection(IEnumerable<CubeBlock> blocks) : IReadOnlyCollection<CubeBlock>
{
    private readonly List<CubeBlock> _blocks = blocks.ToList();
    public int Count { get => _blocks.Count; }

    public BlueprintCubeBlockResult FindBlueprintCubeBlocks(IEnumerable<BlueprintCubeBlock> blocks)
    {
        List<BlueprintCubeBlock> notFound = [];
        var found = blocks.Select(block => GetCubeBlock(block, notFound)).Where(block => block is not null).Cast<CubeBlock>().ToList();

        return new BlueprintCubeBlockResult(new CubeBlockCollection(found), notFound);
    }

    public IEnumerable<ProductionCubeBlock> GetProductionCubeBlocks()
    {
        return _blocks.Where(b => b is ProductionCubeBlock).Cast<ProductionCubeBlock>().ToList();
    }

    public IEnumerable<PhysicalItemListItem> GetComponents()
    {
        var comp = _blocks.SelectMany(b => b.Components)
            .GroupBy(pi => pi.Component)
            .Select(g => new PhysicalItemListItem() { Component = g.Key, Quantity = g.Sum(p => p.Quantity) })
            .OrderByDescending(p => p.Quantity)
            .ToList();

        return comp;
    }

    public IEnumerator<CubeBlock> GetEnumerator()
    {
        return _blocks.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _blocks.GetEnumerator();
    }
    private CubeBlock? GetCubeBlock(BlueprintCubeBlock block, List<BlueprintCubeBlock> notFoundList)
    {
        var t = _blocks.FirstOrDefault(cb => CompareBlueprintCubeBlockToCubeBlock(block, cb));
        if (t is null)
            notFoundList.Add(block);
        return t;
    }

    private static bool CompareBlueprintCubeBlockToCubeBlock(BlueprintCubeBlock bpcb, CubeBlock cb)
    {
        return Enum.Parse<CubeSize>(bpcb.CubeSize, true) == cb.CubeSize &&
            (bpcb.TypeId == cb.TypeId || bpcb.TypeId.Replace("MyObjectBuilder_", "") == cb.TypeId) &&
            bpcb.SubTypeId == cb.SubTypeId;
    }
}

