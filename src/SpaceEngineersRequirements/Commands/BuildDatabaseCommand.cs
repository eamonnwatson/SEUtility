using MediatR;
using SpaceEngineers.Application.Commands;
using SpaceEngineers.Application.Commands.Blueprint;
using SpaceEngineers.Application.Commands.CubeBlock;
using SpaceEngineers.Application.Commands.PhysicalItem;
using SpaceEngineers.Application.Interfaces;
using SpaceEngineers.Parser.Interfaces;
using SpaceEngineersRequirements.Parameters;
using Spectre.Console.Cli;

namespace SpaceEngineersRequirements.Commands;
internal class BuildDatabaseCommand(ISbcDataUtility dataUtility,
                            IConsoleWriter console,
                            ISbcMapper mapper,
                            ISpaceEngineersLocator locator,
                            ISender sender) : AsyncCommand<BuildDatabaseSettings>
{
    private readonly ISbcDataUtility _dataUtility = dataUtility;
    private readonly IConsoleWriter _console = console;
    private readonly ISbcMapper _mapper = mapper;
    private readonly ISpaceEngineersLocator _locator = locator;
    private readonly ISender _sender = sender;

    private const string SPACE_ENGINEERS_CONTENT_FOLDER = "content\\data";

    public override async Task<int> ExecuteAsync(CommandContext context, BuildDatabaseSettings settings)
    {
        if (settings.Path is null)
        {
            _console.Output("Finding Space Engineers...");
            var path = _locator.GetLocation();
            if (path.IsFailed)
            {
                _console.Error(path.Errors);
                return -1;
            }
            _console.Output("Space Engineers Found : {0}", path.Value);
            settings.Path = path.Value;
        }

        _console.Output("Build Database...");
        var result = await _dataUtility.GetRawDataAsync(Path.Combine(settings.Path, SPACE_ENGINEERS_CONTENT_FOLDER));

        if (result.IsFailed)
        {
            _console.Error(result.Errors);
            return -1;
        }

        _console.Output("Mapping Objects...");
        var ammo = _mapper.MapAmmoMagazines(result.Value.AmmoMagazines);
        var comp = _mapper.MapComponents(result.Value.Components);
        var pi = _mapper.MapPhysicalItems(result.Value.PhysicalItems);

        pi = ammo.Concat(comp).Concat(pi).ToList();

        var bp = _mapper.MapBlueprints(result.Value.Blueprints, pi);
        var blocks = _mapper.MapCubeBlocks(result.Value, pi, bp);

        await _sender.Send(new ResetDatabaseCommand());
        await _sender.Send(new SavePhysicalItemsRequest(pi));
        await _sender.Send(new SaveBlueprintRequest(bp));
        await _sender.Send(new SaveCubeBlocksRequest(blocks));

        _console.Success("Database Update Completed");
        return 0;
    }
}
