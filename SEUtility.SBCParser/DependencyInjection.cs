using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SEUtility.Parser.Parsers;
using System.Reflection;

namespace SEUtility.Parser;

public static class DependencyInjection
{
    public static IServiceCollection AddParser(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<List<ParserType>>().BindConfiguration("Parsers").Validate(parsers =>
        {
            foreach (var item in parsers)
            {
                if (item.Name is null || item.XmlNode is null || item.SearchPattern is null)
                    return false;

            }
            return true;
        }, "Name / XmlNode and SearchPattern are all required fields");

        services.AddTransient<IShipParser, ShipBlueprintParser>()
                .AddTransient<ISBCParser, SBCParser>();

        Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(type => type.GetInterface(nameof(IParser)) != null)
                .ToList()
                .ForEach(a => services.TryAddTransient(a));

        return services;
    }
}
