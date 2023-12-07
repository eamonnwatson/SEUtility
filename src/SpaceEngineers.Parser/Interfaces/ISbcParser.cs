using FluentResults;

namespace SpaceEngineers.Parser.Interfaces;
public interface ISbcParser<TObject> where TObject : IModel
{
    Task<Result<IEnumerable<TObject>>> ParseAsync(string filename, CancellationToken cancellationToken = default);
}
