using FluentResults;
using SpaceEngineers.Application.Interfaces;
using SpaceEngineers.Parser.Errors;
using SpaceEngineers.Parser.Interfaces;
using SpaceEngineers.Parser.Models;

namespace SpaceEngineers.Parser.Builders;
internal class LocalizationBuilder : BuilderBase<Localization>
{
    private const string FILENAMEPATTERN = "Localization\\MyTexts.resx";
    public override Error GeneralError { get; }
    protected override Error ResultEmpty { get; }
    public LocalizationBuilder(ISbcParser<Localization> parser, IConsoleWriter console)
        : base(parser, FILENAMEPATTERN, console)
    {
        GeneralError = ParserError.GeneralLocalizationError;
        ResultEmpty = ParserError.NoLocalization;
    }
    public override void AssignData(SBCData data, IList<Localization> values)
    {
        data.Localizations = values.ToList();
    }
}
