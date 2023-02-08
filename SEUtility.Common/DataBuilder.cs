using SEUtility.Common.Interfaces;
using SEUtility.Common.Models;

namespace SEUtility.Common;

public class DataBuilder : IDataBuilder
{
    private readonly SpaceEngineersData data = new();
    private readonly List<PhysicalItem> physicalItems = new();
    private readonly List<AmmoMagazine> ammoMagazines = new();
    private readonly List<Blueprint> blueprints = new();
    private readonly List<BlueprintClass> blueprintClasses = new();
    private readonly List<CubeBlock> cubeBlocks = new();
    private readonly List<BlockCategory> blockCategories = new();
    private readonly Dictionary<string, string> displayNames = new();

    private DataBuilder() { }
    public static IDataBuilder Create() { return new DataBuilder(); }
    public static IShipBlueprintBuilder CreateShipBuilder() { return new ShipBlueprintBuilder(); }
    public IDataBuilder AddBlockCategory(ItemType itemType, string typeID, string subtypeID, string displayName, string fileName, string name, List<string> itemIDs)
    {
        blockCategories.Add(new BlockCategory()
        {
            Type = itemType,
            DisplayName = displayName,
            FileName = fileName,
            SubTypeId = subtypeID,
            Name = name,
            TypeId = typeID,
            ItemIDs = itemIDs
        });

        return this;
    }

    public IDataBuilder AddAmmoMagazine(ItemType itemType, string typeID, string subtypeID, string displayName, string fileName, decimal mass, decimal volume, bool canPlayerOrder, int capacity)
    {
        ammoMagazines.Add(new AmmoMagazine()
        {
            Type = itemType,
            TypeId = typeID,
            SubTypeId = subtypeID,
            DisplayName = displayName,
            FileName = fileName,
            Capacity = capacity,
            CanPlayerOrder = canPlayerOrder,
            Mass = mass,
            Volume = volume
        });

        return this;
    }

    public IDataBuilder AddBlueprint(ItemType itemType, string typeID, string subtypeID, string displayName, string fileName, decimal baseProdTime, bool isPrimary, List<BlueprintItem> prerequisites, List<BlueprintItem> results)
    {
        blueprints.Add(new Blueprint()
        {
            Type = itemType,
            TypeId = typeID,
            SubTypeId = subtypeID,
            DisplayName = displayName,
            FileName = fileName,
            BaseProductionTimeInSeconds = baseProdTime,
            IsPrimary = isPrimary,
            Results = results,
            Prerequisites = prerequisites
        });

        return this;
    }

    public IDataBuilder AddBluePrintClass(ItemType itemType, string typeID, string subtypeID, string displayName, string fileName, string description)
    {
        blueprintClasses.Add(new BlueprintClass()
        {
            Type = itemType,
            TypeId = typeID,
            SubTypeId = subtypeID,
            DisplayName = displayName,
            FileName = fileName,
            Description = description
        });

        return this;
    }

    public IDataBuilder AddBluePrintClassEntry(string entryClass, string entry)
    {
        var bpc = blueprintClasses.FirstOrDefault(b => b.SubTypeId.Equals(entryClass, StringComparison.InvariantCultureIgnoreCase));

        if (bpc is not null)
            bpc.AddEntry(entry);

        return this;
    }

    public IDataBuilder AddCubeBlock(ItemType itemType, string typeID, string subtypeID, string displayName, string fileName, CubeSize cubeSize, int pcu, decimal? refineSpeed, decimal? materialEfficiency, decimal? assemblySpeed, bool isPublic, decimal buildTime, string description, List<ComponentItem> components, List<string> blueprintClasses, ComponentItem criticalComponent)
    {
        cubeBlocks.Add(new CubeBlock()
        {
            Type = itemType,
            TypeId = typeID,
            SubTypeId = subtypeID,
            DisplayName = displayName,
            FileName = fileName,
            Components = components,
            BlueprintClasses = blueprintClasses,
            AssemblySpeed = assemblySpeed,
            BuildTimeSeconds = buildTime,
            CubeSize = cubeSize,
            RefineSpeed = refineSpeed,
            CriticalComponent = criticalComponent,
            PCU = pcu,
            MaterialEfficiency = materialEfficiency,
            Public = isPublic,
            Description = description
        });

        return this;
    }

    public IDataBuilder AddLocalization(string name, string value)
    {
        displayNames.TryAdd(name, value);

        return this;
    }

    public IDataBuilder AddPhysicalItem(ItemType itemType, string typeID, string subtypeID, string displayName, string fileName, decimal mass, decimal volume, bool canPlayerOrder)
    {
        physicalItems.Add(new PhysicalItem()
        {
            Type = itemType,
            TypeId = typeID,
            SubTypeId = subtypeID,
            DisplayName = displayName,
            FileName = fileName,
            Mass = mass,
            Volume = volume,
            CanPlayerOrder = canPlayerOrder
        });

        return this;
    }

    public SpaceEngineersData BuildData()
    {
        var allItems = ammoMagazines.Concat<BaseItem>(blueprints).Concat(blueprintClasses).Concat(cubeBlocks).Concat(physicalItems).Concat(blockCategories);

        data.AmmoMagazines = ammoMagazines;
        data.CubeBlocks = cubeBlocks;
        data.Blueprints = blueprints;
        data.BlueprintClasses = blueprintClasses;
        data.PhysicalItems = physicalItems;
        data.BlockCategories = blockCategories;

        if (displayNames.Count > 0)
        {
            foreach (var item in allItems)
            {
                if (item is CubeBlock cb)
                {
                    if (displayNames.TryGetValue(cb.Description, out string? descValue))
                        cb.Description = descValue;
                }

                if (displayNames.TryGetValue(item.DisplayName, out string? value))
                    item.DisplayName = value;
            }
        }

        data.AllItems = allItems.GroupBy(a => a.Type).ToDictionary(a => a.Key, b => b.Select(c => c));

        return data;
    }

    public IDataBuilder AddBlockCategories(IEnumerable<BlockCategory> items)
    {
        blockCategories.AddRange(items);
        return this;
    }

    public IDataBuilder AddAmmoMagazines(IEnumerable<AmmoMagazine> items)
    {
        ammoMagazines.AddRange(items);
        return this;
    }

    public IDataBuilder AddPhysicalItems(IEnumerable<PhysicalItem> items)
    {
        physicalItems.AddRange(items);
        return this;
    }

    public IDataBuilder AddBlueprints(IEnumerable<Blueprint> items)
    {
        blueprints.AddRange(items);
        return this;
    }

    public IDataBuilder AddBlueprintClasses(IEnumerable<BlueprintClass> items)
    {
        blueprintClasses.AddRange(items);
        return this;
    }

    public IDataBuilder AddCubeBlocks(IEnumerable<CubeBlock> items)
    {
        cubeBlocks.AddRange(items);
        return this;
    }

}
