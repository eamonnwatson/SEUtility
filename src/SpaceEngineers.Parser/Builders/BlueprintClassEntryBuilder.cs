using FluentResults;
using SpaceEngineers.Application.Interfaces;
using SpaceEngineers.Parser.Errors;
using SpaceEngineers.Parser.Interfaces;
using SpaceEngineers.Parser.Models;

namespace SpaceEngineers.Parser.Builders;
internal class BlueprintClassEntryBuilder : BuilderBase<BlueprintClassEntry>
{
    private const string FILENAMEPATTERN = "BlueprintClasses*.sbc";
    public override Error GeneralError { get; }
    protected override Error ResultEmpty { get; }
    public BlueprintClassEntryBuilder(ISbcParser<BlueprintClassEntry> parser, IConsoleWriter console)
        : base(parser, FILENAMEPATTERN, console)
    {
        GeneralError = ParserError.GeneralBlueprintClassEntryError;
        ResultEmpty = ParserError.NoBlueprintClassEntries;
    }
    public override void AssignData(SBCData data, IList<BlueprintClassEntry> values)
    {
        data.BlueprintClassEntries = values.ToList();
    }
}
