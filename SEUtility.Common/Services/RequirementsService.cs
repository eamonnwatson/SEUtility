using Microsoft.Extensions.Options;
using SEUtility.Common.Exceptions;
using SEUtility.Common.Interfaces;
using SEUtility.Common.Models;
using SEUtility.Common.Options;

namespace SEUtility.Common.Services;

public class RequirementsService : IRequirementsService
{
    private readonly RequirementsOptions options;
    private const string LARGE_REFINERY_NAME = "LargeRefinery";
    private const string SMALL_REFINERY_NAME = "BlastFurnace";
    private const string SURVIVAL_KIT_NAME = "SurvivalKit";
    private readonly List<string> PrimaryOres = new() { "iron", "nickel", "silicon" };

    public SpaceEngineersData? Data { get; set; }

    public RequirementsService(IOptions<RequirementsOptions> options)
    {
        this.options = options.Value;
    }

    public Recipe GetRequirements(ShipBlueprint blueprint)
    {
        if (Data is null)
            throw new SEException("No Space Engineers Data has been loaded");

        var recipe = new Recipe();
        GetBlocks(recipe, blueprint);
        GetComponents(recipe);
        GetIngots(recipe);
        GetOres(recipe);

        return recipe;
    }
    private IEnumerable<Blueprint> GetBlueprintsForIngots()
    {
        if (options.RefineryType switch
        {
            RefineryType.BASIC_REFINERY => Data!.GetItem("Refinery", SMALL_REFINERY_NAME),
            RefineryType.SURVIVAL_KIT => Data!.GetItem("SurvivalKit", SURVIVAL_KIT_NAME),
            _ => Data!.GetItem("Refinery", LARGE_REFINERY_NAME),
        } is not CubeBlock cb)
            throw new SEException("Unable to find refinery");

        var bpc = Data.BlueprintClasses
            .Where(a => cb.BlueprintClasses.Contains(a.SubTypeId))
            .SelectMany(a => a.SubTypeIDList)
            .Where(a => !a.Contains("scrap", StringComparison.InvariantCultureIgnoreCase))
            .Where(a => a.Contains("ingot", StringComparison.InvariantCultureIgnoreCase))
            .Distinct();

        if (options.UseStone)
            bpc = bpc.Where(a => !PrimaryOres.Any(p => a.Contains(p, StringComparison.InvariantCultureIgnoreCase)));

        var bp = Data.Blueprints.Where(a => bpc.Contains(a.SubTypeId));

        return bp;
    }


    private void GetOres(Recipe recipe)
    {
        var bpc = GetBlueprintsForIngots();
        var ores = new List<(decimal qty, BaseItem item)>();

        foreach (var ingot in recipe.Ingots)
        {
            var bp = bpc
                .Where(a => CheckRemoveStoneBlueprint(a, ingot))
                .First(a => a.Results.Any(b => b.TypeID.Equals(ingot.Item.TypeId) && b.SubTypeID.Equals(ingot.Item.SubTypeId)));

            var result = bp.Results.First(a => a.TypeID.Equals(ingot.Item.TypeId) && a.SubTypeID.Equals(ingot.Item.SubTypeId));

            foreach (var pre in bp.Prerequisites)
            {
                if (Data!.GetItem(pre.TypeID, pre.SubTypeID) is not PhysicalItem ore)
                    continue;

                ores.Add((ingot.Quantity / result.Amount * pre.Amount, ore));
            }
        }

        foreach (var item in ores.GroupBy(a => a.item).Select(b => (qty: b.Max(c => c.qty), item: b.Key)))
        {
            recipe.AddItems(item.qty, item.item);
        }

    }

    private bool CheckRemoveStoneBlueprint(Blueprint bp, RecipeItem recipeItem)
    {
        if (options.UseStone)
            return true;

        if (recipeItem.Item.SubTypeId.Contains("stone", StringComparison.InvariantCultureIgnoreCase))
            return true;

        if (!bp.SubTypeId.Contains("stone", StringComparison.InvariantCultureIgnoreCase))
            return true;

        return false;
    }

    private void GetIngots(Recipe recipe)
    {
        foreach (var item in recipe.AllItems.Where(a => a.Item is PhysicalItem && a.ItemType != ItemType.INGOT && a.ItemType != ItemType.ORE))
        {
            if (item.Item is not PhysicalItem component)
                continue;

            var bps = Data!.GetBlueprints(component.TypeId, component.SubTypeId);
            if (!bps.Any())
                continue;

            var bp = bps.First();
            var result = bp.Results.First(a => a.TypeID.Equals(component.TypeId) && a.SubTypeID.Equals(component.SubTypeId));

            foreach (var pre in bp.Prerequisites)
            {
                if (Data!.GetItem(pre.TypeID, pre.SubTypeID) is not PhysicalItem ingot)
                    continue;

                recipe.AddItems(item.Quantity / result.Amount * pre.Amount / options.AssemblerEfficiency, ingot);
            }
        }
    }

    private void GetComponents(Recipe recipe)
    {
        foreach (var item in recipe.Blocks)
        {
            if (item.Item is not CubeBlock block)
                continue;

            foreach (var component in block.Components)
            {
                var comp = Data!.GetItem("Component", component.Subtype);
                if (comp is null)
                    continue;

                recipe.AddItems(component.Count * item.Quantity, comp);
            }
        }
    }

    private void GetBlocks(Recipe recipe, ShipBlueprint blueprint)
    {
        foreach (var item in blueprint.Items)
        {
            var block = Data!.GetItem(item.TypeID, item.SubTypeID);
            if (block is null)
            {
                recipe.AddError($"Missing Block : {item.Amount:g0} - {item.TypeID}/{item.SubTypeID}");
                continue;
            }

            recipe.AddItems(item.Amount, block);
        }
    }
}
