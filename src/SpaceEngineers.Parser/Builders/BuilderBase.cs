using FluentResults;
using SpaceEngineers.Application.Interfaces;
using SpaceEngineers.Parser.Errors;
using SpaceEngineers.Parser.Interfaces;
using SpaceEngineers.Parser.Models;

namespace SpaceEngineers.Parser.Builders;
internal abstract class BuilderBase<T> : IDataBuilder where T : IModel
{
    private readonly IConsoleWriter _console;
    protected ISbcParser<T> Parser { get; init; }
    protected string FileNamePattern { get; init; }
    protected abstract Error ResultEmpty { get; }
    public abstract Error GeneralError { get; }

    public BuilderBase(ISbcParser<T> parser, string fileNamePattern, IConsoleWriter console)
    {
        Parser = parser;
        FileNamePattern = fileNamePattern;
        _console = console;
    }

    public async Task<Result> BuildDataAsync(SBCData data, string spaceEngineersPath, CancellationToken token = default)
    {
        try
        {
            var files = Directory.GetFiles(spaceEngineersPath, FileNamePattern);
            var listOfT = new List<T>();

            foreach (var file in files)
            {
                _console.Output("Parsing {0}", new FileInfo(file).Name);

                var parserResult = await Parser.ParseAsync(file, token);

                if (parserResult.IsFailed)
                    return parserResult.ToResult();

                listOfT.AddRange(parserResult.Value);
            }

            if (listOfT.Count == 0)
                return Result.Fail(ResultEmpty);


            AssignData(data, listOfT);

            return Result.Ok();

        }
        catch (DirectoryNotFoundException ex)
        {
            return Result.Fail(ParserError.DirectoryNotFound.CausedBy(ex));
        }
        catch (Exception ex)
        {
            return Result.Fail(ParserError.ParsingError.CausedBy(ex));
        }
    }

    public abstract void AssignData(SBCData data, IList<T> values);
}
