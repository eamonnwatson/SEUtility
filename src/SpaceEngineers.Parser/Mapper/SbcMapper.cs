using SpaceEngineers.Data.Entities.Blueprints;
using SpaceEngineers.Data.Shared;
using SpaceEngineers.Parser.Interfaces;
using SpaceEngineers.Parser.Models;
using Entities = SpaceEngineers.Data.Entities;

namespace SpaceEngineers.Parser.Mapper;
internal class SbcMapper : ISbcMapper
{
    public IEnumerable<Entities.PhysicalItem> MapAmmoMagazines(IEnumerable<AmmoMagazine> ammoMagazines)
    {
        return ammoMagazines
            .Select(a =>
                Data.Entities.PhysicalItem.Create(
                    null,
                    a.TypeId,
                    a.SubtypeId,
                    a.DisplayName,
                    a.FileName,
                    a.Mass,
                    a.Volume,
                    a.CanPlayerOrder))
                .ToList();
    }

    public IEnumerable<BlueprintCubeBlock> MapBlueprintCubeBlocks(IEnumerable<CubeGrid> cubeGrids)
    {
        return cubeGrids.SelectMany(i => i.Prerequisites.Select(p => new BlueprintCubeBlock(i.GridSizeEnum, p.Type, p.SubtypeName))).ToList();
    }

    public IEnumerable<Entities.Blueprint> MapBlueprints(IEnumerable<Blueprint> blueprints, IEnumerable<Entities.PhysicalItem> physicalItems)
    {
        return blueprints
            .Select(b =>
            {
                var bp = Data.Entities.Blueprint.Create(
                    null,
                    b.TypeId,
                    b.SubtypeId,
                    b.DisplayName,
                    b.FileName,
                    b.BaseProductionTimeInSeconds);

                foreach (var item in b.Prerequisites)
                {
                    var pre = GetItem(item, physicalItems);
                    if (pre is not null)
                        bp.AddPrerequisite(item.Amount, pre);
                }

                foreach (var item in b.Results)
                {
                    var res = GetItem(item, physicalItems);
                    if (res is not null)
                        bp.AddResult(item.Amount, res);
                }

                if (b.Result is not null)
                {
                    var res = GetItem(b.Result, physicalItems);
                    if (res is not null)
                        bp.AddResult(b.Result.Amount, res);
                }

                return bp;
            })
            .ToList();

        Entities.PhysicalItem? GetItem(Models.BlueprintItem item, IEnumerable<Entities.PhysicalItem> physicalItems)
            => physicalItems.FirstOrDefault(p => p.TypeId == item.TypeId && p.SubTypeId == item.SubtypeId);

    }

    public IEnumerable<Entities.PhysicalItem> MapComponents(IEnumerable<Component> components)
    {
        return components
            .Select(c =>
            Data.Entities.PhysicalItem.Create(
                null,
                c.TypeId,
                c.SubtypeId,
                c.DisplayName,
                c.FileName,
                c.Mass,
                c.Volume,
                c.CanPlayerOrder))
            .ToList();

    }

    public IEnumerable<Entities.CubeBlock> MapCubeBlocks(SBCData data, IEnumerable<Entities.PhysicalItem> physicalItems, IEnumerable<Entities.Blueprint> blueprints)
    {
        return data.CubeBlocks
            .Select(cb =>
            {
                Entities.CubeBlock? cubeBlock;

                if (cb.BlueprintClasses.Count > 0)
                {
                    var prodCube = Entities.ProductionCubeBlock.Create(
                        null,
                        cb.TypeId,
                        cb.SubtypeId,
                        cb.DisplayName,
                        cb.FileName,
                        cb.Public,
                        cb.CubeSize.Equals("large", StringComparison.InvariantCultureIgnoreCase) ? CubeSize.LARGE : CubeSize.SMALL,
                        cb.BuildTimeSeconds,
                        cb.PCU,
                        cb.RefineSpeed,
                        cb.MaterialEfficiency);

                    var blueprintClasses = cb.BlueprintClasses.SelectMany(bpc => data.BlueprintClassEntries.Where(b => b.Class.Equals(bpc.Class, StringComparison.InvariantCultureIgnoreCase)));
                    var bps = blueprints.Where(bp => blueprintClasses.Any(bpc => bpc.BlueprintSubtypeId == bp.SubTypeId));

                    foreach (var bp in bps)
                    {
                        prodCube.AddBlueprint(bp);
                    }

                    cubeBlock = prodCube;
                }
                else
                {
                    cubeBlock = Entities.CubeBlock.Create(
                        null,
                        cb.TypeId,
                        cb.SubtypeId,
                        cb.DisplayName,
                        cb.FileName,
                        cb.Public,
                        cb.CubeSize.Equals("large", StringComparison.InvariantCultureIgnoreCase) ? CubeSize.LARGE : CubeSize.SMALL,
                        cb.BuildTimeSeconds,
                        cb.PCU);
                }

                foreach (var item in cb.Components)
                {
                    bool isCritial = false;
                    if (cb.CriticalComponent is not null && cb.CriticalComponent.Subtype == item.Subtype)
                    {
                        if (cb.CriticalComponent.Count == 0)
                            isCritial = true;
                        else
                            cb.CriticalComponent.Count--;
                    }

                    var component = physicalItems.FirstOrDefault(pi => pi.TypeId == "Component" && pi.SubTypeId == item.Subtype);
                    if (component is not null)
                        cubeBlock.AddComponent(item.Count, component, isCritial);
                }

                return cubeBlock;
            })
            .ToList();
    }

    public IEnumerable<Entities.PhysicalItem> MapPhysicalItems(IEnumerable<PhysicalItem> physicalItems)
    {
        return physicalItems
            .Select(pi =>
                Data.Entities.PhysicalItem.Create(
                    null,
                    pi.TypeId,
                    pi.SubtypeId,
                    pi.DisplayName,
                    pi.FileName,
                    pi.Mass,
                    pi.Volume,
                    pi.CanPlayerOrder))
                .ToList();
    }
}
