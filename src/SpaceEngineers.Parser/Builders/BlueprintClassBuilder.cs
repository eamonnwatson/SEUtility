using FluentResults;
using SpaceEngineers.Application.Interfaces;
using SpaceEngineers.Parser.Errors;
using SpaceEngineers.Parser.Interfaces;
using SpaceEngineers.Parser.Models;

namespace SpaceEngineers.Parser.Builders;
internal class BlueprintClassBuilder : BuilderBase<BlueprintClass>
{
    private const string FILENAMEPATTERN = "BlueprintClasses*.sbc";
    public override Error GeneralError { get; }
    protected override Error ResultEmpty { get; }
    public BlueprintClassBuilder(ISbcParser<BlueprintClass> parser, IConsoleWriter console)
        : base(parser, FILENAMEPATTERN, console)
    {
        GeneralError = ParserError.GeneralBlueprintClassError;
        ResultEmpty = ParserError.NoBlueprintClasses;
    }

    public override void AssignData(SBCData data, IList<BlueprintClass> values)
    {
        data.BlueprintClasss = values.ToList();
    }

}
