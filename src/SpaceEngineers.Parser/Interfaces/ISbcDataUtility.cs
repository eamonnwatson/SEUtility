using FluentResults;
using SpaceEngineers.Parser.Models;

namespace SpaceEngineers.Parser.Interfaces;
public interface ISbcDataUtility
{
    Task<Result<SBCData>> GetRawDataAsync(string spaceEngineersPath, CancellationToken token = default);
}