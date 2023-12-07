using MediatR;
using SpaceEngineers.Application.Interfaces;
using SpaceEngineers.Application.Queries.CubeBlock;
using SpaceEngineers.Data.Collections;
using SpaceEngineers.Data.Entities;
using SpaceEngineers.Parser.Interfaces;
using SpaceEngineers.Parser.Models;
using SpaceEngineersRequirements.Parameters;
using Spectre.Console.Cli;

namespace SpaceEngineersRequirements.Commands;
internal class ParseBlueprintCommand(ISbcParser<CubeGrid> parser,
                                     IConsoleWriter console,
                                     ISbcMapper mapper,
                                     ISender sender,
                                     IReportWriter reportWriter) : AsyncCommand<ParseBlueprintSettings>
{
    private readonly ISbcParser<CubeGrid> _parser = parser;
    private readonly IConsoleWriter _console = console;
    private readonly ISbcMapper _mapper = mapper;
    private readonly ISender _sender = sender;
    private readonly IReportWriter _reportWriter = reportWriter;

    public override async Task<int> ExecuteAsync(CommandContext context, ParseBlueprintSettings settings)
    {
        var cubeBlocks = await GetCubeBlockCollection();
        var cubeGrids = await GetCubeGrids(settings.Path);
        if (cubeBlocks == null || !cubeGrids.Any())
            return -1;

        var blueprintCubeBlocks = _mapper.MapBlueprintCubeBlocks(cubeGrids);
        var blueprintCubeBlockResult = cubeBlocks.FindBlueprintCubeBlocks(blueprintCubeBlocks);
        var shipBluePrint = new ShipBlueprint(cubeGrids.First().DisplayName, cubeBlocks, blueprintCubeBlocks, blueprintCubeBlockResult);

        var report = _reportWriter.CreateReport(shipBluePrint);

        if (settings.OutputToConsole)
        {
            _console.Output("");
            _console.Output(report);
            return 0;
        }

        var outputFilename = GetValidFileName(shipBluePrint.GridName);

        if (!string.IsNullOrWhiteSpace(settings.FileName))
            outputFilename = settings.FileName;

        await File.WriteAllTextAsync(outputFilename, report);

        return 0;
    }

    private static string GetValidFileName(string gridName)
    {
        foreach (var c in Path.GetInvalidFileNameChars())
        {
            gridName = gridName.Replace(c, char.MinValue);
        }
        return string.Concat("BP_", gridName, ".txt");
    }

    private async Task<IEnumerable<CubeGrid>> GetCubeGrids(string path)
    {
        _console.Output("Parsing {0}", path);
        var items = await _parser.ParseAsync(path);

        if (items.IsFailed)
        {
            _console.Error(items.Errors);
            return Enumerable.Empty<CubeGrid>();
        }

        if (!items.Value.Any())
        {
            _console.Warning("No Cubegrids found...");
            return Enumerable.Empty<CubeGrid>();
        }

        return items.Value;
    }

    private async Task<CubeBlockCollection?> GetCubeBlockCollection()
    {
        var cbs = await _sender.Send(new GetCubeBlocksQuery());

        if (cbs.IsFailed)
        {
            _console.Error(cbs.Errors);
            return null;
        }

        return cbs.Value;
    }
}
