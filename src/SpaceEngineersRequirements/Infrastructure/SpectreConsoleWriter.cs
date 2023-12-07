using FluentResults;
using SpaceEngineers.Application.Interfaces;
using Spectre.Console;

namespace SpaceEngineersRequirements.Infrastructure;
internal class SpectreConsoleWriter : IConsoleWriter
{
    private readonly IAnsiConsole _console;

    public SpectreConsoleWriter()
    {
        _console = AnsiConsole.Create(
            new AnsiConsoleSettings()
            {
                ColorSystem = ColorSystemSupport.Detect,
                Interactive = InteractionSupport.Detect
            });
    }

    public void Error(string message)
    {
        _console.MarkupLineInterpolated($"[red]Error: {message}[/]");
    }

    public void Error(string message, params object?[] args)
    {
        Error(string.Format(message, args));
    }

    public void Error(IEnumerable<IError> errors)
    {
        foreach (var error in errors)
        {
            Error(error.Message);
            foreach (var reason in error.Reasons)
            {
                if (reason is ExceptionalError exError)
                    Output("[red]{0}: {1}[/]", exError.Exception.GetType().Name, exError.Message);
                else
                    Output("[red]Reason: {0}[/]", reason.Message);
            }
        }
    }

    public void Exception(Exception exception)
    {
        Error("An Unhandled Exception has occured");
        _console.WriteException(exception, ExceptionFormats.ShortenEverything);
    }

    public void Output(string message)
    {
        _console.MarkupLine(message);
    }

    public void Output(string message, params object?[] args)
    {
        Output(string.Format(message, args));
    }

    public void Success(string message)
    {
        _console.MarkupLineInterpolated($"[green]{message}[/]");
    }

    public void Success(string message, params object?[] args)
    {
        Success(string.Format(message, args));
    }

    public void Warning(string message)
    {
        _console.MarkupLineInterpolated($"[yellow]{message}[/]");
    }

    public void Warning(string message, params object?[] args)
    {
        Warning(string.Format(message, args));
    }
}
