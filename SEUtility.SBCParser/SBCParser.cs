using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SEUtility.Common;
using SEUtility.Common.Exceptions;
using SEUtility.Common.Models;
using SEUtility.Common.Notifications;
using SEUtility.Parser.Parsers;
using System.Reflection;

namespace SEUtility.Parser;
internal class SBCParser : ISBCParser
{
    private readonly ILogger<SBCParser> logger;
    private readonly IOptions<List<ParserType>> parserOptions;
    private readonly IServiceProvider serviceProvider;
    private readonly IShipParser shipParser;
    private readonly IMediator mediator;
    private readonly Dictionary<string, IParser> parsers;
    public SBCParser(ILogger<SBCParser> logger,
                     IOptions<List<ParserType>> parserOptions,
                     IServiceProvider serviceProvider,
                     IShipParser shipParser,
                     IMediator mediator)
    {
        this.logger = logger;
        this.parserOptions = parserOptions;
        this.serviceProvider = serviceProvider;
        this.shipParser = shipParser;
        this.mediator = mediator;
        parsers = Assembly.GetExecutingAssembly()
                          .GetTypes()
                          .Where(type => type.GetInterface(nameof(IParser)) != null)
                          .Select(type => serviceProvider.GetService(type))
                          .Cast<IParser>()
                          .ToDictionary(a => a.GetType().GetCustomAttribute<ParserAttribute>()!.Name,
                                             StringComparer.InvariantCultureIgnoreCase);

    }
    public SpaceEngineersData GetData(string spaceEngineersLocation)
    {
        if (!Directory.Exists(spaceEngineersLocation))
            throw new SEException($"{spaceEngineersLocation} does not exist");

        var dataBuilder = DataBuilder.Create();

        foreach (var item in parserOptions.Value)
        {
            if (parsers.TryGetValue(item.Name, out IParser? parser))
            {
                var files = Directory.GetFiles(spaceEngineersLocation, item.SearchPattern);
                if (files.Length == 0)
                {
                    logger.LogWarning("Unable to find files to parse using Search Pattern : {SearchPattern}", item.SearchPattern);
                    mediator.Publish(new ConsoleNotification($"Unable to find files to parse using Search Pattern : {item.SearchPattern}", ConsoleStatus.WARNING));
                    continue;
                }

                mediator.Publish(new ConsoleNotification($"Using Parser {item.Name} with Search Pattern : {item.SearchPattern}"));

                foreach (var file in files)
                {
                    mediator.Publish(new ConsoleNotification($"Parsing File : {file}"));
                    parser.Parse(file, dataBuilder, item);
                }
            }
            else
            {
                logger.LogWarning("Unable to find parser {Name}", item.Name);
                mediator.Publish(new ConsoleNotification($"Unable to find parser {item.Name}", ConsoleStatus.WARNING));
            }
        }

        mediator.Publish(new ConsoleNotification("Space Engineers Data Retreived", ConsoleStatus.SUCCESS));

        return dataBuilder.BuildData();

    }

    public ShipBlueprint GetBlueprint(string blueprintFile)
    {
        if (!File.Exists(blueprintFile))
            throw new SEException($"{blueprintFile} does not exist");

        var databuilder = DataBuilder.CreateShipBuilder();

        shipParser.Parse(blueprintFile, databuilder);

        return databuilder.Build();
    }
}
