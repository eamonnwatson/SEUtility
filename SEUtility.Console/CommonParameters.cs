using Cocona;

namespace SEUtility.Console;
public record CommonParameters([Option('v', Description = "Verbose output from program.")] bool Verbose = false) : ICommandParameterSet;