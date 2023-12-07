using FluentResults;
using SpaceEngineers.Application.Interfaces;
using SpaceEngineers.Parser.Errors;
using SpaceEngineers.Parser.Interfaces;
using SpaceEngineers.Parser.Models;

namespace SpaceEngineers.Parser.Builders;
internal class ComponentBuilder : BuilderBase<Component>
{
    private const string FILENAMEPATTERN = "Components*.sbc";
    public override Error GeneralError { get; }
    protected override Error ResultEmpty { get; }
    public ComponentBuilder(ISbcParser<Component> parser, IConsoleWriter console)
        : base(parser, FILENAMEPATTERN, console)
    {
        GeneralError = ParserError.GeneralComponentError;
        ResultEmpty = ParserError.NoComponents;
    }
    public override void AssignData(SBCData data, IList<Component> values)
    {
        data.Components = values.ToList();
    }
}
