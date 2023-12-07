using SpaceEngineers.Data.Collections;
using SpaceEngineers.Data.Entities.Blueprints;
using SpaceEngineers.Data.Entities.Recipies;
using SpaceEngineers.Data.Shared;

namespace SpaceEngineers.Data.Entities;

public class ShipBlueprint
{
    public string GridName { get; private init; }
    public AssemblerEfficiency AssemblerEfficiency { get; private init; }
    public CubeBlockCollection AllSpaceEngineersCubeBlocks { get; private init; }
    public IReadOnlyCollection<ProductionCubeBlock> ProductionBlocks { get; private init; }
    public IReadOnlyCollection<BlueprintCubeBlock> ShipBlocks { get; private init; }
    public CubeBlockCollection FoundBlocks { get; private init; }
    public IReadOnlyCollection<BlueprintCubeBlock> NotFoundBlocks { get; private init; }
    public IReadOnlyCollection<PhysicalItemListItem> ComponentsNeeded { get; private init; }
    public AssemblerRecipe AssemblerRecipe { get; private init; }
    public List<RefineryRecipe> RefineryRecipes { get; private init; }

    public ShipBlueprint(string gridName, CubeBlockCollection allSpaceEngineersBlocks, IEnumerable<BlueprintCubeBlock> shipBlocks, BlueprintCubeBlockResult blueprintCubeBlockResult, AssemblerEfficiency assemblerEfficiency = AssemblerEfficiency.x3)
    {
        GridName = gridName;
        AssemblerEfficiency = assemblerEfficiency;
        AllSpaceEngineersCubeBlocks = allSpaceEngineersBlocks;
        ShipBlocks = shipBlocks.ToList();
        FoundBlocks = blueprintCubeBlockResult.Found;
        NotFoundBlocks = blueprintCubeBlockResult.NotFound.ToList();
        ComponentsNeeded = FoundBlocks.GetComponents().ToList();
        ProductionBlocks = AllSpaceEngineersCubeBlocks.GetProductionCubeBlocks().ToList();
        AssemblerRecipe = GetAssemberRequiredToCraftComponents(ComponentsNeeded);
        RefineryRecipes = GetRefineryRecipies(AssemblerRecipe);
    }

    private static List<RefineryRecipe> GetRefineryRecipies(AssemblerRecipe recipe)
    {

        var output = new List<RefineryRecipe>();
        foreach (var refinery in recipe.RefineriesThatCanCraftIngots)
        {
            var ingots = new List<PhysicalItemListItem>(recipe.Ingots);

            PhysicalItemListItem stoneList = GetStone(ingots, refinery);
            List<PhysicalItemListItem> oreList = GetOres(ingots, refinery);

            output.Add(new RefineryRecipe(refinery) { Ores = oreList, Stone = stoneList });
        }

        output[0].Distinct = true;

        if (output.Count > 1)
        {
            List<RefineryRecipe> distinctRefinery = [output[0]];

            foreach (var item in output.GetRange(1, output.Count - 1))
            {
                if (!CheckRecipe(item, distinctRefinery))
                {
                    item.Distinct = true;
                    distinctRefinery.Add(item);
                }
            }
        }

        return output;

    }

    private static bool CheckRecipe(RefineryRecipe refinery, List<RefineryRecipe> distinctRefinery)
    {
        foreach (var item in distinctRefinery)
        {
            if (item.Refinery.Blueprints.Count == refinery.Refinery.Blueprints.Count &&
                    item.Refinery.Blueprints.SequenceEqual(refinery.Refinery.Blueprints))
                return true;
        }

        return false;
    }

    private static PhysicalItemListItem GetStone(List<PhysicalItemListItem> ingots, ProductionCubeBlock refinery)
    {
        var oreList = new List<PhysicalItemListItem>();

        foreach (var ingotListItem in ingots)
        {
            var bp = refinery.GetBlueprintsToCraftIngots(ingotListItem.Component).FirstOrDefault(bp => bp.SubTypeId.Contains("StoneOreToIngot", StringComparison.InvariantCultureIgnoreCase));
            if (bp is null)
                continue;


            var qty = ingotListItem.Quantity / bp.Results[0].Quantity;
            var ores = bp.Prerequisites.Select(p => new PhysicalItemListItem() { Quantity = p.Quantity * qty, Component = p.Component });
            oreList.AddRange(ores);
        }

        return oreList.MaxBy(a => a.Quantity)!;
    }

