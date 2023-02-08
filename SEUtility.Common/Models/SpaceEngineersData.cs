using SEUtility.Common.Exceptions;

namespace SEUtility.Common.Models;

public class SpaceEngineersData
{
    public IDictionary<ItemType, IEnumerable<BaseItem>> AllItems { get; internal set; } = default!;
    public IReadOnlyList<AmmoMagazine> AmmoMagazines { get; internal set; } = default!;
    public IReadOnlyList<Blueprint> Blueprints { get; internal set; } = default!;
    public IReadOnlyList<BlueprintClass> BlueprintClasses { get; internal set; } = default!;
    public IReadOnlyList<PhysicalItem> PhysicalItems { get; internal set; } = default!;
    public IReadOnlyList<CubeBlock> CubeBlocks { get; internal set; } = default!;
    public IReadOnlyList<BlockCategory> BlockCategories { get; internal set; } = default!;
    internal SpaceEngineersData() { }

    public BaseItem? GetItem(string typeID, string subTypeID)
    {
        var item = AllItems.SelectMany(a => a.Value.Where(a => a.TypeId.Equals(typeID, StringComparison.InvariantCultureIgnoreCase) &&
                                                               a.SubTypeId.Equals(subTypeID, StringComparison.InvariantCultureIgnoreCase)));

        if (item.Count() > 1)
            throw new SEException("More than 1 item found.");

        return item.FirstOrDefault();
    }

    public IEnumerable<Blueprint> GetBlueprints(string resultTypeID, string resultSubTypeID)
    {
        return Blueprints.Where(a => a.Results.Any(r => r.TypeID.Equals(resultTypeID, StringComparison.InvariantCultureIgnoreCase) &&
                                                        r.SubTypeID.Equals(resultSubTypeID, StringComparison.InvariantCultureIgnoreCase)));
    }
}
