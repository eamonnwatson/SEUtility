using FluentResults;
using SpaceEngineers.Application.Interfaces;
using SpaceEngineers.Parser.Errors;
using SpaceEngineers.Parser.Interfaces;
using SpaceEngineers.Parser.Models;

namespace SpaceEngineers.Parser.Builders;
internal class CubeBlockBuilder : BuilderBase<CubeBlock>
{
    private const string FILENAMEPATTERN = "CubeBlocks\\CubeBlocks*.sbc";
    public override Error GeneralError { get; }
    protected override Error ResultEmpty { get; }
    public CubeBlockBuilder(ISbcParser<CubeBlock> parser, IConsoleWriter console)
        : base(parser, FILENAMEPATTERN, console)
    {
        GeneralError = ParserError.GeneralCubeBlockError;
        ResultEmpty = ParserError.NoCubeBlock;
    }
    public override void AssignData(SBCData data, IList<CubeBlock> values)
    {
        data.CubeBlocks = values.ToList();
    }
}
