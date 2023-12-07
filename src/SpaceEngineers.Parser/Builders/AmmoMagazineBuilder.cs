using FluentResults;
using SpaceEngineers.Application.Interfaces;
using SpaceEngineers.Parser.Errors;
using SpaceEngineers.Parser.Interfaces;
using SpaceEngineers.Parser.Models;

namespace SpaceEngineers.Parser.Builders;

internal class AmmoMagazineBuilder : BuilderBase<AmmoMagazine>
{
    private const string FILENAMEPATTERN = "AmmoMagazines.sbc";
    protected override Error ResultEmpty { get; }
    public override Error GeneralError { get; }
    public AmmoMagazineBuilder(ISbcParser<AmmoMagazine> parser, IConsoleWriter console)
        : base(parser, FILENAMEPATTERN, console)
    {
        ResultEmpty = ParserError.NoAmmoMagazine;
        GeneralError = ParserError.GeneralAmmoError;
    }
    public override void AssignData(SBCData data, IList<AmmoMagazine> values)
    {
        data.AmmoMagazines = values.ToList();
    }
}
