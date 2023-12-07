using Spectre.Console.Cli;
using System.ComponentModel;

namespace SpaceEngineersRequirements.Parameters;
internal class BuildDatabaseSettings : CommandSettings
{
    [CommandArgument(0, "[path]")]
    [Description("Space Engineers Path")]
    public string? Path { get; set; }
}
