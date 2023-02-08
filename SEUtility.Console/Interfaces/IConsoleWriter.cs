using Spectre.Console;

namespace SEUtility.Console.Interfaces;

public interface IConsoleWriter
{
    public bool Verbose { get; set; }
    public void Warning(string message);
    public void Warning(string message, params object?[] args);
    public void Error(string message);
    public void Error(string message, params object?[] args);
    public void Success(string message);
    public void Success(string message, params object?[] args);
    public void Output(string message);
    public void Output(string message, string hexCodeColour);
    public void Output(string message, params object?[] args);
    public void Output(string message, string hexCodeColour, params object?[] args);
    public void WriteTable(Table table);
    void Exception(Exception exception);
}
