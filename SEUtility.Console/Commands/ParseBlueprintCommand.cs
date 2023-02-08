using Cocona;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SEUtility.Common.Interfaces;
using SEUtility.Common.Models;
using SEUtility.Common.Options;
using SEUtility.Console.Interfaces;
using SEUtility.Parser;
using Spectre.Console;

namespace SEUtility.Console.Commands;

internal class ParseBlueprintCommand
{
    private readonly ILogger<ParseBlueprintCommand> logger;
    private readonly IDataService dataService;
    private readonly IConsoleWriter console;
    private readonly IRequirementsService requirements;
    private readonly ISBCParser parser;
    private readonly RequirementsOptions options;

    public ParseBlueprintCommand(ILogger<ParseBlueprintCommand> logger,
                                 IDataService dataService,
                                 IConsoleWriter console,
                                 IRequirementsService requirements,
                                 ISBCParser parser,
                                 IOptions<RequirementsOptions> options)
    {
        this.logger = logger;
        this.dataService = dataService;
        this.console = console;
        this.requirements = requirements;
        this.parser = parser;
        this.options = options.Value;
    }

    [Command(Description = "Parse Blueprint data")]
    public void ParseBlueprint(CommonParameters parameters,
                              [Argument(Description = "Blueprint file to parse")] string blueprint,
                              ParseParameters parseParam)
    {
        console.Verbose = parameters.Verbose;

        if (parseParam.DatabaseLocation is not null)
            dataService.Database.SetLocation(parseParam.DatabaseLocation);

        options.AssemblerEfficiency = parseParam.AssemblerEfficiency;
        options.UseStone = parseParam.UseStone;

        if (parseParam.UseBasicAssembler)
            options.RefineryType = RefineryType.BASIC_REFINERY;

        if (parseParam.UseSurvivalKit)
            options.RefineryType = RefineryType.SURVIVAL_KIT;

        if (!parseParam.ShowBlocks &&
            !parseParam.ShowComponents &&
            !parseParam.ShowIngots &&
            !parseParam.ShowOres &&
            !parseParam.ShowAll)
        {
            return;
        }

        AnsiConsole.Status()
            .Spinner(Spinner.Known.BouncingBar)
            .Start("Parsing Blueprint...", ctx =>
            {
                try
                {
                    var bp = parser.GetBlueprint(blueprint);
                    console.Success("Blueprint Parsed..");
                    ctx.Status("Gathering Data");
                    var data = dataService.GetData();
                    console.Success("Data Gathered..");
                    ctx.Status("Gathering Requirements");
                    requirements.Data = data;
                    var recipe = requirements.GetRequirements(bp);
                    console.Success("Retrieved Requirements..");

                    ctx.Status("Outputting Results");
                    if (parseParam.ShowBlocks || parseParam.ShowAll)
                        OutputBlocks(recipe);

                    if (parseParam.ShowComponents || parseParam.ShowAll)
                        OutputComponents(recipe);

                    if (parseParam.ShowIngots || parseParam.ShowAll)
                        OutputIngots(recipe);

                    if (parseParam.ShowOres || parseParam.ShowAll)
                        OutputOres(recipe);

                    var panel = new Panel(string.Join(Environment.NewLine, recipe.Errors)).HeavyBorder().Header("Errors");
                    AnsiConsole.Write(panel);
                }
                catch (Exception ex)
                {
                    console.Exception(ex);
                    logger.LogError(ex, "Error");
                }
            });
    }

    private static Table MakeTable(string title, IEnumerable<string[]> rows)
    {
        var table = new Table() { Title = new TableTitle($"[[ [bold rgb(234,279,8)]{title}[/] ]]"), Border = TableBorder.Heavy };
        table.AddColumns(new TableColumn("[blue]Qty[/]0").RightAligned(), new TableColumn($"[blue]{title}[/]"));
        foreach (var row in rows)
        {
            table.AddRow(row);
        }
        return table;
    }

    private void OutputBlocks(Recipe recipe)
    {
        var blocks = recipe.Blocks.OrderByDescending(a => a.Quantity)
            .ThenBy(a => a.Item.DisplayName)
            .Select(a => new string[] { a.Quantity.ToString("n0"), a.Item.DisplayName });

        var table = MakeTable("Blocks", blocks);
        console.WriteTable(table);
    }
    private void OutputOres(Recipe recipe)
    {
        var ores = recipe.Ores.OrderByDescending(a => a.Quantity)
            .ThenBy(a => a.Item.DisplayName)
            .Select(a => new string[] { a.Quantity.ToString("n2"), a.Item.DisplayName });

        var table = MakeTable("Ores", ores);
        console.WriteTable(table);
    }

    private void OutputIngots(Recipe recipe)
    {
        var ingots = recipe.Ingots.OrderByDescending(a => a.Quantity)
            .ThenBy(a => a.Item.DisplayName)
            .Select(a => new string[] { a.Quantity.ToString("n2"), a.Item.DisplayName });

        var table = MakeTable("Ingots", ingots);
        console.WriteTable(table);

    }

    private void OutputComponents(Recipe recipe)
    {
        var components = recipe.Components.OrderByDescending(a => a.Quantity)
            .ThenBy(a => a.Item.DisplayName)
            .Select(a => new string[] { a.Quantity.ToString("n0"), a.Item.DisplayName });

        var table = MakeTable("Components", components);
        console.WriteTable(table);

    }
}

