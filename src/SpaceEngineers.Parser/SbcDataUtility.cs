using FluentResults;
using Microsoft.Extensions.Logging;
using SpaceEngineers.Parser.Interfaces;
using SpaceEngineers.Parser.Models;

namespace SpaceEngineers.Parser;
internal class SbcDataUtility(IEnumerable<IDataBuilder> dataBuilders, ILogger<SbcDataUtility> logger) : ISbcDataUtility
{
    private readonly IEnumerable<IDataBuilder> _dataBuilders = dataBuilders;
    private readonly ILogger<SbcDataUtility> _logger = logger;

    public async Task<Result<SBCData>> GetRawDataAsync(string spaceEngineersPath, CancellationToken token = default)
    {
        var sbcData = new SBCData();

        foreach (var dataBuilder in _dataBuilders)
        {
            _logger.LogDebug("Starting Databuilder : {databuilderName}", dataBuilder.GetType().Name);
            var result = await dataBuilder.BuildDataAsync(sbcData, spaceEngineersPath, token);

            if (result.IsFailed)
                return Result.Fail(dataBuilder.GeneralError).WithErrors(result.Errors);
        }

        UpdateDisplayNames(sbcData);

        return sbcData;
    }

    private static void UpdateDisplayNames(SBCData data)
    {
        UpdateItems(data.AmmoMagazines, data.Localizations);
        UpdateItems(data.Components, data.Localizations);
        UpdateItems(data.CubeBlocks, data.Localizations);
        UpdateItems(data.Blueprints, data.Localizations);
        UpdateItems(data.BlueprintClasss, data.Localizations);
        UpdateItems(data.PhysicalItems, data.Localizations);
    }
    private static void UpdateItems(IReadOnlyList<IModel> models, IReadOnlyList<Localization> localizations)
    {
        foreach (var model in models)
        {
            model.DisplayName = localizations.FirstOrDefault(l => l.Name == model.DisplayName)?.Value ?? string.Empty;
        }
    }

}
