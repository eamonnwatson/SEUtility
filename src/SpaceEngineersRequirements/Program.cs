using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
#if !DEBUG
using Microsoft.Extensions.Logging.Abstractions;
#endif
using SpaceEngineers.Application.Commands.PhysicalItem;
using SpaceEngineers.Application.Interfaces;
using SpaceEngineers.Parser.Setup;
using SpaceEngineers.Persistence.Setup;
using SpaceEngineersRequirements.Commands;
using SpaceEngineersRequirements.Infrastructure;
using Spectre.Console.Cli;

var services = new ServiceCollection()
    .AddSingleton<IConsoleWriter, SpectreConsoleWriter>()
    .AddTransient<ISpaceEngineersLocator, SpaceEngineersLocator>()
    .AddTransient<IReportWriter, ReportWriter>()
    .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<SavePhysicalItemsRequest>())
    .AddLogging(builder =>
    {
#if DEBUG
        builder.AddDebug();
        builder.SetMinimumLevel(LogLevel.Debug);
        builder.AddFilter("Microsoft.EntityFramework", LogLevel.Information);
#else
        builder.AddProvider(NullLoggerProvider.Instance);
       
#endif
    })
    .SetupParser()
    .SetupPersistence();

var registrar = new TypeRegistrar(services);
var app = new CommandApp<ParseBlueprintCommand>(registrar);

app.Configure(cfg =>
{
    cfg.AddCommand<BuildDatabaseCommand>("build");
});


return await app.RunAsync(args);
