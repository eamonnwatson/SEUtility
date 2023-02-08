using Microsoft.Extensions.DependencyInjection;
using SEUtility.Common.Interfaces;
using System.Reflection;

namespace SEUtility.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddSingleton<IDatabase, Database>();
        services.AddTransient<IDataService, DataService>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
