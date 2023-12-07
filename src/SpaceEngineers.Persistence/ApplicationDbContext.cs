using Microsoft.EntityFrameworkCore;
using SpaceEngineers.Application.Interfaces;
using SpaceEngineers.Data.Entities;

namespace SpaceEngineers.Persistence;
public class ApplicationDbContext(DbContextOptions options) : DbContext(options), IUnitOfWork
{
    public DbSet<PhysicalItem> PhysicalItems { get; init; }
    public DbSet<Blueprint> Blueprints { get; init; }
    public DbSet<CubeBlock> CubeBlocks { get; init; }
    public DbSet<ProductionCubeBlock> ProductionCubeBlocks { get; init; }

    public async Task PrepareDatabase()
    {
        await Database.EnsureDeletedAsync();
        await Database.EnsureCreatedAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
