using Cocona;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SEUtility.Common.Interfaces;
using SEUtility.Common.Options;
using SEUtility.Common.Services;
using SEUtility.Console.Commands;
using SEUtility.Console.Interfaces;
using SEUtility.Console.Services;
using SEUtility.Data;
using SEUtility.Parser;
using System.Reflection;

var builder = CoconaApp.CreateBuilder();

builder.Logging.ClearProviders();

if (builder.Environment.IsDevelopment())
{
    builder.Logging.AddDebug();
}

builder.Services.AddOptions<RequirementsOptions>();

builder.Services.AddSingleton<IConsoleWriter, ConsoleWriter>()
                .AddSingleton<ISELocator, SELocator>()
                .AddTransient<IRequirementsService, RequirementsService>()
                .AddParser(builder.Configuration)
                .AddDatabase()
                .AddMediatR(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.AddCommands<BuildDatabaseCommand>();
app.AddCommands<ParseBlueprintCommand>();

app.Run();

//parse-blueprint C:\Users\eamon\OneDrive\SEData\Blueprint\bp.sbc -v