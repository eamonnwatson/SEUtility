using SpaceEngineers.Data.Entities.Blueprints;
using SpaceEngineers.Parser.Models;
using Entities = SpaceEngineers.Data.Entities;

namespace SpaceEngineers.Parser.Interfaces;
public interface ISbcMapper
{
    IEnumerable<Entities.PhysicalItem> MapAmmoMagazines(IEnumerable<AmmoMagazine> ammoMagazines);
    IEnumerable<Entities.Blueprint> MapBlueprints(IEnumerable<Blueprint> blueprints, IEnumerable<Entities.PhysicalItem> physicalItems);
    IEnumerable<Entities.PhysicalItem> MapComponents(IEnumerable<Component> components);
    IEnumerable<Entities.CubeBlock> MapCubeBlocks(SBCData data, IEnumerable<Entities.PhysicalItem> physicalItems, IEnumerable<Entities.Blueprint> blueprints);
    IEnumerable<Entities.PhysicalItem> MapPhysicalItems(IEnumerable<PhysicalItem> physicalItems);
    IEnumerable<BlueprintCubeBlock> MapBlueprintCubeBlocks(IEnumerable<CubeGrid> cubeGrids);
}
