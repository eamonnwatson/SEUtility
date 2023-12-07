using FluentResults;
using SpaceEngineers.Application.Interfaces;
using SpaceEngineers.Parser.Errors;
using SpaceEngineers.Parser.Interfaces;
using SpaceEngineers.Parser.Models;

namespace SpaceEngineers.Parser.Builders;
internal class PhysicalItemBuilder : BuilderBase<PhysicalItem>
{
    private const string FILENAMEPATTERN = "PhysicalItems*.sbc";
    public override Error GeneralError { get; }
    protected override Error ResultEmpty { get; }
    public PhysicalItemBuilder(ISbcParser<PhysicalItem> parser, IConsoleWriter console)
        : base(parser, FILENAMEPATTERN, console)
    {
        GeneralError = ParserError.GeneralPhysicalItemError;
        ResultEmpty = ParserError.NoPhysicalItems;
    }
    public override void AssignData(SBCData data, IList<PhysicalItem> values)
    {
        data.PhysicalItems = values.ToList();
    }
}
