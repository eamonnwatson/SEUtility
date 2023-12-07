using FluentResults;
using SpaceEngineers.Application.Interfaces;
using SpaceEngineers.Parser.Errors;
using SpaceEngineers.Parser.Interfaces;
using SpaceEngineers.Parser.Models;

namespace SpaceEngineers.Parser.Builders;
internal class BlueprintBuilder : BuilderBase<Blueprint>
{
    private const string FILENAMEPATTERN = "Blueprints*.sbc";
    public override Error GeneralError { get; }
    protected override Error ResultEmpty { get; }
    public BlueprintBuilder(ISbcParser<Blueprint> parser, IConsoleWriter console)
        : base(parser, FILENAMEPATTERN, console)
    {
        GeneralError = ParserError.GeneralBlueprintError;
        ResultEmpty = ParserError.NoBlueprints;
    }
    public override void AssignData(SBCData data, IList<Blueprint> values)
    {
        data.Blueprints = values
            .Where(bp => !bp.SubtypeId.Contains("scrap", StringComparison.InvariantCultureIgnoreCase))  // removed to avoid blueprint problems as scrap can be converted into iron
            .ToList();
    }
}
