using SEUtility.Common.Models;

namespace SEUtility.Common.Interfaces;

public interface IDataBuilder
{
    IDataBuilder AddBlockCategory(ItemType itemType, string typeID, string subtypeID, string displayName, string fileName, string name, List<string> itemIDs);
    IDataBuilder AddAmmoMagazine(ItemType itemType, string typeID, string subtypeID, string displayName, string fileName, decimal mass, decimal volume, bool canPlayerOrder, int capacity);
    IDataBuilder AddPhysicalItem(ItemType itemType, string typeID, string subtypeID, string displayName, string fileName, decimal mass, decimal volume, bool canPlayerOrder);
    IDataBuilder AddLocalization(string name, string value);
    IDataBuilder AddBlueprint(ItemType itemType, string typeID, string subtypeID, string displayName, string fileName, decimal baseProdTime, bool isPrimary, List<BlueprintItem> prerequisites, List<BlueprintItem> results);
    IDataBuilder AddBluePrintClass(ItemType itemType, string typeID, string subtypeID, string displayName, string fileName, string description);
    IDataBuilder AddBluePrintClassEntry(string entryClass, string entry);
    IDataBuilder AddCubeBlock(ItemType itemType, string typeID, string subtypeID, string displayName, string fileName, CubeSize cubeSize, int pcu, decimal? refineSpeed, decimal? materialEfficiency, decimal? assemblySpeed, bool isPublic, decimal buildTime,
        string description, List<ComponentItem> components, List<string> blueprintClasses, ComponentItem criticalComponent);

    IDataBuilder AddBlockCategories(IEnumerable<BlockCategory> items);
    IDataBuilder AddAmmoMagazines(IEnumerable<AmmoMagazine> items);
    IDataBuilder AddPhysicalItems(IEnumerable<PhysicalItem> items);
    IDataBuilder AddBlueprints(IEnumerable<Blueprint> items);
    IDataBuilder AddBlueprintClasses(IEnumerable<BlueprintClass> items);
    IDataBuilder AddCubeBlocks(IEnumerable<CubeBlock> items);

    SpaceEngineersData BuildData();
}
