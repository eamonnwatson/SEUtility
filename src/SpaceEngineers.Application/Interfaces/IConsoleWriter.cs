using FluentResults;

namespace SpaceEngineers.Application.Interfaces;
public interface IConsoleWriter
{
    void Error(string message);
    void Error(string message, params object?[] args);
    void Error(IEnumerable<IError> errors);
    void Exception(Exception exception);
    void Output(string message);
    void Output(string message, params object?[] args);
    void Success(string message);
    void Success(string message, params object?[] args);
    void Warning(string message);
    void Warning(string message, params object?[] args);

}
