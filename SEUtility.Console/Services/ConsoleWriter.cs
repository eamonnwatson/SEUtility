using SEUtility.Common.Exceptions;
using SEUtility.Console.Interfaces;
using Spectre.Console;

namespace SEUtility.Console.Services;

internal class ConsoleWriter : IConsoleWriter
{
    private const string COLOR_RED = "#ff0000";
    private const string COLOR_YELLOW = "#ffff00";
    private const string COLOR_GREEN = "#00ff00";

    public bool Verbose { get; set; }

    public void Error(string message)
    {
        AnsiConsole.MarkupLineInterpolated($"[{COLOR_RED}]{message}[/]");
    }

    public void Error(string message, params object?[] args)
    {
        Error(string.Format(message, args));
    }

    public void Exception(Exception exception)
    {
        if (exception is SEException)
        {
            Error(exception.Message);
            return;
        }
        Error("An Unahndled Exception has occured");
        AnsiConsole.WriteException(exception, ExceptionFormats.ShortenEverything);
    }

    public void Output(string message)
    {
        if (Verbose)
            AnsiConsole.MarkupLine(message);
    }

    public void Output(string message, string hexCodeColour)
    {
        Output($"[{hexCodeColour}]{message}[/]");
    }

    public void Output(string message, params object?[] args)
    {
        Output(string.Format(message, args));
    }

    public void Output(string message, string hexCodeColour, params object?[] args)
    {
        Output($"[{hexCodeColour}]{string.Format(message, args)}[/]");
    }

    public void Success(string message)
    {
        AnsiConsole.MarkupLineInterpolated($"[{COLOR_GREEN}]{message}[/]");
    }

    public void Success(string message, params object?[] args)
    {
        Success(string.Format(message, args));
    }

    public void Warning(string message)
    {
        AnsiConsole.MarkupLineInterpolated($"[{COLOR_YELLOW}]{message}[/]");
    }

    public void Warning(string message, params object?[] args)
    {
        Warning(string.Format(message, args));
    }

    public void WriteTable(Table table)
    {
        AnsiConsole.Write(table);
    }
}
