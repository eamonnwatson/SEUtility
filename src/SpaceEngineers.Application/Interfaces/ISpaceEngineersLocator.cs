using FluentResults;

namespace SpaceEngineers.Application.Interfaces;
public interface ISpaceEngineersLocator
{
    Result<string> GetLocation();
}
