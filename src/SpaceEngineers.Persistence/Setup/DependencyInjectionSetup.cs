using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SpaceEngineers.Application.Interfaces;

namespace SpaceEngineers.Persistence.Setup;
public static class DependencyInjectionSetup
{
    public static IServiceCollection SetupPersistence(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseSqlite("Data Source=requirements.db");
            opt.EnableDetailedErrors();
            opt.EnableSensitiveDataLogging();

        });
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IRepository, Repository>();

        return services;
    }
}
