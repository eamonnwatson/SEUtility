using SpaceEngineers.Application.Interfaces;
using SpaceEngineers.Data.Entities;
using SpaceEngineers.Data.Entities.Blueprints;
using SpaceEngineers.Data.Entities.Recipies;
using System.Text;

namespace SpaceEngineersRequirements.Infrastructure;
internal class ReportWriter() : IReportWriter
{
    private readonly StringBuilder _sb = new();
    private readonly string _underline = new('-', 20);
    public string CreateReport(ShipBlueprint shipBlueprint)
    {
        _sb.Clear();

        WriteTitle(shipBlueprint.GridName);
        WriteNotFoundBlocks(shipBlueprint.NotFoundBlocks);

        _sb.AppendLine($"{shipBlueprint.FoundBlocks.Count} Blocks Found");
        _sb.AppendLine();

        WriteComponents(shipBlueprint.ComponentsNeeded);
        WriteAssemblers(shipBlueprint.AssemblerRecipe);
        WriteIngots(shipBlueprint.AssemblerRecipe.Ingots);
        WriteRefineries(shipBlueprint.RefineryRecipes);

        return _sb.ToString();
    }
    private void WriteTitle(string title)
    {
        _sb.AppendLine(title);
        _sb.AppendLine(_underline);
        _sb.AppendLine();

    }
    private void WriteNotFoundBlocks(IReadOnlyCollection<BlueprintCubeBlock> notFoundBlocks)
    {
        if (notFoundBlocks.Count != 0)
        {
            _sb.AppendLine("Not Found Blocks");
            _sb.AppendLine(new string('-', 20));
            foreach (var item in notFoundBlocks)
            {
                _sb.AppendLine($"{IfEmpty(item.TypeId)}/{IfEmpty(item.SubTypeId)} ({item.CubeSize})");
            }
            _sb.AppendLine();
        }
    }
    private void WriteComponents(IReadOnlyCollection<PhysicalItemListItem> componentsNeeded)
    {
        _sb.AppendLine("Components Used");
        _sb.AppendLine(new string('-', 20));
        foreach (var item in componentsNeeded)
        {
            _sb.AppendLine($"{item.Quantity,-10:n0} {item.Component.DisplayName}");
        }
        _sb.AppendLine();
    }
    private void WriteAssemblers(AssemblerRecipe assemblerRecipe)
    {
        _sb.AppendLine("Assemblers Capable of Producing Components");
        _sb.AppendLine(new string('-', 20));
        foreach (var item in assemblerRecipe.Assemblers)
        {
            _sb.AppendLine($"{item.DisplayName}");
        }
        _sb.AppendLine();
    }
    private void WriteIngots(IReadOnlyCollection<PhysicalItemListItem> ingots)
    {
        _sb.AppendLine("Ingots Needed");
        _sb.AppendLine(new string('-', 20));
        foreach (var item in ingots)
        {
            _sb.AppendLine($"{item.Quantity,-10:n0} {item.Component.DisplayName}");
        }
        _sb.AppendLine();
    }
    private void WriteRefineries(List<RefineryRecipe> refineryRecipes)
    {
        _sb.AppendLine("Refineries Capable of Producing Ingots");
        _sb.AppendLine(new string('-', 20));
        foreach (var item in refineryRecipes)
        {
            _sb.AppendLine($"{item.Refinery.DisplayName}");
        }

        foreach (var refinery in refineryRecipes.Where(a => a.Distinct))
        {
            _sb.AppendLine();
            _sb.AppendLine($"Ores Needed {refinery.Refinery.DisplayName}");
            _sb.AppendLine("** AMOUNTS CAN VARY IF YIELD MODULES USED ON REFINERIES");
            _sb.AppendLine(new string('-', 20));
            foreach (var item in refinery.Ores)
            {
                _sb.AppendLine($"{item.Quantity,-10:n0} {item.Component.DisplayName}");
            }
            _sb.AppendLine();
            _sb.AppendLine("Stone Needed for All Silicon/Nickel/Iron Requirements");
            _sb.AppendLine("** ONLY IF YOU DO NOT WISH TO MINE SILICON/NICKEL/IRON");
            _sb.AppendLine(new string('-', 20));
            _sb.AppendLine($"{refinery.Stone.Quantity,-10:n0} {refinery.Stone.Component.DisplayName}");

        }
    }
    private static string IfEmpty(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return "\"\"";

        return value;
    }

}
