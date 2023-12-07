using FluentResults;
using SpaceEngineers.Parser.Models;

namespace SpaceEngineers.Parser.Interfaces;
public interface IDataBuilder
{
    Error GeneralError { get; }
    Task<Result> BuildDataAsync(SBCData data, string spaceEngineersPath, CancellationToken token);
}
