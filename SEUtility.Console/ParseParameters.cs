using Cocona;
using SEUtility.Console.Attributes;

namespace SEUtility.Console;
public record ParseParameters([Option("location", new char[] { 'l' }, Description = "Location of Space Engineers Database")][PathExists] string? DatabaseLocation,
                              [Option("basic", Description = "Use Basic Assembler")] bool UseBasicAssembler,
                              [Option("survival", Description = "Use Survival Kit")] bool UseSurvivalKit,
                              [Option('a', Description = "Show All")] bool ShowAll,
                              [Option('b', Description = "Show Blocks")] bool ShowBlocks,
                              [Option('c', Description = "Show Components")] bool ShowComponents,
                              [Option('i', Description = "Show Ingots")] bool ShowIngots,
                              [Option('o', Description = "Show Ores")] bool ShowOres,
                              [Option('s', Description = "Use Stone (for Iron/Nickel/Silicon)")] bool UseStone,
                              [Option("eff", Description = "Assembler Efficiency")] int AssemblerEfficiency = 3) : ICommandParameterSet;