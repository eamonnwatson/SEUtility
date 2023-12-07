using Microsoft.Extensions.DependencyInjection;
using SpaceEngineers.Parser.Builders;
using SpaceEngineers.Parser.Interfaces;
using SpaceEngineers.Parser.Mapper;

namespace SpaceEngineers.Parser.Setup;
public static class DependencyInjectionSetup
{
    public static IServiceCollection SetupParser(this IServiceCollection services)
    {
        services.AddTransient(typeof(ISbcParser<>), typeof(SbcParser<>));
        services.AddTransient<ISbcMapper, SbcMapper>();
        services.AddTransient<ISbcDataUtility, SbcDataUtility>();
        services.AddTransient<IDataBuilder, AmmoMagazineBuilder>();
        services.AddTransient<IDataBuilder, BlueprintBuilder>();
        services.AddTransient<IDataBuilder, BlueprintClassBuilder>();
        services.AddTransient<IDataBuilder, BlueprintClassEntryBuilder>();
        services.AddTransient<IDataBuilder, ComponentBuilder>();
        services.AddTransient<IDataBuilder, CubeBlockBuilder>();
        services.AddTransient<IDataBuilder, LocalizationBuilder>();
        services.AddTransient<IDataBuilder, PhysicalItemBuilder>();

        return services;
    }
}
