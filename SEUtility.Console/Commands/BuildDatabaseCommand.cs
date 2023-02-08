using Cocona;
using Microsoft.Extensions.Logging;
using SEUtility.Common.Exceptions;
using SEUtility.Common.Interfaces;
using SEUtility.Console.Attributes;
using SEUtility.Console.Interfaces;
using SEUtility.Parser;
using Spectre.Console;

namespace SEUtility.Console.Commands;

internal class BuildDatabaseCommand
{
    private readonly ILogger<BuildDatabaseCommand> logger;
    private readonly ISELocator locator;
    private readonly ISBCParser parser;
    private readonly IDataService dataService;
    private readonly IConsoleWriter console;

    public BuildDatabaseCommand(ILogger<BuildDatabaseCommand> logger,
                                ISELocator locator,
                                ISBCParser parser,
                                IDataService dataService,
                                IConsoleWriter console)
    {
        this.logger = logger;
        this.locator = locator;
        this.parser = parser;
        this.dataService = dataService;
        this.console = console;
    }

    [Command(Description = "Created Database file based on Space Engineers data")]
    public void BuildDatabase(CommonParameters parameters,
                             [Option('l', Description = "Location of Space Engineers Folder")][PathExists] string? spaceEngineersLocation,
                             [Option('o', Description = "Output Folder for database file")][PathExists] string? outputFolder)
    {
        console.Verbose = parameters.Verbose;

        outputFolder ??= Environment.CurrentDirectory;

        if (!Directory.Exists(outputFolder))
            throw new SEException("Output Foler does not exist");

        dataService.Database.SetLocation(outputFolder);

        AnsiConsole.Status()
            .Spinner(Spinner.Known.BouncingBar)
            .Start("Building Database...", ctx =>
            {
                try
                {
                    ctx.Status("Finding Space Engineers");
                    spaceEngineersLocation ??= locator.GetLocation();

                    if (spaceEngineersLocation is null)
                        throw new SEException("Space Engineers was not found.");

                    spaceEngineersLocation = Path.Combine(spaceEngineersLocation, "Content\\Data");
                    //spaceEnginnersLocation = "C:\\Users\\eamon\\OneDrive\\SEData";

                    ctx.Status("Parsing Data");
                    var items = parser.GetData(spaceEngineersLocation);

                    ctx.Status("Saving to Database");
                    dataService.SaveData(items);

                    console.Success("Database Built");
                    console.Success($"    => {dataService.Database.Location}");

                }
                catch (Exception ex)
                {
                    console.Exception(ex);
                    logger.LogError(ex, "Error");
                }

            });
    }
}
