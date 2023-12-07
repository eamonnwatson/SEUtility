using Spectre.Console.Cli;
using System.ComponentModel;

namespace SpaceEngineersRequirements.Parameters;
internal class ParseBlueprintSettings : CommandSettings
{
    [CommandArgument(0, "<path>")]
    [Description("File to Process")]
    public string Path { get; set; } = string.Empty;

    [CommandOption("-c|--console")]
    [Description("Outputs to Console")]
    public bool OutputToConsole { get; set; }

    [CommandOption("-f|--file")]
    [Description("Specify filename as output (DEFAULT:bp_gridname.txt)")]
    public string? FileName { get; set; }
}