    private static List<PhysicalItemListItem> GetOres(List<PhysicalItemListItem> ingots, ProductionCubeBlock refinery)
    {
        var oreList = new List<PhysicalItemListItem>();

        // checks to see if Gravel is included in Recipe and if so gathers the amount of stone needed for it.
        // Also removes ingot amounts of Iron/Nickel/Silicone based on stone needed since that will be refined into those ingots on top of gravel
        PhysicalItemListItem? gravel = GetGravel(ingots, refinery);

        foreach (var ingotListItem in ingots)
        {
            var bp = refinery.GetBlueprintsToCraftIngots(ingotListItem.Component).Where(bp => !bp.SubTypeId.Contains("StoneOreToIngot", StringComparison.InvariantCultureIgnoreCase));
            if (bp is null)
                continue;

            if (bp.Count() != 1)
                throw new Exception("CRAP");

            var qty = ingotListItem.Quantity / bp.First().Results[0].Quantity;
            var ores = bp.First().Prerequisites.Select(p => new PhysicalItemListItem() { Quantity = p.Quantity * qty, Component = p.Component });
            oreList.AddRange(ores);
        }

        if (gravel is not null)
            oreList.Add(gravel);

        return oreList.Where(a => a.Quantity > 0).OrderByDescending(a => a.Quantity).ToList();
    }

    private static PhysicalItemListItem? GetGravel(List<PhysicalItemListItem> ingots, ProductionCubeBlock refinery)
    {
        var gravel = ingots.FirstOrDefault(i => i.Component.DisplayName == "Gravel");

        if (gravel is not null)
        {
            ingots.Remove(gravel);
            var bp = refinery.GetBlueprintsToCraftIngots(gravel.Component);

            if (bp.Count() != 1)
                throw new Exception("CRAP");

            var stone = bp.First().Prerequisites.Select(p => new PhysicalItemListItem() { Component = p.Component, Quantity = p.Quantity * gravel.Quantity / bp.First().Results.First(r => r.Component == gravel.Component).Quantity }).First();
            var stoneToIngots = bp.First().Results.Select(p => new PhysicalItemListItem() { Component = p.Component, Quantity = stone.Quantity / bp.First().Prerequisites[0].Quantity * p.Quantity }).Where(a => a.Component.SubTypeId != "Stone");

            foreach (var item in stoneToIngots)
            {
                var ingot = ingots.First(a => a.Component == item.Component);
                ingots.Remove(ingot);
                ingots.Add(new PhysicalItemListItem() { Quantity = Math.Max(0, ingot.Quantity - item.Quantity), Component = ingot.Component });
            }
        }

        return gravel;
    }

    private List<ProductionCubeBlock> GetRefineriesNeededToCraftIngots(IReadOnlyCollection<PhysicalItemListItem> ingotsNeeded)
    {
        var refineryList = ProductionBlocks.ToList();

        foreach (var ingotListItem in ingotsNeeded)
        {
            var pb = ProductionBlocks.Where(c => c.CanCreateComponent(ingotListItem.Component));
            refineryList = refineryList.Intersect(pb).ToList();
        }

        return refineryList;
    }

    private AssemblerRecipe GetAssemberRequiredToCraftComponents(IReadOnlyCollection<PhysicalItemListItem> componentsNeeded)
    {
        var prodBlocks = ProductionBlocks.ToList();

        foreach (var component in componentsNeeded)
        {
            var pb = ProductionBlocks.Where(c => c.CanCreateComponent(component.Component));
            prodBlocks = prodBlocks.Intersect(pb).ToList();
        }

        return CreateAssemberRecipe(prodBlocks);
    }

    private AssemblerRecipe CreateAssemberRecipe(List<ProductionCubeBlock> pbs)
    {
        var recipe = new AssemblerRecipe(pbs) { Ingots = GetIngotsNeededToCraftComponents(ComponentsNeeded, pbs.First()) };
        recipe.RefineriesThatCanCraftIngots = GetRefineriesNeededToCraftIngots(recipe.Ingots);

        return recipe;
    }

    private List<PhysicalItemListItem> GetIngotsNeededToCraftComponents(IEnumerable<PhysicalItemListItem> componentList, ProductionCubeBlock productionCubeBlock)
    {
        var ingotList = new List<PhysicalItemListItem>();

        foreach (var componentItem in componentList)
        {
            var bp = productionCubeBlock.GetBlueprintToCraftComponent(componentItem.Component);
            if (bp is null)
                continue;

            //Assume one result for each blueprint
            var qty = componentItem.Quantity / bp.Results[0].Quantity;
            var ingots = bp.Prerequisites.Select(p => new PhysicalItemListItem() { Quantity = p.Quantity * qty / ((decimal)AssemblerEfficiency), Component = p.Component });
            ingotList.AddRange(ingots);
        }

        return ingotList
            .GroupBy(pi => pi.Component)
            .Select(g => new PhysicalItemListItem() { Component = g.Key, Quantity = g.Sum(p => p.Quantity) })
            .OrderByDescending(p => p.Quantity)
            .ToList();

    }
}
